using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using ToDoList.Models;

namespace ToDoList.Repository
{
    public class XmlRepository<T> : IRepository<T>
    {
        private readonly string _filePath;
        private readonly string _fileName;
        public XmlRepository(IConfiguration Configuration)
        {
            _filePath = Configuration.GetDefaultXMLStorage();
            _fileName = $"{typeof(T).Name}";
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (!File.Exists($"{_filePath}{_fileName}.xml"))
                return Enumerable.Empty<T>();

            using (var stream = File.OpenRead($"{_filePath}{_fileName}.xml"))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                return (List<T>)serializer.Deserialize(stream);
            }
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var entities = await GetAllAsync();
            return entities.SingleOrDefault(e => GetId(e) == id);
        }
        public async Task AddAsync(T entity)
        {
            var entities = (await GetAllAsync()).ToList();
            if (entity is IModelWithId ent)
                ent.Id = GetNextId();
            entities.Add(entity);
            SaveToFile(entities);
        }
        public async Task UpdateAsync(T entity)
        {
            var entities = (await GetAllAsync()).ToList();
            var existingEntity = entities.FirstOrDefault(e => GetId(e) == GetId(entity));
            if (existingEntity != null)
            {
                entities.Remove(existingEntity);
                entities.Add(entity);
                SaveToFile(entities);
            }
        }
        public async Task DeleteAsync(int id)
        {
            var entities = (await GetAllAsync()).ToList();
            var entityToRemove = entities.FirstOrDefault(e => GetId(e) == id);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
                SaveToFile(entities);
            }
        }
        private void SaveToFile(IEnumerable<T> entities)
        {
            using (var stream = File.Create($"{_filePath}{_fileName}.xml"))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(stream, entities);
            }
        }
        private int GetId(T entity)
        {
            var property = typeof(T).GetProperty("Id");
            return (int)(property.GetValue(entity) ?? 0);
        }
        #region NextId
        private static readonly object _lock = new object();
        private int GetNextId()
        {
            lock (_lock)
            {
                XDocument document;
                try
                {
                    document = XDocument.Load($"{_filePath}ModelsId.xml");
                }
                catch
                {
                    document = CreateModelsId();
                }
                XElement nextTaskIdElement = document.Root.Element($"Next{_fileName}Id");
                if (nextTaskIdElement == null)
                    nextTaskIdElement = CreateIdField(document);

                int nextTaskId = int.Parse(nextTaskIdElement.Value);
                nextTaskIdElement.Value = (nextTaskId + 1).ToString();
                document.Save($"{_filePath}ModelsId.xml");
                return nextTaskId;
            }
        }
        private XDocument CreateModelsId()
        {
            lock (_lock)
            {
                XDocument doc = new XDocument(
                    new XElement("ModelsId",
                        new XElement($"Next{_fileName}Id", "1")
                    )
                );
                doc.Save($"{_filePath}ModelsId.xml");
                return doc;
            }
        }
        private XElement CreateIdField(XDocument doc)
        {
            lock (_lock)
            {
                XElement newTaskIdElement = new XElement($"Next{_fileName}Id", "1");
                doc.Root.Add(newTaskIdElement);
                doc.Save($"{_filePath}ModelsId.xml");
                return newTaskIdElement;
            }
        }
        #endregion
    }
}

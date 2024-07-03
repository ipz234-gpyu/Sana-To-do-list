using ToDoList.Repository;
using System.ComponentModel;
using ToDoList.Models;
using ToDoList.Repository.RepositoryModel;

namespace ToDoList.Factory
{
    public class RepositoryFactory(IServiceProvider _serviceProvider, IHttpContextAccessor _httpContextAccessor) : IRepositoryFactory
    {
        private IRepositoryFactory factory = _serviceProvider.GetRequiredService<SqlRepositoryFactory>();
        public RepositoryTasks GetRepositoryTasks()
        {
            return factory.GetRepositoryTasks();
        }
        public IRepository<T> GetRepository<T>()
        {
            return factory.GetRepository<T>();
        }
        public StorageType GetStorageType()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var storageType = session.GetString("StorageType");
            return storageType switch
            {
                "SQL" => StorageType.SQL,
                "XML" => StorageType.XML,
                _ => StorageType.SQL
            };
        }
        public void SetStorageType(StorageType storageType)
        {
            factory = storageType switch
            {
                StorageType.SQL => _serviceProvider.GetRequiredService<SqlRepositoryFactory>(),
                StorageType.XML => _serviceProvider.GetRequiredService<XmlRepositoryFactory>(),
                _ => throw new InvalidEnumArgumentException(nameof(storageType))
            };
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString("StorageType", storageType.ToString());
        }
    }
    public enum StorageType
    {
        SQL,
        XML
    }
}

using ToDoList.Repository;
using System.ComponentModel;

namespace ToDoList.Factory
{
    public class RepositoryFactory(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor) : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private IRepositoryFactory factory = serviceProvider.GetRequiredService<SqlRepositoryFactory>();
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

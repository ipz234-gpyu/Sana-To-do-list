using ToDoList.Repository;
using System.ComponentModel;

namespace ToDoList.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RepositoryFactory(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public IRepository<T> GetRepository<T>()
        {
            var storageType = GetStorageType();
            IRepositoryFactory factory = storageType switch
            {
                StorageType.SQL => _serviceProvider.GetRequiredService<SqlRepositoryFactory>(),
                StorageType.XML => _serviceProvider.GetRequiredService<XmlRepositoryFactory>(),
                _ => throw new InvalidEnumArgumentException(nameof(storageType))
            };
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

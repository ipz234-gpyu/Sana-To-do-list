using ToDoList.Repository;
using System.ComponentModel;

namespace ToDoList.Factory
{
    public class RepositoryFactory(IServiceProvider serviceProvider) : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private StorageType _storageType = StorageType.SQL;
        private IRepositoryFactory _factory = serviceProvider.GetRequiredService<SqlRepositoryFactory>();
        public IRepository<T> GetRepository<T>()
        {
            return _factory.GetRepository<T>();
        }
        public RepositoryFactory SetStorageType(StorageType storageType)
        {
            _storageType = storageType;
            switch (_storageType)
            {
                case StorageType.SQL: _factory = _serviceProvider.GetRequiredService<SqlRepositoryFactory>(); break;
                case StorageType.XML: _factory = _serviceProvider.GetRequiredService<XmlRepositoryFactory>(); break;
                default: throw new InvalidEnumArgumentException(nameof(storageType));
            }
            return this;
        }
        public StorageType GetStorageType()
        {
            return _storageType;
        }
    }
    public enum StorageType
    {
        SQL,
        XML
    }
}

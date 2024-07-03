using ToDoList.Repository;
using System.ComponentModel;
using ToDoList.Models;
using ToDoList.Repository.RepositoryModel;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Factory
{
    public class RepositoryFactory(IServiceProvider _serviceProvider, IHttpContextAccessor httpContextAccessor) : IRepositoryFactory
    {
        public IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
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
        public IRepositoryFactory GetRepositoryFactory(HttpContext context)
        {
            string? storageTypeString = context.Items["StorageType"]?.ToString();
            if (!Enum.TryParse<StorageType>(storageTypeString, out StorageType storageType))
                storageType = StorageType.SQL;
            
            return storageType switch
            {
                StorageType.SQL => _serviceProvider.GetRequiredService<SqlRepositoryFactory>(),
                StorageType.XML => _serviceProvider.GetRequiredService<XmlRepositoryFactory>(),
                _ => throw new ArgumentException("Invalid repository type", nameof(storageType))
            };
        }
    }
    public enum StorageType
    {
        SQL,
        XML
    }
}

using ToDoList.Repository;

namespace ToDoList.Factory
{
    public class XmlRepositoryFactory(IServiceProvider serviceProvider) : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        public IRepository<T> GetRepository<T>()
        {
            return _serviceProvider.GetRequiredService<XmlRepository<T>>();
        }
    }
}

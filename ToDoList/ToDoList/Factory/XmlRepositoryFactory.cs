using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Repository.RepositoryModel;

namespace ToDoList.Factory
{
    public class XmlRepositoryFactory(IServiceProvider serviceProvider) : IRepositoryFactory
    {
        public RepositoryTasks GetRepositoryTasks()
        {
            return serviceProvider.GetRequiredService<XmlRepositoryTasks>();
        }
        public IRepository<T> GetRepository<T>()
        {
            return serviceProvider.GetRequiredService<XmlRepository<T>>();
        }
    }
}

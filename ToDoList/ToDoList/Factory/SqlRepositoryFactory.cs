using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Repository.RepositoryModel;

namespace ToDoList.Factory
{
    public class SqlRepositoryFactory(IServiceProvider serviceProvider) : IRepositoryFactory
    {
        public RepositoryTasks GetRepositoryTasks()
        {
            return serviceProvider.GetRequiredService<DapperRepositoryTasks>();
        }
        public IRepository<T> GetRepository<T>()
        {
            return serviceProvider.GetRequiredService<DapperRepository<T>>();
        }
    }
}

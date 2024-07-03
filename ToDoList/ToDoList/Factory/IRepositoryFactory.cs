using ToDoList.Models;
using ToDoList.Repository;
using ToDoList.Repository.RepositoryModel;

namespace ToDoList.Factory
{
    public interface IRepositoryFactory
    {
        public RepositoryTasks GetRepositoryTasks();
        public IRepository<T> GetRepository<T>();
    }
}

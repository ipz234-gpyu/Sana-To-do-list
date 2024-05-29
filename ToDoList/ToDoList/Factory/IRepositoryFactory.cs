using ToDoList.Repository;

namespace ToDoList.Factory
{
    public interface IRepositoryFactory
    {
        public IRepository<T> GetRepository<T>();
    }
}

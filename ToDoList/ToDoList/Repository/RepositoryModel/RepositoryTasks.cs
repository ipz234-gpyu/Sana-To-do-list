using ToDoList.Models;

namespace ToDoList.Repository.RepositoryModel
{
    public interface RepositoryTasks : IRepository<Tasks>
    {
        Task<Tasks> Completed(int Id);
    }
}

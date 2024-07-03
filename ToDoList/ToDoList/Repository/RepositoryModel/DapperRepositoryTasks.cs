using ToDoList.Models;

namespace ToDoList.Repository.RepositoryModel
{
    public class DapperRepositoryTasks : DapperRepository<Tasks>, RepositoryTasks
    {
        public DapperRepositoryTasks(IConfiguration Configuration) : base(Configuration)
        {
        }
        public async Task Completed(int Id)
        {
            Tasks task = await GetByIdAsync(Id);
            task.Completed = !task.Completed;
            await UpdateAsync(task);
        }
    }
}

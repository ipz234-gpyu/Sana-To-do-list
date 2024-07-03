using ToDoList.Models;

namespace ToDoList.Repository.RepositoryModel
{
    public class XmlRepositoryTasks : XmlRepository<Tasks>, RepositoryTasks
    {
        public XmlRepositoryTasks(IConfiguration Configuration) : base(Configuration)
        {
        }
        public async Task<Tasks> Completed(int Id)
        {
            Tasks task = await GetByIdAsync(Id);
            if (task != null)
            {
                task.Completed = !task.Completed;
                await UpdateAsync(task);
            }
            return task;
        }
    }
}

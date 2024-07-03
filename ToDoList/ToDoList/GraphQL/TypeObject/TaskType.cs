using GraphQL.Types;
using ToDoList.Models;

namespace ToDoListAPI.TypeObject
{
    public class TaskType : ObjectGraphType<Tasks>
    {
        public TaskType()
        {
            Field(x => x.Id);
            Field(x => x.Description);
            Field(x => x.Completed);
            Field(x => x.CategoryId, nullable: true);
            Field(x => x.DeadLine, nullable: true);
        }
    }
}

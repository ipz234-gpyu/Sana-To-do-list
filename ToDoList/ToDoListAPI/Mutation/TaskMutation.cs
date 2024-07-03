using GraphQL;
using GraphQL.Types;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoListAPI.TypeObject;

namespace ToDoListAPI.Mutation
{
    public sealed class TaskMutation : ObjectGraphType
    {
        public TaskMutation(IRepository<Tasks> taskRepository)
        {
            Field<TaskType>("add").Arguments(new QueryArguments(new QueryArgument<TaskInputType>
            {
                Name = "task"
            })).ResolveAsync(async context =>
            {
                Tasks task = context.GetArgument<Tasks>("task");
                await taskRepository.AddAsync(task);
                return task;
            });

            Field<TaskType>("update").Arguments(new QueryArguments(new QueryArgument<TaskInputType>
            {
                Name = "task"
            })).ResolveAsync(async context =>
            {
                var task = context.GetArgument<Tasks>("task");
                await taskRepository.UpdateAsync(task);
                return task;
            });

            Field<StringGraphType>("delete").Arguments(new QueryArguments(new QueryArgument<IdGraphType>
            {
                Name = "id"
            })).ResolveAsync(async context =>
            {
                var taskId = context.GetArgument<int>("id");
                await taskRepository.DeleteAsync(taskId);
                return $"Task with id {taskId} has been deleted";
            });
        }
    }

}
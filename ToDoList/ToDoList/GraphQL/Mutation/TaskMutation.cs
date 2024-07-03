using GraphQL;
using GraphQL.Types;
using ToDoList.Factory;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoListAPI.TypeObject;

namespace ToDoListAPI.Mutation
{
    public sealed class TaskMutation : ObjectGraphType
    {
        public TaskMutation(RepositoryFactory factory)
        {
            var repositoryFactory = factory.GetRepositoryFactory(factory._httpContextAccessor.HttpContext);
            Field<TaskType>("add").Arguments(new QueryArguments(new QueryArgument<TaskInputType>
            {
                Name = "task"
            })).ResolveAsync(async context =>
            {
                Tasks task = context.GetArgument<Tasks>("task");
                await repositoryFactory.GetRepositoryTasks().AddAsync(task);
                return task;
            });

            Field<TaskType>("update").Arguments(new QueryArguments(new QueryArgument<TaskInputType>
            {
                Name = "task"
            })).ResolveAsync(async context =>
            {
                var task = context.GetArgument<Tasks>("task");
                await repositoryFactory.GetRepositoryTasks().UpdateAsync(task);
                return task;
            });

            Field<StringGraphType>("delete").Arguments(new QueryArguments(new QueryArgument<IdGraphType>
            {
                Name = "id"
            })).ResolveAsync(async context =>
            {
                var taskId = context.GetArgument<int>("id");
                await repositoryFactory.GetRepositoryTasks().DeleteAsync(taskId);
                return $"Task with id {taskId} has been deleted";
            });

            Field<TaskType>("completed").Arguments(new QueryArguments(new QueryArgument<IdGraphType>
            {
                Name = "id"
            })).ResolveAsync(async context =>
            {
                var taskId = context.GetArgument<int>("id");
                Tasks task = await repositoryFactory.GetRepositoryTasks().Completed(taskId);
                return task;
            });
        }
    }

}
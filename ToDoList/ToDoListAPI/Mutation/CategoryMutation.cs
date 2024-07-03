using GraphQL;
using GraphQL.Types;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoListAPI.TypeObject;

namespace ToDoListAPI.Mutation
{
    public sealed class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(IRepository<Category> categoryRepository)
        {
            Field<CategoryType>("add").Arguments(new QueryArguments(new QueryArgument<CategoryInputType>
            {
                Name = "category"
            })).ResolveAsync(async context =>
            {
                var category = context.GetArgument<Category>("category");
                await categoryRepository.AddAsync(category);
                return (object?)category;
            });

            Field<CategoryType>("update").Arguments(new QueryArguments(new QueryArgument<CategoryInputType>
            {
                Name = "category"
            })).ResolveAsync(async context =>
            {
                var category = context.GetArgument<Category>("category");
                await categoryRepository.UpdateAsync(category);
                return (object?)category;
            });

            Field<StringGraphType>("delete").Arguments(new QueryArguments(new QueryArgument<IdGraphType>
            {
                Name = "id"
            })).ResolveAsync(async context =>
            {
                var taskId = context.GetArgument<int>("id");
                await categoryRepository.DeleteAsync(taskId);
                return $"Task with id {taskId} has been deleted";
            });
        }
    }
}

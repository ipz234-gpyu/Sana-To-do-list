using GraphQL;
using GraphQL.Types;
using ToDoList.Factory;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoListAPI.TypeObject;

namespace ToDoListAPI.Mutation
{
    public sealed class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(RepositoryFactory factory)
        {
            var repositoryFactory = factory.GetRepositoryFactory(factory._httpContextAccessor.HttpContext);
            Field<CategoryType>("add").Arguments(new QueryArguments(new QueryArgument<CategoryInputType>
            {
                Name = "category"
            })).ResolveAsync(async context =>
            {
                var category = context.GetArgument<Category>("category");
                await repositoryFactory.GetRepository<Category>().AddAsync(category);
                return (object?)category;
            });

            Field<CategoryType>("update").Arguments(new QueryArguments(new QueryArgument<CategoryInputType>
            {
                Name = "category"
            })).ResolveAsync(async context =>
            {
                var category = context.GetArgument<Category>("category");
                await repositoryFactory.GetRepository<Category>().UpdateAsync(category);
                return (object?)category;
            });

            Field<StringGraphType>("delete").Arguments(new QueryArguments(new QueryArgument<IdGraphType>
            {
                Name = "id"
            })).ResolveAsync(async context =>
            {
                var categoryId = context.GetArgument<int>("id");
                await repositoryFactory.GetRepository<Category>().DeleteAsync(categoryId);
                return $"{categoryId}";
            });
        }
    }
}

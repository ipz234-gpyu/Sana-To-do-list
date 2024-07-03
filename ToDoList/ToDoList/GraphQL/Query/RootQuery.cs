using GraphQL;
using GraphQL.Types;
using ToDoList.Factory;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoListAPI.TypeObject;

namespace ToDoListAPI.Query
{
    public sealed class RootQuery : ObjectGraphType
    {
        public RootQuery(RepositoryFactory factory)
        {
            var repositoryFactory = factory.GetRepositoryFactory(factory._httpContextAccessor.HttpContext);
            Field<ListGraphType<TaskType>>("tasks").ResolveAsync(async context =>
               await repositoryFactory.GetRepository<Tasks>().GetAllAsync());

            Field<ListGraphType<TaskType>>("task").Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id"})
                .ResolveAsync(async context =>
                {
                   IEnumerable<Tasks> tasks = new List<Tasks>()
                   {
                       await repositoryFactory.GetRepository<Tasks>().GetByIdAsync(context.GetArgument<int>("id"))
                   };
                   return tasks;
                });

            Field<ListGraphType<CategoryType>>("categories").ResolveAsync(async context =>
               await repositoryFactory.GetRepository<Category>().GetAllAsync());

            Field<ListGraphType<CategoryType>>("category").Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" })
                .ResolveAsync(async context =>
                {
                    IEnumerable<Category> tasks = new List<Category>()
                   {
                       await repositoryFactory.GetRepository<Category>().GetByIdAsync(context.GetArgument<int>("id"))
                   };
                    return tasks;
                });
        }

    }
}

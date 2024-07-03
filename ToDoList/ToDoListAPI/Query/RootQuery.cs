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
        public RootQuery(IRepositoryFactory repository)
        {
            Field<ListGraphType<TaskType>>("tasks").ResolveAsync(async context =>
               await repository.GetRepository<Tasks>().GetAllAsync());

            Field<ListGraphType<TaskType>>("task").Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id"})
                .ResolveAsync(async context =>
                {
                   IEnumerable<Tasks> tasks = new List<Tasks>()
                   {
                       await repository.GetRepository<Tasks>().GetByIdAsync(context.GetArgument<int>("id"))
                   };
                   return tasks;
                });

            Field<ListGraphType<CategoryType>>("categories").ResolveAsync(async context =>
               await repository.GetRepository<Category>().GetAllAsync());

            Field<ListGraphType<CategoryType>>("category").Arguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" })
                .ResolveAsync(async context =>
                {
                    IEnumerable<Category> tasks = new List<Category>()
                   {
                       await repository.GetRepository<Category>().GetByIdAsync(context.GetArgument<int>("id"))
                   };
                    return tasks;
                });
        }

    }
}

using GraphQL.MicrosoftDI;
using GraphQL.Types;
using ToDoList.Models;
using ToDoList.Repository;

namespace ToDoListAPI.Mutation;

public sealed class RootMutation : ObjectGraphType
{
    public RootMutation()
    {
        Field<TaskMutation>("taskMutation")
            .Resolve(context => context.RequestServices.GetRequiredService<TaskMutation>());
        Field<CategoryMutation>("categoryMutation")
            .Resolve(context => context.RequestServices.GetRequiredService<CategoryMutation>());
    }
}
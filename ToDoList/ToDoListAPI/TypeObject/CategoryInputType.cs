using GraphQL.Types;

namespace ToDoListAPI.TypeObject
{
    public sealed class CategoryInputType : InputObjectGraphType
    {
        public CategoryInputType()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("name");
        }
    }
}

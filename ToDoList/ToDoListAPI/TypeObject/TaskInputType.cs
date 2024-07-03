using GraphQL.Types;

namespace ToDoListAPI.TypeObject
{
    public sealed class TaskInputType : InputObjectGraphType
    {
        public TaskInputType()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("description");
            Field<BooleanGraphType>("completed");
            Field<DateGraphType>("deadLine");
            Field<IntGraphType>("categoryId");
        }
    }
}
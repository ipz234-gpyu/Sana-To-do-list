using GraphQL.Types;
using ToDoList.Models;

namespace ToDoListAPI.TypeObject
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}

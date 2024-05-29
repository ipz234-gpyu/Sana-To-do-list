namespace ToDoList.Models
{
    public class Category : IModelWithId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}

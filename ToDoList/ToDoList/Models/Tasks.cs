namespace ToDoList.Models
{
    public class Tasks : IModelWithId
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public DateTime? DeadLine { get; set; }
        public int? CategoryId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using ToDoList.Factory;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "The category name is mandatory")]
        public string Name { get; set; }
        #region NewTask
        [Required(ErrorMessage = "The task description is mandatory")]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        #endregion
        public List<Tasks> TasksList { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public StorageType StorageTypeSelected { get; set; }
        public void SortedTasksList()
        {
            TasksList.Sort((x, y) =>
            {
                int completedComparison = x.Completed.CompareTo(y.Completed);
                if (completedComparison != 0)
                    return completedComparison;
                return y.Id.CompareTo(x.Id);
            });
        }
    }
}

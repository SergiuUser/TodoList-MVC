using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Todo_List_WebApp.Models;

namespace TodoList_MVC.Models.DTOs
{
    public class TaskDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Due date is required")]
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
    }
}

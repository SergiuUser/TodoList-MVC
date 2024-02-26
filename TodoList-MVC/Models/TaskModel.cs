
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList_MVC.Models;

namespace Todo_List_WebApp.Models
{
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Due date is required")]
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
        public bool isCompleted { get; set; } = false;
        [Required]

        public UserModel User { get; set; }
    }
}

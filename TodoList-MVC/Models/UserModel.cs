using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Todo_List_WebApp.Models;

namespace TodoList_MVC.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Username address is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password address is required")]
        public string Password { get; set; }
        public RolesEnum Role { get; set; } = RolesEnum.User;

        public ICollection<TaskModel> Tasks { get; set; }

    }
}

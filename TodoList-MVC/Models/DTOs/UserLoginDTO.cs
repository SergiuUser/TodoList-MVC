using System.ComponentModel.DataAnnotations;

namespace TodoList_MVC.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }
    }
}

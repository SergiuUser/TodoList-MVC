using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Todo_List_WebApp.Models
{
    public enum TaskPriority
    {
        [Display(Name = "Low")]
        Low,
        [Display(Name = "Medium")]
        Medium,
        [Display(Name = "High")]
        High,
        [Display(Name = "Urgent")]
        Urgent
    }
}

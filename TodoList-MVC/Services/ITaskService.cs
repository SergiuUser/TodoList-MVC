using Microsoft.AspNetCore.Mvc.Rendering;
using Todo_List_WebApp.Models;

namespace TodoList_MVC.Services
{
    public interface ITaskService
    {
        List<SelectListItem> GetPriorities();
    }
}

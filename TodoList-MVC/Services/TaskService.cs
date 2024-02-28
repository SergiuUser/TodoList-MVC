using Microsoft.AspNetCore.Mvc.Rendering;
using Todo_List_WebApp.Models;

namespace TodoList_MVC.Services
{
    public class TaskService : ITaskService
    {
        public List<SelectListItem> GetPriorities()
        {
            var _priorities = Enum.GetValues(typeof(TaskPriority))
                     .Cast<TaskPriority>()
                     .Select(p => new SelectListItem
                     {
                         Value = p.ToString(),
                         Text = p.ToString()
                     })
                     .ToList();
            return _priorities;
        }
    }
}

using TodoList_MVC.Models;

namespace TodoList_MVC.Services
{
    public interface IAuthService
    {
        string CreateToken(UserModel user);
    }
}

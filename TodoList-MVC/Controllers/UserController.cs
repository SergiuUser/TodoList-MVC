using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList_MVC.Data;
using TodoList_MVC.Models;
using TodoList_MVC.Models.DTOs;
using TodoList_MVC.Services;

namespace TodoList_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly TodoContext _context;
        private readonly IAuthService _service;
        private readonly IMapper _mapper;

        public UserController(TodoContext context, IAuthService service, IMapper mapper)
        {
            _context = context;
            _service = service;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("User/Register")]
        public async Task<ActionResult> Register(UserRegisterDTO request)
        {
            if (ModelState.IsValid)
            {
                var emailVerification = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
                if (emailVerification != null)
                {
                    TempData["ErrorMessage"] = "This email already exists";
                    return View();
                }
                else
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                    var user = _mapper.Map<UserModel>(request);
                    user.Password = hashedPassword;

                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    var token = _service.CreateToken(user);

                    HttpContext.Response.Cookies.Append("authToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true
                    });

                    ViewBag.UserName = user.Username;
                    return RedirectToAction("Index", "Task");
                }
            } else return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("User/Login")]
        public async Task<ActionResult> Login(UserLoginDTO request)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "Incorrect email";
                    return View();
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    TempData["ErrorMessage"] = "Incorrect Password";
                    return View();
                }

                var token = _service.CreateToken(user);

                HttpContext.Response.Cookies.Append("authToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                });

                ViewBag.UserName = user.Username;
                return RedirectToAction("Index", "Task");
            } else return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("authToken");
            return RedirectToAction("Index", "Task");
        }
    }
}

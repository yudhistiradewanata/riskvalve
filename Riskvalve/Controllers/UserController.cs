using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UserModel login = new();
            if (!login.isLogin(HttpContext))
            {
                TempData["Message"] = "Please login first";
                return Redirect("/Login/Index");
            }
            List<UserModel> userList = new UserModel().GetUserList();
            ViewData["UserList"] = userList;
            return View();
        }

        [HttpGet]
        public IActionResult GetUser(int id)
        {
            UserModel user = new();
            user = new UserModel().GetUserModel(id);
            return Json(user);
        }

        [HttpPost]
        public IActionResult AddUser(UserModel user)
        {
            new UserModel().AddUser(user);
            return Redirect("/User/Index");
        }

        [HttpPost]
        public IActionResult UpdateUser(UserModel user)
        {
            new UserModel().UpdateUser(user);
            return Redirect("/User/Index");
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            new UserModel().DeleteUser(id);
            return Redirect("/User/Index");
        }
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UserModel user = new();
            bool isLogin = user.isLogin(HttpContext);
            if (isLogin)
            {
                return Redirect("/Home/Index");
            }
            string message = TempData["Message"] as string ?? "";
            ViewData["message"] = message;
            ViewData["IsLogin"] = isLogin;
            return View();
        }

        [HttpPost]
        public bool Login(string username, string password)
        {
            UserModel user = new();
            bool loginSuccess = user.doLogin(HttpContext, username, password);
            if (loginSuccess)
            {
                return true;
            }
            return false;
        }

        public IActionResult Logout()
        {
            UserModel user = new();
            user.doLogout(HttpContext);
            return Redirect("/Login/Index");
        }
    }
}

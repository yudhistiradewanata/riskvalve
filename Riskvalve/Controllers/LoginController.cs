using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedLayer;
using BusinessLogicLayer;

namespace Riskvalve.Controllers;
public class LoginController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

    public IActionResult Index()
    {
        bool IsLogin = Session.IsLogin(HttpContext);
        if (IsLogin)
        {
            return Redirect(SharedEnvironment.app_path + "/Home/Index");
        }
        else
        {
            string message = TempData["Message"] as string ?? "";
            ViewData["message"] = message;
            ViewData["IsLogin"] = IsLogin.ToString();
            ViewData["AppVersion"] = SharedEnvironment.GetAppVersion();
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string username, string password)
    {
        ResultClass result = new();
        try
        {
            var user = _userService.GetUser(username);
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new Exception("Password is incorrect");
            }
            string loginUsername = user.Username ?? "";
            string loginRole = user.Role ?? "";
            string loginId = user.Id.ToString() ?? "";
            string loginIsAdmin = user.IsAdmin.ToString() ?? "";
            string loginIsEngineer = user.IsEngineer.ToString() ?? "";
            string loginIsViewer = user.IsViewer.ToString() ?? "";
            HttpContext.Session.SetString("IsLogin", "true");
            HttpContext.Session.SetString("Username", loginUsername);
            HttpContext.Session.SetString("Role", loginRole);
            HttpContext.Session.SetString("Id", loginId);
            HttpContext.Session.SetString("IsAdmin", loginIsAdmin);
            HttpContext.Session.SetString("IsEngineer", loginIsEngineer);
            HttpContext.Session.SetString("IsViewer", loginIsViewer);
            result.IsSuccess = true;
            result.Message = "Login Success";
            result.Data = null;
            return Json(result);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.Message = e.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        ResultClass result = new();
        HttpContext.Session.Clear();
        result.IsSuccess = true;
        result.Message = "Logout Success";
        return Json(result);
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

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
            return Redirect(Environment.app_path+"/Login/Index");
        }
        else
        {
            Dictionary<string, string> session = login.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
            if (ViewData["IsAdmin"].ToString().ToLower().Equals("false"))
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect(Environment.app_path+"/Home/Index");
            }
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
    public bool AddUser(UserModel user)
    {
        bool result = new UserModel().AddUser(user);
        return result;
    }

    [HttpPost]
    public IActionResult UpdateUser(UserModel user)
    {
        new UserModel().UpdateUser(user);
        return Redirect(Environment.app_path+"/User/Index");
    }

    [HttpPost]
    public IActionResult DeleteUser(int id)
    {
        new UserModel().DeleteUser(id);
        return Redirect(Environment.app_path+"/User/Index");
    }
}

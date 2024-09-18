using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedLayer;
using BusinessLogicLayer;

namespace Riskvalve.Controllers;

public class UserController(IUserService userService) : Controller
{
    private readonly IUserService _userService = userService;

    public IActionResult Index()
    {
        bool IsLogin = Session.IsLogin(HttpContext);
        if (!IsLogin)
        {
            TempData["Message"] = "Please login first";
            return Redirect(SharedEnvironment.app_path + "/Login/Index");
        }
        else
        {
            TempData["Message"] = null;
            Dictionary<string, string> session = Session.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
            if (ViewData.ContainsKey("IsAdmin") && ViewData["IsAdmin"]?.ToString()?.ToLower().Equals("false") == true)
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect(SharedEnvironment.app_path + "/Home/Index");
            }
        }
        List<UserData> userList = _userService.GetUserList();
        ViewData["UserList"] = userList;
        return View();
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetUser(int id)
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "User");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            var user = _userService.GetUser(id);
            result.IsSuccess = true;
            result.Message = "User found";
            result.Data = user;
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
    public IActionResult AddUser()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "User");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int createdBy = 0;
            if (HttpContext.Session.GetString("Id") != null)
            {
                if (!int.TryParse(HttpContext.Session.GetString("Id"), out createdBy))
                {
                    throw new Exception("Invalid session Id");
                }
            }
            UserClass user =
                new()
                {
                    Username = Request.Form["Username"],
                    Password = Request.Form["Password"],
                    Role = Request.Form["Role"],
                    IsAdmin = Request.Form["IsAdmin"].ToString().ToLower().Equals("true"),
                    IsEngineer = Request.Form["IsEngineer"].ToString().ToLower().Equals("true"),
                    IsViewer = Request.Form["IsViewer"].ToString().ToLower().Equals("true"),
                    IsDeleted = false,
                    CreatedBy = createdBy,
                    CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
                };
            UserData userresult = _userService.AddUser(user);
            result.IsSuccess = true;
            result.Message = "User added";
            result.Data = userresult;
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
    public IActionResult UpdateUser()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "User");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int updateby = 0;
            if (HttpContext.Session.GetString("Id") != null)
            {
                if (!int.TryParse(HttpContext.Session.GetString("Id"), out updateby))
                {
                    throw new Exception("Invalid session Id");
                }
            }
            if (!int.TryParse(Request.Form["Id"], out int id))
            {
                throw new Exception("Invalid Id");
            }
            UserClass user =
                new()
                {
                    Id = id,
                    Username = Request.Form["Username"],
                    Password = Request.Form["Password"],
                    Role = Request.Form["Role"],
                    IsAdmin = Request.Form["IsAdmin"].ToString().ToLower().Equals("true"),
                    IsEngineer = Request.Form["IsEngineer"].ToString().ToLower().Equals("true"),
                    IsViewer = Request.Form["IsViewer"].ToString().ToLower().Equals("true"),
                    IsDeleted = false,
                    UpdatedBy = updateby,
                    UpdatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
                };
            UserData userresult = _userService.UpdateUser(user);
            result.IsSuccess = true;
            result.Message = "User updated";
            result.Data = userresult;
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
    public IActionResult DeleteUser()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "User");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            if (!int.TryParse(Request.Form["Id"], out int id))
            {
                throw new Exception("Invalid Id");
            }

            int deletedBy = 0;
            if (HttpContext.Session.GetString("Id") != null)
            {
                if (!int.TryParse(HttpContext.Session.GetString("Id"), out deletedBy))
                {
                    throw new Exception("Invalid session Id");
                }
            }
            UserClass user =
                new()
                {
                    Id = id,
                    IsDeleted = true,
                    DeletedBy = deletedBy,
                    DeletedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
                };
            UserData userresult = _userService.DeleteUser(user);
            result.IsSuccess = true;
            result.Message = "User deleted";
            result.Data = userresult;
            return Json(result);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.Message = e.Message;
            return Json(result);
        }
    }
}

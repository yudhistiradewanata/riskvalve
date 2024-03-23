using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class ToolController : Controller
{
    private readonly ILogger<ToolController> _logger;

    public ToolController(ILogger<ToolController> logger)
    {
        _logger = logger;
    }

    public IActionResult ImportAssetRegister()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect("/Login/Index");
        }
        return View();
    }

    public IActionResult ImportAssesment()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect("/Login/Index");
        }
        return View();
    }
}
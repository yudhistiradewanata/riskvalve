using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class AssesmentController : Controller
{
    private readonly ILogger<AssesmentController> _logger;

    public AssesmentController(ILogger<AssesmentController> logger)
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
        InspectionSidebarHistory inspectionSidebarHistory = new();
        List<InspectionSidebarModel> inspectionSidebar = inspectionSidebarHistory.GetInspectionSidebarHistory();
        ViewData["InspectionSidebar"] = inspectionSidebar;
        ViewData["pageType"] = "Assesment";
        return View();
    }
}

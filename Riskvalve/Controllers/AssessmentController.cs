using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class AssessmentController : Controller
{
    private readonly ILogger<AssessmentController> _logger;

    public AssessmentController(ILogger<AssessmentController> logger)
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
        } else {
            Dictionary<string, string> session = login.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
        }
        InspectionSidebarHistory inspectionSidebarHistory = new();
        List<InspectionSidebarModel> inspectionSidebar = inspectionSidebarHistory.GetInspectionSidebarHistory("Assessment");
        ViewData["InspectionSidebar"] = inspectionSidebar;
        ViewData["pageType"] = "Assessment";
        return View();
    }
}

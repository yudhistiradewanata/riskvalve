using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers
{
    public class InspectionController : Controller
    {
        private readonly ILogger<InspectionController> _logger;

        public InspectionController(ILogger<InspectionController> logger)
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
            return View();
        }
    }
}

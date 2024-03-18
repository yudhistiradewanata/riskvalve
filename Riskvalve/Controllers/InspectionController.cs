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
            InspectionSidebarHistory inspectionSidebarHistory = new();
            List<InspectionSidebarModel> inspectionSidebar = inspectionSidebarHistory.GetInspectionSidebarHistory();
            ViewData["InspectionSidebar"] = inspectionSidebar;
            return View();
        }
    }
}

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
			var json = "";
            List<AssetModel> AssetList = new List<AssetModel>();
            AssetList.Add(new AssetModel { Id = 1, TagNo = "WIB-PSV-V05A-4" });
            return View();
        }


    }
}

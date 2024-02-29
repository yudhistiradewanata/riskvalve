using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class AssetRegisterController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AssetRegisterController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Area()
    {
        List<AreaModel> areaList = new List<AreaModel>();
        areaList.Add(new AreaModel { Id = 1, BusinessArea = "Area 1" });
        areaList.Add(new AreaModel { Id = 2, BusinessArea = "Area 2" });
        areaList.Add(new AreaModel { Id = 3, BusinessArea = "Area 3" });
        areaList.Add(new AreaModel { Id = 4, BusinessArea = "Area 4" });
        areaList.Add(new AreaModel { Id = 5, BusinessArea = "Area 5" });
        ViewData["AreaList"] = areaList;
        return View();
    }

    public IActionResult Platform()
    {
        return View();
    }

    public IActionResult Asset()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


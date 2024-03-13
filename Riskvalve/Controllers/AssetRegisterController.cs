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
        AreaModel areaModel = new AreaModel();
        List<AreaModel> areaList = areaModel.GetAreaList();
        ViewData["AreaList"] = areaList;
        return View();
    }

    [HttpGet]
    public IActionResult GetAreaDetail()
    {
        AreaModel areaModel = new AreaModel();
        int id = Convert.ToInt32(Request.Query["id"]);
        AreaModel area = areaModel.GetAreaModel(id);
        return Json(area);
    }

    [HttpPost]
    public IActionResult UpdateArea()
    {
        AreaModel areaModel = new()
        {
            Id = Convert.ToInt32(Request.Form["Id"]),
            BusinessArea = Request.Form["BusinessArea"]
        };
        areaModel.UpdateArea(areaModel);
        return RedirectToAction("Area");
    }

    [HttpPost]
    public IActionResult AddArea()
    {
        AreaModel areaModel = new()
        {
            BusinessArea = Request.Form["BusinessArea"]
        };
        areaModel.AddArea(areaModel);
        return RedirectToAction("Area");
    }

    [HttpPost]
    public IActionResult DeleteArea()
    {
        AreaModel areaModel = new();
        int id = Convert.ToInt32(Request.Form["Id"]);
        areaModel.DeleteArea(id);
        return RedirectToAction("Area");
    }

    public IActionResult Platform()
    {
        PlatformModel platformModel = new();
        List<PlatformModel> platformList = platformModel.GetPlatformList();
        ViewData["PlatformList"] = platformList;
        List<AreaModel> areaList = new AreaModel().GetAreaList();
        ViewData["AreaList"] = areaList;
        return View();
    }

    [HttpGet]
    public IActionResult GetPlatformDetail()
    {
        PlatformModel platformModel = new();
        int id = Convert.ToInt32(Request.Query["id"]);
        PlatformModel platform = platformModel.GetPlatformModel(id);
        return Json(platform);
    }

    [HttpPost]
    public IActionResult UpdatePlatform()
    {
        PlatformModel platformModel = new();
        PlatformDB platformDb = new()
        {
            Id = Convert.ToInt32(Request.Form["Id"]),
            AreaID = Convert.ToInt32(Request.Form["AreaID"]),
            Platform = Request.Form["Platform"],
            Code = Request.Form["Code"]
        };
        platformModel.UpdatePlatform(platformDb);
        return RedirectToAction("Platform");
    }

    [HttpPost]
    public IActionResult AddPlatform()
    {
        PlatformModel platformModel = new();
        PlatformDB platformDb = new()
        {
            AreaID = Convert.ToInt32(Request.Form["AreaID"]),
            Platform = Request.Form["Platform"],
            Code = Request.Form["Code"]
        };
        platformModel.AddPlatform(platformDb);
        return RedirectToAction("Platform");
    }

    [HttpPost]
    public IActionResult DeletePlatform()
    {
        PlatformModel platformModel = new();
        int id = Convert.ToInt32(Request.Form["Id"]);
        platformModel.DeletePlatform(id);
        return RedirectToAction("Platform");
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


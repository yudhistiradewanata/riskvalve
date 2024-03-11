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
        AreaModel areaModel = new AreaModel();
        areaModel.Id = Convert.ToInt32(Request.Form["Id"]);
        areaModel.BusinessArea = Request.Form["BusinessArea"];
        areaModel.UpdateArea(areaModel);
        return RedirectToAction("Area");
    }

    [HttpPost]
    public IActionResult AddArea()
    {
        AreaModel areaModel = new AreaModel();
        areaModel.BusinessArea = Request.Form["BusinessArea"];
        // areaModel.BusinessArea = "Lanjutan";
        areaModel.AddArea(areaModel);
        return RedirectToAction("Area");
    }

    [HttpPost]
    public IActionResult DeleteArea()
    {
        AreaModel areaModel = new AreaModel();
        int id = Convert.ToInt32(Request.Form["Id"]);
        areaModel.DeleteArea(id);
        return RedirectToAction("Area");
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


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
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
        else
        {
            Dictionary<string, string> session = login.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
        }
        return View();
    }

    [HttpGet]
    public IActionResult Detail()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect("/Login/Index");
        }
        else
        {
            Dictionary<string, string> session = login.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
        }

        AssetModel assetModel = new();
        List<AssetModel> assetList = assetModel.GetAssetList();
        ViewData["AssetList"] = assetList;

        List<PlatformModel> platformList = new PlatformModel().GetPlatformList();
        ViewData["PlatformList"] = platformList;

        List<ValveTypeModel> valveTypeList = new ValveTypeModel().GetValveTypeList();
        ViewData["ValveTypeList"] = valveTypeList;

        List<ManualOverrideModel> manualOverrideList =
            new ManualOverrideModel().GetManualOverrideList();
        ViewData["ManualOverrideList"] = manualOverrideList;

        List<FluidPhaseModel> fluidPhaseList = new FluidPhaseModel().GetFluidPhaseList();
        ViewData["FluidPhaseList"] = fluidPhaseList;

        List<ToxicOrFlamableFluidModel> toxicOrFlamableFluidList =
            new ToxicOrFlamableFluidModel().GetToxicOrFlamableFluidList();
        ViewData["ToxicOrFlamableFluidList"] = toxicOrFlamableFluidList;

        List<AreaModel> areaList = new AreaModel().GetAreaList();
        ViewData["AreaList"] = areaList;
        return View();
    }

    public IActionResult Privacy()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect("/Login/Index");
        }
        else
        {
            Dictionary<string, string> session = login.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}

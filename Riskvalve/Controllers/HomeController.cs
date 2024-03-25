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
        return View();
    }

    public IActionResult Detail()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect("/Login/Index");
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

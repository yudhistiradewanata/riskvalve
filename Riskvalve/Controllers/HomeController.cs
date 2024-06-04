using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            return Redirect(Environment.app_path + "/Login/Index");
        }
        else
        {
            TempData["Message"] = null;
            Dictionary<string, string> session = login.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
        }

        AssessmentModel assessmentModel = new();
        Dictionary<string, string> assessmentHeatMap = new();
        Dictionary<string, string> assessmentPieChart = new();
        Dictionary<string, string> assessmentBarChart = new();
        Dictionary<string, Dictionary<string, string>> assessmentBarChartFinal = new();
        Dictionary<string, Dictionary<string, string>> recap_assessment = new();
        recap_assessment = assessmentModel.GetAssessmentRecap();
        assessmentHeatMap = recap_assessment["heatmap"];
        assessmentPieChart = recap_assessment["piechart"];
        assessmentBarChart = recap_assessment["barchart"];
        foreach (var item in assessmentBarChart)
        {
            Dictionary<string, string> temp = new();
            temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.Value);
            assessmentBarChartFinal.Add(item.Key, temp);
        }
        ViewData["AssessmentHeatMap"] = assessmentHeatMap;
        ViewData["AssessmentPieChart"] = assessmentPieChart;
        ViewData["AssessmentBarChart"] = assessmentBarChartFinal;
        return View();
    }

    [HttpGet]
    public IActionResult _Detail_BAK()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect(Environment.app_path + "/Login/Index");
        }
        else
        {
            TempData["Message"] = null;
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

    public IActionResult Detail()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect(Environment.app_path + "/Login/Index");
        }
        else
        {
            TempData["Message"] = null;
            Dictionary<string, string> session = login.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
            if (ViewData["IsEngineer"].ToString().ToLower().Equals("false"))
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect(Environment.app_path + "/Home/Index");
            }
        }
        List<AreaModel> areaList = new AreaModel().GetAreaList();
        List<AssessmentModel> assessmentList = new AssessmentModel().GetAssessmentRecapList(
            0,
            0,
            false,
            false
        );
        ViewData["AreaList"] = areaList;
        ViewData["AssessmentList"] = assessmentList;
        return View();
    }

    public IActionResult Privacy()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect(Environment.app_path + "/Login/Index");
        }
        else
        {
            TempData["Message"] = null;
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

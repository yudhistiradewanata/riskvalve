using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;
using Newtonsoft.Json;

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
        // string assessmentHeatMapString = JsonConvert.SerializeObject(assessmentBarChartFinal);
        // Console.WriteLine("AssessmentHeatMap: " + assessmentHeatMapString);
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect(Environment.app_path+"/Login/Index");
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
            return Redirect(Environment.app_path+"/Login/Index");
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
            return Redirect(Environment.app_path+"/Login/Index");
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

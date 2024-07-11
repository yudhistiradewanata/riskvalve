using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedLayer;

namespace Riskvalve.Controllers;

public class HomeController(
    IAreaService areaService,
    IAssessmentService assessmentService,
    IAssetService assetService
) : Controller{
    private readonly IAreaService _areaService = areaService;
    private readonly IAssessmentService _assessmentService = assessmentService;
    private readonly IAssetService _assetService = assetService;
    public IActionResult Index()
    {
        bool IsLogin = Session.IsLogin(HttpContext);
        if (!IsLogin)
        {
            TempData["Message"] = "Please login first";
            return Redirect(SharedEnvironment.app_path + "/Login/Index");
        }
        else
        {
            TempData["Message"] = null;
            Dictionary<string, string> session = Session.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
        }

        Dictionary<string, string> assessmentHeatMap = [];
        Dictionary<string, string> assessmentPieChart = [];
        Dictionary<string, string> assessmentBarChart = [];
        Dictionary<string, Dictionary<string, string>> assessmentBarChartFinal = [];
        Dictionary<string, string> assessmentIntegrity = [];
        Dictionary<string, Dictionary<string, string>> recap_assessment = [];
        recap_assessment = _assessmentService.GetAssessmentRecap();
        assessmentHeatMap = recap_assessment["heatmap"];
        assessmentPieChart = recap_assessment["piechart"];
        assessmentBarChart = recap_assessment["barchart"];
        foreach (var item in assessmentBarChart)
        {
            Dictionary<string, string> temp = [];
            temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.Value) ?? [];
            assessmentBarChartFinal.Add(item.Key, temp);
        }
        assessmentIntegrity = recap_assessment["integritystatus"];
        Dictionary<string, int> assetDistribution = _assetService.GetAssetDistribution();
        ViewData["AssessmentHeatMap"] = assessmentHeatMap;
        ViewData["AssessmentPieChart"] = assessmentPieChart;
        ViewData["AssessmentBarChart"] = assessmentBarChartFinal;
        ViewData["AssessmentIntegrity"] = assessmentIntegrity;
        ViewData["AssetDistribution"] = assetDistribution;
        // return Json(ViewData);
        return View();
    }

    public IActionResult Detail()
    {
        bool IsLogin = Session.IsLogin(HttpContext);
        if (!IsLogin)
        {
            TempData["Message"] = "Please login first";
            return Redirect(SharedEnvironment.app_path + "/Login/Index");
        }
        else
        {
            TempData["Message"] = null;
            Dictionary<string, string> session = Session.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
            if (ViewData.ContainsKey("IsEngineer") && ViewData["IsEngineer"]?.ToString()?.ToLower().Equals("false") == true)
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect(SharedEnvironment.app_path + "/Home/Index");
            }
        }
        List<AreaData> areaList = _areaService.GetAreaList();
        List<AssessmentData> assessmentList = _assessmentService.GetAssessmentRecapList();
        ViewData["AreaList"] = areaList;
        ViewData["AssessmentList"] = assessmentList;
        return View();
    }

    // public IActionResult RegenAssessment()
    // {
    //     List<int> ints = _assessmentService.GetAssessmentList().Select(x => x.Id).ToList();
    //     AssessmentData assessment = new AssessmentData();
    //     foreach (var item in ints)
    //     {
    //         try{
    //         _assessmentService.CalculateAssessment(item);
    //         }
    //         catch(Exception e){
    //             Console.WriteLine(e.Message);
    //         }
    //     }
    //     return Json(assessment);
    // }
}

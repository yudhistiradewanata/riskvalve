using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class AssessmentController : Controller
{
    private readonly ILogger<AssessmentController> _logger;

    public AssessmentController(ILogger<AssessmentController> logger)
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
            if (ViewData["IsEngineer"].ToString().ToLower().Equals("false"))
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect(Environment.app_path + "/Home/Index");
            }
        }
        InspectionSidebarHistory inspectionSidebarHistory = new();
        List<InspectionSidebarModel> inspectionSidebar =
            inspectionSidebarHistory.GetInspectionSidebarHistory("Assessment");
        ViewData["InspectionSidebar"] = inspectionSidebar;
        ViewData["pageType"] = "Assessment";
        ViewData["date"] = DateTime.Now.ToString(Environment.GetDateFormatString(false));
        return View();
    }

    public IActionResult PrintAssessment(int id)
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
        AssessmentModel assessment = new();
        AssessmentModel assessmentModel = assessment.GetAssessmentModel(id);
        ViewData["Assessment"] = assessmentModel;

        string tp1 = assessmentModel.TP1Risk;
        string tp1_color = assessmentModel.GetHeatColor(tp1);
        Dictionary<string, int> tp1_xypos = assessmentModel.GetHeatXYpos(tp1);
        string tp1_xpos = tp1_xypos["xpos"].ToString();
        string tp1_ypos = tp1_xypos["ypos"].ToString();
        string tp2 = assessmentModel.TP2Risk;
        string tp2_color = assessmentModel.GetHeatColor(tp2);
        Dictionary<string, int> tp2_xypos = assessmentModel.GetHeatXYpos(tp2);
        string tp2_xpos = tp2_xypos["xpos"].ToString();
        string tp2_ypos = tp2_xypos["ypos"].ToString();
        string tp3 = assessmentModel.TP3Risk;
        string tp3_color = assessmentModel.GetHeatColor(tp3);
        Dictionary<string, int> tp3_xypos = assessmentModel.GetHeatXYpos(tp3);
        string tp3_xpos = tp3_xypos["xpos"].ToString();
        string tp3_ypos = tp3_xypos["ypos"].ToString();
        Dictionary<string, Dictionary<string, string>> assessmentHeatMap = new();
        assessmentHeatMap.Add(
            "TP1",
            new Dictionary<string, string>
            {
                { "value", tp1 },
                { "color", tp1_color },
                { "xpos", tp1_xpos },
                { "ypos", tp1_ypos }
            }
        );
        assessmentHeatMap.Add(
            "TP2",
            new Dictionary<string, string>
            {
                { "value", tp2 },
                { "color", tp2_color },
                { "xpos", tp2_xpos },
                { "ypos", tp2_ypos }
            }
        );
        assessmentHeatMap.Add(
            "TP3",
            new Dictionary<string, string>
            {
                { "value", tp3 },
                { "color", tp3_color },
                { "xpos", tp3_xpos },
                { "ypos", tp3_ypos }
            }
        );
        ViewData["AssessmentHeatMap"] = assessmentHeatMap;
        return View();
    }

    [HttpGet]
    public IActionResult GetAssessmentDetail(int id)
    {
        AssessmentModel assessment = new();
        AssessmentModel assessmentModel = assessment.GetAssessmentModel(id);
        return Json(assessmentModel);
    }

    [HttpPost]
    public IActionResult AddAssessment()
    {
        AssessmentModel assessment = new();
        AssessmentDB assessmentDB =
            new()
            {
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                AssessmentNo = Request.Form["AssessmentNo"],
                AssessmentDate = Request.Form["AssessmentDate"],
                TimePeriode = Request.Form["TimePeriode"],
                TimeToLimitStateLeakageToAtmosphere = Request.Form[
                    "TimeToLimitStateLeakageToAtmosphere"
                ],
                TimeToLimitStateFailureOfFunction = Request.Form[
                    "TimeToLimitStateFailureOfFunction"
                ],
                TimeToLimitStatePassingAccrosValve = Request.Form[
                    "TimeToLimitStatePassingAccrosValve"
                ],
                LeakageToAtmosphereID = Environment.StringToInt(
                    Request.Form["LeakageToAtmosphereID"]
                ),
                FailureOfFunctionID = Environment.StringToInt(Request.Form["FailureOfFunctionID"]),
                PassingAccrosValveID = Environment.StringToInt(
                    Request.Form["PassingAccrosValveID"]
                ),
                LeakageToAtmosphereTP1ID = Environment.StringToInt(
                    Request.Form["LeakageToAtmosphereTP1ID"]
                ),
                LeakageToAtmosphereTP2ID = Environment.StringToInt(
                    Request.Form["LeakageToAtmosphereTP2ID"]
                ),
                LeakageToAtmosphereTP3ID = Environment.StringToInt(
                    Request.Form["LeakageToAtmosphereTP3ID"]
                ),
                FailureOfFunctionTP1ID = Environment.StringToInt(
                    Request.Form["FailureOfFunctionTP1ID"]
                ),
                FailureOfFunctionTP2ID = Environment.StringToInt(
                    Request.Form["FailureOfFunctionTP2ID"]
                ),
                FailureOfFunctionTP3ID = Environment.StringToInt(
                    Request.Form["FailureOfFunctionTP3ID"]
                ),
                PassingAccrosValveTP1ID = Environment.StringToInt(
                    Request.Form["PassingAccrosValveTP1ID"]
                ),
                PassingAccrosValveTP2ID = Environment.StringToInt(
                    Request.Form["PassingAccrosValveTP2ID"]
                ),
                PassingAccrosValveTP3ID = Environment.StringToInt(
                    Request.Form["PassingAccrosValveTP3ID"]
                ),
                InspectionEffectivenessID = Environment.StringToInt(
                    Request.Form["InspectionEffectivenessID"]
                ),
                ImpactOfInternalFluidImpuritiesID = Environment.StringToInt(
                    Request.Form["ImpactOfInternalFluidImpuritiesID"]
                ),
                ImpactOfOperatingEnvelopesID = Environment.StringToInt(
                    Request.Form["ImpactOfOperatingEnvelopesID"]
                ),
                UsedWithinOEMSpecificationID = Environment.StringToInt(
                    Request.Form["UsedWithinOEMSpecificationID"]
                ),
                RepairedID = Environment.StringToInt(Request.Form["RepairedID"]),
                ProductLossDefinition = Request.Form["ProductLossDefinition"],
                HSSEDefinisionID = Environment.StringToInt(Request.Form["HSSEDefinisionID"]),
                Summary = Request.Form["Summary"],
                RecommendationActionID = Environment.StringToInt(
                    Request.Form["RecommendationActionID"]
                ),
                DetailedRecommendation = Request.Form["DetailedRecommendation"],
                ConsequenceOfFailure = Request.Form["consequenceOfFailure"],
                TP1A = Request.Form["tP1A"],
                TP1B = Request.Form["tP1B"],
                TP1C = Request.Form["tP1C"],
                TP2A = Request.Form["tP2A"],
                TP2B = Request.Form["tP2B"],
                TP2C = Request.Form["tP2C"],
                TP3A = Request.Form["tP3A"],
                TP3B = Request.Form["tP3B"],
                TP3C = Request.Form["tP3C"],
                TPTimeToActionA = Request.Form["tpTimeToActionA"],
                TPTimeToActionB = Request.Form["tpTimeToActionB"],
                TPTimeToActionC = Request.Form["tpTimeToActionC"],
                TP1Risk = Request.Form["tP1Risk"],
                TP2Risk = Request.Form["tP2Risk"],
                TP3Risk = Request.Form["tP3Risk"],
                TPTimeToActionRisk = Request.Form["tpTimeToActionRisk"],
                LoFScoreLeakageToAtmophereTP1 = Environment.StringToDouble(
                    Request.Form["LoFScoreLeakageToAtmophereTP1"]
                ),
                LoFScoreLeakageToAtmophereTP2 = Environment.StringToDouble(
                    Request.Form["LoFScoreLeakageToAtmophereTP2"]
                ),
                LoFScoreLeakageToAtmophereTP3 = Environment.StringToDouble(
                    Request.Form["LoFScoreLeakageToAtmophereTP3"]
                ),
                LoFScoreFailureOfFunctionTP1 = Environment.StringToDouble(
                    Request.Form["LoFScoreFailureOfFunctionTP1"]
                ),
                LoFScoreFailureOfFunctionTP2 = Environment.StringToDouble(
                    Request.Form["LoFScoreFailureOfFunctionTP2"]
                ),
                LoFScoreFailureOfFunctionTP3 = Environment.StringToDouble(
                    Request.Form["LoFScoreFailureOfFunctionTP3"]
                ),
                LoFScorePassingAccrosValveTP1 = Environment.StringToDouble(
                    Request.Form["LoFScorePassingAccrossValveTP1"]
                ),
                LoFScorePassingAccrosValveTP2 = Environment.StringToDouble(
                    Request.Form["LoFScorePassingAccrossValveTP2"]
                ),
                LoFScorePassingAccrosValveTP3 = Environment.StringToDouble(
                    Request.Form["LoFScorePassingAccrossValveTP3"]
                ),
                CoFScore = Environment.StringToDouble(Request.Form["CoFScore"]),
                IntegrityStatus = Request.Form["IntegrityStatus"],
                IsDeleted = false,
                CreatedBy = Environment.StringToInt(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        int assessmentID = assessment.AddAssessment(assessmentDB);
        List<int> inspectionIDs = Request
            .Form["selectedInspectionId"]
            .ToString()
            .Split(',')
            .Select(int.Parse)
            .ToList();
        assessment.AddInspectionToAssessment(assessmentID, inspectionIDs);
        if (Request.Form["selectedMaintenanceId"].ToString() != "")
        {
            List<int> maintenanceIDs = Request
                .Form["selectedMaintenanceId"]
                .ToString()
                .Split(',')
                .Select(int.Parse)
                .ToList();
            assessment.AddMaintenanceToAssessment(assessmentID, maintenanceIDs);
        }
        assessment = assessment.GetAssessmentModel(assessmentID);
        return Json(assessment);
    }

    [HttpPost]
    public IActionResult UpdateAssessment()
    {
        int assessmentID = Convert.ToInt32(Request.Form["Id"]);
        AssessmentModel assessment = new();
        AssessmentDB assessmentDB =
            new()
            {
                Id = assessmentID,
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                AssessmentNo = Request.Form["AssessmentNo"],
                AssessmentDate = Request.Form["AssessmentDate"],
                TimePeriode = Request.Form["TimePeriode"],
                TimeToLimitStateLeakageToAtmosphere = Request.Form[
                    "TimeToLimitStateLeakageToAtmosphere"
                ],
                TimeToLimitStateFailureOfFunction = Request.Form[
                    "TimeToLimitStateFailureOfFunction"
                ],
                TimeToLimitStatePassingAccrosValve = Request.Form[
                    "TimeToLimitStatePassingAccrosValve"
                ],
                LeakageToAtmosphereID = Environment.StringToInt(
                    Request.Form["LeakageToAtmosphereID"]
                ),
                FailureOfFunctionID = Environment.StringToInt(Request.Form["FailureOfFunctionID"]),
                PassingAccrosValveID = Environment.StringToInt(
                    Request.Form["PassingAccrosValveID"]
                ),
                LeakageToAtmosphereTP1ID = Environment.StringToInt(
                    Request.Form["LeakageToAtmosphereTP1ID"]
                ),
                LeakageToAtmosphereTP2ID = Environment.StringToInt(
                    Request.Form["LeakageToAtmosphereTP2ID"]
                ),
                LeakageToAtmosphereTP3ID = Environment.StringToInt(
                    Request.Form["LeakageToAtmosphereTP3ID"]
                ),
                FailureOfFunctionTP1ID = Environment.StringToInt(
                    Request.Form["FailureOfFunctionTP1ID"]
                ),
                FailureOfFunctionTP2ID = Environment.StringToInt(
                    Request.Form["FailureOfFunctionTP2ID"]
                ),
                FailureOfFunctionTP3ID = Environment.StringToInt(
                    Request.Form["FailureOfFunctionTP3ID"]
                ),
                PassingAccrosValveTP1ID = Environment.StringToInt(
                    Request.Form["PassingAccrosValveTP1ID"]
                ),
                PassingAccrosValveTP2ID = Environment.StringToInt(
                    Request.Form["PassingAccrosValveTP2ID"]
                ),
                PassingAccrosValveTP3ID = Environment.StringToInt(
                    Request.Form["PassingAccrosValveTP3ID"]
                ),
                InspectionEffectivenessID = Environment.StringToInt(
                    Request.Form["InspectionEffectivenessID"]
                ),
                ImpactOfInternalFluidImpuritiesID = Environment.StringToInt(
                    Request.Form["ImpactOfInternalFluidImpuritiesID"]
                ),
                ImpactOfOperatingEnvelopesID = Environment.StringToInt(
                    Request.Form["ImpactOfOperatingEnvelopesID"]
                ),
                UsedWithinOEMSpecificationID = Environment.StringToInt(
                    Request.Form["UsedWithinOEMSpecificationID"]
                ),
                RepairedID = Environment.StringToInt(Request.Form["RepairedID"]),
                ProductLossDefinition = Request.Form["ProductLossDefinition"],
                HSSEDefinisionID = Environment.StringToInt(Request.Form["HSSEDefinisionID"]),
                Summary = Request.Form["Summary"],
                RecommendationActionID = Environment.StringToInt(
                    Request.Form["RecommendationActionID"]
                ),
                DetailedRecommendation = Request.Form["DetailedRecommendation"],
                ConsequenceOfFailure = Request.Form["consequenceOfFailure"],
                TP1A = Request.Form["tP1A"],
                TP1B = Request.Form["tP1B"],
                TP1C = Request.Form["tP1C"],
                TP2A = Request.Form["tP2A"],
                TP2B = Request.Form["tP2B"],
                TP2C = Request.Form["tP2C"],
                TP3A = Request.Form["tP3A"],
                TP3B = Request.Form["tP3B"],
                TP3C = Request.Form["tP3C"],
                TPTimeToActionA = Request.Form["tpTimeToActionA"],
                TPTimeToActionB = Request.Form["tpTimeToActionB"],
                TPTimeToActionC = Request.Form["tpTimeToActionC"],
                TP1Risk = Request.Form["tP1Risk"],
                TP2Risk = Request.Form["tP2Risk"],
                TP3Risk = Request.Form["tP3Risk"],
                TPTimeToActionRisk = Request.Form["tpTimeToActionRisk"],
                LoFScoreLeakageToAtmophereTP1 = Environment.StringToDouble(
                    Request.Form["LoFScoreLeakageToAtmophereTP1"]
                ),
                LoFScoreLeakageToAtmophereTP2 = Environment.StringToDouble(
                    Request.Form["LoFScoreLeakageToAtmophereTP2"]
                ),
                LoFScoreLeakageToAtmophereTP3 = Environment.StringToDouble(
                    Request.Form["LoFScoreLeakageToAtmophereTP3"]
                ),
                LoFScoreFailureOfFunctionTP1 = Environment.StringToDouble(
                    Request.Form["LoFScoreFailureOfFunctionTP1"]
                ),
                LoFScoreFailureOfFunctionTP2 = Environment.StringToDouble(
                    Request.Form["LoFScoreFailureOfFunctionTP2"]
                ),
                LoFScoreFailureOfFunctionTP3 = Environment.StringToDouble(
                    Request.Form["LoFScoreFailureOfFunctionTP3"]
                ),
                LoFScorePassingAccrosValveTP1 = Environment.StringToDouble(
                    Request.Form["LoFScorePassingAccrossValveTP1"]
                ),
                LoFScorePassingAccrosValveTP2 = Environment.StringToDouble(
                    Request.Form["LoFScorePassingAccrossValveTP2"]
                ),
                LoFScorePassingAccrosValveTP3 = Environment.StringToDouble(
                    Request.Form["LoFScorePassingAccrossValveTP3"]
                ),
                CoFScore = Environment.StringToDouble(Request.Form["CoFScore"]),
                IntegrityStatus = Request.Form["IntegrityStatus"],
                IsDeleted = false,
                CreatedBy = Environment.StringToInt(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        // string json = JsonConvert.SerializeObject(assessmentDB);
        // return Json(json);
        // return Json(assessmentDB);
        assessment.UpdateAssessment(assessmentDB);
        List<int> inspectionIDs = Request
            .Form["selectedInspectionId"]
            .ToString()
            .Split(',')
            .Select(int.Parse)
            .ToList();
        assessment.AddInspectionToAssessment(assessmentID, inspectionIDs, true);
        if (Request.Form["selectedMaintenanceId"].ToString() != "")
        {
            List<int> maintenanceIDs = Request
                .Form["selectedMaintenanceId"]
                .ToString()
                .Split(',')
                .Select(int.Parse)
                .ToList();
            assessment.AddMaintenanceToAssessment(assessmentID, maintenanceIDs, true);
        }
        assessment = assessment.GetAssessmentModel(assessmentDB.Id);
        return Json(assessment);
    }

    [HttpPost]
    public void DeleteAssessment()
    {
        int AssessmentID = Convert.ToInt32(Request.Form["Id"]);
        AssessmentModel assessmentModel = new();
        AssessmentDB assessment =
            new()
            {
                Id = AssessmentID,
                IsDeleted = true,
                DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        assessmentModel.DeleteAssessment(assessment);
    }

    [HttpGet]
    public IActionResult GetAssessmentList(int AreaID = 0, int PlatformID = 0)
    {
        List<AssessmentModel> assessmentList = new AssessmentModel().GetAssessmentRecapList(
            AreaID,
            PlatformID,
            false,
            false
        );
        return Json(assessmentList);
    }

    [HttpGet]
    public IActionResult GetAssessmentSidebar(int AssetID)
    {
        List<AssessmentModel> assessmentList = new AssessmentModel().GetAssessmentList(
            0,
            0,
            false,
            false,
            AssetID
        );
        List<Dictionary<string, string>> assessmentSidebar = new();
        foreach (var assessment in assessmentList)
        {
            Dictionary<string, string> assessmentSidebarItem =
                new()
                {
                    { "Id", assessment.Id.ToString() },
                    { "Name", assessment.AssessmentDate },
                    { "Area", assessment.Asset.BusinessArea },
                    { "Platform", assessment.Asset.Platform },
                    { "Asset", assessment.Asset.TagNo },
                    { "AssetID", assessment.Asset.Id.ToString() }
                };
            assessmentSidebar.Add(assessmentSidebarItem);
        }
        assessmentSidebar = assessmentSidebar
            .OrderByDescending(i =>
                DateTime.ParseExact(i["Name"], "dd-MM-yyyy", CultureInfo.InvariantCulture)
            )
            .ToList();
        return Json(assessmentSidebar);
    }
}

using System.Diagnostics;
using System.Globalization;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SharedLayer;

namespace Riskvalve.Controllers;

public class AssessmentController(
    IAreaService areaService,
    IAssessmentService assessmentService
) : Controller
{
    private readonly IAreaService _areaService = areaService;
    private readonly IAssessmentService _assessmentService = assessmentService;
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
            if (
                ViewData.ContainsKey("IsEngineer") &&
                ViewData["IsEngineer"]?.ToString()?.ToLower().Equals("false") == true
            )
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect(SharedEnvironment.app_path + "/Home/Index");
            }
        }
        ViewData["CurrentConditionLimitStateData"] = _assessmentService.CurrentConditionLimitStateDatas();
        ViewData["InspectionEffectivenessData"] = _assessmentService.InspectionEffectivenessDatas();
        ViewData["IsValveRepairedData"] = _assessmentService.IsValveRepairedDatas();
        ViewData["InspectionMethodData"] = _assessmentService.InspectionMethodDatas();
        ViewData["TimeToLimitStateData"] = _assessmentService.TimeToLimitStateDatas();
        ViewData["ImpactEffectData"] = _assessmentService.ImpactEffectDatas();
        ViewData["UsedWithinOEMSpecificationData"] = _assessmentService.UsedWithinOEMSpecificationDatas();
        ViewData["RepairedData"] = _assessmentService.RepairedDatas();
        ViewData["HSSEDefinisionData"] = _assessmentService.HSSEDefinisionDatas();
        ViewData["RecommendationActionData"] = _assessmentService.RecomendationActionDatas();
        ViewData["InspectionSidebar"] = _areaService.GetSidebarData();
        ViewData["pageType"] = "Assessment";
        ViewData["date"] = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString(false));
        return View();
    }

    public IActionResult PrintAssessment(int id)
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
            if (
                ViewData.ContainsKey("IsEngineer") &&
                ViewData["IsEngineer"]?.ToString()?.ToLower().Equals("false") == true
            )
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect(SharedEnvironment.app_path + "/Home/Index");
            }
        }
        AssessmentData assessmentData = _assessmentService.GetAssessment(id);
        string tp1 = assessmentData.TP1Risk ?? "";
        string tp2 = assessmentData.TP2Risk ?? "";
        string tp3 = assessmentData.TP3Risk ?? "";
        string tp1_color =  AssessmentStaticClass.GetHeatColor(tp1);
        Dictionary<string, int> tp1_xypos = AssessmentStaticClass.GetHeatXYpos(tp1);
        string tp1_xpos = tp1_xypos["xpos"].ToString();
        string tp1_ypos = tp1_xypos["ypos"].ToString();
        string tp2_color = AssessmentStaticClass.GetHeatColor(tp2);
        Dictionary<string, int> tp2_xypos = AssessmentStaticClass.GetHeatXYpos(tp2);
        string tp2_xpos = tp2_xypos["xpos"].ToString();
        string tp2_ypos = tp2_xypos["ypos"].ToString();
        string tp3_color = AssessmentStaticClass.GetHeatColor(tp3);
        Dictionary<string, int> tp3_xypos = AssessmentStaticClass.GetHeatXYpos(tp3);
        string tp3_xpos = tp3_xypos["xpos"].ToString();
        string tp3_ypos = tp3_xypos["ypos"].ToString();
        Dictionary<string, Dictionary<string, string>> assessmentHeatMap = new()
        {
            {
                "TP1",
                new Dictionary<string, string>
            {
                { "value", tp1 },
                { "color", tp1_color },
                { "xpos", tp1_xpos },
                { "ypos", tp1_ypos }
            }
            },
            {
                "TP2",
                new Dictionary<string, string>
            {
                { "value", tp2 },
                { "color", tp2_color },
                { "xpos", tp2_xpos },
                { "ypos", tp2_ypos }
            }
            },
            {
                "TP3",
                new Dictionary<string, string>
            {
                { "value", tp3 },
                { "color", tp3_color },
                { "xpos", tp3_xpos },
                { "ypos", tp3_ypos }
            }
            }
        };
        ViewData["Assessment"] = assessmentData;
        ViewData["AssessmentHeatMap"] = assessmentHeatMap;
        return View();
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetAssessmentDetail()
    {
        ResultClass result = new();
        try{
            int id = Convert.ToInt32(Request.Query["id"]);
            var assessment = _assessmentService.GetAssessment(id);
            result.IsSuccess = true;
            result.Message = "Success";
            result.Data = assessment;
            return Json(result);
        } catch (Exception ex) {
            result.IsSuccess = false;
            result.Message = ex.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddAssessment()
    {
        AssessmentClass assessment = new()
        {
            AssetID = Convert.ToInt32(Request.Form["AssetID"]),
            AssessmentNo = Request.Form["AssessmentNo"],
            AssessmentDate = Request.Form["AssessmentDate"],
            TimePeriode = Request.Form["TimePeriode"],
            TimeToLimitStateLeakageToAtmosphere = Request.Form[
                "TimeToLimitStateLeakageToAtmosphere"
            ],
            TimeToLimitStateFailureOfFunction = Request.Form["TimeToLimitStateFailureOfFunction"],
            TimeToLimitStatePassingAccrosValve = Request.Form[
                "TimeToLimitStatePassingAccrosValve"
            ],
            LeakageToAtmosphereID = Convert.ToInt32(Request.Form["LeakageToAtmosphereID"]),
            FailureOfFunctionID = Convert.ToInt32(Request.Form["FailureOfFunctionID"]),
            PassingAccrosValveID = Convert.ToInt32(Request.Form["PassingAccrosValveID"]),
            LeakageToAtmosphereTP1ID = Convert.ToInt32(Request.Form["LeakageToAtmosphereTP1ID"]),
            LeakageToAtmosphereTP2ID = Convert.ToInt32(Request.Form["LeakageToAtmosphereTP2ID"]),
            LeakageToAtmosphereTP3ID = Convert.ToInt32(Request.Form["LeakageToAtmosphereTP3ID"]),
            FailureOfFunctionTP1ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP1ID"]),
            FailureOfFunctionTP2ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP2ID"]),
            FailureOfFunctionTP3ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP3ID"]),
            PassingAccrosValveTP1ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP1ID"]),
            PassingAccrosValveTP2ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP2ID"]),
            PassingAccrosValveTP3ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP3ID"]),
            InspectionEffectivenessID = Convert.ToInt32(Request.Form["InspectionEffectivenessID"]),
            ImpactOfInternalFluidImpuritiesID = Convert.ToInt32(
                Request.Form["ImpactOfInternalFluidImpuritiesID"]
            ),
            ImpactOfOperatingEnvelopesID = Convert.ToInt32(Request.Form["ImpactOfOperatingEnvelopesID"]),
            UsedWithinOEMSpecificationID = Convert.ToInt32(Request.Form["UsedWithinOEMSpecificationID"]),
            RepairedID = Convert.ToInt32(Request.Form["RepairedID"]),
            ProductLossDefinition = Request.Form["ProductLossDefinition"],
            HSSEDefinisionID = Convert.ToInt32(Request.Form["HSSEDefinisionID"]),
            Summary = Request.Form["Summary"],
            RecommendationActionID = Convert.ToInt32(Request.Form["RecommendationActionID"]),
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
            LoFScoreLeakageToAtmophereTP1 = Convert.ToDouble(
                Request.Form["LoFScoreLeakageToAtmophereTP1"]
            ),
            LoFScoreLeakageToAtmophereTP2 = Convert.ToDouble(
                Request.Form["LoFScoreLeakageToAtmophereTP2"]
            ),
            LoFScoreLeakageToAtmophereTP3 = Convert.ToDouble(
                Request.Form["LoFScoreLeakageToAtmophereTP3"]
            ),
            LoFScoreFailureOfFunctionTP1 = Convert.ToDouble(
                Request.Form["LoFScoreFailureOfFunctionTP1"]
            ),
            LoFScoreFailureOfFunctionTP2 = Convert.ToDouble(
                Request.Form["LoFScoreFailureOfFunctionTP2"]
            ),
            LoFScoreFailureOfFunctionTP3 = Convert.ToDouble(
                Request.Form["LoFScoreFailureOfFunctionTP3"]
            ),
            LoFScorePassingAccrosValveTP1 = Convert.ToDouble(
                Request.Form["LoFScorePassingAccrossValveTP1"]
            ),
            LoFScorePassingAccrosValveTP2 = Convert.ToDouble(
                Request.Form["LoFScorePassingAccrossValveTP2"]
            ),
            LoFScorePassingAccrosValveTP3 = Convert.ToDouble(
                Request.Form["LoFScorePassingAccrossValveTP3"]
            ),
            CoFScore = Convert.ToDouble(Request.Form["CoFScore"]),
            IntegrityStatus = Request.Form["IntegrityStatus"],
            IsDeleted = false,
            CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        ResultClass result = new();
        try {
            AssessmentData assessmentData = _assessmentService.AddAssessment(assessment);
            List<int> inspectionIDs = Request
                .Form["selectedInspectionId"]
                .ToString()
                .Split(',')
                .Select(int.Parse)
                .ToList();
            _assessmentService.AddInspectionToAssessment(assessmentData.Id, inspectionIDs);
            List<int> maintenanceIDs = [];
            if (!StringValues.IsNullOrEmpty(Request.Form["selectedMaintenanceId"]))
            {
                maintenanceIDs = Request.Form["selectedMaintenanceId"]
                    .ToString()
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();
            }
            _assessmentService.AddMaintenanceToAssessment(assessmentData.Id, maintenanceIDs);
            assessmentData = _assessmentService.GetAssessment(assessmentData.Id);
            result.IsSuccess = true;
            result.Message = "Assessment added successfully";
            result.Data = assessmentData;
            return Json(result);
        } catch (Exception ex) {
            result.IsSuccess = false;
            result.Message = ex.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateAssessment()
    {
        int assessmentID = Convert.ToInt32(Request.Form["Id"]);
        AssessmentClass assessment = new()
        {
            Id = assessmentID,
            AssetID = Convert.ToInt32(Request.Form["AssetID"]),
            AssessmentNo = Request.Form["AssessmentNo"],
            AssessmentDate = Request.Form["AssessmentDate"],
            TimePeriode = Request.Form["TimePeriode"],
            TimeToLimitStateLeakageToAtmosphere = Request.Form[
                "TimeToLimitStateLeakageToAtmosphere"
            ],
            TimeToLimitStateFailureOfFunction = Request.Form["TimeToLimitStateFailureOfFunction"],
            TimeToLimitStatePassingAccrosValve = Request.Form[
                "TimeToLimitStatePassingAccrosValve"
            ],
            LeakageToAtmosphereID = Convert.ToInt32(Request.Form["LeakageToAtmosphereID"]),
            FailureOfFunctionID = Convert.ToInt32(Request.Form["FailureOfFunctionID"]),
            PassingAccrosValveID = Convert.ToInt32(Request.Form["PassingAccrosValveID"]),
            LeakageToAtmosphereTP1ID = Convert.ToInt32(Request.Form["LeakageToAtmosphereTP1ID"]),
            LeakageToAtmosphereTP2ID = Convert.ToInt32(Request.Form["LeakageToAtmosphereTP2ID"]),
            LeakageToAtmosphereTP3ID = Convert.ToInt32(Request.Form["LeakageToAtmosphereTP3ID"]),
            FailureOfFunctionTP1ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP1ID"]),
            FailureOfFunctionTP2ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP2ID"]),
            FailureOfFunctionTP3ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP3ID"]),
            PassingAccrosValveTP1ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP1ID"]),
            PassingAccrosValveTP2ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP2ID"]),
            PassingAccrosValveTP3ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP3ID"]),
            InspectionEffectivenessID = Convert.ToInt32(Request.Form["InspectionEffectivenessID"]),
            ImpactOfInternalFluidImpuritiesID = Convert.ToInt32(
                Request.Form["ImpactOfInternalFluidImpuritiesID"]
            ),
            ImpactOfOperatingEnvelopesID = Convert.ToInt32(Request.Form["ImpactOfOperatingEnvelopesID"]),
            UsedWithinOEMSpecificationID = Convert.ToInt32(Request.Form["UsedWithinOEMSpecificationID"]),
            RepairedID = Convert.ToInt32(Request.Form["RepairedID"]),
            ProductLossDefinition = Request.Form["ProductLossDefinition"],
            HSSEDefinisionID = Convert.ToInt32(Request.Form["HSSEDefinisionID"]),
            Summary = Request.Form["Summary"],
            RecommendationActionID = Convert.ToInt32(Request.Form["RecommendationActionID"]),
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
            LoFScoreLeakageToAtmophereTP1 = Convert.ToDouble(
                Request.Form["LoFScoreLeakageToAtmophereTP1"]
            ),
            LoFScoreLeakageToAtmophereTP2 = Convert.ToDouble(
                Request.Form["LoFScoreLeakageToAtmophereTP2"]
            ),
            LoFScoreLeakageToAtmophereTP3 = Convert.ToDouble(
                Request.Form["LoFScoreLeakageToAtmophereTP3"]
            ),
            LoFScoreFailureOfFunctionTP1 = Convert.ToDouble(
                Request.Form["LoFScoreFailureOfFunctionTP1"]
            ),
            LoFScoreFailureOfFunctionTP2 = Convert.ToDouble(
                Request.Form["LoFScoreFailureOfFunctionTP2"]
            ),
            LoFScoreFailureOfFunctionTP3 = Convert.ToDouble(
                Request.Form["LoFScoreFailureOfFunctionTP3"]
            ),
            LoFScorePassingAccrosValveTP1 = Convert.ToDouble(
                Request.Form["LoFScorePassingAccrossValveTP1"]
            ),
            LoFScorePassingAccrosValveTP2 = Convert.ToDouble(
                Request.Form["LoFScorePassingAccrossValveTP2"]
            ),
            LoFScorePassingAccrosValveTP3 = Convert.ToDouble(
                Request.Form["LoFScorePassingAccrossValveTP3"]
            ),
            CoFScore = Convert.ToDouble(Request.Form["CoFScore"]),
            IntegrityStatus = Request.Form["IntegrityStatus"],
            IsDeleted = false
        };
        ResultClass result = new();
        try {
            AssessmentData assessmentData = _assessmentService.UpdateAssessment(assessment);
            List<int> inspectionIDs = Request
                .Form["selectedInspectionId"]
                .ToString()
                .Split(',')
                .Select(int.Parse)
                .ToList();
            _assessmentService.AddInspectionToAssessment(assessmentData.Id, inspectionIDs, true);
            List<int> maintenanceIDs = [];
            if (!StringValues.IsNullOrEmpty(Request.Form["selectedMaintenanceId"]))
            {
                maintenanceIDs = Request.Form["selectedMaintenanceId"]
                    .ToString()
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();
            }
            _assessmentService.AddMaintenanceToAssessment(assessmentData.Id, maintenanceIDs, true);
            assessmentData = _assessmentService.GetAssessment(assessmentData.Id);
            result.IsSuccess = true;
            result.Message = "Assessment updated successfully";
            result.Data = assessmentData;
            return Json(result);
        } catch (Exception ex) {
            result.IsSuccess = false;
            result.Message = ex.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteAssessment()
    {
        ResultClass result = new();
        try {
            if(!int.TryParse(Request.Form["Id"], out int AssessmentID)) {
                throw new Exception("Invalid Assessment ID");
            }
            int deletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            AssessmentClass assessment = new()
            {
                Id = AssessmentID,
                IsDeleted = true,
                DeletedBy = deletedBy,
                DeletedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
            };
            AssessmentData assessmentData = _assessmentService.DeleteAssessment(assessment);
            result.IsSuccess = true;
            result.Message = "Assessment deleted successfully";
            result.Data = assessmentData;
            return Json(result);
        }
        catch (Exception ex) {
            result.IsSuccess = false;
            result.Message = ex.Message;
            return Json(result);
        }
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetAssessmentList(int AreaID = 0, int PlatformID = 0)
    {
        List<AssessmentData> assessmentList = _assessmentService.GetAssessmentRecapList(
            AreaID,
            PlatformID,
            false,
            false
        );
        return Json(assessmentList);
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetAssessmentSidebar(int AssetID)
    {
        List<AssessmentData> assessmentList = _assessmentService.GetAssessmentList(
            0,
            0,
            false,
            false,
            AssetID
        );
        List<Dictionary<string, string>> assessmentSidebar = [];
        foreach (var assessment in assessmentList)
        {
            if(assessment.Asset == null) continue;
            Dictionary<string, string> assessmentSidebarItem =
                new()
                {
                    { "Id", assessment.Id.ToString() },
                    { "Name", assessment.AssessmentDate ?? "" },
                    { "Area", assessment.Asset.BusinessArea ?? "" },
                    { "Platform", assessment.Asset.Platform ?? "" },
                    { "Asset", assessment.Asset.TagNo ?? "" },
                    { "AssetID", assessment.Asset.Id.ToString() }
                };
            assessmentSidebar.Add(assessmentSidebarItem);
        }
        assessmentSidebar = [.. assessmentSidebar
            .OrderByDescending(i =>
                DateTime.ParseExact(i["Name"], "dd-MM-yyyy", CultureInfo.InvariantCulture)
            )];
        return Json(assessmentSidebar);
    }
}

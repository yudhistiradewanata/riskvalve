using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
            return Redirect("/Login/Index");
        }
        else
        {
            Dictionary<string, string> session = login.GetLoginSession(HttpContext);
            foreach (var item in session)
            {
                ViewData[item.Key] = item.Value;
            }
            if (ViewData["IsEngineer"].ToString().ToLower().Equals("false"))
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect("/Home/Index");
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
                LeakageToAtmosphereID = Convert.ToInt32(Request.Form["LeakageToAtmosphereID"]),
                FailureOfFunctionID = Convert.ToInt32(Request.Form["FailureOfFunctionID"]),
                PassingAccrosValveID = Convert.ToInt32(Request.Form["PassingAccrosValveID"]),
                LeakageToAtmosphereTP1ID = Convert.ToInt32(
                    Request.Form["LeakageToAtmosphereTP1ID"]
                ),
                LeakageToAtmosphereTP2ID = Convert.ToInt32(
                    Request.Form["LeakageToAtmosphereTP2ID"]
                ),
                LeakageToAtmosphereTP3ID = Convert.ToInt32(
                    Request.Form["LeakageToAtmosphereTP3ID"]
                ),
                FailureOfFunctionTP1ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP1ID"]),
                FailureOfFunctionTP2ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP2ID"]),
                FailureOfFunctionTP3ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP3ID"]),
                PassingAccrosValveTP1ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP1ID"]),
                PassingAccrosValveTP2ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP2ID"]),
                PassingAccrosValveTP3ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP3ID"]),
                InspectionEffectivenessID = Convert.ToInt32(
                    Request.Form["InspectionEffectivenessID"]
                ),
                ImpactOfInternalFluidImpuritiesID = Convert.ToInt32(
                    Request.Form["ImpactOfInternalFluidImpuritiesID"]
                ),
                ImpactOfOperatingEnvelopesID = Convert.ToInt32(
                    Request.Form["ImpactOfOperatingEnvelopesID"]
                ),
                UsedWithinOEMSpecificationID = Convert.ToInt32(
                    Request.Form["UsedWithinOEMSpecificationID"]
                ),
                RepairedID = Convert.ToInt32(Request.Form["RepairedID"]),
                ProductLossDefinition = Request.Form["ProductLossDefinition"],
                HSSEDefinisionID = Convert.ToInt32(Request.Form["HSSEDefinisionID"]),
                Summary = Request.Form["Summary"],
                RecommendationActionID = Convert.ToInt32(Request.Form["RecommendationActionID"]),
                DetailedRecommendation = Request.Form["DetailedRecommendation"],
                IsDeleted = false,
                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        int assessmentID = assessment.AddAssessment(assessmentDB);
        List<int> inspectionIDs = Request.Form["selectedInspectionId"].ToString().Split(',').Select(
            int.Parse
        ).ToList();
        assessment.AddInspectionToAssessment(assessmentID, inspectionIDs);
        List<int> maintenanceIDs = Request.Form["selectedMaintenanceId"].ToString().Split(',').Select(
            int.Parse
        ).ToList();
        assessment.AddMaintenanceToAssessment(assessmentID, maintenanceIDs);
        assessment = assessment.GetAssessmentModel(assessmentID);
        return Json(assessment);
    }

    [HttpPost]
    public IActionResult UpdateAssessment()
    {
        AssessmentModel assessment = new();
        AssessmentDB assessmentDB =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                AssessmentNo = Request.Form["AssessmentNo"],
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
                LeakageToAtmosphereID = Convert.ToInt32(Request.Form["LeakageToAtmosphereID"]),
                FailureOfFunctionID = Convert.ToInt32(Request.Form["FailureOfFunctionID"]),
                PassingAccrosValveID = Convert.ToInt32(Request.Form["PassingAccrosValveID"]),
                LeakageToAtmosphereTP1ID = Convert.ToInt32(
                    Request.Form["LeakageToAtmosphereTP1ID"]
                ),
                LeakageToAtmosphereTP2ID = Convert.ToInt32(
                    Request.Form["LeakageToAtmosphereTP2ID"]
                ),
                LeakageToAtmosphereTP3ID = Convert.ToInt32(
                    Request.Form["LeakageToAtmosphereTP3ID"]
                ),
                FailureOfFunctionTP1ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP1ID"]),
                FailureOfFunctionTP2ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP2ID"]),
                FailureOfFunctionTP3ID = Convert.ToInt32(Request.Form["FailureOfFunctionTP3ID"]),
                PassingAccrosValveTP1ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP1ID"]),
                PassingAccrosValveTP2ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP2ID"]),
                PassingAccrosValveTP3ID = Convert.ToInt32(Request.Form["PassingAccrosValveTP3ID"]),
                InspectionEffectivenessID = Convert.ToInt32(
                    Request.Form["InspectionEffectivenessID"]
                ),
                ImpactOfInternalFluidImpuritiesID = Convert.ToInt32(
                    Request.Form["ImpactOfInternalFluidImpuritiesID"]
                ),
                ImpactOfOperatingEnvelopesID = Convert.ToInt32(
                    Request.Form["ImpactOfOperatingEnvelopesID"]
                ),
                UsedWithinOEMSpecificationID = Convert.ToInt32(
                    Request.Form["UsedWithinOEMSpecificationID"]
                ),
                RepairedID = Convert.ToInt32(Request.Form["RepairedID"]),
                ProductLossDefinition = Request.Form["ProductLossDefinition"],
                HSSEDefinisionID = Convert.ToInt32(Request.Form["HSSEDefinisionID"]),
                Summary = Request.Form["Summary"],
                RecommendationActionID = Convert.ToInt32(Request.Form["RecommendationActionID"]),
                DetailedRecommendation = Request.Form["DetailedRecommendation"],
                IsDeleted = false,
                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        assessment.UpdateAssessment(assessmentDB);
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
}

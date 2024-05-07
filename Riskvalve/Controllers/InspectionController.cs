using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class InspectionController : Controller
{
    private readonly ILogger<InspectionController> _logger;

    public InspectionController(ILogger<InspectionController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect(Environment.app_path+"/Login/Index");
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
                return Redirect(Environment.app_path+"/Home/Index");
            }
        }
        InspectionSidebarHistory inspectionSidebarHistory = new();
        List<InspectionSidebarModel> inspectionSidebar =
            inspectionSidebarHistory.GetInspectionSidebarHistory("Inspection");
        ViewData["InspectionSidebar"] = inspectionSidebar;
        ViewData["pageType"] = "Inspection";
        return View();
    }

    [HttpGet]
    public IActionResult GetInspectionDetail(int id)
    {
        InspectionModel inspection = new();
        InspectionModel inspectionModel = inspection.GetInspectionModel(id);
        return Json(inspectionModel);
    }

    [HttpPost]
    public IActionResult AddInspection()
    {
        List<IFormFile> files = Request.Form.Files.ToList();
        InspectionModel inspection = new();
        InspectionDB inspectionDB =
            new()
            {
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                InspectionDate = Request.Form["InspectionDate"],
                InspectionMethodID = Convert.ToInt32(Request.Form["InspectionMethodID"]),
                InspectionEffectivenessID = Convert.ToInt32(
                    Request.Form["InspectionEffectivenessID"]
                ),
                InspectionDescription = Request.Form["InspectionDescription"],
                CurrentConditionLeakeageToAtmosphereID = Convert.ToInt32(
                    Request.Form["CurrentConditionLeakeageToAtmosphereID"]
                ),
                CurrentConditionFailureOfFunctionID = Convert.ToInt32(
                    Request.Form["CurrentConditionFailureOfFunctionID"]
                ),
                CurrentConditionPassingAcrossValveID = Convert.ToInt32(
                    Request.Form["CurrentConditionPassingAcrossValveID"]
                ),
                FunctionCondition = Request.Form["FunctionCondition"],
                TestPressureIfAny = Request.Form["TestPressureIfAny"],
                CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
            };
        try
        {
            int inspectionID = inspection.AddInspection(inspectionDB);
            IWebHostEnvironment environment =
                HttpContext.RequestServices.GetService<IWebHostEnvironment>();
            string path = Path.Combine(
                environment.WebRootPath,
                "Uploads",
                "Inspection",
                inspectionID.ToString()
            );
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(path, fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        formFile.CopyTo(stream);
                        InspectionFileModel inspectionFile =
                            new()
                            {
                                MaintenanceID = null,
                                InspectionID = inspectionID,
                                FileName = fileName,
                                FileSize = formFile.Length,
                                FileType = formFile.ContentType,
                                FilePath = Path.Combine(
                                    "Uploads",
                                    "Inspection",
                                    inspectionID.ToString(),
                                    fileName
                                ),
                                CreatedAt = DateTime.Now.ToString(
                                    Environment.GetDateFormatString()
                                ),
                                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                            };
                        inspectionFile.AddInspectionFile(inspectionFile);
                    }
                }
            }
            InspectionModel result = inspection.GetInspectionModel(inspectionID);
            string resultstring = JsonSerializer.Serialize(result);
            return Json(
                new Dictionary<string, string>
                {
                    { "Status", "Success"},
                    { "Message", "Inspection added successfully" },
                    { "InspectionData", resultstring }
                }
            );
        }
        catch (Exception ex)
        {
            string message = ex.Message;
            return Json(
                new Dictionary<string, string> { { "Status", "Error" }, { "Message", message } }
            );
        }
    }

    [HttpPost]
    public IActionResult UpdateInspection()
    {
        List<IFormFile> files = Request.Form.Files.ToList();
        List<int> deletedImageIDs = new();
        foreach (var key in Request.Form.Keys)
        {
            if (key.Contains("deletedImageIDs"))
            {
                if (Request.Form[key] == "true")
                {
                    deletedImageIDs.Add(
                        Convert.ToInt32(key.Replace("deletedImageIDs[", "").Replace("]", ""))
                    );
                }
            }
        }
        int inspectionID = Convert.ToInt32(Request.Form["Id"]);
        InspectionModel inspection = new();
        InspectionFileModel inspectionFileModel = new();
        InspectionDB inspectionDB =
            new()
            {
                Id = inspectionID,
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                InspectionDate = Request.Form["InspectionDate"],
                InspectionMethodID = Convert.ToInt32(Request.Form["InspectionMethodID"]),
                InspectionEffectivenessID = Convert.ToInt32(
                    Request.Form["InspectionEffectivenessID"]
                ),
                InspectionDescription = Request.Form["InspectionDescription"],
                CurrentConditionLeakeageToAtmosphereID = Convert.ToInt32(
                    Request.Form["CurrentConditionLeakeageToAtmosphereID"]
                ),
                CurrentConditionFailureOfFunctionID = Convert.ToInt32(
                    Request.Form["CurrentConditionFailureOfFunctionID"]
                ),
                CurrentConditionPassingAcrossValveID = Convert.ToInt32(
                    Request.Form["CurrentConditionPassingAcrossValveID"]
                ),
                FunctionCondition = Request.Form["FunctionCondition"],
                TestPressureIfAny = Request.Form["TestPressureIfAny"],
            };
        inspection.UpdateInspection(inspectionDB);
        IWebHostEnvironment environment =
            HttpContext.RequestServices.GetService<IWebHostEnvironment>();
        string path = Path.Combine(
            environment.WebRootPath,
            "Uploads",
            "Inspection",
            inspectionID.ToString()
        );
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                var filePath = Path.Combine(path, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    formFile.CopyTo(stream);
                    InspectionFileModel inspectionFile =
                        new()
                        {
                            MaintenanceID = null,
                            InspectionID = inspectionID,
                            FileName = fileName,
                            FileSize = formFile.Length,
                            FileType = formFile.ContentType,
                            FilePath = Path.Combine(
                                "Uploads",
                                "Inspection",
                                inspectionID.ToString(),
                                fileName
                            ),
                            CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                            CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                        };
                    inspectionFile.AddInspectionFile(inspectionFile);
                }
            }
        }
        // delete file that not in the form
        foreach (var id in deletedImageIDs)
        {
            InspectionFileDB inspectionFileDB =
                new()
                {
                    Id = id,
                    DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                    DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"))
                };
            inspectionFileModel.DeleteInspectionFile(inspectionFileDB);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteInspection()
    {
        InspectionModel inspection = new();
        int id = Convert.ToInt32(Request.Form["id"]);
        InspectionDB inspectionDB =
            new()
            {
                Id = id,
                IsDeleted = true,
                DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
            };
        inspection.DeleteInspection(inspectionDB);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public List<InspectionModel> GetInspectionList()
    {
        int AssetID = Convert.ToInt32(Request.Query["AssetID"]);
        InspectionModel inspection = new();
        List<InspectionModel> inspectionList = inspection.GetInspectionList(false, AssetID);
        return inspectionList;
    }
}

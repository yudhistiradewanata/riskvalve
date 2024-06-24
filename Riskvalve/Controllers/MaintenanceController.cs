using System.Globalization;
using System.Text.Json;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using SharedLayer;

namespace Riskvalve.Controllers;

public class MaintenanceController(
    IMaintenanceService maintenanceService,
    IAreaService areaService,
    IAssessmentService assessmentService
) : Controller
{
    private readonly IAreaService _areaService = areaService;
    private readonly IMaintenanceService _maintenanceService = maintenanceService;
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
                ViewData.ContainsKey("IsEngineer")
                && ViewData["IsEngineer"]?.ToString()?.ToLower().Equals("false") == true
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
        ViewData["InspectionSidebar"] = _areaService.GetSidebarData();
        ViewData["pageType"] = "Maintenance";
        return View();
    }

    public IActionResult PrintMaintenance(int id)
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
                ViewData.ContainsKey("IsEngineer")
                && ViewData["IsEngineer"]?.ToString()?.ToLower().Equals("false") == true
            )
            {
                TempData["Message"] = "You are not authorized to access that page";
                return Redirect(SharedEnvironment.app_path + "/Home/Index");
            }
        }
        MaintenanceData maintenance = _maintenanceService.GetMaintenance(id);
        ViewData["maintenance"] = maintenance;
        return View();
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetMaintenanceDetail()
    {
        ResultClass result = new();
        try
        {
            int id = Convert.ToInt32(Request.Query["id"]);
            var maintenance = _maintenanceService.GetMaintenance(id);
            result.IsSuccess = true;
            result.Message = "Success";
            result.Data = maintenance;
            return Json(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            result.IsSuccess = false;
            result.Message = e.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddMaintenance()
    {
        List<IFormFile> files = [.. Request.Form.Files];
        MaintenanceClass maintenanceClass =
            new()
            {
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                IsValveRepairedID = Convert.ToInt32(Request.Form["IsValveRepairedID"]),
                MaintenanceDate = Request.Form["MaintenanceDate"],
                MaintenanceDescription = Request.Form["MaintenanceDescription"],
                IsDeleted = false,
                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
            };
        ResultClass result = new();
        try
        {
            MaintenanceData maintenance = _maintenanceService.AddMaintenance(maintenanceClass);
            IWebHostEnvironment environment =
                Request.HttpContext.RequestServices.GetService<IWebHostEnvironment>() ?? throw new Exception("Environment not found");
            string path = Path.Combine(
                environment.WebRootPath,
                "Uploads",
                "Maintenance",
                maintenance.Id.ToString()
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
                    using var stream = System.IO.File.Create(filePath);
                    formFile.CopyTo(stream);
                    InspectionFileClass maintenanceFile =
                        new()
                        {
                            MaintenanceID = maintenance.Id,
                            FileName = fileName,
                            FileSize = formFile.Length,
                            FileType = formFile.ContentType,
                            FilePath = Path.Combine(
                                "Uploads",
                                "Maintenance",
                                maintenance.Id.ToString(),
                                fileName
                            )
                        };
                    _maintenanceService.AddMaintenanceFile(maintenanceFile);
                }
            }
            result.IsSuccess = true;
            result.Message = "Maintenance added successfully";
            result.Data = maintenance;
            return Json(result);
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateMaintenance()
    {
        ResultClass result = new();
        try
        {
            List<IFormFile> files = [.. Request.Form.Files];
            List<InspectionFileClass>? inspectionFileClass = [];
            foreach (var key in Request.Form.Keys)
            {
                if (key.Contains("deletedImageIDs"))
                {
                    if (Request.Form[key] == "true")
                    {
                        int delete = Convert.ToInt32(
                            key.Replace("deletedImageIDs[", "").Replace("]", "")
                        );
                        inspectionFileClass.Add(
                            new InspectionFileClass
                            {
                                Id = delete,
                                DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                                DeletedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
                            }
                        );
                    }
                }
            }
            if (inspectionFileClass != null && inspectionFileClass.Count > 0)
            {
                _maintenanceService.DeleteMaintenanceFiles(inspectionFileClass);
            }
            int maintenanceID = Convert.ToInt32(Request.Form["Id"]);
            MaintenanceClass maintenance =
                new()
                {
                    Id = maintenanceID,
                    AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                    IsValveRepairedID = Convert.ToInt32(Request.Form["IsValveRepairedID"]),
                    MaintenanceDate = Request.Form["MaintenanceDate"],
                    MaintenanceDescription = Request.Form["MaintenanceDescription"]
                };
            Console.WriteLine("==APP AYAYA==");
            Console.WriteLine(JsonSerializer.Serialize(maintenance));
            IWebHostEnvironment environment =
                Request.HttpContext.RequestServices.GetService<IWebHostEnvironment>() ?? throw new Exception("Environment not found");
            string path = Path.Combine(
                environment.WebRootPath,
                "Uploads",
                "Maintenance",
                maintenance.Id.ToString()
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
                    using var stream = System.IO.File.Create(filePath);
                    formFile.CopyTo(stream);
                    InspectionFileClass maintenanceFile =
                        new()
                        {
                            MaintenanceID = maintenance.Id,
                            FileName = fileName,
                            FileSize = formFile.Length,
                            FileType = formFile.ContentType,
                            FilePath = Path.Combine(
                                "Uploads",
                                "Maintenance",
                                maintenance.Id.ToString(),
                                fileName
                            )
                        };
                    _maintenanceService.AddMaintenanceFile(maintenanceFile);
                }
            }
            MaintenanceData maintenanceData = _maintenanceService.UpdateMaintenance(maintenance);
            result.IsSuccess = true;
            result.Message = "Maintenance updated successfully";
            result.Data = maintenanceData;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
            return Json(result);
        }
        return Json(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteMaintenance()
    {
        ResultClass result = new();
        try
        {
            if (!int.TryParse(Request.Form["Id"], out int id))
            {
                throw new Exception("Invalid Id");
            }
            int deletedBy = 0;
            if (HttpContext.Session.GetString("Id") != null)
            {
                if (!int.TryParse(HttpContext.Session.GetString("Id"), out deletedBy))
                {
                    throw new Exception("Invalid session Id");
                }
            }
            MaintenanceClass maintenance =
                new()
                {
                    Id = id,
                    DeletedBy = deletedBy,
                    DeletedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
                };
            MaintenanceData maintenanceData = _maintenanceService.DeleteMaintenance(maintenance);
            result.IsSuccess = true;
            result.Message = "Maintenance deleted successfully";
            result.Data = maintenanceData;
            return Json(result);
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
            return Json(result);
        }
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetMaintenanceList()
    {
        ResultClass resultClass = new();
        try
        {
            int AssetID = Convert.ToInt32(Request.Query["AssetID"]);
            List<MaintenanceData> maintenanceDatas = _maintenanceService.GetMaintenanceList(
                false,
                AssetID
            );
            resultClass.IsSuccess = true;
            resultClass.Message = "Success";
            resultClass.Data = maintenanceDatas;
            return Json(resultClass);
        }
        catch (Exception e)
        {
            resultClass.IsSuccess = false;
            resultClass.Message = e.Message;
            return Json(resultClass);
        }
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetMaintenanceSidebar(int AssetID)
    {
        List<MaintenanceData> maintenanceList = _maintenanceService.GetMaintenanceList(false, AssetID);
        List<Dictionary<string, string>> maintenanceSidebar = [];
        foreach (var item in maintenanceList)
        {
            if(
                item.Asset == null
                || item.Asset.BusinessArea == null
                || item.Asset.Platform == null
                || item.Asset.TagNo == null
                || item.MaintenanceDate == null
            )
            {
                continue;
            }
            Dictionary<string, string> maintenanceSidebarItem =
                new()
                {
                    { "Id", item.Id.ToString() },
                    { "Name", item.MaintenanceDate},
                    { "Area", item.Asset.BusinessArea},
                    { "Platform", item.Asset.Platform},
                    { "Asset", item.Asset.TagNo},
                    { "AssetID", item.Asset.Id.ToString() }
                };
            maintenanceSidebar.Add(maintenanceSidebarItem);
        }
        maintenanceSidebar = maintenanceSidebar
            .OrderByDescending(i =>
                DateTime.ParseExact(i["Name"], "dd-MM-yyyy", CultureInfo.InvariantCulture)
            )
            .ToList();
        return Json(maintenanceSidebar);
    }
}

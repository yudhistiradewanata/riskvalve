using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class MaintenanceController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public MaintenanceController(ILogger<HomeController> logger)
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
            inspectionSidebarHistory.GetInspectionSidebarHistory("Maintenance");
        ViewData["InspectionSidebar"] = inspectionSidebar;
        ViewData["pageType"] = "Maintenance";
        return View();
    }

    [HttpGet]
    public IActionResult GetMaintenanceDetail(int id)
    {
        MaintenanceModel maintenance = new();
        MaintenanceModel maintenanceModel = maintenance.GetMaintenanceModel(id);
        return Json(maintenanceModel);
    }

    [HttpPost]
    public MaintenanceModel AddMaintenance()
    {
        List<IFormFile> files = Request.Form.Files.ToList();
        MaintenanceModel maintenance = new();
        MaintenanceDB maintenanceDB =
            new()
            {
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                IsValveRepairedID = Convert.ToInt32(Request.Form["IsValveRepairedID"]),
                MaintenanceDate = Request.Form["MaintenanceDate"],
                MaintenanceDescription = Request.Form["MaintenanceDescription"],
                IsDeleted = false,
                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
            };
        int maintenanceID = maintenance.AddMaintenance(maintenanceDB);
        IWebHostEnvironment environment =
            Request.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
        string path = Path.Combine(
            environment.WebRootPath,
            "Uploads",
            "Maintenance",
            maintenanceID.ToString()
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
                    InspectionFileModel maintenanceFile =
                        new()
                        {
                            MaintenanceID = maintenanceID,
                            FileName = fileName,
                            FileSize = formFile.Length,
                            FileType = formFile.ContentType,
                            FilePath = Path.Combine(
                                "Uploads",
                                "Inspection",
                                maintenanceID.ToString(),
                                fileName
                            ),
                        };
                    maintenanceFile.AddInspectionFile(maintenanceFile);
                }
            }
        }
        return maintenance.GetMaintenanceModel(maintenanceID);
        // return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateMaintenance()
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
        int maintenanceID = Convert.ToInt32(Request.Form["Id"]);
        MaintenanceModel maintenance = new();
        InspectionFileModel inspectionFileModel = new();
        MaintenanceDB maintenanceDB =
            new()
            {
                Id = maintenanceID,
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                IsValveRepairedID = Convert.ToInt32(Request.Form["IsValveRepairedID"]),
                MaintenanceDate = Request.Form["MaintenanceDate"],
                MaintenanceDescription = Request.Form["MaintenanceDescription"]
            };
        maintenance.UpdateMaintenance(maintenanceDB);
        IWebHostEnvironment environment =
            Request.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
        string path = Path.Combine(
            environment.WebRootPath,
            "Uploads",
            "Maintenance",
            maintenanceDB.Id.ToString()
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
                    InspectionFileModel maintenanceFile =
                        new()
                        {
                            InspectionID = null,
                            MaintenanceID = maintenanceDB.Id,
                            FileName = fileName,
                            FileSize = formFile.Length,
                            FileType = formFile.ContentType,
                            FilePath = Path.Combine(
                                "Uploads",
                                "Maintenance",
                                maintenanceDB.Id.ToString(),
                                fileName
                            ),
                        };
                    maintenanceFile.AddInspectionFile(maintenanceFile);
                }
            }
        }
        foreach (var id in deletedImageIDs)
        {
            InspectionFileDB inspectionFileDB =
                new()
                {
                    Id = id,
                    DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                    DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
                };
            inspectionFileModel.DeleteInspectionFile(inspectionFileDB);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteMaintenance()
    {
        MaintenanceModel maintenance = new();
        MaintenanceDB maintenanceDB =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        maintenance.DeleteMaintenance(maintenanceDB);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public List<MaintenanceModel> GetMaintenanceList()
    {
        int AssetID = Convert.ToInt32(Request.Query["AssetID"]);
        MaintenanceModel maintenance = new();
        List<MaintenanceModel> maintenanceList = maintenance.GetMaintenanceList(false, AssetID);
        return maintenanceList;
    }
}
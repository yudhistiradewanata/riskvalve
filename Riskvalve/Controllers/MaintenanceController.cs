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
        }
        InspectionSidebarHistory inspectionSidebarHistory = new();
        List<InspectionSidebarModel> inspectionSidebar =
            inspectionSidebarHistory.GetInspectionSidebarHistory("Maintenance");
        ViewData["InspectionSidebar"] = inspectionSidebar;
        ViewData["pageType"] = "Inspection";
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
    public async Task<IActionResult> AddMaintenance()
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
                CreatedBy = Convert.ToInt32(Request.Form["CreatedBy"]),
                CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
            };
        int maintenanceID = maintenance.AddMaintenance(maintenanceDB);
        IWebHostEnvironment environment = Request.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
        string path = Path.Combine(environment.WebRootPath, "Uploads","Maintenance", maintenanceID.ToString());
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
                        await formFile.CopyToAsync(stream);
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
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateMaintenance()
    {
        MaintenanceModel maintenance = new();
        MaintenanceDB maintenanceDB =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                IsValveRepairedID = Convert.ToInt32(Request.Form["IsValveRepairedID"]),
                MaintenanceDate = Request.Form["MaintenanceDate"],
                MaintenanceDescription = Request.Form["MaintenanceDescription"]
            };
        maintenance.UpdateMaintenance(maintenanceDB);
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
                DeletedBy = Convert.ToInt32(Request.Form["DeletedBy"]),
                DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        maintenance.DeleteMaintenance(maintenanceDB);
        return RedirectToAction("Index");
    }
}

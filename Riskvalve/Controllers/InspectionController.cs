using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Riskvalve.Models;

namespace Riskvalve.Controllers
{
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
        public async Task<IActionResult> AddInspection()
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
                };

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
                        await formFile.CopyToAsync(stream);
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateInspection()
        {
            List<IFormFile> files = Request.Form.Files.ToList();
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
            List<InspectionFileModel> inspectionFiles = inspectionFileModel.GetInspectionFiles(
                inspectionID
            );
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
                if (inspectionFiles.Where(f => f.FileName == formFile.FileName).Count() == 0)
                {
                    if (formFile.Length > 0)
                    {
                        var fileName =
                            Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
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
                                    CreatedBy = Convert.ToInt32(
                                        HttpContext.Session.GetString("Id")
                                    ),
                                };
                            inspectionFile.AddInspectionFile(inspectionFile);
                        }
                    }
                }
            }
            // delete file that not in the form
            foreach (var inspectionFile in inspectionFiles)
            {
                if (files.Where(f => f.FileName == inspectionFile.FileName).Count() == 0)
                {
                    InspectionFileDB inspectionFileDB =
                        new()
                        {
                            Id = inspectionFile.Id,
                            DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                            DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"))
                        };
                    inspectionFile.DeleteInspectionFile(inspectionFileDB);
                }
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
    }
}

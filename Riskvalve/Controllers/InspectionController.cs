using System.Globalization;
using System.Text.Json;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using SharedLayer;

namespace Riskvalve.Controllers;

public class InspectionController(
    IAreaService areaService,
    IInspectionService inspectionService,
    IAssessmentService assessmentService
) : Controller
{
    private readonly IAreaService _areaService = areaService;
    private readonly IInspectionService _inspectionService = inspectionService;
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
        ViewData["InspectionMethodData"] = _assessmentService.InspectionMethodDatas();
        ViewData["IsValveRepairedData"] = _assessmentService.IsValveRepairedDatas();
        ViewData["InspectionSidebar"] = _areaService.GetSidebarData();
        ViewData["pageType"] = "Inspection";
        return View();
    }

    public IActionResult PrintInspection(int id)
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
        InspectionData inspection = _inspectionService.GetInspection(id);
        if(inspection.InspectionFiles != null)
        {
            foreach (var item in inspection.InspectionFiles)
            {
                item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
            }
        }
        ViewData["Inspection"] = inspection;
        return View();
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetInspectionDetail()
    {
        ResultClass result = new();
        try {
            int id = Convert.ToInt32(Request.Query["id"]);
            var inspection = _inspectionService.GetInspection(id);
            if(inspection.InspectionFiles != null)
            {
                foreach (var item in inspection.InspectionFiles)
                {
                    item.FilePath = SharedEnvironment.app_path.Replace("/", "") + "/" + item.FilePath;
                }
            }
            result.IsSuccess = true;
            result.Message = "Success";
            result.Data = inspection;
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
    public IActionResult AddInspection()
    {
        List<IFormFile> files = [.. Request.Form.Files];
        InspectionClass inspection = new()
        {
            AssetID = Convert.ToInt32(Request.Form["AssetID"]),
            InspectionDate = Request.Form["InspectionDate"],
            InspectionMethodID = Convert.ToInt32(Request.Form["InspectionMethodID"]),
            InspectionEffectivenessID = Convert.ToInt32(Request.Form["InspectionEffectivenessID"]),
            InspectionDescription = Request.Form["InspectionDescription"],
            CurrentConditionLeakeageToAtmosphereID = Convert.ToInt32(Request.Form["CurrentConditionLeakeageToAtmosphereID"]),
            CurrentConditionFailureOfFunctionID = Convert.ToInt32(Request.Form["CurrentConditionFailureOfFunctionID"]),
            CurrentConditionPassingAcrossValveID = Convert.ToInt32(Request.Form["CurrentConditionPassingAcrossValveID"]),
            FunctionCondition = Request.Form["FunctionCondition"],
            TestPressureIfAny = Request.Form["TestPressureIfAny"],
            CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
            CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
        };
        ResultClass result = new();
        try
        {
            InspectionData inspectionData = _inspectionService.AddInspection(inspection);
            IWebHostEnvironment environment =
                HttpContext.RequestServices.GetService<IWebHostEnvironment>() ?? throw new Exception("Environment not found");
            string path = Path.Combine(
                environment.WebRootPath,
                "Uploads",
                "Inspection",
                inspectionData.Id.ToString()
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
                        InspectionFileClass inspectionFile =
                            new()
                            {
                                InspectionID = inspectionData.Id,
                                FileName = fileName,
                                FileSize = formFile.Length,
                                FileType = formFile.ContentType,
                                FilePath = Path.Combine(
                                    "Uploads",
                                    "Inspection",
                                    inspectionData.Id.ToString(),
                                    fileName
                                ),
                                CreatedAt = DateTime.Now.ToString(
                                    SharedEnvironment.GetDateFormatString()
                                ),
                                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                            };
                        _inspectionService.AddInspectionFile(inspectionFile);
                    }
                }
            }
            result.IsSuccess = true;
            result.Message = "Success";
            result.Data = inspectionData;
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
    public IActionResult UpdateInspection()
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
            if(inspectionFileClass != null && inspectionFileClass.Count > 0)
            {
                _inspectionService.DeleteInspectionFiles(inspectionFileClass);
            }
            int inspectionID = Convert.ToInt32(Request.Form["Id"]);
            InspectionClass inspection = new()
            {
                Id = inspectionID,
                AssetID = Convert.ToInt32(Request.Form["AssetID"]),
                InspectionDate = Request.Form["InspectionDate"],
                InspectionMethodID = Convert.ToInt32(Request.Form["InspectionMethodID"]),
                InspectionEffectivenessID = Convert.ToInt32(Request.Form["InspectionEffectivenessID"]),
                InspectionDescription = Request.Form["InspectionDescription"],
                CurrentConditionLeakeageToAtmosphereID = Convert.ToInt32(Request.Form["CurrentConditionLeakeageToAtmosphereID"]),
                CurrentConditionFailureOfFunctionID = Convert.ToInt32(Request.Form["CurrentConditionFailureOfFunctionID"]),
                CurrentConditionPassingAcrossValveID = Convert.ToInt32(Request.Form["CurrentConditionPassingAcrossValveID"]),
                FunctionCondition = Request.Form["FunctionCondition"],
                TestPressureIfAny = Request.Form["TestPressureIfAny"],
            };
            IWebHostEnvironment environment =
                HttpContext.RequestServices.GetService<IWebHostEnvironment>() ?? throw new Exception("Environment not found");
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
                    using var stream = System.IO.File.Create(filePath);
                    formFile.CopyTo(stream);
                    InspectionFileClass inspectionFile =
                        new()
                        {
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
                            CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
                            CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                        };
                    _inspectionService.AddInspectionFile(inspectionFile);
                }
            }
            InspectionData resultupdate = _inspectionService.UpdateInspection(inspection);
            result.IsSuccess = true;
            result.Message = "Success";
            result.Data = resultupdate;
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
    public IActionResult DeleteInspection()
    {
        ResultClass result = new();
        try{
            if(!int.TryParse(Request.Form["id"], out int id))
            {
                throw new Exception("Invalid ID");
            }
            int deletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            InspectionClass inspection = new()
            {
                Id = id,
                IsDeleted = true,
                DeletedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
                DeletedBy = deletedBy,
            };
            InspectionData inspectionData = _inspectionService.DeleteInspection(inspection);
            result.IsSuccess = true;
            result.Message = "Inspection deleted successfully";
            result.Data = inspectionData;
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

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetInspectionList()
    {
        ResultClass result = new();
        try {
            int AssetID = Convert.ToInt32(Request.Query["AssetID"]);
            List<InspectionData> inspectionList = _inspectionService.GetInspectionList(false, AssetID);
            result.IsSuccess = true;
            result.Message = "Success";
            result.Data = inspectionList;
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

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetInspectionSidebar(int AssetID)
    {
        List<InspectionData> inspectionSidebar = _inspectionService.GetInspectionList(false, AssetID);
        List<Dictionary<string, string>> inspectionSidebarList = [];
        foreach (var item in inspectionSidebar)
        {
            if(
                item.InspectionDate == null
                || item.Asset == null
                || item.Asset.BusinessArea == null
                || item.Asset.Platform == null
                || item.Asset.TagNo == null
            )
            {
                continue;
            }
            Dictionary<string, string> inspectionSidebarItem =
                new()
                {
                    { "Id", item.Id.ToString() },
                    { "Name", item.InspectionDate },
                    { "Area", item.Asset.BusinessArea },
                    { "Platform", item.Asset.Platform },
                    { "Asset", item.Asset.TagNo },
                    { "AssetID", item.Asset.Id.ToString() }
                };
            inspectionSidebarList.Add(inspectionSidebarItem);
        }
        inspectionSidebarList = inspectionSidebarList
            .OrderByDescending(i => DateTime.ParseExact(i["Name"], "dd-MM-yyyy", CultureInfo.InvariantCulture))
            .ToList();
        return Json(inspectionSidebarList);
    }
}

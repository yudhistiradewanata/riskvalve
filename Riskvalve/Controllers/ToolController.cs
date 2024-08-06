using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using SharedLayer;

namespace Riskvalve.Controllers;

public class ToolController(
    IInspectionService inspectionService,
    IMaintenanceService maintenanceService,
    IAssetService assetService,
    IAssessmentService assessmentService
) : Controller
{
    private readonly IInspectionService _inspectionService = inspectionService;
    private readonly IMaintenanceService _maintenanceService = maintenanceService;
    private readonly IAssetService _assetService = assetService;
    private readonly IAssessmentService _assessmentService = assessmentService;

    public IActionResult ImportAssetRegister()
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
        return View();
    }

    public IActionResult ImportInspectionMaintenance()
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
        return View();
    }

    public IActionResult ImportAssessment()
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
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ImportExcelFile()
    {
        Dictionary<string, string>? datares = null;
        int total = 0;
        int success = 0;
        int failed = 0;
        List<string> failedDatas = [];

        IFormFile formFile = Request.Form.Files[0];
        string mode = Request.Form["mode"].ToString().ToLower();
        string currentYear = DateTime.Now.Year.ToString();
        IWebHostEnvironment environment =
            HttpContext.RequestServices.GetService<IWebHostEnvironment>() ?? throw new Exception("Environment not found");
        string path = Path.Combine(environment.WebRootPath, "Uploads", "Temporary", currentYear);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
        var filePath = Path.Combine(path, fileName);
        using (var stream = System.IO.File.Create(filePath))
        {
            formFile.CopyTo(stream);
        }
        //read uploaded excel file
        List<Dictionary<string, string>> data = [];
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;
            int max_col = 0;
            if (mode.Equals("asset"))
            {
                max_col = 31;
            }
            else if (mode.Equals("inspection"))
            {
                max_col = 13;
            }
            else if (mode.Equals("maintenance"))
            {
                max_col = 7;
            }
            else if (mode.Equals("assessment"))
            {
                max_col = 15;
            }
            if (colCount < max_col)
            {
                return Json(
                    new Dictionary<string, string>
                    {
                        { "total", total.ToString() },
                        { "success", success.ToString() },
                        { "failed", failed.ToString() },
                        { "failedDatas", JsonConvert.SerializeObject(failedDatas) },
                        {
                            "message",
                            "Invalid column count, column required is "
                                + max_col
                                + ", but found "
                                + colCount
                                + "."
                        }
                    }
                );
            }
            for (int row = 3; row <= rowCount; row++)
            {
                Dictionary<string, string> rowValues = new();
                if (worksheet.Cells[row, 1].Value == null)
                {
                    failed++;
                    total++;
                    failedDatas.Add("Tag No is empty on row " + row);
                    continue;
                }
                for (int col = 1; col <= colCount; col++)
                {
                    if (
                        worksheet.Cells[2, col].Value != null
                        && worksheet.Cells[row, col].Value != null
                    )
                    {
                        string key = worksheet.Cells[2, col].Value.ToString() ?? "";
                        string value = worksheet.Cells[row, col].Value.ToString() ?? "";
                        rowValues.Add(
                            key.Trim(),
                            value.Trim()
                        );
                    }
                }
                data.Add(rowValues);
            }
        }
        List<Dictionary<string, string>> result = [];
        try{
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "Tool");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            if (mode.Equals("asset"))
            {
                datares = _assetService.ImportAsset(data, int.Parse(HttpContext.Session.GetString("Id") ?? "0"));
            }
            else if (mode.Equals("inspection"))
            {
                datares = _inspectionService.ImportInspection(data, int.Parse(HttpContext.Session.GetString("Id") ?? "0"));
            }
            else if (mode.Equals("maintenance"))
            {
                datares =  _maintenanceService.ImportMaintenance(data, int.Parse(HttpContext.Session.GetString("Id") ?? "0"));
            }
            else if (mode.Equals("assessment"))
            {
                datares = _assessmentService.ImportAssessment(data, int.Parse(HttpContext.Session.GetString("Id") ?? "0"));
            }
        }
        catch(Exception e){
            ResultClass resultClass = new()
            {
                IsSuccess = false,
                Message = e.Message,
                Data = null
            };
            return Json(resultClass);
        }
        if(datares != null)
        {
            ResultClass resultClass = new()
            {
                IsSuccess = true,
                Message = datares["message"],
                Data = datares
            };
            return Json(resultClass);
        }
        return Json("Error API");
    }

    public IActionResult Help()
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
        return View();
    }
}

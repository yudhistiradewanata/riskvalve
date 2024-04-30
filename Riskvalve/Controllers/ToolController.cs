using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class ToolController : Controller
{
    private readonly ILogger<ToolController> _logger;

    public ToolController(ILogger<ToolController> logger)
    {
        _logger = logger;
    }

    public IActionResult ImportAssetRegister()
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
        return View();
    }

    public IActionResult ImportInspectionMaintenance()
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
        return View();
    }

    public IActionResult ImportAssessment()
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
        return View();
    }

    [HttpPost]
    public IActionResult ImportExcelFile()
    {
        int total = 0;
        int success = 0;
        int failed = 0;
        List<string> failedDatas = new();
        LogDB logDB = new();
        LogModel logModel = new();
        AssetModel assetModel = new();
        InspectionModel inspectionModel = new();
        MaintenanceModel maintenanceModel = new();
        AssessmentModel assessmentModel = new();

        IFormFile formFile = Request.Form.Files[0];
        string mode = Request.Form["mode"].ToString().ToLower();
        string currentYear = DateTime.Now.Year.ToString();
        IWebHostEnvironment environment =
            HttpContext.RequestServices.GetService<IWebHostEnvironment>();
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
        List<Dictionary<string, string>> data = new();
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;
            for (int row = 3; row <= rowCount; row++)
            {
                Dictionary<string, string> rowValues = new();
                for (int col = 1; col <= colCount; col++)
                {
                    rowValues.Add(
                        worksheet.Cells[2, col].Value.ToString().Trim(),
                        worksheet.Cells[row, col].Value.ToString().Trim()
                    );
                }
                data.Add(rowValues);
            }
        }
        List<Dictionary<string, string>> result = new();
        if (mode.Equals("asset"))
        {
            ToolImportModel toolImport = assetModel.MapAssetRegister(data);
            // result = assetModel.MapAssetRegister(data);
            result = toolImport.mappedRecords;
            AssetDB assetDB = new();
            foreach (var item in result)
            {
                total++;
                try
                {
                    string json = JsonConvert.SerializeObject(item);
                    assetDB = JsonConvert.DeserializeObject<AssetDB>(json);
                    assetDB.CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString());
                    assetDB.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    assetModel.AddAsset(assetDB);
                }
                catch (Exception ex)
                {
                    logDB = new LogDB
                    {
                        Module = "ImportAssetRegister",
                        CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                        CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                        Message = ex.Message,
                        Data = JsonConvert.SerializeObject(assetDB)
                    };
                    logModel.AddLog(logDB);
                    failed++;
                    failedDatas.Add(assetDB.TagNo);
                    continue;
                }
            }
            success = total - failed;
            string message =
                "Success import "
                + success
                + " data(s) of "
                + total
                + " data(s). Failed "
                + failed
                + " data(s)";
            if (failed > 0)
            {
                if (failedDatas.Count > 1)
                {
                    string failedData = "";
                    foreach (var item in failedDatas)
                    {
                        failedData += item + ", ";
                    }
                    failedData = failedData.Substring(0, failedData.Length - 2);
                    message += " with Tag No: " + failedData + ".";
                }
                else
                {
                    message += ".";
                }
            }
            else
            {
                message += ".";
            }
            string messageFailed = "";
            foreach (var item in toolImport.failedRecords)
            {
                total++;
                failed++;
                messageFailed += item + ", ";
                if (item.Equals(toolImport.failedRecords.Last()))
                {
                    messageFailed = messageFailed.Substring(0, messageFailed.Length - 2);
                    message += " Exception Error: " + messageFailed + ".";
                }
            }
            return Json(
                new Dictionary<string, string>
                {
                    { "total", total.ToString() },
                    { "success", success.ToString() },
                    { "failed", failed.ToString() },
                    { "failedDatas", JsonConvert.SerializeObject(failedDatas) },
                    { "message", message }
                }
            );
        }
        else if (mode.Equals("inspection"))
        {
            ToolImportModel toolImport = inspectionModel.MapInspection(data);
            // result = inspectionModel.MapInspection(data);
            result = toolImport.mappedRecords;
            InspectionDB inspectionDB = new();
            foreach (var item in result)
            {
                total++;
                try
                {
                    string json = JsonConvert.SerializeObject(item);
                    inspectionDB = JsonConvert.DeserializeObject<InspectionDB>(json);
                    inspectionDB.CreatedAt = DateTime.Now.ToString(
                        Environment.GetDateFormatString()
                    );
                    inspectionDB.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    inspectionModel.AddInspection(inspectionDB);
                }
                catch (Exception ex)
                {
                    logDB = new LogDB
                    {
                        Module = "ImportInspection",
                        CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                        CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                        Message = ex.Message,
                        Data = JsonConvert.SerializeObject(inspectionDB)
                    };
                    logModel.AddLog(logDB);
                    failed++;
                    failedDatas.Add(inspectionDB.InspectionDate);
                    continue;
                }
            }
            success = total - failed;
            string message =
                "Success import "
                + success
                + " data(s) of "
                + total
                + " data(s). Failed "
                + failed
                + " data(s)";
            if (failed > 0)
            {
                if(failedDatas.Count > 0){
                    string failedData = "";
                    foreach (var item in failedDatas)
                    {
                        failedData += item + ", ";
                    }
                    failedData = failedData.Substring(0, failedData.Length - 2);
                    message += " with Inspection Date: " + failedData + ".";
                }
                else
                {
                    message += ".";
                }
            }
            else
            {
                message += ".";
            }
            string messageFailed = "";
            foreach (var item in toolImport.failedRecords)
            {
                total++;
                failed++;
                messageFailed += item + ", ";
                if (item.Equals(toolImport.failedRecords.Last()))
                {
                    messageFailed = messageFailed.Substring(0, messageFailed.Length - 2);
                    message += " Exception Error: " + messageFailed + ".";
                }
            }
            return Json(
                new Dictionary<string, string>
                {
                    { "total", total.ToString() },
                    { "success", success.ToString() },
                    { "failed", failed.ToString() },
                    { "failedDatas", JsonConvert.SerializeObject(failedDatas) },
                    { "message", message }
                }
            );
        }
        else if (mode.Equals("maintenance"))
        {
            ToolImportModel toolImport = maintenanceModel.MapMaintenanceRegister(data);
            // result = maintenanceModel.MapMaintenanceRegister(data);
            result = toolImport.mappedRecords;
            MaintenanceDB maintenanceDB = new();
            foreach (var item in result)
            {
                total++;
                try
                {
                    string json = JsonConvert.SerializeObject(item);
                    maintenanceDB = JsonConvert.DeserializeObject<MaintenanceDB>(json);
                    maintenanceDB.CreatedAt = DateTime.Now.ToString(
                        Environment.GetDateFormatString()
                    );
                    maintenanceDB.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    maintenanceModel.AddMaintenance(maintenanceDB);
                }
                catch (Exception ex)
                {
                    logDB = new LogDB
                    {
                        Module = "ImportMaintenance",
                        CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                        CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                        Message = ex.Message,
                        Data = JsonConvert.SerializeObject(maintenanceDB)
                    };
                    logModel.AddLog(logDB);
                    failed++;
                    failedDatas.Add(maintenanceDB.MaintenanceDate);
                    continue;
                }
            }
            success = total - failed;
            string message =
                "Success import "
                + success
                + " data(s) of "
                + total
                + " data(s). Failed "
                + failed
                + " data(s)";
            if (failed > 0)
            {
                if (failedDatas.Count > 0)
                {
                    string failedData = "";
                    foreach (var item in failedDatas)
                    {
                        failedData += item + ", ";
                    }
                    failedData = failedData.Substring(0, failedData.Length - 2);
                    message += " with Maintenance Date: " + failedData + ".";
                }
                else
                {
                    message += ".";
                }
            }
            else
            {
                message += ".";
            }
            string messageFailed = "";
            foreach (var item in toolImport.failedRecords)
            {
                total++;
                failed++;
                messageFailed += item + ", ";
                if (item.Equals(toolImport.failedRecords.Last()))
                {
                    messageFailed = messageFailed.Substring(0, messageFailed.Length - 2);
                    message += " Exception Error: " + messageFailed + ".";
                }
            }
            return Json(
                new Dictionary<string, string>
                {
                    { "total", total.ToString() },
                    { "success", success.ToString() },
                    { "failed", failed.ToString() },
                    { "failedDatas", JsonConvert.SerializeObject(failedDatas) },
                    { "message", message }
                }
            );
        }
        else if (mode.Equals("assessment"))
        {
            ToolImportModel toolImport = assessmentModel.MapAssessment(data);
            // result = assessmentModel.MapAssessment(data);
            result = toolImport.mappedRecords;
            AssessmentDB assessmentDB = new();
            foreach (var item in result)
            {
                total++;
                try
                {
                    string json = JsonConvert.SerializeObject(item);
                    assessmentDB = JsonConvert.DeserializeObject<AssessmentDB>(json);
                    assessmentDB.CreatedAt = DateTime.Now.ToString(
                        Environment.GetDateFormatString()
                    );
                    assessmentDB.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    assessmentModel.AddAssessment(assessmentDB);
                }
                catch (Exception ex)
                {
                    logDB = new LogDB
                    {
                        Module = "ImportAssessment",
                        CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                        CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString()),
                        Message = ex.Message,
                        Data = JsonConvert.SerializeObject(assessmentDB)
                    };
                    logModel.AddLog(logDB);
                    failed++;
                    failedDatas.Add(assessmentDB.AssessmentDate);
                    continue;
                }
            }
            success = total - failed;
            string message =
                "Success import "
                + success
                + " data(s) of "
                + total
                + " data(s). Failed "
                + failed
                + " data(s)";
            if (failed > 0)
            {
                if (failedDatas.Count > 0)
                {
                    string failedData = "";
                    foreach (var item in failedDatas)
                    {
                        failedData += item + ", ";
                    }
                    failedData = failedData.Substring(0, failedData.Length - 2);
                    message += " with Assessment Date: " + failedData + ".";
                }
                else
                {
                    message += ".";
                }
            }
            else
            {
                message += ".";
            }
            string messageFailed = "";
            foreach (var item in toolImport.failedRecords)
            {
                total++;
                failed++;
                messageFailed += item + ", ";
                if (item.Equals(toolImport.failedRecords.Last()))
                {
                    messageFailed = messageFailed.Substring(0, messageFailed.Length - 2);
                    message += " Exception Error: " + messageFailed + ".";
                }
            }
            return Json(
                new Dictionary<string, string>
                {
                    { "total", total.ToString() },
                    { "success", success.ToString() },
                    { "failed", failed.ToString() },
                    { "failedDatas", JsonConvert.SerializeObject(failedDatas) },
                    { "message", message }
                }
            );
        }
        return Json("Error API");
    }
}

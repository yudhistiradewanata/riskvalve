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
            try
            {
                List<AssetDB> pushData = new();
                result = assetModel.MapAssetRegister(data);
                foreach (var item in result)
                {
                    string json = JsonConvert.SerializeObject(item);
                    AssetDB assetDB = JsonConvert.DeserializeObject<AssetDB>(json);
                    assetDB.CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString());
                    assetDB.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    pushData.Add(assetDB);
                }
                foreach (var item in pushData)
                {
                    assetModel.AddAsset(item);
                }
            }
            catch (Exception ex)
            {
                return Json(new Dictionary<string, string> { { "error", ex.Message } });
            }
        }
        else if(mode.Equals("inspection"))
        {
            List<InspectionDB> pushData = new();
            try{
                result = inspectionModel.MapInspection(data);
                foreach (var item in result)
                {
                    string json = JsonConvert.SerializeObject(item);
                    InspectionDB inspectionDB = JsonConvert.DeserializeObject<InspectionDB>(json);
                    inspectionDB.CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString());
                    inspectionDB.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    pushData.Add(inspectionDB);
                }
                foreach (var item in pushData)
                {
                    inspectionModel.AddInspection(item);
                }
            }
            catch (Exception ex)
            {
                return Json(new Dictionary<string, string> { { "error", ex.Message } });
            }
        }
        else if (mode.Equals("maintenance"))
        {
            List<MaintenanceDB> pushData = new();
            try{
                result = maintenanceModel.MapMaintenanceRegister(data);
                foreach (var item in result)
                {
                    string json = JsonConvert.SerializeObject(item);
                    MaintenanceDB maintenanceDB = JsonConvert.DeserializeObject<MaintenanceDB>(json);
                    maintenanceDB.CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString());
                    maintenanceDB.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    pushData.Add(maintenanceDB);
                }
                foreach (var item in pushData)
                {
                    maintenanceModel.AddMaintenance(item);
                }
            }
            catch (Exception ex)
            {
                return Json(new Dictionary<string, string> { { "error", ex.Message } });
            }
        }
        else if(mode.Equals("assessment"))
        {
            List<AssessmentDB> pushData = new();
            try{
                result = assessmentModel.MapAssessment(data);
                foreach (var item in result)
                {
                    string json = JsonConvert.SerializeObject(item);
                    AssessmentDB assessmentDB = JsonConvert.DeserializeObject<AssessmentDB>(json);
                    assessmentDB.CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString());
                    assessmentDB.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    pushData.Add(assessmentDB);
                }
                foreach (var item in pushData)
                {
                    assessmentModel.AddAssessment(item);
                }
            }
            catch (Exception ex)
            {
                return Json(new Dictionary<string, string> { { "error", ex.Message } });
            }
        }
        return Json(result);
    }
}

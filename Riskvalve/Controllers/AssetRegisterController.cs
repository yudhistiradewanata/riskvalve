using System.Diagnostics;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedLayer;

namespace Riskvalve.Controllers;

public class AssetRegisterController(
    IAreaService areaService,
    IPlatformService platformService,
    IAssetService assetService,
    IInspectionService inspectionService,
    IMaintenanceService maintenanceService,
    IAssessmentService assessmentService
) : Controller
{
    private readonly IAreaService _areaService = areaService;
    private readonly IPlatformService _platformService = platformService;
    private readonly IAssetService _assetService = assetService;
    private readonly IInspectionService _inspectionService = inspectionService;
    private readonly IMaintenanceService _maintenanceService = maintenanceService;
    private readonly IAssessmentService _assessmentService = assessmentService;

    public IActionResult Area()
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
        List<AreaData> areaList = _areaService.GetAreaList();
        ViewData["AreaList"] = areaList;
        return View();
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetAreaDetail()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int id = Convert.ToInt32(Request.Query["id"]);
            var area = _areaService.GetArea(id);
            result.IsSuccess = true;
            result.Message = "Area found";
            result.Data = area;
            return Json(result);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.Message = e.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateArea()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            if (!int.TryParse(Request.Form["Id"], out int id))
            {
                throw new Exception("Invalid Id");
            }
            AreaClass area = new() { Id = id, BusinessArea = Request.Form["BusinessArea"] };
            AreaData areaData = _areaService.UpdateArea(area);
            result.IsSuccess = true;
            result.Message = "Area updated successfully";
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
    public IActionResult AddArea()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int createdBy = 0;
            if (HttpContext.Session.GetString("Id") != null)
            {
                if (!int.TryParse(HttpContext.Session.GetString("Id"), out createdBy))
                {
                    throw new Exception("Invalid session Id");
                }
            }
            AreaClass area =
                new()
                {
                    BusinessArea = Request.Form["BusinessArea"],
                    IsDeleted = false,
                    CreatedBy = createdBy,
                    CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
                };
            AreaData areaData = _areaService.AddArea(area);
            result.IsSuccess = true;
            result.Message = "Area added successfully";
            result.Data = areaData;
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
    public IActionResult DeleteArea()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
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
            List<PlatformData> platformList = _platformService.GetPlatformList(id);
            if (platformList.Count > 0)
            {
                result.IsSuccess = false;
                result.Message =
                    "Area cannot be deleted because it has " + platformList.Count + " platform(s)";
            }
            else
            {
                AreaClass area =
                    new()
                    {
                        Id = id,
                        IsDeleted = true,
                        DeletedBy = deletedBy,
                        DeletedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
                    };
                AreaData areaData = _areaService.DeleteArea(area);
                result.IsSuccess = true;
                result.Message = "Area deleted successfully";
            }
            return Json(result);
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = "Failed to delete: " + ex.Message;
            return Json(result);
        }
    }

    public IActionResult Platform()
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
        List<PlatformData> platformList = _platformService.GetPlatformList();
        List<AreaData> areaList = _areaService.GetAreaList();

        ViewData["PlatformList"] = platformList;
        ViewData["AreaList"] = areaList;
        return View();
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetPlatformList()
    {
        List<PlatformData> platformList = [];
        try {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int AreaID = Convert.ToInt32(Request.Query["AreaID"]);
            platformList = _platformService.GetPlatformList(AreaID);
            return Json(platformList);
        } catch (Exception ex) {
            return Json(platformList);
        }
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetPlatformDetail()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int id = Convert.ToInt32(Request.Query["id"]);
            var platform = _platformService.GetPlatform(id);
            result.IsSuccess = true;
            result.Message = "Platform found";
            result.Data = platform;
            return Json(result);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.Message = e.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdatePlatform()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            if (!int.TryParse(Request.Form["Id"], out int id))
            {
                throw new Exception("Invalid Id");
            }
            PlatformClass platform =
                new()
                {
                    Id = id,
                    AreaID = Convert.ToInt32(Request.Form["AreaID"]),
                    Platform = Request.Form["Platform"],
                    Code = Request.Form["Code"]
                };
            PlatformData platformData = _platformService.UpdatePlatform(platform);
            result.IsSuccess = true;
            result.Message = "Platform updated successfully";
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
    public IActionResult AddPlatform()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int createdBy = 0;
            if (HttpContext.Session.GetString("Id") != null)
            {
                if (!int.TryParse(HttpContext.Session.GetString("Id"), out createdBy))
                {
                    throw new Exception("Invalid session Id");
                }
            }
            PlatformClass platform =
                new()
                {
                    AreaID = Convert.ToInt32(Request.Form["AreaID"]),
                    Platform = Request.Form["Platform"],
                    Code = Request.Form["Code"],
                    IsDeleted = false,
                    CreatedBy = createdBy,
                    CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
                };
            PlatformData platformData = _platformService.AddPlatform(platform);
            result.IsSuccess = true;
            result.Message = "Platform added successfully";
            result.Data = platformData;
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
    public IActionResult DeletePlatform()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
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
            List<AssetData> assetList = _assetService.GetAssetList(0, id);
            if (assetList.Count > 0)
            {
                result.IsSuccess = false;
                result.Message =
                    "Platform cannot be deleted because it has " + assetList.Count + " asset(s)";
                return Json(result);
            }
            else
            {
                PlatformClass platform =
                    new()
                    {
                        Id = id,
                        IsDeleted = true,
                        DeletedBy = deletedBy,
                        DeletedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
                    };
                PlatformData platformData = _platformService.DeletePlatform(platform);
                result.IsSuccess = true;
                result.Message = "Platform deleted successfully";
            }
            return Json(result);
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = "Failed to delete: " + ex.Message;
            return Json(result);
        }
    }

    public IActionResult Asset()
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

        ViewData["PlatformList"] = _platformService.GetPlatformList();
        ViewData["ValveTypeList"] = _assetService.GetValveTypeList();
        ViewData["ManualOverrideList"] = _assetService.GetManualOverrideList();
        ViewData["FluidPhaseList"] = _assetService.GetFluidPhaseList();
        ViewData["ToxicOrFlamableFluidList"] = _assetService.GetToxicOrFlamableFluidList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetAssetDatatable(
        int draw,
        int start,
        int length,
        string searchValue,
        string sortColumn,
        string sortColumnDirection,
        string searchColumnValues,
        string searchColumns
    )
    {
        Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
        if(Permission["Login"] == "false" || Permission["Permission"] == "false")
        {
            return Json(
                new
                {
                    draw = draw,
                    recordsFiltered = 0,
                    recordsTotal = 0,
                    data = new List<AssetData>()
                }
            );
        }
        List<AssetData> assetList = _assetService.GetAssetList();

        // Console.WriteLine("=== DEBUG MODE ===");
        // Console.WriteLine("searchValue: " + searchValue);
        // Console.WriteLine("searchColumnValues: " + searchColumnValues);
        // Console.WriteLine("searchColumns: " + searchColumns);
        // Console.WriteLine("sortColumn: " + sortColumn);
        // Console.WriteLine("sortColumnDirection: " + sortColumnDirection);
        // foreach(var formx in Request.Form)
        // {
        //     Console.WriteLine("formx: " + formx.Key + " - " + formx.Value);
        // }
        Console.WriteLine("=== END DEBUG ===");

        // Search
        if (!string.IsNullOrEmpty(searchValue))
        {
            assetList = assetList
                .Where(x =>
                    (
                        x.TagNo != null
                        && x.TagNo.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase)
                    )
                    || (
                        x.AssetName != null
                        && x.AssetName.Contains(
                            searchValue,
                            StringComparison.CurrentCultureIgnoreCase
                        )
                    )
                    || (
                        x.BusinessArea != null
                        && x.BusinessArea.Contains(
                            searchValue,
                            StringComparison.CurrentCultureIgnoreCase
                        )
                    )
                    || (
                        x.Platform != null
                        && x.Platform.Contains(
                            searchValue,
                            StringComparison.CurrentCultureIgnoreCase
                        )
                    )
                    || (
                        x.ValveType != null
                        && x.ValveType.Contains(
                            searchValue,
                            StringComparison.CurrentCultureIgnoreCase
                        )
                    )
                    || (
                        x.Size != null
                        && x.Size.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase)
                    )
                    || (
                        x.ClassRating != null
                        && x.ClassRating.Contains(
                            searchValue,
                            StringComparison.CurrentCultureIgnoreCase
                        )
                    )
                    || (
                        x.PIDNo != null
                        && x.PIDNo.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase)
                    )
                    || (
                        x.ParentEquipmentNo != null
                        && x.ParentEquipmentNo.Contains(
                            searchValue,
                            StringComparison.CurrentCultureIgnoreCase
                        )
                    )
                    || (
                        x.ParentEquipmentDescription != null
                        && x.ParentEquipmentDescription.Contains(
                            searchValue,
                            StringComparison.CurrentCultureIgnoreCase
                        )
                    )
                    || (
                        x.ServiceFluid != null
                        && x.ServiceFluid.Contains(
                            searchValue,
                            StringComparison.CurrentCultureIgnoreCase
                        )
                    )
                )
                .ToList();
        }

        // Individual column search
        Dictionary<string, string> searchColumnList = [];
        if(!string.IsNullOrEmpty(searchColumns))
        {
            searchColumnList = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchColumns);
        }
        Dictionary<string, string> searchColumnValueList = [];
        if(!string.IsNullOrEmpty(searchColumnValues))
        {
            searchColumnValueList = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchColumnValues);
        }
        
        foreach(var searchColumnst in searchColumnList)
        {
            string searchColumn = searchColumnst.Value;
            string searchColumnValue = searchColumnValueList[searchColumnst.Key];
            Console.WriteLine("ayaya masuk: " + searchColumn + " - " + searchColumnValue);
            if (!string.IsNullOrEmpty(searchColumnValue))
            {
                if(searchColumn.Equals("TagNo"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.TagNo != null
                            && x.TagNo.Contains(searchColumnValue, StringComparison.CurrentCultureIgnoreCase)
                        )
                        .ToList();
                }
                if(searchColumn.Equals("BusinessArea"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.BusinessArea != null
                            && x.BusinessArea.Contains(
                                searchColumnValue,
                                StringComparison.CurrentCultureIgnoreCase
                            )
                        )
                        .ToList();
                }
                if(searchColumn.Equals("Platform"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.Platform != null
                            && x.Platform.Contains(
                                searchColumnValue,
                                StringComparison.CurrentCultureIgnoreCase
                            )
                        )
                        .ToList();
                }
                if(searchColumn.Equals("ValveType"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.ValveType != null
                            && x.ValveType.Contains(
                                searchColumnValue,
                                StringComparison.CurrentCultureIgnoreCase
                            )
                        )
                        .ToList();
                }
                if(searchColumn.Equals("Size"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.Size != null
                            && x.Size.Contains(searchColumnValue, StringComparison.CurrentCultureIgnoreCase)
                        )
                        .ToList();
                }
                if(searchColumn.Equals("ClassRating"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.ClassRating != null
                            && x.ClassRating.Contains(
                                searchColumnValue,
                                StringComparison.CurrentCultureIgnoreCase
                            )
                        )
                        .ToList();
                }
                if(searchColumn.Equals("ParentEquipmentNo"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.ParentEquipmentNo != null
                            && x.ParentEquipmentNo.Contains(
                                searchColumnValue,
                                StringComparison.CurrentCultureIgnoreCase
                            )
                        )
                        .ToList();
                }
                if(searchColumn.Equals("ParentEquipmentDescription"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.ParentEquipmentDescription != null
                            && x.ParentEquipmentDescription.Contains(
                                searchColumnValue,
                                StringComparison.CurrentCultureIgnoreCase
                            )
                        )
                        .ToList();
                }
                if(searchColumn.Equals("PIDNo"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.PIDNo != null
                            && x.PIDNo.Contains(searchColumnValue, StringComparison.CurrentCultureIgnoreCase)
                        )
                        .ToList();
                }
                if(searchColumn.Equals("ServiceFluid"))
                {
                    assetList = assetList
                        .Where(x =>
                            x.ServiceFluid != null
                            && x.ServiceFluid.Contains(
                                searchColumnValue,
                                StringComparison.CurrentCultureIgnoreCase
                            )
                        )
                        .ToList();
                }
            }
        }
        

        // Sorting
        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
        {
            if (sortColumn.Equals("1", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.TagNo)]
                    : [.. assetList.OrderByDescending(x => x.TagNo)];
            }
            else if (sortColumn.Equals("2", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.BusinessArea)]
                    : [.. assetList.OrderByDescending(x => x.BusinessArea)];
            }
            else if (sortColumn.Equals("3", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.Platform)]
                    : [.. assetList.OrderByDescending(x => x.Platform)];
            }
            else if (sortColumn.Equals("4", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.ValveType)]
                    : [.. assetList.OrderByDescending(x => x.ValveType)];
            }
            else if (sortColumn.Equals("5", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => int.Parse(x.Size ?? "0"))]
                    : [.. assetList.OrderByDescending(x => int.Parse(x.Size ?? "0"))];
            }
            else if (sortColumn.Equals("6", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.ClassRating)]
                    : [.. assetList.OrderByDescending(x => x.ClassRating)];
            }
            else if (sortColumn.Equals("7", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.PIDNo)]
                    : [.. assetList.OrderByDescending(x => x.PIDNo)];
            }
            else if (sortColumn.Equals("8", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.ParentEquipmentNo)]
                    : [.. assetList.OrderByDescending(x => x.ParentEquipmentNo)];
            }
            else if (sortColumn.Equals("9", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.ParentEquipmentDescription)]
                    : [.. assetList.OrderByDescending(x => x.ParentEquipmentDescription)];
            }
            else if (sortColumn.Equals("10", StringComparison.CurrentCultureIgnoreCase))
            {
                assetList = sortColumnDirection.Equals(
                    "asc",
                    StringComparison.CurrentCultureIgnoreCase
                )
                    ? [.. assetList.OrderBy(x => x.ServiceFluid)]
                    : [.. assetList.OrderByDescending(x => x.ServiceFluid)];
            }
        }

        // Paging
        int recordsTotal = assetList.Count;
        var data = length == -1 ? [.. assetList] : assetList.Skip(start).Take(length).ToList();

        return Json(
            new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data
            }
        );
    }

    [HttpGet]
    [ValidateAntiForgeryToken]
    public IActionResult GetAssetDetail()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int id = Convert.ToInt32(Request.Query["id"]);
            var asset = _assetService.GetAsset(id);
            result.IsSuccess = true;
            result.Message = "Asset found";
            result.Data = asset;
            return Json(result);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.Message = e.Message;
            return Json(result);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateAsset()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            if (!int.TryParse(Request.Form["Id"], out int id))
            {
                throw new Exception("Invalid Id");
            }
            string? platformIdString = Request.Form["PlatformID"];
            int platformId = string.IsNullOrEmpty(platformIdString)
                ? 0
                : SharedEnvironment.StringToInt(platformIdString);
            string? valveTypeIdString = Request.Form["ValveTypeID"];
            int valveTypeId = string.IsNullOrEmpty(valveTypeIdString)
                ? 0
                : SharedEnvironment.StringToInt(valveTypeIdString);
            string? manualOverrideIdString = Request.Form["ManualOverrideID"];
            int manualOverrideId = string.IsNullOrEmpty(manualOverrideIdString)
                ? 0
                : SharedEnvironment.StringToInt(manualOverrideIdString);
            string? fluidPhaseIdString = Request.Form["FluidPhaseID"];
            int fluidPhaseId = string.IsNullOrEmpty(fluidPhaseIdString)
                ? 0
                : SharedEnvironment.StringToInt(fluidPhaseIdString);
            string? toxicOrFlamableFluidIdString = Request.Form["ToxicOrFlamableFluidID"];
            int toxicOrFlamableFluidId = string.IsNullOrEmpty(toxicOrFlamableFluidIdString)
                ? 0
                : SharedEnvironment.StringToInt(toxicOrFlamableFluidIdString);
            AssetClass asset =
                new()
                {
                    Id = id,
                    TagNo = Request.Form["TagNo"],
                    PlatformID = platformId,
                    ValveTypeID = valveTypeId,
                    Size = Request.Form["Size"],
                    ClassRating = Request.Form["ClassRating"],
                    ParentEquipmentNo = Request.Form["ParentEquipmentNo"],
                    ParentEquipmentDescription = Request.Form["ParentEquipmentDescription"],
                    InstallationDate = Request.Form["InstallationDate"],
                    PIDNo = Request.Form["PIDNo"],
                    Manufacturer = Request.Form["Manufacturer"],
                    BodyModel = Request.Form["BodyModel"],
                    BodyMaterial = Request.Form["BodyMaterial"],
                    EndConnection = Request.Form["EndConnection"],
                    SerialNo = Request.Form["SerialNo"],
                    ManualOverrideID = manualOverrideId,
                    ActuatorMfg = Request.Form["ActuatorMfg"],
                    ActuatorSerialNo = Request.Form["ActuatorSerialNo"],
                    ActuatorTypeModel = Request.Form["ActuatorTypeModel"],
                    ActuatorPower = Request.Form["ActuatorPower"],
                    OperatingTemperature = Request.Form["OperatingTemperature"],
                    OperatingPressure = Request.Form["OperatingPressure"],
                    FlowRate = Request.Form["FlowRate"],
                    ServiceFluid = Request.Form["ServiceFluid"],
                    FluidPhaseID = fluidPhaseId,
                    ToxicOrFlamableFluidID = toxicOrFlamableFluidId,
                    AssetName = Request.Form["AssetName"],
                    CostOfReplacementAndRepair = Request.Form["CostOfReplacementAndRepair"],
                    Status = Request.Form["Status"],
                    UsageType = Request.Form["UsageType"],
                    Actuation = Request.Form["Actuation"],
                };
            AssetData assetData = _assetService.UpdateAsset(asset);
            result.IsSuccess = true;
            result.Message = "Asset updated successfully";
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
    public IActionResult AddAsset()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            int createdBy = 0;
            if (HttpContext.Session.GetString("Id") != null)
            {
                if (!int.TryParse(HttpContext.Session.GetString("Id"), out createdBy))
                {
                    throw new Exception("Invalid session Id");
                }
            }
            string? platformIdString = Request.Form["PlatformID"];
            int platformId = string.IsNullOrEmpty(platformIdString)
                ? 0
                : SharedEnvironment.StringToInt(platformIdString);
            string? valveTypeIdString = Request.Form["ValveTypeID"];
            int valveTypeId = string.IsNullOrEmpty(valveTypeIdString)
                ? 0
                : SharedEnvironment.StringToInt(valveTypeIdString);
            string? manualOverrideIdString = Request.Form["ManualOverrideID"];
            int manualOverrideId = string.IsNullOrEmpty(manualOverrideIdString)
                ? 0
                : SharedEnvironment.StringToInt(manualOverrideIdString);
            string? fluidPhaseIdString = Request.Form["FluidPhaseID"];
            int fluidPhaseId = string.IsNullOrEmpty(fluidPhaseIdString)
                ? 0
                : SharedEnvironment.StringToInt(fluidPhaseIdString);
            string? toxicOrFlamableFluidIdString = Request.Form["ToxicOrFlamableFluidID"];
            int toxicOrFlamableFluidId = string.IsNullOrEmpty(toxicOrFlamableFluidIdString)
                ? 0
                : SharedEnvironment.StringToInt(toxicOrFlamableFluidIdString);
            AssetClass asset =
                new()
                {
                    TagNo = Request.Form["TagNo"],
                    PlatformID = platformId,
                    ValveTypeID = valveTypeId,
                    Size = Request.Form["Size"],
                    ClassRating = Request.Form["ClassRating"],
                    ParentEquipmentNo = Request.Form["ParentEquipmentNo"],
                    ParentEquipmentDescription = Request.Form["ParentEquipmentDescription"],
                    InstallationDate = Request.Form["InstallationDate"],
                    PIDNo = Request.Form["PIDNo"],
                    Manufacturer = Request.Form["Manufacturer"],
                    BodyModel = Request.Form["BodyModel"],
                    BodyMaterial = Request.Form["BodyMaterial"],
                    EndConnection = Request.Form["EndConnection"],
                    SerialNo = Request.Form["SerialNo"],
                    ManualOverrideID = manualOverrideId,
                    ActuatorMfg = Request.Form["ActuatorMfg"],
                    ActuatorSerialNo = Request.Form["ActuatorSerialNo"],
                    ActuatorTypeModel = Request.Form["ActuatorTypeModel"],
                    ActuatorPower = Request.Form["ActuatorPower"],
                    OperatingTemperature = Request.Form["OperatingTemperature"],
                    OperatingPressure = Request.Form["OperatingPressure"],
                    FlowRate = Request.Form["FlowRate"],
                    ServiceFluid = Request.Form["ServiceFluid"],
                    FluidPhaseID = fluidPhaseId,
                    ToxicOrFlamableFluidID = toxicOrFlamableFluidId,
                    AssetName = Request.Form["AssetName"],
                    CostOfReplacementAndRepair = Request.Form["CostOfReplacementAndRepair"],
                    Status = Request.Form["Status"],
                    UsageType = Request.Form["UsageType"],
                    Actuation = Request.Form["Actuation"],
                    IsDeleted = false,
                };
            AssetData assetData = _assetService.AddAsset(asset);
            result.IsSuccess = true;
            result.Message = "Asset added successfully";
            result.Data = assetData;
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
    public IActionResult DeleteAsset()
    {
        ResultClass result = new();
        try
        {
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
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
            List<InspectionData> inspectionList = _inspectionService.GetInspectionList(false, id);
            List<MaintenanceData> maintenanceList = _maintenanceService.GetMaintenanceList(
                false,
                id
            );
            List<AssessmentData> assessmentList = _assessmentService.GetAssessmentList(
                0,
                0,
                false,
                false,
                id
            );
            if (inspectionList.Count > 0 || maintenanceList.Count > 0 || assessmentList.Count > 0)
            {
                result.IsSuccess = false;
                result.Message =
                    "Asset cannot be deleted because it has "
                    + inspectionList.Count
                    + " inspection(s), "
                    + maintenanceList.Count
                    + " maintenance(s), and "
                    + assessmentList.Count
                    + " assessment(s)";
                return Json(result);
            }
            else
            {
                AssetClass asset =
                    new()
                    {
                        Id = id,
                        IsDeleted = true,
                        DeletedBy = deletedBy,
                        DeletedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString())
                    };
                AssetData assetData = _assetService.DeleteAsset(asset);
                result.IsSuccess = true;
                result.Message = "Asset deleted successfully";
            }
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
    public IActionResult GetAssetSidebar(int PlatformID)
    {
        ResultClass result = new();
        try{
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            List<AssetData> assetList = _assetService.GetAssetList(0, PlatformID);
            var selectedAssetList = assetList
                .Select(asset => new
                {
                    asset.Id,
                    asset.TagNo,
                    asset.BusinessArea,
                    asset.Platform
                })
                .ToList();
            result =
                new()
                {
                    Data = selectedAssetList,
                    IsSuccess = true,
                    Message = "Asset sidebar found"
                };
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
    public IActionResult GetAssetSidebarSearch(string search)
    {
        ResultClass result = new();
        try{
            Dictionary<string, string> Permission = Session.CheckPermission(HttpContext, "AreaRegister");
            if(Permission["Login"] == "false" || Permission["Permission"] == "false")
            {
                throw new Exception(Permission["Message"]);
            }
            List<AssetData> assetDatas = _assetService
                .GetAssetList()
                .Select(asset => new AssetData
                {
                    Id = asset.Id,
                    TagNo = asset.TagNo,
                    BusinessArea = asset.BusinessArea,
                    Platform = asset.Platform
                })
                .Where(a =>
                    a.TagNo != null
                    && a.TagNo.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                )
                .ToList();
            Dictionary<string, List<AssetData>> areaPlatformAssetList = [];
            foreach (var item in assetDatas)
            {
                if (!areaPlatformAssetList.ContainsKey(item.BusinessArea + "-" + item.Platform))
                {
                    areaPlatformAssetList.Add(item.BusinessArea + "-" + item.Platform, []);
                }
                areaPlatformAssetList[item.BusinessArea + "-" + item.Platform].Add(item);
            }
            result =
                new()
                {
                    Data = areaPlatformAssetList,
                    IsSuccess = true,
                    Message = "Asset sidebar found"
                };
            return Json(result);
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
            return Json(result);
        }
    }
}

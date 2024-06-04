using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Riskvalve.Models;

namespace Riskvalve.Controllers;

public class AssetRegisterController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AssetRegisterController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Area()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect(Environment.app_path + "/Login/Index");
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
                return Redirect(Environment.app_path + "/Home/Index");
            }
        }
        AreaModel areaModel = new AreaModel();
        List<AreaModel> areaList = areaModel.GetAreaList(false);
        ViewData["AreaList"] = areaList;
        return View();
    }

    [HttpGet]
    public IActionResult GetAreaDetail()
    {
        AreaModel areaModel = new AreaModel();
        int id = Convert.ToInt32(Request.Query["id"]);
        AreaModel area = areaModel.GetAreaModel(id);
        return Json(area);
    }

    [HttpPost]
    public ResultModel UpdateArea()
    {
        AreaDB areaModel =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                BusinessArea = Request.Form["BusinessArea"]
            };
        AreaModel area = new();
        ResultModel result = area.UpdateArea(areaModel);
        return result;
    }

    [HttpPost]
    public ResultModel AddArea()
    {
        AreaDB areaModel =
            new()
            {
                BusinessArea = Request.Form["BusinessArea"],
                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        AreaModel area = new();
        ResultModel result = area.AddArea(areaModel);
        return result;
    }

    [HttpPost]
    public ResultModel DeleteArea()
    {
        AreaModel areaModel = new();
        int id = Convert.ToInt32(Request.Form["Id"]);
        AreaDB areaToDelete =
            new()
            {
                Id = id,
                IsDeleted = true,
                DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        ResultModel result = areaModel.DeleteArea(areaToDelete);
        return result;
    }

    public IActionResult Platform()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect(Environment.app_path + "/Login/Index");
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
                return Redirect(Environment.app_path + "/Home/Index");
            }
        }
        PlatformModel platformModel = new();
        List<PlatformModel> platformList = platformModel.GetPlatformList();
        ViewData["PlatformList"] = platformList;
        List<AreaModel> areaList = new AreaModel().GetAreaList();
        ViewData["AreaList"] = areaList;
        return View();
    }

    [HttpGet]
    public IActionResult GetPlatformList(int AreaID)
    {
        PlatformModel platformModel = new();
        List<PlatformModel> platformList = platformModel.GetPlatformList(AreaID);
        return Json(platformList);
    }

    [HttpGet]
    public IActionResult GetPlatformDetail()
    {
        PlatformModel platformModel = new();
        int id = Convert.ToInt32(Request.Query["id"]);
        PlatformModel platform = platformModel.GetPlatformModel(id);
        return Json(platform);
    }

    [HttpPost]
    public ResultModel UpdatePlatform()
    {
        PlatformModel platformModel = new();
        PlatformDB platformDb =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                AreaID = Convert.ToInt32(Request.Form["AreaID"]),
                Platform = Request.Form["Platform"],
                Code = Request.Form["Code"]
            };
        ResultModel result = platformModel.UpdatePlatform(platformDb);
        return result;
    }

    [HttpPost]
    public ResultModel AddPlatform()
    {
        PlatformModel platformModel = new();
        PlatformDB platformDb =
            new()
            {
                AreaID = Convert.ToInt32(Request.Form["AreaID"]),
                Platform = Request.Form["Platform"],
                Code = Request.Form["Code"],
                CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        ResultModel result = platformModel.AddPlatform(platformDb);
        return result;
    }

    [HttpPost]
    public ResultModel DeletePlatform()
    {
        PlatformModel platformModel =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                IsDeleted = true,
                DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        ResultModel result = platformModel.DeletePlatform(platformModel);
        return result;
    }

    public IActionResult Asset()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect(Environment.app_path + "/Login/Index");
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
                return Redirect(Environment.app_path + "/Home/Index");
            }
        }
        // AssetModel assetModel = new();
        // List<AssetModel> assetList = assetModel.GetAssetList();
        // ViewData["AssetList"] = assetList;

        List<PlatformModel> platformList = new PlatformModel().GetPlatformList();
        ViewData["PlatformList"] = platformList;

        List<ValveTypeModel> valveTypeList = new ValveTypeModel().GetValveTypeList();
        ViewData["ValveTypeList"] = valveTypeList;

        List<ManualOverrideModel> manualOverrideList =
            new ManualOverrideModel().GetManualOverrideList();
        ViewData["ManualOverrideList"] = manualOverrideList;

        List<FluidPhaseModel> fluidPhaseList = new FluidPhaseModel().GetFluidPhaseList();
        ViewData["FluidPhaseList"] = fluidPhaseList;

        List<ToxicOrFlamableFluidModel> toxicOrFlamableFluidList =
            new ToxicOrFlamableFluidModel().GetToxicOrFlamableFluidList();
        ViewData["ToxicOrFlamableFluidList"] = toxicOrFlamableFluidList;
        return View();
    }

    [HttpGet]
    public IActionResult GetAssetDatatable(
        int draw,
        int start,
        int length,
        string searchValue,
        string sortColumn,
        string sortColumnDirection
    )
    {
        AssetModel assetModel = new();
        List<AssetModel> assetList = assetModel.GetAssetList();

        // Search
        // Console.WriteLine("=== DEBUG MODE ===");
        // Console.WriteLine("searchValue: " + searchValue);
        // Console.WriteLine("sortColumn: " + sortColumn);
        // Console.WriteLine("sortColumnDirection: " + sortColumnDirection);
        // Console.WriteLine("=== END DEBUG ===");
        if (!string.IsNullOrEmpty(searchValue))
        {
            assetList = assetList
                .Where(x =>
                    x.TagNo.ToLower().Contains(searchValue.ToLower())
                    || x.AssetName.ToLower().Contains(searchValue.ToLower())
                    || x.BusinessArea.ToLower().Contains(searchValue.ToLower())
                    || x.Platform.ToLower().Contains(searchValue.ToLower())
                    || x.ValveType.ToLower().Contains(searchValue.ToLower())
                    || x.Size.ToLower().Contains(searchValue.ToLower())
                    || x.ClassRating.ToLower().Contains(searchValue.ToLower())
                    || x.PIDNo.ToLower().Contains(searchValue.ToLower())
                    || x.ParentEquipmentNo.ToLower().Contains(searchValue.ToLower())
                    || x.ParentEquipmentDescription.ToLower().Contains(searchValue.ToLower())
                    || x.ServiceFluid.ToLower().Contains(searchValue.ToLower())
                )
                .ToList();
        }

        // Sorting
        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
        {
            if (sortColumn.ToLower() == "1")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.TagNo).ToList()
                        : assetList.OrderByDescending(x => x.TagNo).ToList();
            }
            else if (sortColumn.ToLower() == "2")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.BusinessArea).ToList()
                        : assetList.OrderByDescending(x => x.BusinessArea).ToList();
            }
            else if (sortColumn.ToLower() == "3")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.Platform).ToList()
                        : assetList.OrderByDescending(x => x.Platform).ToList();
            }
            else if (sortColumn.ToLower() == "4")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.ValveType).ToList()
                        : assetList.OrderByDescending(x => x.ValveType).ToList();
            }
            else if (sortColumn.ToLower() == "5")
            {
                // assetList =
                //     sortColumnDirection.ToLower() == "asc"
                //         ? assetList.OrderBy(x => x.Size).ToList()
                //         : assetList.OrderByDescending(x => x.Size).ToList();
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => int.Parse(x.Size)).ToList()
                        : assetList.OrderByDescending(x => int.Parse(x.Size)).ToList();
            }
            else if (sortColumn.ToLower() == "6")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.ClassRating).ToList()
                        : assetList.OrderByDescending(x => x.ClassRating).ToList();
            }
            else if (sortColumn.ToLower() == "7")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.PIDNo).ToList()
                        : assetList.OrderByDescending(x => x.PIDNo).ToList();
            }
            else if (sortColumn.ToLower() == "8")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.ParentEquipmentNo).ToList()
                        : assetList.OrderByDescending(x => x.ParentEquipmentNo).ToList();
            }
            else if (sortColumn.ToLower() == "9")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.ParentEquipmentDescription).ToList()
                        : assetList.OrderByDescending(x => x.ParentEquipmentDescription).ToList();
            }
            else if (sortColumn.ToLower() == "10")
            {
                assetList =
                    sortColumnDirection.ToLower() == "asc"
                        ? assetList.OrderBy(x => x.ServiceFluid).ToList()
                        : assetList.OrderByDescending(x => x.ServiceFluid).ToList();
            }
        }

        // Paging
        int recordsTotal = assetList.Count();
        var data = assetList.Skip(start).Take(length).ToList();

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
    public IActionResult GetAssetList(int AreaID = 0, int PlatformID = 0)
    {
        AssetModel assetModel = new();
        List<AssetModel> assetList = assetModel.GetAssetList(AreaID, PlatformID);
        return Json(assetList);
    }

    [HttpGet]
    public IActionResult GetAssetDetail()
    {
        AssetModel assetModel = new();
        int id = Convert.ToInt32(Request.Query["id"]);
        AssetModel asset = assetModel.GetAssetModel(id);
        return Json(asset);
    }

    [HttpPost]
    public ResultModel UpdateAsset()
    {
        AssetModel assetModel = new();
        AssetDB assetDb =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                TagNo = Request.Form["TagNo"],
                PlatformID = Environment.StringToInt(Request.Form["PlatformID"]),
                ValveTypeID = Environment.StringToInt(Request.Form["ValveTypeID"]),
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
                ManualOverrideID = Environment.StringToInt(Request.Form["ManualOverrideID"]),
                ActuatorMfg = Request.Form["ActuatorMfg"],
                ActuatorSerialNo = Request.Form["ActuatorSerialNo"],
                ActuatorTypeModel = Request.Form["ActuatorTypeModel"],
                ActuatorPower = Request.Form["ActuatorPower"],
                OperatingTemperature = Request.Form["OperatingTemperature"],
                OperatingPressure = Request.Form["OperatingPressure"],
                FlowRate = Request.Form["FlowRate"],
                ServiceFluid = Request.Form["ServiceFluid"],
                FluidPhaseID = Environment.StringToInt(Request.Form["FluidPhaseID"]),
                ToxicOrFlamableFluidID = Environment.StringToInt(
                    Request.Form["ToxicOrFlamableFluidID"]
                ),
                AssetName = Request.Form["AssetName"],
                CostOfReplacementAndRepair = Request.Form["CostOfReplacementAndRepair"],
                Status = Request.Form["Status"],
                UsageType = Request.Form["UsageType"],
                Actuation = Request.Form["Actuation"],
            };
        ResultModel result = assetModel.UpdateAsset(assetDb);
        return result;
    }

    [HttpPost]
    public IActionResult AddAsset()
    {
        AssetModel assetModel = new();
        // Console.WriteLine("ALDOOOOOO");
        // string json = JsonConvert.SerializeObject(Request.Form);
        // Console.WriteLine(json);
        AssetDB assetDb =
            new()
            {
                TagNo = Request.Form["TagNo"],
                PlatformID = Environment.StringToInt(Request.Form["PlatformID"]),
                ValveTypeID = Environment.StringToInt(Request.Form["ValveTypeID"]),
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
                ManualOverrideID = Environment.StringToInt(Request.Form["ManualOverrideID"]),
                ActuatorMfg = Request.Form["ActuatorMfg"],
                ActuatorSerialNo = Request.Form["ActuatorSerialNo"],
                ActuatorTypeModel = Request.Form["ActuatorTypeModel"],
                ActuatorPower = Request.Form["ActuatorPower"],
                OperatingTemperature = Request.Form["OperatingTemperature"],
                OperatingPressure = Request.Form["OperatingPressure"],
                FlowRate = Request.Form["FlowRate"],
                ServiceFluid = Request.Form["ServiceFluid"],
                FluidPhaseID = Environment.StringToInt(Request.Form["FluidPhaseID"]),
                ToxicOrFlamableFluidID = Environment.StringToInt(
                    Request.Form["ToxicOrFlamableFluidID"]
                ),
                AssetName = Request.Form["AssetName"],
                CostOfReplacementAndRepair = Request.Form["CostOfReplacementAndRepair"],
                Status = Request.Form["Status"],
                UsageType = Request.Form["UsageType"],
                Actuation = Request.Form["Actuation"],
                CreatedBy = Environment.StringToInt(HttpContext.Session.GetString("Id")),
                CreatedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        try
        {
            int assetId = assetModel.AddAsset(assetDb);
            return Json(
                new Dictionary<string, string>
                {
                    { "Status", "Success" },
                    { "Message", "Asset added successfully" },
                    { "AssetId", assetId.ToString() }
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
    public ResultModel DeleteAsset()
    {
        AssetModel assetModel =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                IsDeleted = true,
                DeletedBy = Convert.ToInt32(HttpContext.Session.GetString("Id")),
                DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString())
            };
        ResultModel result = assetModel.DeleteAsset(assetModel);
        return result;
    }

    [HttpGet]
    public IActionResult GetAssetSidebar(int PlatformID)
    {
        AssetModel assetModel = new();
        List<AssetModel> assetList = assetModel.GetAssetList(0, PlatformID);
        return Json(assetList);
    }

    [HttpGet]
    public IActionResult GetAssetSidebarSearch(string search)
    {
        AssetModel assetModel = new();
        List<AssetModel> asset = assetModel.GetAssetList().Where(a => a.TagNo.ToLower().Contains(search.ToLower())).ToList();
        Dictionary<string, Dictionary<string, List<AssetModel>>> areaPlatformAssetList = new();
        foreach (var item in asset)
        {
            if (!areaPlatformAssetList.ContainsKey(item.BusinessArea))
            {
                areaPlatformAssetList.Add(
                    item.BusinessArea,
                    new Dictionary<string, List<AssetModel>>()
                );
            }
            if (!areaPlatformAssetList[item.BusinessArea].ContainsKey(item.Platform))
            {
                areaPlatformAssetList[item.BusinessArea].Add(item.Platform, new List<AssetModel>());
            }
            areaPlatformAssetList[item.BusinessArea][item.Platform].Add(item);
        }
        return Json(areaPlatformAssetList);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}

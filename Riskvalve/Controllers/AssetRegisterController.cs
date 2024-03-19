using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
            return Redirect("/Login/Index");
        }
        AreaModel areaModel = new AreaModel();
        List<AreaModel> areaList = areaModel.GetAreaList();
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
    public IActionResult UpdateArea()
    {
        AreaModel areaModel =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                BusinessArea = Request.Form["BusinessArea"]
            };
        areaModel.UpdateArea(areaModel);
        return RedirectToAction("Area");
    }

    [HttpPost]
    public IActionResult AddArea()
    {
        AreaModel areaModel = new() { BusinessArea = Request.Form["BusinessArea"] };
        areaModel.AddArea(areaModel);
        return RedirectToAction("Area");
    }

    [HttpPost]
    public IActionResult DeleteArea()
    {
        AreaModel areaModel = new();
        int id = Convert.ToInt32(Request.Form["Id"]);
        areaModel.DeleteArea(id);
        return RedirectToAction("Area");
    }

    public IActionResult Platform()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect("/Login/Index");
        }
        PlatformModel platformModel = new();
        List<PlatformModel> platformList = platformModel.GetPlatformList();
        ViewData["PlatformList"] = platformList;
        List<AreaModel> areaList = new AreaModel().GetAreaList();
        ViewData["AreaList"] = areaList;
        return View();
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
    public IActionResult UpdatePlatform()
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
        platformModel.UpdatePlatform(platformDb);
        return RedirectToAction("Platform");
    }

    [HttpPost]
    public IActionResult AddPlatform()
    {
        PlatformModel platformModel = new();
        PlatformDB platformDb =
            new()
            {
                AreaID = Convert.ToInt32(Request.Form["AreaID"]),
                Platform = Request.Form["Platform"],
                Code = Request.Form["Code"]
            };
        platformModel.AddPlatform(platformDb);
        return RedirectToAction("Platform");
    }

    [HttpPost]
    public IActionResult DeletePlatform()
    {
        PlatformModel platformModel = new();
        int id = Convert.ToInt32(Request.Form["Id"]);
        platformModel.DeletePlatform(id);
        return RedirectToAction("Platform");
    }

    public IActionResult Asset()
    {
        UserModel login = new();
        if (!login.isLogin(HttpContext))
        {
            TempData["Message"] = "Please login first";
            return Redirect("/Login/Index");
        }
        AssetModel assetModel = new();
        List<AssetModel> assetList = assetModel.GetAssetList();
        ViewData["AssetList"] = assetList;

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
    public IActionResult GetAssetList()
    {
        AssetModel assetModel = new();
        List<AssetModel> assetList = assetModel.GetAssetList();
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
    public IActionResult UpdateAsset()
    {
        AssetModel assetModel = new();
        AssetDB assetDb =
            new()
            {
                Id = Convert.ToInt32(Request.Form["Id"]),
                TagNo = Request.Form["TagNo"],
                PlatformID = Convert.ToInt32(Request.Form["PlatformID"]),
                ValveTypeID = Convert.ToInt32(Request.Form["ValveTypeID"]),
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
                ManualOverrideID = Convert.ToInt32(Request.Form["ManualOverrideID"]),
                ActuatorMfg = Request.Form["ActuatorMfg"],
                ActuatorSerialNo = Request.Form["ActuatorSerialNo"],
                ActuatorTypeModel = Request.Form["ActuatorTypeModel"],
                ActuatorPower = Request.Form["ActuatorPower"],
                OperatingTemperature = Request.Form["OperatingTemperature"],
                OperatingPressure = Request.Form["OperatingPressure"],
                FlowRate = Request.Form["FlowRate"],
                ServiceFluid = Request.Form["ServiceFluid"],
                FluidPhaseID = Convert.ToInt32(Request.Form["FluidPhaseID"]),
                ToxicOrFlamableFluidID = Convert.ToInt32(Request.Form["ToxicOrFlamableFluidID"])
            };
        assetModel.UpdateAsset(assetDb);
        return RedirectToAction("Asset");
    }

    [HttpPost]
    public IActionResult AddAsset()
    {
        AssetModel assetModel = new();
        AssetDB assetDb =
            new()
            {
                TagNo = Request.Form["TagNo"],
                PlatformID = Convert.ToInt32(Request.Form["PlatformID"]),
                ValveTypeID = Convert.ToInt32(Request.Form["ValveTypeID"]),
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
                ManualOverrideID = Convert.ToInt32(Request.Form["ManualOverrideID"]),
                ActuatorMfg = Request.Form["ActuatorMfg"],
                ActuatorSerialNo = Request.Form["ActuatorSerialNo"],
                ActuatorTypeModel = Request.Form["ActuatorTypeModel"],
                ActuatorPower = Request.Form["ActuatorPower"],
                OperatingTemperature = Request.Form["OperatingTemperature"],
                OperatingPressure = Request.Form["OperatingPressure"],
                FlowRate = Request.Form["FlowRate"],
                ServiceFluid = Request.Form["ServiceFluid"],
                FluidPhaseID = Convert.ToInt32(Request.Form["FluidPhaseID"]),
                ToxicOrFlamableFluidID = Convert.ToInt32(Request.Form["ToxicOrFlamableFluidID"])
            };
        assetModel.AddAsset(assetDb);
        return RedirectToAction("Asset");
    }

    [HttpPost]
    public IActionResult DeleteAsset()
    {
        AssetModel assetModel = new();
        int id = Convert.ToInt32(Request.Form["Id"]);
        assetModel.DeleteAsset(id);
        return RedirectToAction("Asset");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}

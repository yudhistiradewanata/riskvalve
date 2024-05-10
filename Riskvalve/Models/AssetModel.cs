using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class AssetContext : DbContext
{
    public DbSet<AssetDB> Asset { get; set; }
    public DbSet<PlatformDB> Platform { get; set; }
    public DbSet<AreaModel> Area { get; set; }
    public DbSet<ValveTypeModel> ValveType { get; set; }
    public DbSet<ManualOverrideModel> ManualOverride { get; set; }
    public DbSet<FluidPhaseModel> FluidPhase { get; set; }
    public DbSet<ToxicOrFlamableFluidModel> ToxicOrFlamableFluid { get; set; }
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class AssetDB
{
    public int Id { get; set; }
    public string? TagNo { get; set; }
    public string? AssetName { get; set; }
    public int? PlatformID { get; set; } // FK
    public int? ValveTypeID { get; set; } // FK
    public string? Size { get; set; }
    public string? ClassRating { get; set; }
    public string? ParentEquipmentNo { get; set; }
    public string? ParentEquipmentDescription { get; set; }
    public string? InstallationDate { get; set; }
    public string? PIDNo { get; set; }
    public string? Manufacturer { get; set; }
    public string? BodyModel { get; set; }
    public string? BodyMaterial { get; set; }
    public string? EndConnection { get; set; }
    public string? SerialNo { get; set; }
    public int? ManualOverrideID { get; set; } // FK
    public string? ActuatorMfg { get; set; }
    public string? ActuatorSerialNo { get; set; }
    public string? ActuatorTypeModel { get; set; }
    public string? ActuatorPower { get; set; }
    public string? OperatingTemperature { get; set; }
    public string? OperatingPressure { get; set; }
    public string? FlowRate { get; set; }
    public string? ServiceFluid { get; set; }
    public int? FluidPhaseID { get; set; } // FK
    public int? ToxicOrFlamableFluidID { get; set; } // FK
    public string? UsageType { get; set; }
    public string? CostOfReplacementAndRepair { get; set; }
    public string? Actuation { get; set; }
    public string? Status { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class AssetModel : AssetDB
{
    public string? Platform { get; set; }
    public string? BusinessArea { get; set; }
    public string? ValveType { get; set; }
    public string? ManualOverride { get; set; }
    public string? FluidPhase { get; set; }
    public string? ToxicOrFlamableFluid { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
    public InspectionModel? lastInspection { get; set; }
    public MaintenanceModel? lastMaintenance { get; set; }
    public AssessmentModel? lastAssessment { get; set; }

    public AssetModel GetAssetModel(int id)
    {
        AssetModel asset = new();
        using (var context = new AssetContext())
        {
            List<AssetModel> assetList = (
                from a in context.Asset
                // join p in context.Platform on a.PlatformID equals p.Id
                // join ar in context.Area on p.AreaID equals ar.Id
                // join v in context.ValveType on a.ValveTypeID equals v.Id
                // join m in context.ManualOverride on a.ManualOverrideID equals m.Id
                // join f in context.FluidPhase on a.FluidPhaseID equals f.Id
                // join t in context.ToxicOrFlamableFluid on a.ToxicOrFlamableFluidID equals t.Id
                where a.Id == id
                select new AssetModel
                {
                    Id = a.Id,
                    TagNo = a.TagNo,
                    PlatformID = a.PlatformID,
                    ValveTypeID = a.ValveTypeID,
                    Size = a.Size,
                    ClassRating = a.ClassRating,
                    ParentEquipmentNo = a.ParentEquipmentNo,
                    ParentEquipmentDescription = a.ParentEquipmentDescription,
                    InstallationDate = a.InstallationDate,
                    PIDNo = a.PIDNo,
                    Manufacturer = a.Manufacturer,
                    BodyModel = a.BodyModel,
                    BodyMaterial = a.BodyMaterial,
                    EndConnection = a.EndConnection,
                    SerialNo = a.SerialNo,
                    ManualOverrideID = a.ManualOverrideID,
                    ActuatorMfg = a.ActuatorMfg,
                    ActuatorSerialNo = a.ActuatorSerialNo,
                    ActuatorTypeModel = a.ActuatorTypeModel,
                    ActuatorPower = a.ActuatorPower,
                    OperatingTemperature = a.OperatingTemperature,
                    OperatingPressure = a.OperatingPressure,
                    FlowRate = a.FlowRate,
                    ServiceFluid = a.ServiceFluid,
                    FluidPhaseID = a.FluidPhaseID,
                    ToxicOrFlamableFluidID = a.ToxicOrFlamableFluidID,
                    Platform = context
                        .Platform.Where(p => p.Id == a.PlatformID)
                        .FirstOrDefault()
                        .Platform,
                    BusinessArea = context
                        .Area.Where(ar =>
                            ar.Id
                            == context
                                .Platform.Where(p => p.Id == a.PlatformID)
                                .FirstOrDefault()
                                .AreaID
                        )
                        .FirstOrDefault()
                        .BusinessArea,
                    ValveType = context
                        .ValveType.Where(v => v.Id == a.ValveTypeID)
                        .FirstOrDefault()
                        .ValveType,
                    ManualOverride = context
                        .ManualOverride.Where(m => m.Id == a.ManualOverrideID)
                        .FirstOrDefault()
                        .ManualOverride,
                    FluidPhase = context
                        .FluidPhase.Where(f => f.Id == a.FluidPhaseID)
                        .FirstOrDefault()
                        .FluidPhase,
                    ToxicOrFlamableFluid = context
                        .ToxicOrFlamableFluid.Where(t => t.Id == a.ToxicOrFlamableFluidID)
                        .FirstOrDefault()
                        .ToxicOrFlamableFluid,
                    AssetName = a.AssetName,
                    CostOfReplacementAndRepair = a.CostOfReplacementAndRepair,
                    Status = a.Status,
                    UsageType = a.UsageType,
                    Actuation = a.Actuation,
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    DeletedBy = a.DeletedBy,
                    DeletedAt = a.DeletedAt,
                    CreatedByUser = context
                        .User.Where(u => u.Id == a.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == a.DeletedBy)
                        .FirstOrDefault()
                        .Username
                }
            ).ToList();
            asset = assetList[0];
        }
        return asset;
    }

    public List<AssetModel> GetAssetList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false
    )
    {
        List<AssetModel> assetList = new();
        using (var context = new AssetContext())
        {
            assetList = (
                from a in context.Asset
                join p in context.Platform on a.PlatformID equals p.Id
                // join ar in context.Area on p.AreaID equals ar.Id
                // join v in context.ValveType on a.ValveTypeID equals v.Id
                // join m in context.ManualOverride on a.ManualOverrideID equals m.Id
                // join f in context.FluidPhase on a.FluidPhaseID equals f.Id
                // join t in context.ToxicOrFlamableFluid on a.ToxicOrFlamableFluidID equals t.Id
                where
                    (AreaID == 0 || p.AreaID == AreaID)
                    && (PlatformID == 0 || a.PlatformID == PlatformID)
                    && (IncludeDeleted == true || a.IsDeleted == false)
                select new AssetModel
                {
                    Id = a.Id,
                    TagNo = a.TagNo,
                    PlatformID = a.PlatformID,
                    ValveTypeID = a.ValveTypeID,
                    Size = a.Size,
                    ClassRating = a.ClassRating,
                    ParentEquipmentNo = a.ParentEquipmentNo,
                    ParentEquipmentDescription = a.ParentEquipmentDescription,
                    InstallationDate = a.InstallationDate,
                    PIDNo = a.PIDNo,
                    Manufacturer = a.Manufacturer,
                    BodyModel = a.BodyModel,
                    BodyMaterial = a.BodyMaterial,
                    EndConnection = a.EndConnection,
                    SerialNo = a.SerialNo,
                    ManualOverrideID = a.ManualOverrideID,
                    ActuatorMfg = a.ActuatorMfg,
                    ActuatorSerialNo = a.ActuatorSerialNo,
                    ActuatorTypeModel = a.ActuatorTypeModel,
                    ActuatorPower = a.ActuatorPower,
                    OperatingTemperature = a.OperatingTemperature,
                    OperatingPressure = a.OperatingPressure,
                    FlowRate = a.FlowRate,
                    ServiceFluid = a.ServiceFluid,
                    FluidPhaseID = a.FluidPhaseID,
                    ToxicOrFlamableFluidID = a.ToxicOrFlamableFluidID,
                    Platform = context
                        .Platform.Where(p => p.Id == a.PlatformID)
                        .FirstOrDefault()
                        .Platform,
                    BusinessArea = context
                        .Area.Where(ar =>
                            ar.Id
                            == context
                                .Platform.Where(p => p.Id == a.PlatformID)
                                .FirstOrDefault()
                                .AreaID
                        )
                        .FirstOrDefault()
                        .BusinessArea,
                    ValveType = context
                        .ValveType.Where(v => v.Id == a.ValveTypeID)
                        .FirstOrDefault()
                        .ValveType,
                    ManualOverride = context
                        .ManualOverride.Where(m => m.Id == a.ManualOverrideID)
                        .FirstOrDefault()
                        .ManualOverride,
                    FluidPhase = context
                        .FluidPhase.Where(f => f.Id == a.FluidPhaseID)
                        .FirstOrDefault()
                        .FluidPhase,
                    ToxicOrFlamableFluid = context
                        .ToxicOrFlamableFluid.Where(t => t.Id == a.ToxicOrFlamableFluidID)
                        .FirstOrDefault()
                        .ToxicOrFlamableFluid,
                    AssetName = a.AssetName,
                    CostOfReplacementAndRepair = a.CostOfReplacementAndRepair,
                    Status = a.Status,
                    UsageType = a.UsageType,
                    Actuation = a.Actuation,
                    lastInspection = new InspectionModel().GetLastAssetInspection(a.Id),
                    lastMaintenance = new MaintenanceModel().GetLastAssetMaintenance(a.Id),
                    lastAssessment = new AssessmentModel().GetLastAssetAssessment(a.Id),
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    DeletedBy = a.DeletedBy,
                    DeletedAt = a.DeletedAt,
                    CreatedByUser = context
                        .User.Where(u => u.Id == a.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == a.DeletedBy)
                        .FirstOrDefault()
                        .Username
                }
            ).ToList();
        }
        return assetList;
    }

    public int AddAsset(AssetDB asset)
    {
        using (var context = new AssetContext())
        {
            asset.IsDeleted = false;
            if (asset.PlatformID == 0)
            {
                asset.PlatformID = null;
            }
            if (asset.ValveTypeID == 0)
            {
                asset.ValveTypeID = null;
            }
            if (asset.ManualOverrideID == 0)
            {
                asset.ManualOverrideID = null;
            }
            if (asset.FluidPhaseID == 0)
            {
                asset.FluidPhaseID = null;
            }
            if (asset.ToxicOrFlamableFluidID == 0)
            {
                asset.ToxicOrFlamableFluidID = null;
            }
            // Check if there is already an asset with the same tag number
            if (context.Asset.Any(a => a.TagNo == asset.TagNo))
            {
                throw new Exception(
                    "An asset with the tag number " + asset.TagNo + " already exists."
                );
            }
            context.Asset.Add(asset);
            context.SaveChanges();
            return asset.Id;
        }
    }

    public void UpdateAsset(AssetDB asset)
    {
        using (var context = new AssetContext())
        {
            AssetDB oldAsset = context.Asset.Find(asset.Id);
            oldAsset.IsDeleted = false;
            oldAsset.TagNo = asset.TagNo;
            oldAsset.PlatformID = asset.PlatformID;
            oldAsset.ValveTypeID = asset.ValveTypeID;
            oldAsset.Size = asset.Size;
            oldAsset.ClassRating = asset.ClassRating;
            oldAsset.ParentEquipmentNo = asset.ParentEquipmentNo;
            oldAsset.ParentEquipmentDescription = asset.ParentEquipmentDescription;
            oldAsset.InstallationDate = asset.InstallationDate;
            oldAsset.PIDNo = asset.PIDNo;
            oldAsset.Manufacturer = asset.Manufacturer;
            oldAsset.BodyModel = asset.BodyModel;
            oldAsset.BodyMaterial = asset.BodyMaterial;
            oldAsset.EndConnection = asset.EndConnection;
            oldAsset.SerialNo = asset.SerialNo;
            oldAsset.ManualOverrideID = asset.ManualOverrideID;
            oldAsset.ActuatorMfg = asset.ActuatorMfg;
            oldAsset.ActuatorSerialNo = asset.ActuatorSerialNo;
            oldAsset.ActuatorTypeModel = asset.ActuatorTypeModel;
            oldAsset.ActuatorPower = asset.ActuatorPower;
            oldAsset.OperatingTemperature = asset.OperatingTemperature;
            oldAsset.OperatingPressure = asset.OperatingPressure;
            oldAsset.FlowRate = asset.FlowRate;
            oldAsset.ServiceFluid = asset.ServiceFluid;
            oldAsset.FluidPhaseID = asset.FluidPhaseID;
            oldAsset.ToxicOrFlamableFluidID = asset.ToxicOrFlamableFluidID;
            oldAsset.AssetName = asset.AssetName;
            oldAsset.CostOfReplacementAndRepair = asset.CostOfReplacementAndRepair;
            oldAsset.Status = asset.Status;
            oldAsset.UsageType = asset.UsageType;
            oldAsset.Actuation = asset.Actuation;
            context.Asset.Update(oldAsset);
            context.SaveChanges();
        }
    }

    public void DeleteAsset(AssetDB asset)
    {
        using (var context = new AssetContext())
        {
            AssetDB oldAsset = context.Asset.Find(asset.Id);
            oldAsset.IsDeleted = true;
            oldAsset.DeletedBy = asset.DeletedBy;
            oldAsset.DeletedAt = asset.DeletedAt;
            context.Asset.Update(oldAsset);
            context.SaveChanges();
        }
    }

    public ToolImportModel MapAssetRegister(List<Dictionary<string, string>> datas)
    {
        ToolImportModel toolImport = new();
        List<string> failedRecords = new();
        List<PlatformModel> platformList = new PlatformModel().GetPlatformList(0, true);
        List<ValveTypeModel> valveTypeList = new ValveTypeModel().GetValveTypeList();
        List<ManualOverrideModel> manualOverrideModel =
            new ManualOverrideModel().GetManualOverrideList();
        List<FluidPhaseModel> fluidPhaseList = new FluidPhaseModel().GetFluidPhaseList();
        List<ToxicOrFlamableFluidModel> toxicOrFlamableFluidList =
            new ToxicOrFlamableFluidModel().GetToxicOrFlamableFluidList();
        List<Dictionary<string, string>> finalresult = new();
        foreach (var records in datas)
        {
            Dictionary<string, string> result = new();
            foreach (var record in records)
            {
                string key = record.Key;
                string value = record.Value.Trim().ToLower();
                string mappedKey = MapHeader(key);
                string mappedValue = "";
                if (mappedKey == "")
                {
                    continue;
                }
                if (
                    mappedKey.Equals("PlatformID")
                    || mappedKey.Equals("ValveTypeID")
                    || mappedKey.Equals("ManualOverrideID")
                    || mappedKey.Equals("FluidPhaseID")
                    || mappedKey.Equals("ToxicOrFlamableFluidID")
                )
                {
                    if (mappedKey.Equals("PlatformID"))
                    {
                        foreach (var platform in platformList)
                        {
                            if (platform.Platform.Trim().ToLower().Equals(value))
                            {
                                mappedValue = platform.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("ValveTypeID"))
                    {
                        foreach (var valveType in valveTypeList)
                        {
                            if (valveType.ValveType.Trim().ToLower().Equals(value))
                            {
                                mappedValue = valveType.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("ManualOverrideID"))
                    {
                        foreach (var manualOverride in manualOverrideModel)
                        {
                            if (manualOverride.ManualOverride.Trim().ToLower().Equals(value))
                            {
                                mappedValue = manualOverride.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("FluidPhaseID"))
                    {
                        foreach (var fluidPhase in fluidPhaseList)
                        {
                            if (fluidPhase.FluidPhase.Trim().ToLower().Equals(value))
                            {
                                mappedValue = fluidPhase.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("ToxicOrFlamableFluidID"))
                    {
                        foreach (var toxicOrFlamableFluid in toxicOrFlamableFluidList)
                        {
                            if (
                                toxicOrFlamableFluid
                                    .ToxicOrFlamableFluid.Trim()
                                    .ToLower()
                                    .Equals(value)
                            )
                            {
                                mappedValue = toxicOrFlamableFluid.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("InstallationDate"))
                    {
                        // mappedValue = DateTime
                        //     .Parse(value)
                        //     .ToString(Environment.GetDateFormatString(false));
                        if (value.Contains("/"))
                        {
                            string date = value.Split(" ")[0];
                            List<string> dateParts = date.Split("/").ToList();
                            if (dateParts[0].Length == 1)
                            {
                                dateParts[0] = "0" + dateParts[0];
                            }
                            if (dateParts[1].Length == 1)
                            {
                                dateParts[1] = "0" + dateParts[1];
                            }
                            string newDate = dateParts[0] + "-" + dateParts[1] + "-" + dateParts[2];
                            mappedValue = newDate;
                        }
                        else
                        {
                            mappedValue = DateTime
                                .FromOADate(Convert.ToDouble(value))
                                .ToString(Environment.GetDateFormatString(false));
                        }
                    }
                    if (mappedValue == "")
                    {
                        // Exception e =
                        //     new(
                        //         "Value '"
                        //             + record.Value
                        //             + "' on field '"
                        //             + key
                        //             + "' is not match with the database value"
                        //     );
                        // throw e;
                        failedRecords.Add(
                            "Value '"
                                + record.Value
                                + "' on field '"
                                + key
                                + "' is not match with the database value"
                        );
                    }
                    else
                    {
                        value = mappedValue;
                    }
                }
                result.Add(mappedKey, value);
            }
            finalresult.Add(result);
        }
        toolImport.mappedRecords = finalresult;
        toolImport.failedRecords = failedRecords;
        // return finalresult;
        return toolImport;
    }

    private string MapHeader(string name)
    {
        string result = "";
        switch (name.ToLower())
        {
            case "valve tag no.":
                result = "TagNo";
                break;
            case "equipment name":
                result = "AssetName";
                break;
            case "platform":
                result = "PlatformID";
                break;
            case "parent equipment no.":
                result = "ParentEquipmentNo";
                break;
            case "parent equipment \ndescription":
                result = "ParentEquipmentDescription";
                break;
            case "installation date\n(dd/mm/yyyy)":
                result = "InstallationDate";
                break;
            case "pid. no.":
                result = "PIDNo";
                break;
            case "valve type":
                result = "ValveTypeID";
                break;
            case "manufacturer":
                result = "Manufacturer";
                break;
            case "body material":
                result = "BodyMaterial";
                break;
            case "body model":
                result = "BodyModel";
                break;
            case "end connection":
                result = "EndConnection";
                break;
            case "serial number":
                result = "SerialNo";
                break;
            case "usage type":
                result = "UsageType";
                break;
            case "size":
                result = "Size";
                break;
            case "class/rating":
                result = "ClassRating";
                break;
            case "service fluid":
                result = "ServiceFluid";
                break;
            case "fluid phase":
                result = "FluidPhaseID";
                break;
            case "flow rate\n(m3/hr)":
                result = "FlowRate";
                break;
            case "operating temperature\n( °f)":
                result = "OperatingTemperature";
                break;
            case "operating pressure\n(psig)":
                result = "OperatingPressure";
                break;
            case "toxic or flamable fluid?\n(y/n)":
                result = "ToxicOrFlamableFluidID";
                break;
            case "cost of replacement and repair (usd)":
                result = "CostOfReplacementAndRepair";
                break;
            case "actuation":
                result = "Actuation";
                break;
            case "actuator mfg.":
                result = "ActuatorMfg";
                break;
            case "actuator serial no.":
                result = "ActuatorSerialNo";
                break;
            case "actuator type/model":
                result = "ActuatorTypeModel";
                break;
            case "actuator power":
                result = "ActuatorPower";
                break;
            case "manual override":
                result = "ManualOverrideID";
                break;
            case "status":
                result = "Status";
                break;
        }
        return result;
    }
}

public class ValveTypeModel
{
    public int Id { get; set; }
    public string? ValveType { get; set; }

    public List<ValveTypeModel> GetValveTypeList()
    {
        List<ValveTypeModel> valveTypeList = new();
        using (var context = new AssetContext())
        {
            valveTypeList = context.ValveType.ToList();
        }
        return valveTypeList;
    }
}

public class ManualOverrideModel
{
    public int Id { get; set; }
    public string? ManualOverride { get; set; }

    public List<ManualOverrideModel> GetManualOverrideList()
    {
        List<ManualOverrideModel> manualOverrideList = new();
        using (var context = new AssetContext())
        {
            manualOverrideList = context.ManualOverride.ToList();
        }
        return manualOverrideList;
    }
}

public class FluidPhaseModel
{
    public int Id { get; set; }
    public string? FluidPhase { get; set; }

    public List<FluidPhaseModel> GetFluidPhaseList()
    {
        List<FluidPhaseModel> fluidPhaseList = new();
        using (var context = new AssetContext())
        {
            fluidPhaseList = context.FluidPhase.ToList();
        }
        return fluidPhaseList;
    }
}

public class ToxicOrFlamableFluidModel
{
    public int Id { get; set; }
    public string? ToxicOrFlamableFluid { get; set; }

    public List<ToxicOrFlamableFluidModel> GetToxicOrFlamableFluidList()
    {
        List<ToxicOrFlamableFluidModel> toxicOrFlamableFluidList = new();
        using (var context = new AssetContext())
        {
            toxicOrFlamableFluidList = context.ToxicOrFlamableFluid.ToList();
        }
        return toxicOrFlamableFluidList;
    }
}

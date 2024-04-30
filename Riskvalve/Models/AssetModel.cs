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
                string value = record.Value.Trim();
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
                            if (platform.Platform.Trim().Equals(value))
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
                            if (valveType.ValveType.Trim().Equals(value))
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
                            if (manualOverride.ManualOverride.Trim().Equals(value))
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
                            if (fluidPhase.FluidPhase.Trim().Equals(value))
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
                            if (toxicOrFlamableFluid.ToxicOrFlamableFluid.Trim().Equals(value))
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
                            string newDate = dateParts[1] + "-" + dateParts[0] + "-" + dateParts[2];
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
        switch (name)
        {
            case "Valve Tag No.":
                result = "TagNo";
                break;
            case "Equipment Name":
                result = "AssetName";
                break;
            case "Platform":
                result = "PlatformID";
                break;
            case "Parent Equipment No.":
                result = "ParentEquipmentNo";
                break;
            case "Parent Equipment \nDescription":
                result = "ParentEquipmentDescription";
                break;
            case "Installation Date\n(dd/mm/yyyy)":
                result = "InstallationDate";
                break;
            case "PID. No.":
                result = "PIDNo";
                break;
            case "Valve Type":
                result = "ValveTypeID";
                break;
            case "Manufacturer":
                result = "Manufacturer";
                break;
            case "Body Material":
                result = "BodyMaterial";
                break;
            case "Body Model":
                result = "BodyModel";
                break;
            case "End Connection":
                result = "EndConnection";
                break;
            case "Serial Number":
                result = "SerialNo";
                break;
            case "Usage Type":
                result = "UsageType";
                break;
            case "Size":
                result = "Size";
                break;
            case "Class/Rating":
                result = "ClassRating";
                break;
            case "Service Fluid":
                result = "ServiceFluid";
                break;
            case "Fluid Phase":
                result = "FluidPhaseID";
                break;
            case "Flow Rate\n(m3/hr)":
                result = "FlowRate";
                break;
            case "Operating Temperature\n( °F)":
                result = "OperatingTemperature";
                break;
            case "Operating Pressure\n(psig)":
                result = "OperatingPressure";
                break;
            case "Toxic or Flamable Fluid?\n(Y/N)":
                result = "ToxicOrFlamableFluidID";
                break;
            case "Cost of Replacement and repair (USD)":
                result = "CostOfReplacementAndRepair";
                break;
            case "Actuation":
                result = "Actuation";
                break;
            case "Actuator Mfg.":
                result = "ActuatorMfg";
                break;
            case "Actuator Serial No.":
                result = "ActuatorSerialNo";
                break;
            case "Actuator Type/Model":
                result = "ActuatorTypeModel";
                break;
            case "Actuator Power":
                result = "ActuatorPower";
                break;
            case "Manual Override":
                result = "ManualOverrideID";
                break;
            case "Status":
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

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
    public int? PlatformID { get; set; } // FK
    public int ValveTypeID { get; set; } // FK
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
    public int ManualOverrideID { get; set; } // FK
    public string? ActuatorMfg { get; set; }
    public string? ActuatorSerialNo { get; set; }
    public string? ActuatorTypeModel { get; set; }
    public string? ActuatorPower { get; set; }
    public string? OperatingTemperature { get; set; }
    public string? OperatingPressure { get; set; }
    public string? FlowRate { get; set; }
    public string? ServiceFluid { get; set; }
    public int FluidPhaseID { get; set; } // FK
    public int ToxicOrFlamableFluidID { get; set; } // FK
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

    public AssetModel GetAssetModel(int id)
    {
        AssetModel asset = new();
        using (var context = new AssetContext())
        {
            List<AssetModel> assetList = (
                from a in context.Asset
                join p in context.Platform on a.PlatformID equals p.Id
                join ar in context.Area on p.AreaID equals ar.Id
                join v in context.ValveType on a.ValveTypeID equals v.Id
                join m in context.ManualOverride on a.ManualOverrideID equals m.Id
                join f in context.FluidPhase on a.FluidPhaseID equals f.Id
                join t in context.ToxicOrFlamableFluid on a.ToxicOrFlamableFluidID equals t.Id
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
                    Platform = p!.Platform,
                    BusinessArea = ar!.BusinessArea,
                    ValveType = v!.ValveType,
                    ManualOverride = m!.ManualOverride,
                    FluidPhase = f!.FluidPhase,
                    ToxicOrFlamableFluid = t!.ToxicOrFlamableFluid,
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    DeletedBy = a.DeletedBy,
                    DeletedAt = a.DeletedAt,
                    CreatedByUser = context.User.Where(u => u.Id == a.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == a.DeletedBy).FirstOrDefault().Username
                }
            ).ToList();
            asset = assetList[0];
        }
        return asset;
    }

    public List<AssetModel> GetAssetList(int AreaID = 0, int PlatformID = 0)
    {
        List<AssetModel> assetList = new();
        using (var context = new AssetContext())
        {
            assetList = (
                from a in context.Asset
                join p in context.Platform on a.PlatformID equals p.Id
                join ar in context.Area on p.AreaID equals ar.Id
                join v in context.ValveType on a.ValveTypeID equals v.Id
                join m in context.ManualOverride on a.ManualOverrideID equals m.Id
                join f in context.FluidPhase on a.FluidPhaseID equals f.Id
                join t in context.ToxicOrFlamableFluid on a.ToxicOrFlamableFluidID equals t.Id
                where (AreaID == 0 || p.AreaID == AreaID) && (PlatformID == 0 || p.Id == PlatformID)
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
                    Platform = p!.Platform,
                    BusinessArea = ar!.BusinessArea,
                    ValveType = v!.ValveType,
                    ManualOverride = m!.ManualOverride,
                    FluidPhase = f!.FluidPhase,
                    ToxicOrFlamableFluid = t!.ToxicOrFlamableFluid,
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    DeletedBy = a.DeletedBy,
                    DeletedAt = a.DeletedAt,
                    CreatedByUser = context.User.Where(u => u.Id == a.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == a.DeletedBy).FirstOrDefault().Username
                }
            ).ToList();
        }
        return assetList;
    }

    public void AddAsset(AssetDB asset)
    {
        using (var context = new AssetContext())
        {
            asset.IsDeleted = false;
            context.Asset.Add(asset);
            context.SaveChanges();
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
            oldAsset.CreatedBy = asset.CreatedBy;
            oldAsset.CreatedAt = asset.CreatedAt;
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

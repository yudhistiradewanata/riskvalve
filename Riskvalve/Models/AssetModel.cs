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

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(
                "Server=127.0.0.1,1433;Database=Riskvalve;User Id=SA;Password=DB_Password;Encrypt=False;Connection Timeout=30;"
            )
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
}

public class AssetModel : AssetDB
{
    public string? Platform { get; set; }
    public string? BusinessArea { get; set; }
    public string? ValveType { get; set; }
    public string? ManualOverride { get; set; }
    public string? FluidPhase { get; set; }
    public string? ToxicOrFlamableFluid { get; set; }

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
                    ToxicOrFlamableFluid = t!.ToxicOrFlamableFluid
                }
            ).ToList();
            asset = assetList[0];
        }
        return asset;
    }

    public List<AssetModel> GetAssetList()
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
                    ToxicOrFlamableFluid = t!.ToxicOrFlamableFluid
                }
            ).ToList();
        }
        return assetList;
    }

    public void AddAsset(AssetDB asset)
    {
        using (var context = new AssetContext())
        {
            context.Asset.Add(asset);
            context.SaveChanges();
        }
    }

    public void UpdateAsset(AssetDB asset)
    {
        using (var context = new AssetContext())
        {
            context.Asset.Update(asset);
            context.SaveChanges();
        }
    }

    public void DeleteAsset(int id)
    {
        using (var context = new AssetContext())
        {
            AssetDB asset = context.Asset.Find(id);
            context.Asset.Remove(asset);
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

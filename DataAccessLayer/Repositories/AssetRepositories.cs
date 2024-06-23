using SharedLayer;

namespace DataAccessLayer;

public interface IAssetRepository
{
    AssetData GetAsset(int id);
    List<AssetData> GetAssetList(int AreaID = 0, int PlatformId = 0, bool IncludeDeleted = false);
    AssetData AddAsset(AssetClass asset);
    AssetData UpdateAsset(AssetClass asset);
    AssetData DeleteAsset(AssetClass asset);
}

public class AssetRepository(ApplicationDbContext context) : IAssetRepository
{
    private readonly ApplicationDbContext _context = context;

    public AssetData GetAsset(int id)
    {
        AssetData? assetdata;
        var result =
            from asset in _context.Asset
            join platform in _context.Platform on asset.PlatformID equals platform.Id into pc
            from subplatform in pc.DefaultIfEmpty()
            join area in _context.Area on subplatform.AreaID equals area.Id into ac
            from subarea in ac.DefaultIfEmpty()
            join valvetype in _context.ValveType on asset.ValveTypeID equals valvetype.Id into vc
            from subvalvetype in vc.DefaultIfEmpty()
            join manualoverride in _context.ManualOverride
                on asset.ManualOverrideID equals manualoverride.Id
                into moc
            from submanualoverride in moc.DefaultIfEmpty()
            join fluidphase in _context.FluidPhase
                on asset.FluidPhaseID equals fluidphase.Id
                into fc
            from subfluidphase in fc.DefaultIfEmpty()
            join toxicorflamablefluid in _context.ToxicOrFlamableFluid
                on asset.ToxicOrFlamableFluidID equals toxicorflamablefluid.Id
                into tfc
            from subtoxicorflamablefluid in tfc.DefaultIfEmpty()
            join createby in _context.User on asset.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on asset.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where asset.Id == id
            select new AssetData
            {
                Id = asset.Id,
                TagNo = asset.TagNo,
                AssetName = asset.AssetName,
                PlatformID = asset.PlatformID,
                Platform = subplatform.Platform,
                BusinessArea = subarea.BusinessArea,
                ValveTypeID = asset.ValveTypeID,
                ValveType = subvalvetype.ValveType,
                Size = asset.Size,
                ClassRating = asset.ClassRating,
                ParentEquipmentNo = asset.ParentEquipmentNo,
                ParentEquipmentDescription = asset.ParentEquipmentDescription,
                InstallationDate = asset.InstallationDate,
                PIDNo = asset.PIDNo,
                Manufacturer = asset.Manufacturer,
                BodyModel = asset.BodyModel,
                BodyMaterial = asset.BodyMaterial,
                EndConnection = asset.EndConnection,
                SerialNo = asset.SerialNo,
                ManualOverrideID = asset.ManualOverrideID,
                ManualOverride = submanualoverride.ManualOverride,
                ActuatorMfg = asset.ActuatorMfg,
                ActuatorSerialNo = asset.ActuatorSerialNo,
                ActuatorTypeModel = asset.ActuatorTypeModel,
                ActuatorPower = asset.ActuatorPower,
                OperatingTemperature = asset.OperatingTemperature,
                OperatingPressure = asset.OperatingPressure,
                FlowRate = asset.FlowRate,
                ServiceFluid = asset.ServiceFluid,
                FluidPhaseID = asset.FluidPhaseID,
                FluidPhase = subfluidphase.FluidPhase,
                ToxicOrFlamableFluidID = asset.ToxicOrFlamableFluidID,
                ToxicOrFlamableFluid = subtoxicorflamablefluid.ToxicOrFlamableFluid,
                UsageType = asset.UsageType,
                CostOfReplacementAndRepair = asset.CostOfReplacementAndRepair,
                Status = asset.Status,
                Actuation = asset.Actuation,
                IsDeleted = asset.IsDeleted,
                CreatedBy = asset.CreatedBy,
                CreatedAt = asset.CreatedAt,
                DeletedBy = asset.DeletedBy,
                DeletedAt = asset.DeletedAt,
                CreatedByUser = subcreateby.Username ?? "",
                DeletedByUser = subdeleteby.Username ?? ""
            };
        assetdata = result.FirstOrDefault();
        if (assetdata == null)
        {
            throw new Exception("Asset not found");
        }
        return assetdata;
    }

    public List<AssetData> GetAssetList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false
    )
    {
        List<AssetData> assetlist;
        var result =
            from asset in _context.Asset
            join platform in _context.Platform on asset.PlatformID equals platform.Id into pc
            from subplatform in pc.DefaultIfEmpty()
            join area in _context.Area on subplatform.AreaID equals area.Id into ac
            from subarea in ac.DefaultIfEmpty()
            join valvetype in _context.ValveType on asset.ValveTypeID equals valvetype.Id into vc
            from subvalvetype in vc.DefaultIfEmpty()
            join manualoverride in _context.ManualOverride
                on asset.ManualOverrideID equals manualoverride.Id
                into moc
            from submanualoverride in moc.DefaultIfEmpty()
            join fluidphase in _context.FluidPhase
                on asset.FluidPhaseID equals fluidphase.Id
                into fc
            from subfluidphase in fc.DefaultIfEmpty()
            join toxicorflamablefluid in _context.ToxicOrFlamableFluid
                on asset.ToxicOrFlamableFluidID equals toxicorflamablefluid.Id
                into tfc
            from subtoxicorflamablefluid in tfc.DefaultIfEmpty()
            join createby in _context.User on asset.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on asset.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where
                (AreaID == 0 || subplatform.AreaID == AreaID)
                && (PlatformID == 0 || asset.PlatformID == PlatformID)
                && (IncludeDeleted == true || asset.IsDeleted == false)
            select new AssetData
            {
                Id = asset.Id,
                TagNo = asset.TagNo,
                AssetName = asset.AssetName,
                PlatformID = asset.PlatformID,
                Platform = subplatform.Platform,
                BusinessArea = subarea.BusinessArea,
                ValveTypeID = asset.ValveTypeID,
                ValveType = subvalvetype.ValveType,
                Size = asset.Size,
                ClassRating = asset.ClassRating,
                ParentEquipmentNo = asset.ParentEquipmentNo,
                ParentEquipmentDescription = asset.ParentEquipmentDescription,
                InstallationDate = asset.InstallationDate,
                PIDNo = asset.PIDNo,
                Manufacturer = asset.Manufacturer,
                BodyModel = asset.BodyModel,
                BodyMaterial = asset.BodyMaterial,
                EndConnection = asset.EndConnection,
                SerialNo = asset.SerialNo,
                ManualOverrideID = asset.ManualOverrideID,
                ManualOverride = submanualoverride.ManualOverride,
                ActuatorMfg = asset.ActuatorMfg,
                ActuatorSerialNo = asset.ActuatorSerialNo,
                ActuatorTypeModel = asset.ActuatorTypeModel,
                ActuatorPower = asset.ActuatorPower,
                OperatingTemperature = asset.OperatingTemperature,
                OperatingPressure = asset.OperatingPressure,
                FlowRate = asset.FlowRate,
                ServiceFluid = asset.ServiceFluid,
                FluidPhaseID = asset.FluidPhaseID,
                FluidPhase = subfluidphase.FluidPhase,
                ToxicOrFlamableFluidID = asset.ToxicOrFlamableFluidID,
                ToxicOrFlamableFluid = subtoxicorflamablefluid.ToxicOrFlamableFluid,
                UsageType = asset.UsageType,
                CostOfReplacementAndRepair = asset.CostOfReplacementAndRepair,
                Status = asset.Status,
                Actuation = asset.Actuation,
                IsDeleted = asset.IsDeleted,
                CreatedBy = asset.CreatedBy,
                CreatedAt = asset.CreatedAt,
                DeletedBy = asset.DeletedBy,
                DeletedAt = asset.DeletedAt,
                CreatedByUser = subcreateby.Username ?? "",
                DeletedByUser = subdeleteby.Username ?? ""
            };
        assetlist = [.. result];
        return assetlist;
    }

    public AssetData AddAsset(AssetClass asset)
    {
        AssetClass? searchasset = _context
            .Asset.Where(a => a.TagNo == asset.TagNo && a.IsDeleted == false)
            .FirstOrDefault();
        if (searchasset != null)
        {
            throw new Exception("Asset with Tag No " + asset.TagNo + " already exists.");
        }
        _context.Asset.Add(asset);
        _context.SaveChanges();
        return GetAsset(asset.Id);
    }

    public AssetData UpdateAsset(AssetClass asset)
    {
        AssetClass? oldAsset = _context
            .Asset.Where(a => a.Id == asset.Id && a.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("Asset not found");
        AssetClass? searchasset = _context
            .Asset.Where(a => a.TagNo == asset.TagNo && a.IsDeleted == false && a.Id != asset.Id)
            .FirstOrDefault();
        if (searchasset != null)
        {
            throw new Exception("Asset with Tag No " + asset.TagNo + " already exists.");
        }
        oldAsset.TagNo = asset.TagNo;
        oldAsset.AssetName = asset.AssetName;
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
        oldAsset.UsageType = asset.UsageType;
        oldAsset.CostOfReplacementAndRepair = asset.CostOfReplacementAndRepair;
        oldAsset.Actuation = asset.Actuation;
        oldAsset.Status = asset.Status;
        _context.Asset.Update(oldAsset);
        _context.SaveChanges();
        return GetAsset(asset.Id);
    }

    public AssetData DeleteAsset(AssetClass asset)
    {
        AssetClass searchasset = _context
            .Asset.Where(a => a.Id == asset.Id && a.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("Asset not found");
        searchasset.IsDeleted = true;
        searchasset.DeletedBy = asset.DeletedBy;
        searchasset.DeletedAt = asset.DeletedAt;
        _context.Asset.Update(searchasset);
        _context.SaveChanges();
        return GetAsset(asset.Id);
    }
}

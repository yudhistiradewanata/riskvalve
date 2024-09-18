using System.Globalization;
using System.Web;
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
            join updateby in _context.User on asset.UpdatedBy equals updateby.Id into uc
            from subupdateby in uc.DefaultIfEmpty()
            where asset.Id == id
            select new AssetData
            {
                Id = asset.Id,
                TagNo = SharedEnvironment.HtmlEncode(asset.TagNo),
                AssetName = SharedEnvironment.HtmlEncode(asset.AssetName),
                PlatformID = asset.PlatformID,
                Platform = SharedEnvironment.HtmlEncode(subplatform.Platform),
                BusinessArea = SharedEnvironment.HtmlEncode(subarea.BusinessArea),
                ValveTypeID = asset.ValveTypeID,
                ValveType = SharedEnvironment.HtmlEncode(subvalvetype.ValveType),
                Size = SharedEnvironment.HtmlEncode(asset.Size),
                ClassRating = SharedEnvironment.HtmlEncode(asset.ClassRating),
                ParentEquipmentNo = SharedEnvironment.HtmlEncode(asset.ParentEquipmentNo),
                ParentEquipmentDescription = SharedEnvironment.HtmlEncode(asset.ParentEquipmentDescription),
                InstallationDate = asset.InstallationDate,
                PIDNo = SharedEnvironment.HtmlEncode(asset.PIDNo),
                Manufacturer = SharedEnvironment.HtmlEncode(asset.Manufacturer),
                BodyModel = SharedEnvironment.HtmlEncode(asset.BodyModel),
                BodyMaterial = SharedEnvironment.HtmlEncode(asset.BodyMaterial),
                EndConnection = SharedEnvironment.HtmlEncode(asset.EndConnection),
                SerialNo = SharedEnvironment.HtmlEncode(asset.SerialNo),
                ManualOverrideID = asset.ManualOverrideID,
                ManualOverride = SharedEnvironment.HtmlEncode(submanualoverride.ManualOverride),
                ActuatorMfg = SharedEnvironment.HtmlEncode(asset.ActuatorMfg),
                ActuatorSerialNo = SharedEnvironment.HtmlEncode(asset.ActuatorSerialNo),
                ActuatorTypeModel = SharedEnvironment.HtmlEncode(asset.ActuatorTypeModel),
                ActuatorPower = SharedEnvironment.HtmlEncode(asset.ActuatorPower),
                OperatingTemperature = SharedEnvironment.HtmlEncode(asset.OperatingTemperature),
                OperatingPressure = SharedEnvironment.HtmlEncode(asset.OperatingPressure),
                FlowRate = SharedEnvironment.HtmlEncode(asset.FlowRate),
                ServiceFluid = SharedEnvironment.HtmlEncode(asset.ServiceFluid),
                FluidPhaseID = asset.FluidPhaseID,
                FluidPhase = SharedEnvironment.HtmlEncode(subfluidphase.FluidPhase),
                ToxicOrFlamableFluidID = asset.ToxicOrFlamableFluidID,
                ToxicOrFlamableFluid = SharedEnvironment.HtmlEncode(subtoxicorflamablefluid.ToxicOrFlamableFluid),
                UsageType = SharedEnvironment.HtmlEncode(asset.UsageType),
                CostOfReplacementAndRepair = SharedEnvironment.HtmlEncode(asset.CostOfReplacementAndRepair),
                Status = SharedEnvironment.HtmlEncode(asset.Status),
                Actuation = SharedEnvironment.HtmlEncode(asset.Actuation),
                IsDeleted = asset.IsDeleted,
                CreatedBy = asset.CreatedBy,
                CreatedAt = asset.CreatedAt,
                DeletedBy = asset.DeletedBy,
                DeletedAt = asset.DeletedAt,
                UpdatedBy = asset.UpdatedBy,
                UpdatedAt = asset.UpdatedAt,
                CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? ""),
                UpdatedByUser = SharedEnvironment.HtmlEncode(subupdateby.Username ?? "")
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
            join updateby in _context.User on asset.UpdatedBy equals updateby.Id into uc
            from subupdateby in uc.DefaultIfEmpty()
            where
                (AreaID == 0 || subplatform.AreaID == AreaID)
                && (PlatformID == 0 || asset.PlatformID == PlatformID)
                && (IncludeDeleted == true || asset.IsDeleted == false)
            select new AssetData
            {
                Id = asset.Id,
                TagNo = SharedEnvironment.HtmlEncode(asset.TagNo),
                AssetName = SharedEnvironment.HtmlEncode(asset.AssetName),
                PlatformID = asset.PlatformID,
                Platform = SharedEnvironment.HtmlEncode(subplatform.Platform),
                BusinessArea = SharedEnvironment.HtmlEncode(subarea.BusinessArea),
                ValveTypeID = asset.ValveTypeID,
                ValveType = SharedEnvironment.HtmlEncode(subvalvetype.ValveType),
                Size = SharedEnvironment.HtmlEncode(asset.Size),
                ClassRating = SharedEnvironment.HtmlEncode(asset.ClassRating),
                ParentEquipmentNo = SharedEnvironment.HtmlEncode(asset.ParentEquipmentNo),
                ParentEquipmentDescription = SharedEnvironment.HtmlEncode(asset.ParentEquipmentDescription),
                InstallationDate = asset.InstallationDate,
                PIDNo = SharedEnvironment.HtmlEncode(asset.PIDNo),
                Manufacturer = SharedEnvironment.HtmlEncode(asset.Manufacturer),
                BodyModel = SharedEnvironment.HtmlEncode(asset.BodyModel),
                BodyMaterial = SharedEnvironment.HtmlEncode(asset.BodyMaterial),
                EndConnection = SharedEnvironment.HtmlEncode(asset.EndConnection),
                SerialNo = SharedEnvironment.HtmlEncode(asset.SerialNo),
                ManualOverrideID = asset.ManualOverrideID,
                ManualOverride = SharedEnvironment.HtmlEncode(submanualoverride.ManualOverride),
                ActuatorMfg = SharedEnvironment.HtmlEncode(asset.ActuatorMfg),
                ActuatorSerialNo = SharedEnvironment.HtmlEncode(asset.ActuatorSerialNo),
                ActuatorTypeModel = SharedEnvironment.HtmlEncode(asset.ActuatorTypeModel),
                ActuatorPower = SharedEnvironment.HtmlEncode(asset.ActuatorPower),
                OperatingTemperature = SharedEnvironment.HtmlEncode(asset.OperatingTemperature),
                OperatingPressure = SharedEnvironment.HtmlEncode(asset.OperatingPressure),
                FlowRate = SharedEnvironment.HtmlEncode(asset.FlowRate),
                ServiceFluid = SharedEnvironment.HtmlEncode(asset.ServiceFluid),
                FluidPhaseID = asset.FluidPhaseID,
                FluidPhase = SharedEnvironment.HtmlEncode(subfluidphase.FluidPhase),
                ToxicOrFlamableFluidID = asset.ToxicOrFlamableFluidID,
                ToxicOrFlamableFluid = SharedEnvironment.HtmlEncode(subtoxicorflamablefluid.ToxicOrFlamableFluid),
                UsageType = SharedEnvironment.HtmlEncode(asset.UsageType),
                CostOfReplacementAndRepair = SharedEnvironment.HtmlEncode(asset.CostOfReplacementAndRepair),
                Status = SharedEnvironment.HtmlEncode(asset.Status),
                Actuation = SharedEnvironment.HtmlEncode(asset.Actuation),
                IsDeleted = asset.IsDeleted,
                CreatedBy = asset.CreatedBy,
                CreatedAt = asset.CreatedAt,
                DeletedBy = asset.DeletedBy,
                DeletedAt = asset.DeletedAt,
                UpdatedBy = asset.UpdatedBy,
                UpdatedAt = asset.UpdatedAt,
                CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? ""),
                UpdatedByUser = SharedEnvironment.HtmlEncode(subupdateby.Username ?? "")
            };
        assetlist = [.. result];
        return assetlist;
    }

    public AssetData AddAsset(AssetClass asset)
    {
        lock(this){
            if (
                !DateTime.TryParseExact(
                    asset.InstallationDate,
                    SharedEnvironment.GetDateFormatString(false),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _
                )
            )
            {
                throw new FormatException("Installation Date is not in the correct format (dd-MM-yyyy)");
            }
            AssetClass? searchasset = _context
                .Asset.Where(a => a.TagNo == asset.TagNo && a.IsDeleted == false)
                .FirstOrDefault();
            if (searchasset != null)
            {
                throw new Exception("Asset with Tag No " + asset.TagNo + " already exists.");
            }
            asset.UpdatedAt = asset.CreatedAt;
            asset.UpdatedBy = asset.CreatedBy;
            _context.Asset.Add(asset);
            _context.SaveChanges();
            return GetAsset(asset.Id);
        }
    }

    public AssetData UpdateAsset(AssetClass asset)
    {
        if (
            !DateTime.TryParseExact(
                asset.InstallationDate,
                SharedEnvironment.GetDateFormatString(false),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
        {
            throw new FormatException("Installation Date is not in the correct format (dd-MM-yyyy)");
        }
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
        oldAsset.UpdatedBy = asset.UpdatedBy;
        oldAsset.UpdatedAt = asset.UpdatedAt;
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
        searchasset.UpdatedBy = asset.DeletedBy;
        searchasset.UpdatedAt = asset.DeletedAt;
        _context.Asset.Update(searchasset);
        _context.SaveChanges();
        return GetAsset(asset.Id);
    }
}

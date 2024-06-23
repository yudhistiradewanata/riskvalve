using DataAccessLayer;
using Newtonsoft.Json;
using SharedLayer;

namespace BusinessLogicLayer;

public interface IAssetService
{
    AssetData GetAsset(int id);
    List<AssetData> GetAssetList(int AreaID = 0, int PlatformID = 0, bool IncludeDeleted = false);
    AssetData AddAsset(AssetClass asset);
    AssetData UpdateAsset(AssetClass asset);
    AssetData DeleteAsset(AssetClass asset);
    List<ValveTypeData> GetValveTypeList();
    List<ManualOverrideData> GetManualOverrideList();
    List<FluidPhaseData> GetFluidPhaseList();
    List<ToxicOrFlamableFluidData> GetToxicOrFlamableFluidList();
    Dictionary<string, string> ImportAsset(List<Dictionary<string, string>> data, int CreatedBy);
    Dictionary<string, int> GetAssetDistribution();
}

public class AssetService(
    IAssetRepository assetRepository,
    // IInspectionRepository inspectionRepository,
    // IMaintenanceRepository maintenanceRepository,
    // IAssessmentRepository assessmentRepository,
    IPlatformRepository platformRepository,
    IValveTypeRepository valveTypeRepository,
    IManualOverrideRepository manualOverrideRepository,
    IFluidPhaseRepository fluidPhaseRepository,
    IToxicOrFlamableFluidRepository toxicOrFlamableFluidRepository,
    ILogRepository logRepository
) : IAssetService
{
    private readonly IAssetRepository _assetRepository = assetRepository;
    // private readonly IInspectionRepository _inspectionRepository = inspectionRepository;
    // private readonly IMaintenanceRepository _maintenanceRepository = maintenanceRepository;
    // private readonly IAssessmentRepository _assessmentRepository = assessmentRepository;
    private readonly IPlatformRepository _platformRepository = platformRepository;
    private readonly IValveTypeRepository _valveTypeRepository = valveTypeRepository;
    private readonly IManualOverrideRepository _manualOverrideRepository = manualOverrideRepository;
    private readonly IFluidPhaseRepository _fluidPhaseRepository = fluidPhaseRepository;
    private readonly IToxicOrFlamableFluidRepository _toxicOrFlamableFluidRepository =
        toxicOrFlamableFluidRepository;
    private readonly ILogRepository _logRepository = logRepository;

    public AssetData GetAsset(int id)
    {
        try
        {
            AssetData assetdata = _assetRepository.GetAsset(id);
            assetdata.PlatformData = _platformRepository.GetPlatform(assetdata.PlatformID ?? 0);
            // assetdata.LastInspection = _inspectionRepository.GetLastInspection(assetdata.Id);
            // assetdata.LastMaintenance = _maintenanceRepository.GetLastMaintenance(assetdata.Id);
            // assetdata.LastAssessment = _assessmentRepository.GetLastAssessment(assetdata.Id);
            return assetdata;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<AssetData> GetAssetList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false
    )
    {
        List<AssetData> assetDatas = _assetRepository.GetAssetList(AreaID, PlatformID, IncludeDeleted);
        foreach(var assetData in assetDatas)
        {
            // assetData.PlatformData = _platformRepository.GetPlatform(assetData.PlatformID ?? 0);
            // assetData.LastInspection = _inspectionRepository.GetLastInspection(assetData.Id);
            // assetData.LastMaintenance = _maintenanceRepository.GetLastMaintenance(assetData.Id);
            // assetData.LastAssessment = _assessmentRepository.GetLastAssessment(assetData.Id);
        }
        return assetDatas;
    }

    public AssetData AddAsset(AssetClass asset)
    {
        try
        {
            if (asset.TagNo == null || asset.TagNo == "")
            {
                throw new Exception("Tag No is required.");
            }
            return _assetRepository.AddAsset(asset);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public AssetData UpdateAsset(AssetClass asset)
    {
        try
        {
            if (asset.TagNo == null || asset.TagNo == "")
            {
                throw new Exception("Tag No is required.");
            }
            return _assetRepository.UpdateAsset(asset);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public AssetData DeleteAsset(AssetClass asset)
    {
        try
        {
            return _assetRepository.DeleteAsset(asset);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private ToolImportClass MapAssetRegister(List<Dictionary<string, string>> datas)
    {
        ToolImportClass toolImport = new();
        List<string> failedRecords = [];
        List<PlatformData> platformList = _platformRepository.GetPlatformList();
        List<ValveTypeData> valveTypeList = _valveTypeRepository.GetValveTypeList();
        List<ManualOverrideData> manualOverrideList =
            _manualOverrideRepository.GetManualOverrideList();
        List<FluidPhaseData> fluidPhaseList = _fluidPhaseRepository.GetFluidPhaseList();
        List<ToxicOrFlamableFluidData> toxicOrFlamableFluidList =
            _toxicOrFlamableFluidRepository.GetToxicOrFlamableFluidList();
        List<Dictionary<string, string>> finalresult = [];
        foreach (var records in datas)
        {
            Dictionary<string, string> result = [];
            foreach (var record in records)
            {
                string key = record.Key;
                string value = record.Value.Trim().ToLower();
                string valuereal = record.Value.Trim();
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
                            if (platform.Platform == null)
                            {
                                continue;
                            }
                            if (platform.Platform.ToLower().Equals(value))
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
                            if (valveType.ValveType == null)
                            {
                                continue;
                            }
                            if (valveType.ValveType.ToLower().Equals(value))
                            {
                                mappedValue = valveType.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("ManualOverrideID"))
                    {
                        foreach (var manualOverride in manualOverrideList)
                        {
                            if (manualOverride.ManualOverride == null)
                            {
                                continue;
                            }
                            if (manualOverride.ManualOverride.ToLower().Equals(value))
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
                            if (fluidPhase.FluidPhase == null)
                            {
                                continue;
                            }
                            if (fluidPhase.FluidPhase.ToLower().Equals(value))
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
                            if (toxicOrFlamableFluid.ToxicOrFlamableFluid == null)
                            {
                                continue;
                            }
                            if (toxicOrFlamableFluid.ToxicOrFlamableFluid.ToLower().Equals(value))
                            {
                                mappedValue = toxicOrFlamableFluid.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedValue.Equals("InstallationDate"))
                    {
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
                                .ToString(SharedEnvironment.GetDateFormatString(false));
                        }
                    }
                    if (mappedValue == "")
                    {
                        failedRecords.Add(
                            "Value "
                                + record.Value
                                + " on field "
                                + key
                                + " is not found on the list"
                        );
                    }
                    else
                    {
                        value = mappedValue;
                    }
                }
                else
                {
                    value = valuereal;
                }
                result.Add(mappedKey, value);
            }
            finalresult.Add(result);
        }
        toolImport.MappedRecords = finalresult;
        toolImport.FailedRecords = failedRecords;
        return toolImport;
    }

    private static string MapHeader(string name)
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

    public List<ValveTypeData> GetValveTypeList()
    {
        return _valveTypeRepository.GetValveTypeList();
    }

    public List<ManualOverrideData> GetManualOverrideList()
    {
        return _manualOverrideRepository.GetManualOverrideList();
    }

    public List<FluidPhaseData> GetFluidPhaseList()
    {
        return _fluidPhaseRepository.GetFluidPhaseList();
    }

    public List<ToxicOrFlamableFluidData> GetToxicOrFlamableFluidList()
    {
        return _toxicOrFlamableFluidRepository.GetToxicOrFlamableFluidList();
    }

    public Dictionary<string, string> ImportAsset(List<Dictionary<string, string>> data, int CreatedBy)
    {
        ToolImportClass toolImport = MapAssetRegister(data);
        if(toolImport.MappedRecords == null || toolImport.MappedRecords.Count == 0)
        {
            throw new Exception("Failed to import asset data");
        }
        List<Dictionary<string, string>> result = toolImport.MappedRecords;
        int total = 0;
        int success = 0;
        int failed = 0;
        List<string> failedDatas = [];

        foreach(var item in result){
            if(item == null) continue;
            total++;
            AssetClass? asset = null;
            try{
                string json = JsonConvert.SerializeObject(item);
                asset = JsonConvert.DeserializeObject<AssetClass>(json);
                if(asset == null) continue;
                asset.IsDeleted = false;
                asset.CreatedBy = CreatedBy;
                asset.CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString());
                AssetData? assetData = _assetRepository.AddAsset(asset);
            }
            catch(Exception e){
                LogData log = new(){
                    Module = "Import Asset",
                    CreatedBy = CreatedBy,
                    CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
                    Message = e.Message,
                    Data = JsonConvert.SerializeObject(item)
                };
                _logRepository.AddLog(log);
                failed++;
                failedDatas.Add(asset?.TagNo ?? "");
            }
        }
        success = total - failed;
        string message =
            "Success import "
            + success
            + " data(s) of "
            + total
            + " data(s). Failed "
            + failed
            + " data(s)";
        if (failed > 0)
        {
            if (failedDatas.Count > 0)
            {
                string failedData = "";
                foreach (var _failed in failedDatas)
                {
                    failedData += _failed + ", ";
                }
                failedData = failedData[..^2];
                message += " with Tag No: " + failedData + ".";
            }
            else
            {
                message += ".";
            }
        }
        else
        {
            message += ".";
        }
        string messageFailed = "";
        if(toolImport.FailedRecords != null && toolImport.FailedRecords.Count > 0){
            foreach (var _failed in toolImport.FailedRecords)
            {
                total++;
                failed++;
                messageFailed += _failed + ", ";
                if (_failed.Equals(toolImport.FailedRecords.Last()))
                {
                    messageFailed = messageFailed.Substring(0, messageFailed.Length - 2);
                    message += " Exception Error: " + messageFailed + ".";
                }
            }
        }
        return 
            new Dictionary<string, string>
            {
                { "total", total.ToString() },
                { "success", success.ToString() },
                { "failed", failed.ToString() },
                { "failedDatas", JsonConvert.SerializeObject(failedDatas) },
                { "message", message }
            };
    }
    public Dictionary<string, int> GetAssetDistribution()
    {
        List<AssetData> assetList = GetAssetList();
        int asset_cbu = 0;
        int asset_nbu = 0;
        int asset_sbu = 0;
        foreach (var asset in assetList)
        {
            if (asset.BusinessArea == null)
            {
                continue;
            }
            if (asset.BusinessArea.ToLower().Equals("cbu"))
            {
                asset_cbu++;
            }
            else if (asset.BusinessArea.ToLower().Equals("nbu"))
            {
                asset_nbu++;
            }
            else if (asset.BusinessArea.ToLower().Equals("sbu"))
            {
                asset_sbu++;
            }
        }
        return new Dictionary<string, int>
        {
            { "CBU", asset_cbu },
            { "NBU", asset_nbu },
            { "SBU", asset_sbu }
        };
    }
}

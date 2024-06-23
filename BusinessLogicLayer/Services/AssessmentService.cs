using System.Globalization;
using DataAccessLayer;
using Newtonsoft.Json;
using SharedLayer;

namespace BusinessLogicLayer;

public interface IAssessmentService
{
    AssessmentData GetAssessment(int assessmentId);
    List<AssessmentData> GetAssessmentList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false,
        bool withHistory = true,
        int AssetID = 0);
    AssessmentData AddAssessment(AssessmentClass assessment);
    AssessmentData UpdateAssessment(AssessmentClass assessment);
    AssessmentData DeleteAssessment(AssessmentClass assessment);
    void AddMaintenanceToAssessment(int assessmentId, List<int> maintenanceIds, bool update = false);
    void AddInspectionToAssessment(int assessmentId, List<int> inspectionIds, bool update = false);
    Dictionary<string, string> ImportAssessment(List<Dictionary<string, string>> data, int CreatedBy);
    List<AssessmentData> GetAssessmentRecapList(int AreaID = 0, int PlatformID = 0, bool IncludeDeleted = false, bool withHistory = true);
    Dictionary<string, Dictionary<string, string>> GetAssessmentRecap();
    List<CurrentConditionLimitStateData> CurrentConditionLimitStateDatas();
    List<InspectionEffectivenessData> InspectionEffectivenessDatas();
    List<IsValveRepairedData> IsValveRepairedDatas();
    List<InspectionMethodData> InspectionMethodDatas();
    List<TimeToLimitStateData> TimeToLimitStateDatas();
    List<ImpactEffectData> ImpactEffectDatas();
    List<UsedWithinOEMSpecificationData> UsedWithinOEMSpecificationDatas();
    List<RepairedData> RepairedDatas();
    List<HSSEDefinisionData> HSSEDefinisionDatas();
    List<RecommendationActionData> RecomendationActionDatas();
}

public class AssessmentService(
    IAssessmentRepository assessmentRepository,
    IAssetRepository assetRepository,
    IPlatformRepository platformRepository,
    IInspectionFileRepository inspectionFileRepository,
    IInspectionRepository inspectionRepository,
    IMaintenanceRepository maintenanceRepository,
    IAssessmentInspectionRepository assessmentInspectionRepository,
    IAssessmentMaintenanceRepository assessmentMaintenanceRepository,
    ICurrentConditionLimitStateRepository currentConditionLimitStateRepository,
    IHSSEDefinisionRepository hSSEDefinisionRepository,
    IInspectionEffectivenessRepository inspectionEffectivenessRepository,
    IIsValveRepairedRepository isValveRepairedRepository,
    IInspectionMethodRepository inspectionMethodRepository,
    IImpactEffectRepository impactEffectRepository,
    IRecomendationActionRepository recomendationActionRepository,
    IRepairedRepository repairedRepository,
    ITimeToLimitStateRepository timeToLimitStateRepository,
    IUsedWithinOEMSpecificationRepository usedWithinOEMSpecificationRepository,
    ILogRepository logRepository
) : IAssessmentService
{
    private readonly IAssessmentRepository _assessmentRepository = assessmentRepository;
    private readonly IAssetRepository _assetRepository = assetRepository;
    private readonly IPlatformRepository _platformRepository = platformRepository;
    private readonly IInspectionFileRepository _inspectionFileRepository = inspectionFileRepository;
    private readonly IInspectionRepository _inspectionRepository = inspectionRepository;
    private readonly IMaintenanceRepository _maintenanceRepository = maintenanceRepository;
    private readonly IAssessmentInspectionRepository _assessmentInspectionRepository = assessmentInspectionRepository;
    private readonly IAssessmentMaintenanceRepository _assessmentMaintenanceRepository = assessmentMaintenanceRepository;
    private readonly ICurrentConditionLimitStateRepository _currentConditionLimitStateRepository = currentConditionLimitStateRepository;
    private readonly IHSSEDefinisionRepository _hSSEDefinisionRepository = hSSEDefinisionRepository;
    private readonly IInspectionEffectivenessRepository _inspectionEffectivenessRepository = inspectionEffectivenessRepository;
    private readonly IIsValveRepairedRepository _isValveRepairedRepository = isValveRepairedRepository;
    private readonly IInspectionMethodRepository _inspectionMethodRepository = inspectionMethodRepository;
    private readonly IImpactEffectRepository _impactEffectRepository = impactEffectRepository;
    private readonly IRecomendationActionRepository _recomendationActionRepository = recomendationActionRepository;
    private readonly IRepairedRepository _repairedRepository = repairedRepository;
    private readonly ITimeToLimitStateRepository _timeToLimitStateRepository = timeToLimitStateRepository;
    private readonly IUsedWithinOEMSpecificationRepository _usedWithinOEMSpecificationRepository = usedWithinOEMSpecificationRepository;
    private readonly ILogRepository _logRepository = logRepository;

    public AssessmentData GetAssessment(int assessmentId)
    {
        try{
            AssessmentData? assessmentData = _assessmentRepository.GetAssessment(assessmentId) ?? throw new Exception("Assessment not found");
            assessmentData.Asset = _assetRepository.GetAsset(assessmentData.AssetID);
            assessmentData.InspectionHistory = _assessmentInspectionRepository.GetAssessmentInspectionList(assessmentData.Id);
            foreach (var inspection in assessmentData.InspectionHistory)
            {
                inspection.Inspection = _inspectionRepository.GetInspection(inspection.InspectionID ?? 0);
                inspection.Inspection.InspectionFiles = _inspectionFileRepository.GetInspectionFiles(inspection.InspectionID ?? 0);
            }
            assessmentData.MaintenanceHistory = _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessmentData.Id);
            foreach (var maintenance in assessmentData.MaintenanceHistory)
            {
                maintenance.Maintenance = _maintenanceRepository.GetMaintenance(maintenance.MaintenanceID ?? 0);
                maintenance.Maintenance.MaintenanceFiles = _inspectionFileRepository.GetMaintenanceFiles(maintenance.MaintenanceID ?? 0);
            }
            return assessmentData;
        } catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
    public List<AssessmentData> GetAssessmentList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false,
        bool withHistory = true,
        int AssetID = 0)
    {
        try {
            List<AssessmentData> assessmentDataList = _assessmentRepository.GetAssessmentList(false, AssetID);
            foreach (var assessmentData in assessmentDataList)
            {
                assessmentData.Asset = _assetRepository.GetAsset(assessmentData.AssetID);
                if (withHistory)
                {
                    assessmentData.InspectionHistory = _assessmentInspectionRepository.GetAssessmentInspectionList(assessmentData.Id);
                    foreach (var inspection in assessmentData.InspectionHistory)
                    {
                        inspection.Inspection = _inspectionRepository.GetInspection(inspection.InspectionID ?? 0);
                    }
                    assessmentData.MaintenanceHistory = _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessmentData.Id);
                    foreach (var maintenance in assessmentData.MaintenanceHistory)
                    {
                        maintenance.Maintenance = _maintenanceRepository.GetMaintenance(maintenance.MaintenanceID ?? 0);
                    }
                }
            }
            var newlist = assessmentDataList.Where(
                x =>
                x.Asset != null &&
                x.Asset.PlatformData != null &&
                x.Asset.PlatformData.AreaID == AreaID &&
                x.Asset.PlatformID == PlatformID
            ).ToList();
            return assessmentDataList;
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
    public AssessmentData AddAssessment(AssessmentClass assessment)
    {
        try {
            AssessmentData? assessmentData = _assessmentRepository.AddAssessment(assessment) ?? throw new Exception("Assessment not found");
            assessmentData.Asset = _assetRepository.GetAsset(assessmentData.AssetID);
            assessmentData.InspectionHistory = _assessmentInspectionRepository.GetAssessmentInspectionList(assessmentData.Id);
            foreach (var inspection in assessmentData.InspectionHistory)
            {
                inspection.Inspection = _inspectionRepository.GetInspection(inspection.InspectionID ?? 0);
            }
            assessmentData.MaintenanceHistory = _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessmentData.Id);
            foreach (var maintenance in assessmentData.MaintenanceHistory)
            {
                maintenance.Maintenance = _maintenanceRepository.GetMaintenance(maintenance.MaintenanceID ?? 0);
            }
            return assessmentData;
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
    public AssessmentData UpdateAssessment(AssessmentClass assessment)
    {
        try {
            AssessmentData assessmentData = _assessmentRepository.UpdateAssessment(assessment);
            assessmentData.Asset = _assetRepository.GetAsset(assessmentData.AssetID);
            assessmentData.InspectionHistory = _assessmentInspectionRepository.GetAssessmentInspectionList(assessmentData.Id);
            foreach (var inspection in assessmentData.InspectionHistory)
            {
                inspection.Inspection = _inspectionRepository.GetInspection(inspection.InspectionID ?? 0);
            }
            assessmentData.MaintenanceHistory = _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessmentData.Id);
            foreach (var maintenance in assessmentData.MaintenanceHistory)
            {
                maintenance.Maintenance = _maintenanceRepository.GetMaintenance(maintenance.MaintenanceID ?? 0);
            }
            return assessmentData;
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
    public AssessmentData DeleteAssessment(AssessmentClass assessment)
    {
        try {
            return _assessmentRepository.DeleteAssessment(assessment);
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }
    private ToolImportClass MapAssessmentRegister(List<Dictionary<string, string>> datas)
    {
        ToolImportClass toolImport = new();
        List<string> failedRecords = [];
        List<AssetData> assetList = _assetRepository.GetAssetList();
        List<ImpactEffectData> impactEffectList = _impactEffectRepository.GetImpactEffectList();
        List<UsedWithinOEMSpecificationData> usedWithinOEMSpecificationList = _usedWithinOEMSpecificationRepository.GetUsedWithinOEMSpecificationList();
        List<RepairedData> repairedList = _repairedRepository.GetRepairedList();
        List<HSSEDefinisionData> hSSEDefinisionList = _hSSEDefinisionRepository.GetHSSEDefinisionList();
        List<Dictionary<string, string>> finalresult = [];
        foreach (var records in datas)
        {
            int timeperiod = 0;
            Dictionary<string, string> result = [];
            foreach(var record in records)
            {
                string key = record.Key;
                string value = record.Value.Trim().ToLower();
                string valuereal = record.Value.Trim();
                string mappedKey = MapHeader(key);
                string mappedValue = "";
                if (mappedKey.Equals(""))
                {
                    continue;
                }
                if (mappedKey.Equals("TimePeriode"))
                {
                    timeperiod = Convert.ToInt32(value);
                }
                if (
                    mappedKey.Equals("AssetID")
                    || mappedKey.Equals("AssessmentDate")
                    || mappedKey.Equals("ImpactOfInternalFluidImpuritiesID")
                    || mappedKey.Equals("ImpactOfOperatingEnvelopesID")
                    || mappedKey.Equals("UsedWithinOEMSpecificationID")
                    || mappedKey.Equals("RepairedID")
                    || mappedKey.Equals("HSSEDefinisionID")
                )
                {
                    if (mappedKey.Equals("AssetID"))
                    {
                        foreach (var asset in assetList)
                        {
                            if (asset.TagNo == null)
                            {
                                continue;
                            }
                            if (asset.TagNo.Trim().ToLower().Equals(value))
                            {
                                mappedValue = asset.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("AssessmentDate"))
                    {
                        if (value.Contains("/"))
                        {
                            string date = value.Split(" ")[0];
                            List<string> dateParts = [.. date.Split("/")];
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
                                .ToString(SharedEnvironment.GetDateFormatString(false));
                        }
                    }
                    else if (
                        mappedKey.Equals("ImpactOfInternalFluidImpuritiesID")
                        || mappedKey.Equals("ImpactOfOperatingEnvelopesID")
                    )
                    {
                        foreach (var impactEffect in impactEffectList)
                        {
                            if(impactEffect.ImpactEffect == null)
                            {
                                continue;
                            }
                            if (impactEffect.ImpactEffect.Trim().ToLower().Equals(value))
                            {
                                mappedValue = impactEffect.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("UsedWithinOEMSpecificationID"))
                    {
                        foreach (var usedWithinOEMSpecification in usedWithinOEMSpecificationList)
                        {
                            if (
                                usedWithinOEMSpecification.UsedWithinOEMSpecification == null
                            )
                            {
                                continue;
                            }
                            if (
                                usedWithinOEMSpecification
                                    .UsedWithinOEMSpecification.Trim()
                                    .ToLower()
                                    .Equals(value)
                            )
                            {
                                mappedValue = usedWithinOEMSpecification.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("RepairedID"))
                    {
                        foreach (var repaired in repairedList)
                        {
                            if (repaired.Repaired == null)
                            {
                                continue;
                            }
                            if (repaired.Repaired.Trim().ToLower().Equals(value))
                            {
                                mappedValue = repaired.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("HSSEDefinisionID"))
                    {
                        foreach (var hsseDefinision in hSSEDefinisionList)
                        {
                            if (hsseDefinision.HSSEDefinision == null)
                            {
                                continue;
                            }
                            if (hsseDefinision.HSSEDefinision.Trim().ToLower().Equals(value))
                            {
                                mappedValue = hsseDefinision.Id.ToString();
                                break;
                            }
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
            int idImprobable = 1;
            int idDoubtful = 2;
            int idExpected = 3;
            double tp_limit_1 = timeperiod;
            double tp_limit_2 = 2 * timeperiod;
            double tp_limit_3 = 3 * timeperiod;
            int LeakageToAtmosphereTP1ID = 0;
            int LeakageToAtmosphereTP2ID = 0;
            int LeakageToAtmosphereTP3ID = 0;
            int FailureOfFunctionTP1ID = 0;
            int FailureOfFunctionTP2ID = 0;
            int FailureOfFunctionTP3ID = 0;
            int PassingAccrosValveTP1ID = 0;
            int PassingAccrosValveTP2ID = 0;
            int PassingAccrosValveTP3ID = 0;
            if (
                double.TryParse(
                    result["TimeToLimitStateLeakageToAtmosphere"],
                    out double timeToLimitStateLeakage
                )
            )
            {
                // TP 1
                if (timeToLimitStateLeakage >= 2 * tp_limit_1)
                {
                    LeakageToAtmosphereTP1ID = idImprobable;
                }
                else if (timeToLimitStateLeakage > tp_limit_1)
                {
                    LeakageToAtmosphereTP1ID = idDoubtful;
                }
                else
                {
                    LeakageToAtmosphereTP1ID = idExpected;
                }
                // TP 2
                if (timeToLimitStateLeakage >= 2 * tp_limit_2)
                {
                    LeakageToAtmosphereTP2ID = idImprobable;
                }
                else if (timeToLimitStateLeakage > tp_limit_2)
                {
                    LeakageToAtmosphereTP2ID = idDoubtful;
                }
                else
                {
                    LeakageToAtmosphereTP2ID = idExpected;
                }
                // TP 3
                if (timeToLimitStateLeakage >= 2 * tp_limit_3)
                {
                    LeakageToAtmosphereTP3ID = idImprobable;
                }
                else if (timeToLimitStateLeakage > tp_limit_3)
                {
                    LeakageToAtmosphereTP3ID = idDoubtful;
                }
                else
                {
                    LeakageToAtmosphereTP3ID = idExpected;
                }
            }
            result.Add("LeakageToAtmosphereTP1ID", LeakageToAtmosphereTP1ID.ToString());
            result.Add("LeakageToAtmosphereTP2ID", LeakageToAtmosphereTP2ID.ToString());
            result.Add("LeakageToAtmosphereTP3ID", LeakageToAtmosphereTP3ID.ToString());
            if (
                double.TryParse(
                    result["TimeToLimitStateFailureOfFunction"],
                    out double timeToLimitStateFailure
                )
            )
            {
                // TP 1
                if (timeToLimitStateFailure >= 2 * tp_limit_1)
                {
                    FailureOfFunctionTP1ID = idImprobable;
                }
                else if (timeToLimitStateFailure > tp_limit_1)
                {
                    FailureOfFunctionTP1ID = idDoubtful;
                }
                else
                {
                    FailureOfFunctionTP1ID = idExpected;
                }
                // TP 2
                if (timeToLimitStateFailure >= 2 * tp_limit_2)
                {
                    FailureOfFunctionTP2ID = idImprobable;
                }
                else if (timeToLimitStateFailure > tp_limit_2)
                {
                    FailureOfFunctionTP2ID = idDoubtful;
                }
                else
                {
                    FailureOfFunctionTP2ID = idExpected;
                }
                // TP 3
                if (timeToLimitStateFailure >= 2 * tp_limit_3)
                {
                    FailureOfFunctionTP3ID = idImprobable;
                }
                else if (timeToLimitStateFailure > tp_limit_3)
                {
                    FailureOfFunctionTP3ID = idDoubtful;
                }
                else
                {
                    FailureOfFunctionTP3ID = idExpected;
                }
            }
            result.Add("FailureOfFunctionTP1ID", FailureOfFunctionTP1ID.ToString());
            result.Add("FailureOfFunctionTP2ID", FailureOfFunctionTP2ID.ToString());
            result.Add("FailureOfFunctionTP3ID", FailureOfFunctionTP3ID.ToString());
            if (
                double.TryParse(
                    result["TimeToLimitStatePassingAccrosValve"],
                    out double timeToLimitStatePassing
                )
            )
            {
                // TP 1
                if (timeToLimitStatePassing >= 2 * tp_limit_1)
                {
                    PassingAccrosValveTP1ID = idImprobable;
                }
                else if (timeToLimitStatePassing > tp_limit_1)
                {
                    PassingAccrosValveTP1ID = idDoubtful;
                }
                else
                {
                    PassingAccrosValveTP1ID = idExpected;
                }
                // TP 2
                if (timeToLimitStatePassing >= 2 * tp_limit_2)
                {
                    PassingAccrosValveTP2ID = idImprobable;
                }
                else if (timeToLimitStatePassing > tp_limit_2)
                {
                    PassingAccrosValveTP2ID = idDoubtful;
                }
                else
                {
                    PassingAccrosValveTP2ID = idExpected;
                }
                // TP 3
                if (timeToLimitStatePassing >= 2 * tp_limit_3)
                {
                    PassingAccrosValveTP3ID = idImprobable;
                }
                else if (timeToLimitStatePassing > tp_limit_3)
                {
                    PassingAccrosValveTP3ID = idDoubtful;
                }
                else
                {
                    PassingAccrosValveTP3ID = idExpected;
                }
            }
            result.Add("PassingAccrosValveTP1ID", PassingAccrosValveTP1ID.ToString());
            result.Add("PassingAccrosValveTP2ID", PassingAccrosValveTP2ID.ToString());
            result.Add("PassingAccrosValveTP3ID", PassingAccrosValveTP3ID.ToString());
            result.Add("AssessmentNo", "IMPORT");
            finalresult.Add(result);
        }
        toolImport.MappedRecords = finalresult;
        toolImport.FailedRecords = failedRecords;
        return toolImport;
    }
    private static string MapHeader(string name)
    {
        string result = name.ToLower() switch
        {
            "valve tag no." => "AssetID",
            "assessment date\n(dd/mm/yyyy)" => "AssessmentDate",
            "time periode\n(month)" => "TimePeriode",
            "lf2 - time to limit state leakage to atmosphere \n(month)" => "TimeToLimitStateLeakageToAtmosphere",
            "lf2 - time to limit state failure of function (month)" => "TimeToLimitStateFailureOfFunction",
            "lf2 - time to limit state passing accros valve (month)" => "TimeToLimitStatePassingAccrosValve",
            "lf4 - impact of internal fluid impurities" => "ImpactOfInternalFluidImpuritiesID",
            "lf5 - impact of operating envelopes" => "ImpactOfOperatingEnvelopesID",
            "lf6 - used within oem specification" => "UsedWithinOEMSpecificationID",
            "lf7 - repaired" => "RepairedID",
            "cf1 - product loss definition (bbls)" => "ProductLossDefinition",
            "cf2 - hsse definision" => "HSSEDefinisionID",
            _ => "",
        };
        return result;
    }
    public void AddMaintenanceToAssessment(int assessmentId, List<int> maintenanceIds, bool update = false)
    {
        if(update)
        {
            _assessmentMaintenanceRepository.DeleteAssessmentMaintenance(assessmentId);
        }
        _assessmentMaintenanceRepository.AddMaintenanceToAssessment(assessmentId, maintenanceIds);
    }
    public void AddInspectionToAssessment(int assessmentId, List<int> inspectionIds, bool update = false)
    {
        if(update)
        {
            _assessmentInspectionRepository.DeleteAssessmentInspection(assessmentId);
        }
        _assessmentInspectionRepository.AddInspectionToAssessment(assessmentId, inspectionIds);
    }
    public Dictionary<string, string> ImportAssessment(List<Dictionary<string, string>> data, int CreatedBy)
    {
        ToolImportClass toolImport = MapAssessmentRegister(data);
        if(toolImport.MappedRecords == null || toolImport.MappedRecords.Count == 0)
        {
            throw new Exception("Failed to import assessment data");
        }
        List<Dictionary<string, string>> result = toolImport.MappedRecords;
        int total = 0;
        int success = 0;
        int failed = 0;
        List<string> failedDatas = [];

        foreach(var item in result){
            if(item == null) continue;
            total++;
            AssessmentClass? assessment = null;
            try{
                string json = JsonConvert.SerializeObject(item);
                assessment = JsonConvert.DeserializeObject<AssessmentClass>(json);
                if(assessment == null) continue;
                assessment.IsDeleted = false;
                assessment.CreatedBy = CreatedBy;
                assessment.CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString());
                AssessmentData? assessmentData = _assessmentRepository.AddAssessment(assessment);
            }
            catch(Exception e){
                LogData log = new(){
                    Module = "Import Inspection",
                    CreatedBy = CreatedBy,
                    CreatedAt = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString()),
                    Message = e.Message,
                    Data = JsonConvert.SerializeObject(item)
                };
                _logRepository.AddLog(log);
                failed++;
                failedDatas.Add(assessment?.AssessmentDate ?? "");
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
                message += " with Assessment Date: " + failedData + ".";
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
    public List<AssessmentData> GetAssessmentRecapList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false,
        bool withHistory = true
    ){
        List<AssessmentData> assessmentDataList =
            _assessmentRepository.GetAssessmentList(false)
            .Where(x => x.IsDeleted == IncludeDeleted)
            .GroupBy(x => x.AssetID)
            .Select(x => x.OrderByDescending(a => DateTime.ParseExact(a.AssessmentDate ?? "01-01-1900", "dd-MM-yyyy", CultureInfo.InvariantCulture)).First())
            .ToList();
        foreach(var assessment in assessmentDataList)
        {
            assessment.Asset = _assetRepository.GetAsset(assessment.AssetID);
            assessment.Asset.PlatformData = _platformRepository.GetPlatform(assessment.Asset.PlatformID ?? 0);
            assessment.InspectionHistory = _assessmentInspectionRepository.GetAssessmentInspectionList(assessment.Id);
            foreach(var inspection in assessment.InspectionHistory)
            {
                inspection.Inspection = _inspectionRepository.GetInspection(inspection.InspectionID ?? 0);
            }
            if(assessment.InspectionHistory != null && assessment.InspectionHistory.Any()){
                var lastInspection = assessment.InspectionHistory.Last().Inspection;
                if(lastInspection != null){
                    assessment.LastInspectionId = lastInspection.Id;
                    assessment.LastInspectionDate = lastInspection.InspectionDate;
                }
            }
            assessment.MaintenanceHistory = _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessment.Id);
            foreach(var maintenance in assessment.MaintenanceHistory)
            {
                maintenance.Maintenance = _maintenanceRepository.GetMaintenance(maintenance.MaintenanceID ?? 0);
            }
            if(assessment.MaintenanceHistory != null && assessment.MaintenanceHistory.Any()){
                var lastMaintenance = assessment.MaintenanceHistory.Last().Maintenance;
                if(lastMaintenance != null){
                    assessment.LastMaintenanceId = lastMaintenance.Id;
                    assessment.LastMaintenanceDate = lastMaintenance.MaintenanceDate;
                }
            }
        }
        assessmentDataList = assessmentDataList
            .Where(x => x.IsDeleted == IncludeDeleted)
            .Where(x => x.Asset?.PlatformData?.AreaID == AreaID || AreaID == 0)
            .Where(x => x.Asset?.PlatformID == PlatformID || PlatformID == 0)
            .ToList();
        return assessmentDataList;
    }
    public Dictionary<string, Dictionary<string, string>> GetAssessmentRecap(){
        Dictionary<string, Dictionary<string, string>> recap_final = [];
        Dictionary<string, string> recap_heatmap = [];
        Dictionary<string, string> recap_piechart = [];
        Dictionary<string, Dictionary<string, string>> recap_barchart = [];
        Dictionary<string, string> recap_barchart_integritystatus= [];
        Dictionary<string, string> recap_barchart_convert = [];
        int a1 = 0;
        int a2 = 0;
        int a3 = 0;
        int a4 = 0;
        int a5 = 0;
        int b1 = 0;
        int b2 = 0;
        int b3 = 0;
        int b4 = 0;
        int b5 = 0;
        int c1 = 0;
        int c2 = 0;
        int c3 = 0;
        int c4 = 0;
        int c5 = 0;
        int d1 = 0;
        int d2 = 0;
        int d3 = 0;
        int d4 = 0;
        int d5 = 0;
        int e1 = 0;
        int e2 = 0;
        int e3 = 0;
        int e4 = 0;
        int e5 = 0;
        int risk_verylow = 0;
        int risk_low = 0;
        int risk_medium = 0;
        int risk_high = 0;
        int risk_veryhigh = 0;
        int integrity_verylow = 0;
        int integrity_low = 0;
        int integrity_medium = 0;
        int integrity_high = 0;
        int integrity_veryhigh = 0;
        List<AssessmentData> assessmentList = GetAssessmentRecapList(0, 0, false, false);
        foreach (var assessment in assessmentList)
        {
            if (
                assessment.TP1Risk == null
                || assessment.TP2Risk == null
                || assessment.TP3Risk == null
            )
            {
                continue;
            }
            string heat_risk = assessment.TP1Risk;
            switch (heat_risk)
            {
                case "1A":
                    a1++;
                    break;
                case "1B":
                    b1++;
                    break;
                case "1C":
                    c1++;
                    break;
                case "1D":
                    d1++;
                    break;
                case "1E":
                    e1++;
                    break;
                case "2A":
                    a2++;
                    break;
                case "2B":
                    b2++;
                    break;
                case "2C":
                    c2++;
                    break;
                case "2D":
                    d2++;
                    break;
                case "2E":
                    e2++;
                    break;
                case "3A":
                    a3++;
                    break;
                case "3B":
                    b3++;
                    break;
                case "3C":
                    c3++;
                    break;
                case "3D":
                    d3++;
                    break;
                case "3E":
                    e3++;
                    break;
                case "4A":
                    a4++;
                    break;
                case "4B":
                    b4++;
                    break;
                case "4C":
                    c4++;
                    break;
                case "4D":
                    d4++;
                    break;
                case "4E":
                    e4++;
                    break;
                case "5A":
                    a5++;
                    break;
                case "5B":
                    b5++;
                    break;
                case "5C":
                    c5++;
                    break;
                case "5D":
                    d5++;
                    break;
                case "5E":
                    e5++;
                    break;
            }
            // calculate risk_var
            string risk = AssessmentStaticClass.ColorRiskMap[AssessmentStaticClass.GetHeatColor(assessment.TP1Risk)];
            switch (risk)
            {
                case "Very Low":
                    risk_verylow++;
                    break;
                case "Low":
                    risk_low++;
                    break;
                case "Medium":
                    risk_medium++;
                    break;
                case "High":
                    risk_high++;
                    break;
                case "Very High":
                    risk_veryhigh++;
                    break;
            }
            if(assessment.Asset!=null){
                if (!recap_barchart.ContainsKey(assessment.Asset.BusinessArea ?? ""))
                {
                    recap_barchart.Add(
                        assessment.Asset.BusinessArea ?? "",
                        new()
                        {
                            { "Very Low", "0" },
                            { "Low", "0" },
                            { "Medium", "0" },
                            { "High", "0" },
                            { "Very High", "0" }
                        }
                    );
                }
                int curr_risk = int.Parse(recap_barchart[assessment.Asset.BusinessArea ?? ""][risk]);
                curr_risk++;
                recap_barchart[assessment.Asset.BusinessArea ?? ""][risk] = curr_risk.ToString();
                switch(assessment.IntegrityStatus){
                    case "Very Low Priority":
                        integrity_verylow++;
                        break;
                    case "Low Priority":
                        integrity_low++;
                        break;
                    case "Medium Priority":
                        integrity_medium++;
                        break;
                    case "High Priority":
                        integrity_high++;
                        break;
                    case "Very High Priority":
                        integrity_veryhigh++;
                        break;
                }
            }
        }
        recap_heatmap.Add("1A", a1.ToString());
        recap_heatmap.Add("1B", b1.ToString());
        recap_heatmap.Add("1C", c1.ToString());
        recap_heatmap.Add("1D", d1.ToString());
        recap_heatmap.Add("1E", e1.ToString());
        recap_heatmap.Add("2A", a2.ToString());
        recap_heatmap.Add("2B", b2.ToString());
        recap_heatmap.Add("2C", c2.ToString());
        recap_heatmap.Add("2D", d2.ToString());
        recap_heatmap.Add("2E", e2.ToString());
        recap_heatmap.Add("3A", a3.ToString());
        recap_heatmap.Add("3B", b3.ToString());
        recap_heatmap.Add("3C", c3.ToString());
        recap_heatmap.Add("3D", d3.ToString());
        recap_heatmap.Add("3E", e3.ToString());
        recap_heatmap.Add("4A", a4.ToString());
        recap_heatmap.Add("4B", b4.ToString());
        recap_heatmap.Add("4C", c4.ToString());
        recap_heatmap.Add("4D", d4.ToString());
        recap_heatmap.Add("4E", e4.ToString());
        recap_heatmap.Add("5A", a5.ToString());
        recap_heatmap.Add("5B", b5.ToString());
        recap_heatmap.Add("5C", c5.ToString());
        recap_heatmap.Add("5D", d5.ToString());
        recap_heatmap.Add("5E", e5.ToString());
        recap_piechart.Add("Very Low", risk_verylow.ToString());
        recap_piechart.Add("Low", risk_low.ToString());
        recap_piechart.Add("Medium", risk_medium.ToString());
        recap_piechart.Add("High", risk_high.ToString());
        recap_piechart.Add("Very High", risk_veryhigh.ToString());
        recap_barchart_integritystatus.Add("Very Low", integrity_verylow.ToString());
        recap_barchart_integritystatus.Add("Low", integrity_low.ToString());
        recap_barchart_integritystatus.Add("Medium", integrity_medium.ToString());
        recap_barchart_integritystatus.Add("High", integrity_high.ToString());
        recap_barchart_integritystatus.Add("Very High", integrity_veryhigh.ToString());
        recap_final.Add("heatmap", recap_heatmap);
        recap_final.Add("piechart", recap_piechart);
        foreach (var item in recap_barchart)
        {
            recap_barchart_convert.Add(item.Key, JsonConvert.SerializeObject(item.Value));
        }
        recap_final.Add("barchart", recap_barchart_convert);
        recap_final.Add("integritystatus", recap_barchart_integritystatus);
        return recap_final;
    }
    public List<CurrentConditionLimitStateData> CurrentConditionLimitStateDatas()
    {
        return _currentConditionLimitStateRepository.GetCurrentConditionLimitStateList();
    }
    public List<InspectionEffectivenessData> InspectionEffectivenessDatas()
    {
        return _inspectionEffectivenessRepository.GetInspectionEffectivenessList();
    }
    public List<IsValveRepairedData> IsValveRepairedDatas()
    {
        return _isValveRepairedRepository.GetIsValveRepairedList();
    }
    public List<InspectionMethodData> InspectionMethodDatas()
    {
        return _inspectionMethodRepository.GetInspectionMethodList();
    }
    public List<TimeToLimitStateData> TimeToLimitStateDatas()
    {
        return _timeToLimitStateRepository.GetTimeToLimitStateList();
    }
    public List<ImpactEffectData> ImpactEffectDatas()
    {
        return _impactEffectRepository.GetImpactEffectList();
    }
    public List<UsedWithinOEMSpecificationData> UsedWithinOEMSpecificationDatas()
    {
        return _usedWithinOEMSpecificationRepository.GetUsedWithinOEMSpecificationList();
    }
    public List<RepairedData> RepairedDatas()
    {
        return _repairedRepository.GetRepairedList();
    }
    public List<HSSEDefinisionData> HSSEDefinisionDatas()
    {
        return _hSSEDefinisionRepository.GetHSSEDefinisionList();
    }
    public List<RecommendationActionData> RecomendationActionDatas()
    {
        return _recomendationActionRepository.GetRecomendationActionList();
    }
}
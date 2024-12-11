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
        int AssetID = 0
    );
    AssessmentData AddAssessment(AssessmentClass assessment);
    AssessmentData CalculateAssessment(int assessmentID);
    AssessmentData UpdateAssessment(AssessmentClass assessment);
    AssessmentData DeleteAssessment(AssessmentClass assessment);
    void AddMaintenanceToAssessment(
        int assessmentId,
        List<int> maintenanceIds,
        bool update = false
    );
    List<AssessmentMaintenanceData> GetAssessmentMaintenanceDatas(int maintenanceid);
    void AddInspectionToAssessment(int assessmentId, List<int> inspectionIds, bool update = false);
    List<AssessmentInspectionData> GetAssessmentInspectionDatas(int inspectionid);
    Dictionary<string, string> ImportAssessment(
        List<Dictionary<string, string>> data,
        int CreatedBy
    );
    List<AssessmentData> GetAssessmentRecapList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false,
        bool withHistory = true
    );
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
    private readonly IAssessmentInspectionRepository _assessmentInspectionRepository =
        assessmentInspectionRepository;
    private readonly IAssessmentMaintenanceRepository _assessmentMaintenanceRepository =
        assessmentMaintenanceRepository;
    private readonly ICurrentConditionLimitStateRepository _currentConditionLimitStateRepository =
        currentConditionLimitStateRepository;
    private readonly IHSSEDefinisionRepository _hSSEDefinisionRepository = hSSEDefinisionRepository;
    private readonly IInspectionEffectivenessRepository _inspectionEffectivenessRepository =
        inspectionEffectivenessRepository;
    private readonly IIsValveRepairedRepository _isValveRepairedRepository =
        isValveRepairedRepository;
    private readonly IInspectionMethodRepository _inspectionMethodRepository =
        inspectionMethodRepository;
    private readonly IImpactEffectRepository _impactEffectRepository = impactEffectRepository;
    private readonly IRecomendationActionRepository _recomendationActionRepository =
        recomendationActionRepository;
    private readonly IRepairedRepository _repairedRepository = repairedRepository;
    private readonly ITimeToLimitStateRepository _timeToLimitStateRepository =
        timeToLimitStateRepository;
    private readonly IUsedWithinOEMSpecificationRepository _usedWithinOEMSpecificationRepository =
        usedWithinOEMSpecificationRepository;
    private readonly ILogRepository _logRepository = logRepository;

    public AssessmentData GetAssessment(int assessmentId)
    {
        try
        {
            AssessmentData? assessmentData =
                _assessmentRepository.GetAssessment(assessmentId)
                ?? throw new Exception("Assessment not found");
            assessmentData.Asset = _assetRepository.GetAsset(assessmentData.AssetID);
            assessmentData.InspectionHistory =
                _assessmentInspectionRepository.GetAssessmentInspectionList(assessmentData.Id);
            foreach (var inspection in assessmentData.InspectionHistory)
            {
                inspection.Inspection = _inspectionRepository.GetInspection(
                    inspection.InspectionID ?? 0
                );
                inspection.Inspection.InspectionFiles =
                    _inspectionFileRepository.GetInspectionFiles(inspection.InspectionID ?? 0);
                if (inspection.Inspection.InspectionFiles != null)
                {
                    foreach (var file in inspection.Inspection.InspectionFiles)
                    {
                        file.FilePath =
                            SharedEnvironment.app_path.Replace("/", "") + "/" + file.FilePath;
                    }
                }
            }
            assessmentData.InspectionHistory = assessmentData
                .InspectionHistory.OrderByDescending(x =>
                    DateTime.TryParseExact(
                        x.Inspection?.InspectionDate ?? "01-01-1900",
                        SharedEnvironment.GetDateFormatString(false),
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime date
                    )
                        ? date
                        : DateTime.MinValue
                )
                .ToList();
            assessmentData.MaintenanceHistory =
                _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessmentData.Id);
            foreach (var maintenance in assessmentData.MaintenanceHistory)
            {
                maintenance.Maintenance = _maintenanceRepository.GetMaintenance(
                    maintenance.MaintenanceID ?? 0
                );
                maintenance.Maintenance.MaintenanceFiles =
                    _inspectionFileRepository.GetMaintenanceFiles(maintenance.MaintenanceID ?? 0);
                if (maintenance.Maintenance.MaintenanceFiles != null)
                {
                    foreach (var file in maintenance.Maintenance.MaintenanceFiles)
                    {
                        file.FilePath =
                            SharedEnvironment.app_path.Replace("/", "") + "/" + file.FilePath;
                    }
                }
            }
            assessmentData.MaintenanceHistory = assessmentData
                .MaintenanceHistory.OrderByDescending(x =>
                    DateTime.TryParseExact(
                        x.Maintenance?.MaintenanceDate ?? "01-01-1900",
                        SharedEnvironment.GetDateFormatString(false),
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime date
                    )
                        ? date
                        : DateTime.MinValue
                )
                .ToList();
            return assessmentData;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<AssessmentData> GetAssessmentList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false,
        bool withHistory = true,
        int AssetID = 0
    )
    {
        try
        {
            List<AssessmentData> assessmentDataList = _assessmentRepository.GetAssessmentList(
                false,
                AssetID
            );
            foreach (var assessmentData in assessmentDataList)
            {
                assessmentData.Asset = _assetRepository.GetAsset(assessmentData.AssetID);
                if (withHistory)
                {
                    assessmentData.InspectionHistory =
                        _assessmentInspectionRepository.GetAssessmentInspectionList(
                            assessmentData.Id
                        );
                    foreach (var inspection in assessmentData.InspectionHistory)
                    {
                        inspection.Inspection = _inspectionRepository.GetInspection(
                            inspection.InspectionID ?? 0
                        );
                    }
                    assessmentData.MaintenanceHistory =
                        _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(
                            assessmentData.Id
                        );
                    foreach (var maintenance in assessmentData.MaintenanceHistory)
                    {
                        maintenance.Maintenance = _maintenanceRepository.GetMaintenance(
                            maintenance.MaintenanceID ?? 0
                        );
                    }
                }
            }
            var newlist = assessmentDataList
                .Where(x =>
                    x.Asset != null
                    && x.Asset.PlatformData != null
                    && x.Asset.PlatformData.AreaID == AreaID
                    && x.Asset.PlatformID == PlatformID
                )
                .ToList();
            return assessmentDataList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public AssessmentData AddAssessment(AssessmentClass assessment)
    {
        try
        {
            AssessmentData? assessmentData =
                _assessmentRepository.AddAssessment(assessment)
                ?? throw new Exception("Assessment not found");
            assessmentData.Asset = _assetRepository.GetAsset(assessmentData.AssetID);
            assessmentData.InspectionHistory =
                _assessmentInspectionRepository.GetAssessmentInspectionList(assessmentData.Id);
            foreach (var inspection in assessmentData.InspectionHistory)
            {
                inspection.Inspection = _inspectionRepository.GetInspection(
                    inspection.InspectionID ?? 0
                );
            }
            assessmentData.MaintenanceHistory =
                _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessmentData.Id);
            foreach (var maintenance in assessmentData.MaintenanceHistory)
            {
                maintenance.Maintenance = _maintenanceRepository.GetMaintenance(
                    maintenance.MaintenanceID ?? 0
                );
            }
            return assessmentData;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public AssessmentData CalculateAssessment(int assessmentID)
    {
        string logdatetime = DateTime.Now.ToString(SharedEnvironment.GetDateFormatString(true));
        AssessmentData oldAssessmentData = GetAssessment(assessmentID);
        // START LOGGING
        LogClass logclass = new();
        logclass.Module = "XX==ASSESSMENT-1";
        logclass.CreatedAt = logdatetime;
        logclass.Message = assessmentID.ToString();
        logclass.Data = JsonConvert.SerializeObject(oldAssessmentData);
        _logRepository.AddLog(logclass);
        // END LOGGING
        AssessmentClass newAssessmentData =
            new()
            {
                Id = oldAssessmentData.Id,
                AssetID = oldAssessmentData.AssetID,
                AssessmentNo = oldAssessmentData.AssessmentNo,
                AssessmentDate = oldAssessmentData.AssessmentDate,
                TimePeriode = oldAssessmentData.TimePeriode,
                TimeToLimitStateLeakageToAtmosphere =
                    oldAssessmentData.TimeToLimitStateLeakageToAtmosphere,
                TimeToLimitStateFailureOfFunction =
                    oldAssessmentData.TimeToLimitStateFailureOfFunction,
                TimeToLimitStatePassingAccrosValve =
                    oldAssessmentData.TimeToLimitStatePassingAccrosValve,
                ImpactOfInternalFluidImpuritiesID =
                    oldAssessmentData.ImpactOfInternalFluidImpuritiesID,
                ImpactOfOperatingEnvelopesID = oldAssessmentData.ImpactOfOperatingEnvelopesID,
                UsedWithinOEMSpecificationID = oldAssessmentData.UsedWithinOEMSpecificationID,
                RepairedID = oldAssessmentData.RepairedID,
                ProductLossDefinition = oldAssessmentData.ProductLossDefinition,
                HSSEDefinisionID = oldAssessmentData.HSSEDefinisionID,
                Summary = oldAssessmentData.Summary,
                RecommendationActionID = oldAssessmentData.RecommendationActionID,
                DetailedRecommendation = oldAssessmentData.DetailedRecommendation,
                CoFScore = oldAssessmentData.CoFScore,
                IntegrityStatus = oldAssessmentData.IntegrityStatus,
                UpdatedAt = oldAssessmentData.UpdatedAt,
                UpdatedBy = oldAssessmentData.UpdatedBy,
            };
        // GET LAST INSPECTION
        List<AssessmentInspectionData>? inspectionDatas = oldAssessmentData.InspectionHistory;
        if (inspectionDatas == null || inspectionDatas.Count == 0)
        {
            throw new Exception("Inspection Data not found");
        }
        List<InspectionData>? inspectionData = [];
        foreach (var inspection in inspectionDatas)
        {
            if (inspection.Inspection == null || inspection.Inspection.InspectionDate == null)
            {
                continue;
            }
            inspectionData.Add(inspection.Inspection);
        }
        InspectionData latestInspection = inspectionData
            .OrderByDescending(x =>
                DateTime.ParseExact(
                    x.InspectionDate ?? "01-01-1900",
                    SharedEnvironment.GetDateFormatString(false),
                    CultureInfo.InvariantCulture
                )
            )
            .First();
        /**
        LF 1 Contains
        - Leakage To Atmosphere
        - Failure Of Function
        - Passing Accros Valve
        Data is from Latest Inspection
        */
        List<CurrentConditionLimitStateData> currentConditionLimitStateList =
            _currentConditionLimitStateRepository.GetCurrentConditionLimitStateList();
        double LeakageToAtmosphereV =
            currentConditionLimitStateList
                .First(x => x.Id == latestInspection.CurrentConditionLeakeageToAtmosphereID)
                .LimitStateValue ?? 0;
        double LeakageToAtmosphereW =
            currentConditionLimitStateList
                .First(x => x.Id == latestInspection.CurrentConditionLeakeageToAtmosphereID)
                .Weighting ?? 0;
        double FailureOfFunctionV =
            currentConditionLimitStateList
                .First(x => x.Id == latestInspection.CurrentConditionFailureOfFunctionID)
                .LimitStateValue ?? 0;
        double FailureOfFunctionW =
            currentConditionLimitStateList
                .First(x => x.Id == latestInspection.CurrentConditionFailureOfFunctionID)
                .Weighting ?? 0;
        double PassingAccrosValveV =
            currentConditionLimitStateList
                .First(x => x.Id == latestInspection.CurrentConditionPassingAcrossValveID)
                .LimitStateValue ?? 0;
        double PassingAccrosValveW =
            currentConditionLimitStateList
                .First(x => x.Id == latestInspection.CurrentConditionPassingAcrossValveID)
                .Weighting ?? 0;
        newAssessmentData.LeakageToAtmosphereID =
            latestInspection.CurrentConditionLeakeageToAtmosphereID;
        newAssessmentData.FailureOfFunctionID =
            latestInspection.CurrentConditionFailureOfFunctionID;
        newAssessmentData.PassingAccrosValveID =
            latestInspection.CurrentConditionPassingAcrossValveID;
        /**
        LF 2 Contains
        - Leakage To Atmosphere TP 1
        - Leakage To Atmosphere TP 2
        - Leakage To Atmosphere TP 3
        - Failure Of Function TP 1
        - Failure Of Function TP 2
        - Failure Of Function TP 3
        - Passing Accros Valve TP 1
        - Passing Accros Valve TP 2
        - Passing Accros Valve TP 3
        */
        DateTime lastInspectionDate = DateTime.ParseExact(
            latestInspection.InspectionDate ?? "01-01-1900",
            SharedEnvironment.GetDateFormatString(false),
            CultureInfo.InvariantCulture
        );
        int timePeriod = int.Parse(oldAssessmentData.TimePeriode ?? "0");
        int timeToLimitStateLeakageToAtmosphere = int.Parse(
            oldAssessmentData.TimeToLimitStateLeakageToAtmosphere ?? "0"
        );
        int timeToLimitStateFailureOfFunction = int.Parse(
            oldAssessmentData.TimeToLimitStateFailureOfFunction ?? "0"
        );
        int timeToLimitStatePassingAccrosValve = int.Parse(
            oldAssessmentData.TimeToLimitStatePassingAccrosValve ?? "0"
        );
        // int idImprobable = 1;
        // int idDoubtful = 2;
        // int idExpected = 3;
        // int tp_limit_1 = timePeriod;
        // int tp_limit_2 = timePeriod;
        // int tp_limit_3 = timePeriod;
        List<TimeToLimitStateData> timeToLimitStateList =
            _timeToLimitStateRepository.GetTimeToLimitStateList();
        // Leakage To Atmosphere
        // TP1
        int LeakageToAtmosphereTP1ID = DecidePosTTL(
            timeToLimitStateLeakageToAtmosphere,
            timePeriod
        );
        // timeToLimitStateLeakageToAtmosphere >= 2 * tp_limit_1
        //     ? idImprobable
        //     : timeToLimitStateLeakageToAtmosphere > tp_limit_1 && timeToLimitStateLeakageToAtmosphere < 2 * tp_limit_1
        //         ? idDoubtful
        //         : idExpected;
        double LeakageToAtmosphereTP1IDV =
            timeToLimitStateList.First(x => x.Id == LeakageToAtmosphereTP1ID).LimitStateValue ?? 0;
        double LeakageToAtmosphereTP1IDW =
            timeToLimitStateList.First(x => x.Id == LeakageToAtmosphereTP1ID).Weighting ?? 0;
        // TP 2
        // int timeToLimitStateLeakageToAtmosphereTP2 = timeToLimitStateLeakageToAtmosphere - tp_limit_1;
        int LeakageToAtmosphereTP2ID = DecidePosTTL(
            timeToLimitStateLeakageToAtmosphere,
            timePeriod * 2
        );
        // timeToLimitStateLeakageToAtmosphereTP2 >= 2 * tp_limit_2
        //     ? idImprobable
        //     : timeToLimitStateLeakageToAtmosphereTP2 > tp_limit_2 && timeToLimitStateLeakageToAtmosphereTP2 < 2 * tp_limit_2
        //         ? idDoubtful
        //         : idExpected;
        double LeakageToAtmosphereTP2IDV =
            timeToLimitStateList.First(x => x.Id == LeakageToAtmosphereTP2ID).LimitStateValue ?? 0;
        double LeakageToAtmosphereTP2IDW =
            timeToLimitStateList.First(x => x.Id == LeakageToAtmosphereTP2ID).Weighting ?? 0;
        // TP 3
        // int timeToLimitStateLeakageToAtmosphereTP3 = timeToLimitStateLeakageToAtmosphereTP2 - tp_limit_2;
        int LeakageToAtmosphereTP3ID = DecidePosTTL(
            timeToLimitStateLeakageToAtmosphere,
            timePeriod * 3
        );
        // timeToLimitStateLeakageToAtmosphereTP3 >= 2 * tp_limit_3
        //     ? idImprobable
        //     : timeToLimitStateLeakageToAtmosphereTP3 > tp_limit_3 && timeToLimitStateLeakageToAtmosphereTP3 < 2 * tp_limit_3
        //         ? idDoubtful
        //         : idExpected;
        double LeakageToAtmosphereTP3IDV =
            timeToLimitStateList.First(x => x.Id == LeakageToAtmosphereTP3ID).LimitStateValue ?? 0;
        double LeakageToAtmosphereTP3IDW =
            timeToLimitStateList.First(x => x.Id == LeakageToAtmosphereTP3ID).Weighting ?? 0;

        // Failure Of Function
        // TP 1
        int FailureOfFunctionTP1ID = DecidePosTTL(timeToLimitStateFailureOfFunction, timePeriod);
        // timeToLimitStateFailureOfFunction >= 2 * tp_limit_1
        //     ? idImprobable
        //     : timeToLimitStateFailureOfFunction > tp_limit_1 && timeToLimitStateFailureOfFunction < 2 * tp_limit_1
        //         ? idDoubtful
        //         : idExpected;
        double FailureOfFunctionTP1IDV =
            timeToLimitStateList.First(x => x.Id == FailureOfFunctionTP1ID).LimitStateValue ?? 0;
        double FailureOfFunctionTP1IDW =
            timeToLimitStateList.First(x => x.Id == FailureOfFunctionTP1ID).Weighting ?? 0;
        // TP 2
        // int timeToLimitStateFailureOfFunctionTP2 = timeToLimitStateFailureOfFunction - tp_limit_1;
        int FailureOfFunctionTP2ID = DecidePosTTL(
            timeToLimitStateFailureOfFunction,
            timePeriod * 2
        );
        // timeToLimitStateFailureOfFunctionTP2 >= 2 * tp_limit_2
        //     ? idImprobable
        //     : timeToLimitStateFailureOfFunctionTP2 > tp_limit_2 && timeToLimitStateFailureOfFunctionTP2 < 2 * tp_limit_2
        //         ? idDoubtful
        //         : idExpected;
        double FailureOfFunctionTP2IDV =
            timeToLimitStateList.First(x => x.Id == FailureOfFunctionTP2ID).LimitStateValue ?? 0;
        double FailureOfFunctionTP2IDW =
            timeToLimitStateList.First(x => x.Id == FailureOfFunctionTP2ID).Weighting ?? 0;
        // TP 3
        // int timeToLimitStateFailureOfFunctionTP3 = timeToLimitStateFailureOfFunctionTP2 - tp_limit_2;
        int FailureOfFunctionTP3ID = DecidePosTTL(
            timeToLimitStateFailureOfFunction,
            timePeriod * 3
        );
        // timeToLimitStateFailureOfFunctionTP3 >= 2 * tp_limit_3
        //     ? idImprobable
        //     : timeToLimitStateFailureOfFunctionTP3 > tp_limit_3 && timeToLimitStateFailureOfFunctionTP3 < 2 * tp_limit_3
        //         ? idDoubtful
        //         : idExpected;
        double FailureOfFunctionTP3IDV =
            timeToLimitStateList.First(x => x.Id == FailureOfFunctionTP3ID).LimitStateValue ?? 0;
        double FailureOfFunctionTP3IDW =
            timeToLimitStateList.First(x => x.Id == FailureOfFunctionTP3ID).Weighting ?? 0;

        // Passing Accros Valve
        // TP 1
        int PassingAccrosValveTP1ID = DecidePosTTL(timeToLimitStatePassingAccrosValve, timePeriod);
        // timeToLimitStatePassingAccrosValve >= 2 * tp_limit_1
        //     ? idImprobable
        //     : timeToLimitStatePassingAccrosValve > tp_limit_1 && timeToLimitStatePassingAccrosValve < 2 * tp_limit_1
        //         ? idDoubtful
        //         : idExpected;
        double PassingAccrosValveTP1IDV =
            timeToLimitStateList.First(x => x.Id == PassingAccrosValveTP1ID).LimitStateValue ?? 0;
        double PassingAccrosValveTP1IDW =
            timeToLimitStateList.First(x => x.Id == PassingAccrosValveTP1ID).Weighting ?? 0;
        // TP 2
        // int timeToLimitStatePassingAccrosValveTP2 = timeToLimitStatePassingAccrosValve - tp_limit_1;
        int PassingAccrosValveTP2ID = DecidePosTTL(
            timeToLimitStatePassingAccrosValve,
            timePeriod * 2
        );
        // timeToLimitStatePassingAccrosValveTP2 >= 2 * tp_limit_2
        //     ? idImprobable
        //     : timeToLimitStatePassingAccrosValveTP2 > tp_limit_2 && timeToLimitStatePassingAccrosValveTP2 < 2 * tp_limit_2
        //         ? idDoubtful
        //         : idExpected;
        double PassingAccrosValveTP2IDV =
            timeToLimitStateList.First(x => x.Id == PassingAccrosValveTP2ID).LimitStateValue ?? 0;
        double PassingAccrosValveTP2IDW =
            timeToLimitStateList.First(x => x.Id == PassingAccrosValveTP2ID).Weighting ?? 0;
        // TP 3
        // int timeToLimitStatePassingAccrosValveTP3 = timeToLimitStatePassingAccrosValveTP2 - tp_limit_2;
        int PassingAccrosValveTP3ID = DecidePosTTL(
            timeToLimitStatePassingAccrosValve,
            timePeriod * 3
        );
        // timeToLimitStatePassingAccrosValveTP3 >= 2 * tp_limit_3
        //     ? idImprobable
        //     : timeToLimitStatePassingAccrosValveTP3 > tp_limit_3 && timeToLimitStatePassingAccrosValveTP3 < 2 * tp_limit_3
        //         ? idDoubtful
        //         : idExpected;
        double PassingAccrosValveTP3IDV =
            timeToLimitStateList.First(x => x.Id == PassingAccrosValveTP3ID).LimitStateValue ?? 0;
        double PassingAccrosValveTP3IDW =
            timeToLimitStateList.First(x => x.Id == PassingAccrosValveTP3ID).Weighting ?? 0;
        // Fill the newAssessmentData
        newAssessmentData.LeakageToAtmosphereTP1ID = LeakageToAtmosphereTP1ID;
        newAssessmentData.LeakageToAtmosphereTP2ID = LeakageToAtmosphereTP2ID;
        newAssessmentData.LeakageToAtmosphereTP3ID = LeakageToAtmosphereTP3ID;
        newAssessmentData.FailureOfFunctionTP1ID = FailureOfFunctionTP1ID;
        newAssessmentData.FailureOfFunctionTP2ID = FailureOfFunctionTP2ID;
        newAssessmentData.FailureOfFunctionTP3ID = FailureOfFunctionTP3ID;
        newAssessmentData.PassingAccrosValveTP1ID = PassingAccrosValveTP1ID;
        newAssessmentData.PassingAccrosValveTP2ID = PassingAccrosValveTP2ID;
        newAssessmentData.PassingAccrosValveTP3ID = PassingAccrosValveTP3ID;
        /**
        LF 3 Contains
        - Inspection Effectiveness
        */
        List<InspectionEffectivenessData> inspectionEffectivenessList =
            _inspectionEffectivenessRepository.GetInspectionEffectivenessList();
        double InspectionEffectivenessV =
            inspectionEffectivenessList
                .First(x => x.Id == latestInspection.InspectionEffectivenessID)
                .EffectivenessValue ?? 0;
        double InspectionEffectivenessW =
            inspectionEffectivenessList
                .First(x => x.Id == latestInspection.InspectionEffectivenessID)
                .Weighting ?? 0;
        newAssessmentData.InspectionEffectivenessID = latestInspection.InspectionEffectivenessID;
        /**
        LF 4 - 7 Contains
        4: Impact Of Internal Fluid Impurities
        5: Impact Of Operating Envelopes
        6: Used Within OEM Specification
        7: Repaired
        */
        List<ImpactEffectData> impactEffectList = _impactEffectRepository.GetImpactEffectList();
        double ImpactOfInternalFluidImpuritiesV =
            impactEffectList
                .First(x => x.Id == oldAssessmentData.ImpactOfInternalFluidImpuritiesID)
                .ImpactEffectValue ?? 0;
        double ImpactOfInternalFluidImpuritiesW =
            impactEffectList
                .First(x => x.Id == oldAssessmentData.ImpactOfInternalFluidImpuritiesID)
                .Weighting ?? 0;
        double ImpactOfOperatingEnvelopesV =
            impactEffectList
                .First(x => x.Id == oldAssessmentData.ImpactOfOperatingEnvelopesID)
                .ImpactEffectValue ?? 0;
        double ImpactOfOperatingEnvelopesW =
            impactEffectList
                .First(x => x.Id == oldAssessmentData.ImpactOfOperatingEnvelopesID)
                .Weighting2 ?? 0;
        // newAssessmentData.ImpactOfInternalFluidImpuritiesID =
        //     oldAssessmentData.ImpactOfInternalFluidImpuritiesID;
        // newAssessmentData.ImpactOfOperatingEnvelopesID =
        //     oldAssessmentData.ImpactOfOperatingEnvelopesID;

        List<UsedWithinOEMSpecificationData> usedWithinOEMSpecificationList =
            _usedWithinOEMSpecificationRepository.GetUsedWithinOEMSpecificationList();
        double UsedWithinOEMSpecificationV =
            usedWithinOEMSpecificationList
                .First(x => x.Id == oldAssessmentData.UsedWithinOEMSpecificationID)
                .UsedWithinOEMSpecificationValue ?? 0;
        double UsedWithinOEMSpecificationW =
            usedWithinOEMSpecificationList
                .First(x => x.Id == oldAssessmentData.UsedWithinOEMSpecificationID)
                .Weighting ?? 0;
        // newAssessmentData.UsedWithinOEMSpecificationID =
        //     oldAssessmentData.UsedWithinOEMSpecificationID;

        List<RepairedData> repairedList = _repairedRepository.GetRepairedList();
        double RepairedV =
            repairedList.First(x => x.Id == oldAssessmentData.RepairedID).RepairedValue ?? 0;
        double RepairedW =
            repairedList.First(x => x.Id == oldAssessmentData.RepairedID).Weighting ?? 0;
        // newAssessmentData.RepairedID = oldAssessmentData.RepairedID;
        // START CALCULATE
        double LoFScoreLeakageToAtmophereTP1 =
            (LeakageToAtmosphereTP1IDV * LeakageToAtmosphereTP1IDW)
            * (
                (LeakageToAtmosphereV * LeakageToAtmosphereW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScoreLeakageToAtmophereTP1 = LoFScoreLeakageToAtmophereTP1;
        double LoFScoreLeakageToAtmophereTP1XPos = CalculateXPos(LoFScoreLeakageToAtmophereTP1);
        double LoFScoreLeakageToAtmophereTP2 =
            (LeakageToAtmosphereTP2IDV * LeakageToAtmosphereTP2IDW)
            * (
                (LeakageToAtmosphereV * LeakageToAtmosphereW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScoreLeakageToAtmophereTP2 = LoFScoreLeakageToAtmophereTP2;
        double LoFScoreLeakageToAtmophereTP2XPos = CalculateXPos(LoFScoreLeakageToAtmophereTP2);
        double LoFScoreLeakageToAtmophereTP3 =
            (LeakageToAtmosphereTP3IDV * LeakageToAtmosphereTP3IDW)
            * (
                (LeakageToAtmosphereV * LeakageToAtmosphereW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScoreLeakageToAtmophereTP3 = LoFScoreLeakageToAtmophereTP3;
        double LoFScoreLeakageToAtmophereTP3XPos = CalculateXPos(LoFScoreLeakageToAtmophereTP3);
        double LoFScoreFailureOfFunctionTP1 =
            (FailureOfFunctionTP1IDV * FailureOfFunctionTP1IDW)
            * (
                (FailureOfFunctionV * FailureOfFunctionW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScoreFailureOfFunctionTP1 = LoFScoreFailureOfFunctionTP1;
        double LoFScoreFailureOfFunctionTP1XPos = CalculateXPos(LoFScoreFailureOfFunctionTP1);
        double LoFScoreFailureOfFunctionTP2 =
            (FailureOfFunctionTP2IDV * FailureOfFunctionTP2IDW)
            * (
                (FailureOfFunctionV * FailureOfFunctionW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScoreFailureOfFunctionTP2 = LoFScoreFailureOfFunctionTP2;
        double LoFScoreFailureOfFunctionTP2XPos = CalculateXPos(LoFScoreFailureOfFunctionTP2);
        double LoFScoreFailureOfFunctionTP3 =
            (FailureOfFunctionTP3IDV * FailureOfFunctionTP3IDW)
            * (
                (FailureOfFunctionV * FailureOfFunctionW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScoreFailureOfFunctionTP3 = LoFScoreFailureOfFunctionTP3;
        double LoFScoreFailureOfFunctionTP3XPos = CalculateXPos(LoFScoreFailureOfFunctionTP3);
        double LoFScorePassingAccrosValveTP1 =
            (PassingAccrosValveTP1IDV * PassingAccrosValveTP1IDW)
            * (
                (PassingAccrosValveV * PassingAccrosValveW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScorePassingAccrosValveTP1 = LoFScorePassingAccrosValveTP1;
        double LoFScorePassingAccrosValveTP1XPos = CalculateXPos(LoFScorePassingAccrosValveTP1);
        double LoFScorePassingAccrosValveTP2 =
            (PassingAccrosValveTP2IDV * PassingAccrosValveTP2IDW)
            * (
                (PassingAccrosValveV * PassingAccrosValveW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScorePassingAccrosValveTP2 = LoFScorePassingAccrosValveTP2;
        double LoFScorePassingAccrosValveTP2XPos = CalculateXPos(LoFScorePassingAccrosValveTP2);
        double LoFScorePassingAccrosValveTP3 =
            (PassingAccrosValveTP3IDV * PassingAccrosValveTP3IDW)
            * (
                (PassingAccrosValveV * PassingAccrosValveW)
                + (InspectionEffectivenessV * InspectionEffectivenessW)
                + (ImpactOfInternalFluidImpuritiesV * ImpactOfInternalFluidImpuritiesW)
                + (ImpactOfOperatingEnvelopesV * ImpactOfOperatingEnvelopesW)
                + (UsedWithinOEMSpecificationV * UsedWithinOEMSpecificationW)
                + (RepairedV * RepairedW)
            );
        newAssessmentData.LoFScorePassingAccrosValveTP3 = LoFScorePassingAccrosValveTP3;
        double LoFScorePassingAccrosValveTP3XPos = CalculateXPos(LoFScorePassingAccrosValveTP3);
        // Calculate COF
        List<HSSEDefinisionData> hSSEDefinisionList =
            _hSSEDefinisionRepository.GetHSSEDefinisionList();
        double COFByPLD = double.Parse(oldAssessmentData.ProductLossDefinition ?? "0");
        HSSEDefinisionData hssebypld = hSSEDefinisionList
            .Where(x => x.MinBBSValue <= COFByPLD)
            .OrderByDescending(x => x.MinBBSValue)
            .First();
        int COFByHSSE = oldAssessmentData.HSSEDefinisionID ?? 5;
        HSSEDefinisionData hssebyhsse = hSSEDefinisionList.Where(x => x.Id == COFByHSSE).First();
        string cof =
            (
                string.Compare(hssebypld.CoFCategory, hssebyhsse.CoFCategory) > 0
                    ? hssebypld.CoFCategory
                    : hssebyhsse.CoFCategory
            ) ?? "A";
        // Logging COF
        LogClass logclass2a = new();
        logclass2a.Module = "XX==ASSESSMENT-2A";
        logclass2a.CreatedAt = logdatetime;
        logclass2a.Message = assessmentID.ToString();
        string[] logg =
        [
            $"COFByPLD: {COFByPLD}",
            $"hssebypld.CoFCategory: {hssebypld?.CoFCategory}",
            $"hssebyhsse.CoFCategory: {hssebyhsse?.CoFCategory}",
            $"Final COF: {cof}"
        ];
        logclass2a.Data = JsonConvert.SerializeObject(logg);
        _logRepository.AddLog(logclass2a);
        // START LOGGING
        LogClass logclass2 = new();
        logclass2.Module = "XX==ASSESSMENT-2";
        logclass2.CreatedAt = logdatetime;
        logclass2.Message = assessmentID.ToString();
        logclass2.Data = JsonConvert.SerializeObject(newAssessmentData);
        _logRepository.AddLog(logclass2);
        // END LOGGING
        newAssessmentData.ConsequenceOfFailure = cof;
        // START LOGGING
        LogClass logclass3 = new();
        logclass3.Module = "XX==ASSESSMENT-3";
        logclass3.CreatedAt = logdatetime;
        logclass3.Message = assessmentID.ToString();
        logclass3.Data = JsonConvert.SerializeObject(newAssessmentData);
        _logRepository.AddLog(logclass3);
        // END LOGGING
        Dictionary<string, int> cof_rac =
            new()
            {
                { "A", 3769 },
                { "B", 891 },
                { "C", 891 },
                { "D", 49 },
                { "E", 49 }
            };
        int rac = cof_rac[cof];
        var cofToYpos = new Dictionary<string, double>
        {
            { "E", 10 },
            { "D", 30 },
            { "C", 50 },
            { "B", 70 },
            { "A", 90 }
        };
        double ypos = cofToYpos.TryGetValue(cof, out double value) ? value : 0;
        string TP1A =
            Math.Min(Math.Ceiling(LoFScoreLeakageToAtmophereTP1XPos / 20) + 0, 5).ToString() + cof;
        string TP2A =
            Math.Min(Math.Ceiling(LoFScoreLeakageToAtmophereTP2XPos / 20) + 0, 5).ToString() + cof;
        string TP3A =
            Math.Min(Math.Ceiling(LoFScoreLeakageToAtmophereTP3XPos / 20) + 0, 5).ToString() + cof;
        string TP1B =
            Math.Min(Math.Ceiling(LoFScoreFailureOfFunctionTP1XPos / 20) + 0, 5).ToString() + cof;
        string TP2B =
            Math.Min(Math.Ceiling(LoFScoreFailureOfFunctionTP2XPos / 20) + 0, 5).ToString() + cof;
        string TP3B =
            Math.Min(Math.Ceiling(LoFScoreFailureOfFunctionTP3XPos / 20) + 0, 5).ToString() + cof;
        string TP1C =
            Math.Min(Math.Ceiling(LoFScorePassingAccrosValveTP1XPos / 20) + 0, 5).ToString() + cof;
        string TP2C =
            Math.Min(Math.Ceiling(LoFScorePassingAccrosValveTP2XPos / 20) + 0, 5).ToString() + cof;
        string TP3C =
            Math.Min(Math.Ceiling(LoFScorePassingAccrosValveTP3XPos / 20) + 0, 5).ToString() + cof;
        int TP1Riskv = Math.Max(
            Math.Max(int.Parse(TP1A[..1]), int.Parse(TP1B[..1])),
            int.Parse(TP1C[..1])
        );
        string TP1Risk = TP1Riskv.ToString() + TP1A[1..];
        int TP2Riskv = Math.Max(
            Math.Max(int.Parse(TP2A[..1]), int.Parse(TP2B[..1])),
            int.Parse(TP2C[..1])
        );
        string TP2Risk = TP2Riskv.ToString() + TP1B[1..];
        int TP3Riskv = Math.Max(
            Math.Max(int.Parse(TP3A[..1]), int.Parse(TP3B[..1])),
            int.Parse(TP3C[..1])
        );
        string TP3Risk = TP3Riskv.ToString() + TP1C[1..];
        newAssessmentData.TP1A = TP1A;
        newAssessmentData.TP2A = TP2A;
        newAssessmentData.TP3A = TP3A;
        newAssessmentData.TP1B = TP1B;
        newAssessmentData.TP2B = TP2B;
        newAssessmentData.TP3B = TP3B;
        newAssessmentData.TP1C = TP1C;
        newAssessmentData.TP2C = TP2C;
        newAssessmentData.TP3C = TP3C;
        newAssessmentData.TP1Risk = TP1Risk;
        newAssessmentData.TP2Risk = TP2Risk;
        newAssessmentData.TP3Risk = TP3Risk;
        int tp_limit_1 = timePeriod;
        int tp_limit_2 = timePeriod * 2;
        int tp_limit_3 = timePeriod * 3;
        double loftp1aval = DecideTTARAC(
            LoFScoreLeakageToAtmophereTP1,
            LoFScoreLeakageToAtmophereTP2,
            LoFScoreLeakageToAtmophereTP3,
            tp_limit_1,
            tp_limit_2,
            tp_limit_3,
            rac,
            LeakageToAtmosphereTP1ID,
            LeakageToAtmosphereTP2ID,
            LeakageToAtmosphereTP3ID
        );
        DateTime loftp1adate = lastInspectionDate.AddMonths((int)Math.Floor(loftp1aval));
        newAssessmentData.TPTimeToActionA = loftp1adate.ToString(
            SharedEnvironment.GetDateFormatString(false)
        );
        double loftp2aval = DecideTTARAC(
            LoFScoreFailureOfFunctionTP1,
            LoFScoreFailureOfFunctionTP2,
            LoFScoreFailureOfFunctionTP3,
            tp_limit_1,
            tp_limit_2,
            tp_limit_3,
            rac,
            FailureOfFunctionTP1ID,
            FailureOfFunctionTP2ID,
            FailureOfFunctionTP3ID
        );
        DateTime loftp2adate = lastInspectionDate.AddMonths((int)Math.Floor(loftp2aval));
        newAssessmentData.TPTimeToActionB = loftp2adate.ToString(
            SharedEnvironment.GetDateFormatString(false)
        );
        double loftp3aval = DecideTTARAC(
            LoFScorePassingAccrosValveTP1,
            LoFScorePassingAccrosValveTP2,
            LoFScorePassingAccrosValveTP3,
            tp_limit_1,
            tp_limit_2,
            tp_limit_3,
            rac,
            PassingAccrosValveTP1ID,
            PassingAccrosValveTP2ID,
            PassingAccrosValveTP3ID
        );
        DateTime loftp3adate = lastInspectionDate.AddMonths((int)Math.Floor(loftp3aval));
        newAssessmentData.TPTimeToActionC = loftp3adate.ToString(
            SharedEnvironment.GetDateFormatString(false)
        );
        DateTime tptimetoactionrisk = new[] { loftp1adate, loftp2adate, loftp3adate }.Min();
        newAssessmentData.TPTimeToActionRisk = tptimetoactionrisk.ToString(
            SharedEnvironment.GetDateFormatString(false)
        );
        newAssessmentData.TimeToAction = oldAssessmentData.TimeToAction;
        AssessmentData finalAssessmentData = UpdateAssessment(newAssessmentData);
        return finalAssessmentData;
    }

    private static double CalculateXPos(double value)
    {
        double gapx = 20;
        double cat1min = 16;
        double cat1max = 49;
        double cat2min = cat1max + 1;
        double cat2max = 210;
        double cat3min = cat2max + 1;
        double cat3max = 891;
        double cat4min = cat3max + 1;
        double cat4max = 3769;
        double cat5min = cat4max + 1;
        double cat5max = 16050;

        double xpos = 0;
        if (value < cat1min)
        {
            xpos = 0;
        }
        else if (value <= cat1max)
        {
            xpos = ((value - cat1min) / (cat1max - cat1min) * 20) + (gapx * 0);
        }
        else if (value <= cat2max)
        {
            xpos = ((value - cat2min) / (cat2max - cat2min) * 20) + (gapx * 1);
        }
        else if (value <= cat3max)
        {
            xpos = ((value - cat3min) / (cat3max - cat3min) * 20) + (gapx * 2);
        }
        else if (value <= cat4max)
        {
            xpos = ((value - cat4min) / (cat4max - cat4min) * 20) + (gapx * 3);
        }
        else if (value <= cat5max)
        {
            xpos = ((value - cat5min) / (cat5max - cat5min) * 20) + (gapx * 4);
        }
        else
        {
            xpos = 100;
        }
        return xpos;
    }

    private static int DecidePosTTL(double ls, double tp)
    {
        int idImprobable = 1;
        int idDoubtful = 2;
        int idExpected = 3;
        if (ls >= 2 * tp)
        {
            return idImprobable;
        }
        else if (ls > tp && ls < 2 * tp)
        {
            return idDoubtful;
        }
        else
        {
            return idExpected;
        }
    }

    public static double DecideTTARAC(
        double loftp1,
        double loftp2,
        double loftp3,
        double timelimittp1,
        double timelimittp2,
        double timelimittp3,
        double rac,
        double loftp1val,
        double loftp2val,
        double loftp3val
    )
    {
        double calculate;
        if (loftp1 > rac)
        {
            calculate = 0;
        }
        else
        {
            if (loftp3 > rac && loftp2 <= rac)
            {
                calculate = (int)
                    Math.Floor(
                        (
                            (timelimittp3 - timelimittp2)
                                / 12.0
                                * (rac - loftp2)
                                / (loftp3 - loftp2)
                            + (timelimittp2 / 12.0)
                        ) * 12
                    );
            }
            else if (loftp2 > rac)
            {
                calculate = (int)
                    Math.Floor(
                        (
                            (timelimittp2 - timelimittp1)
                                / 12.0
                                * (rac - loftp1)
                                / (loftp2 - loftp1)
                            + (timelimittp1 / 12.0)
                        ) * 12
                    );
            }
            else
            {
                calculate = DecideTTL(
                    loftp1val,
                    loftp2val,
                    loftp3val,
                    timelimittp1,
                    timelimittp2,
                    timelimittp3
                );
            }
        }
        return calculate;
    }

    public static double DecideTTL(
        double tp1,
        double tp2,
        double tp3,
        double tp1val,
        double tp2val,
        double tp3val
    )
    {
        if (tp1 == 1 && tp2 == 1 && tp3 == 1)
        {
            return tp3val;
        }
        else if (tp1 == 1 && tp2 == 1 && tp3 == 2)
        {
            return tp3val;
        }
        else if (tp1 == 1 && tp2 == 2 && tp3 == 2)
        {
            return tp2val;
        }
        else if (tp1 == 2 && tp2 == 2 && tp3 == 2)
        {
            return (tp2val + tp3val) / 2;
        }
        else if (tp1 == 2 && tp2 == 2 && tp3 == 3)
        {
            return (tp1val + tp2val) / 2;
        }
        else if (tp1 == 2 && tp2 == 3 && tp3 == 3)
        {
            return tp1val;
        }
        else
        {
            return 0;
        }
    }

    public AssessmentData UpdateAssessment(AssessmentClass assessment)
    {
        try
        {
            AssessmentData assessmentData = _assessmentRepository.UpdateAssessment(assessment);
            assessmentData.Asset = _assetRepository.GetAsset(assessmentData.AssetID);
            assessmentData.InspectionHistory =
                _assessmentInspectionRepository.GetAssessmentInspectionList(assessmentData.Id);
            foreach (var inspection in assessmentData.InspectionHistory)
            {
                inspection.Inspection = _inspectionRepository.GetInspection(
                    inspection.InspectionID ?? 0
                );
            }
            assessmentData.MaintenanceHistory =
                _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessmentData.Id);
            foreach (var maintenance in assessmentData.MaintenanceHistory)
            {
                maintenance.Maintenance = _maintenanceRepository.GetMaintenance(
                    maintenance.MaintenanceID ?? 0
                );
            }
            return assessmentData;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public AssessmentData DeleteAssessment(AssessmentClass assessment)
    {
        try
        {
            return _assessmentRepository.DeleteAssessment(assessment);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private ToolImportClass MapAssessmentRegister(List<Dictionary<string, string>> datas)
    {
        ToolImportClass toolImport = new();
        List<string> failedRecords = [];
        List<AssetData> assetList = _assetRepository.GetAssetList();
        List<ImpactEffectData> impactEffectList = _impactEffectRepository.GetImpactEffectList();
        List<UsedWithinOEMSpecificationData> usedWithinOEMSpecificationList =
            _usedWithinOEMSpecificationRepository.GetUsedWithinOEMSpecificationList();
        List<RepairedData> repairedList = _repairedRepository.GetRepairedList();
        List<HSSEDefinisionData> hSSEDefinisionList =
            _hSSEDefinisionRepository.GetHSSEDefinisionList();
        List<Dictionary<string, string>> finalresult = [];
        foreach (var records in datas)
        {
            int timeperiod = 0;
            Dictionary<string, string> result = [];
            foreach (var record in records)
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
                            List<string> dateParts = date.Split("/").ToList();
                            string newDate = "";
                            if (dateParts[0].Length == 1)
                            {
                                dateParts[0] = "0" + dateParts[0];
                            }
                            if (dateParts[1].Length == 1)
                            {
                                dateParts[1] = "0" + dateParts[1];
                            }
                            date = string.Join("/", dateParts);
                            string[] formats = ["dd/MM/yyyy", "MM/dd/yyyy"];
                            if (
                                DateTime.TryParseExact(
                                    date,
                                    formats,
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out DateTime parsedDate
                                )
                            )
                            {
                                if (parsedDate.Day == int.Parse(date.Split('/')[0]))
                                {
                                    newDate =
                                        dateParts[0] + "-" + dateParts[1] + "-" + dateParts[2];
                                }
                                else
                                {
                                    newDate =
                                        dateParts[1] + "-" + dateParts[0] + "-" + dateParts[2];
                                }
                            }
                            else
                            {
                                newDate = "01-01-1900";
                            }
                            Console.WriteLine("Mapped Value: " + string.Join(", ", dateParts));
                            mappedValue = newDate;
                        }
                        else
                        {
                            mappedValue = DateTime
                                .FromOADate(Convert.ToDouble(value))
                                .ToString(SharedEnvironment.GetDateFormatString(false));
                        }
                        Console.WriteLine("Mapped Value: " + value);
                        Console.WriteLine("Mapped Value: " + mappedValue);
                    }
                    else if (
                        mappedKey.Equals("ImpactOfInternalFluidImpuritiesID")
                        || mappedKey.Equals("ImpactOfOperatingEnvelopesID")
                    )
                    {
                        foreach (var impactEffect in impactEffectList)
                        {
                            if (impactEffect.ImpactEffect == null)
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
                            if (usedWithinOEMSpecification.UsedWithinOEMSpecification == null)
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
            "lf2 - time to limit state leakage to atmosphere \n(month)"
                => "TimeToLimitStateLeakageToAtmosphere",
            "lf2 - time to limit state failure of function (month)"
                => "TimeToLimitStateFailureOfFunction",
            "lf2 - time to limit state passing accros valve (month)"
                => "TimeToLimitStatePassingAccrosValve",
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

    public void AddMaintenanceToAssessment(
        int assessmentId,
        List<int> maintenanceIds,
        bool update = false
    )
    {
        if (update)
        {
            _assessmentMaintenanceRepository.DeleteAssessmentMaintenance(assessmentId);
        }
        _assessmentMaintenanceRepository.AddMaintenanceToAssessment(assessmentId, maintenanceIds);
    }

    public List<AssessmentMaintenanceData> GetAssessmentMaintenanceDatas(int maintenanceid)
    {
        return _assessmentMaintenanceRepository.GetAssessmentMaintenanceListByMaintenanceID(
            maintenanceid
        );
    }

    public void AddInspectionToAssessment(
        int assessmentId,
        List<int> inspectionIds,
        bool update = false
    )
    {
        if (update)
        {
            _assessmentInspectionRepository.DeleteAssessmentInspection(assessmentId);
        }
        _assessmentInspectionRepository.AddInspectionToAssessment(assessmentId, inspectionIds);
    }

    public List<AssessmentInspectionData> GetAssessmentInspectionDatas(int inspectionid)
    {
        return _assessmentInspectionRepository.GetAssessmentInspectionListByInspectionID(
            inspectionid
        );
    }

    public Dictionary<string, string> ImportAssessment(
        List<Dictionary<string, string>> data,
        int CreatedBy
    )
    {
        ToolImportClass toolImport = MapAssessmentRegister(data);
        if (toolImport.MappedRecords == null || toolImport.MappedRecords.Count == 0)
        {
            throw new Exception("Failed to import assessment data");
        }
        List<Dictionary<string, string>> result = toolImport.MappedRecords;
        int total = 0;
        int success = 0;
        int failed = 0;
        List<string> failedDatas = [];

        foreach (var item in result)
        {
            if (item == null)
                continue;
            total++;
            AssessmentClass? assessment = null;
            try
            {
                string json = JsonConvert.SerializeObject(item);
                assessment = JsonConvert.DeserializeObject<AssessmentClass>(json);
                if (assessment == null)
                    continue;
                assessment.IsDeleted = false;
                assessment.CreatedBy = CreatedBy;
                assessment.CreatedAt = DateTime.Now.ToString(
                    SharedEnvironment.GetDateFormatString()
                );
                AssessmentData? assessmentData = _assessmentRepository.AddAssessment(assessment);
            }
            catch (Exception e)
            {
                LogData log =
                    new()
                    {
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
        if (toolImport.FailedRecords != null && toolImport.FailedRecords.Count > 0)
        {
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
        return new Dictionary<string, string>
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
    )
    {
        List<AssessmentData> assessmentDataList = _assessmentRepository
            .GetAssessmentList(false)
            .Where(x => x.IsDeleted == IncludeDeleted)
            .Where(x => x.TP1Risk != null)
            .GroupBy(x => x.AssetID)
            .Select(x =>
                x.OrderByDescending(a =>
                        DateTime.ParseExact(
                            a.AssessmentDate ?? "01-01-1900",
                            SharedEnvironment.GetDateFormatString(false),
                            CultureInfo.InvariantCulture
                        )
                    )
                    .First()
            )
            .ToList();
        foreach (var assessment in assessmentDataList)
        {
            assessment.Asset = _assetRepository.GetAsset(assessment.AssetID);
            assessment.Asset.PlatformData = _platformRepository.GetPlatform(
                assessment.Asset.PlatformID ?? 0
            );
            assessment.InspectionHistory =
                _assessmentInspectionRepository.GetAssessmentInspectionList(assessment.Id);
            foreach (var inspection in assessment.InspectionHistory)
            {
                inspection.Inspection = _inspectionRepository.GetInspection(
                    inspection.InspectionID ?? 0
                );
            }
            if (assessment.InspectionHistory != null && assessment.InspectionHistory.Any())
            {
                // var lastInspection = assessment.InspectionHistory.Last().Inspection;
                var lastInspection = assessment
                    .InspectionHistory.OrderByDescending(i =>
                        DateTime.ParseExact(
                            i.Inspection?.InspectionDate ?? "01-01-1900",
                            SharedEnvironment.GetDateFormatString(false),
                            CultureInfo.InvariantCulture
                        )
                    )
                    .First()
                    .Inspection;
                if (lastInspection != null)
                {
                    assessment.LastInspectionId = lastInspection.Id;
                    assessment.LastInspectionDate = lastInspection.InspectionDate;
                }
            }
            assessment.MaintenanceHistory =
                _assessmentMaintenanceRepository.GetAssessmentMaintenanceList(assessment.Id);
            foreach (var maintenance in assessment.MaintenanceHistory)
            {
                maintenance.Maintenance = _maintenanceRepository.GetMaintenance(
                    maintenance.MaintenanceID ?? 0
                );
            }
            if (assessment.MaintenanceHistory != null && assessment.MaintenanceHistory.Any())
            {
                // var lastMaintenance = assessment.MaintenanceHistory.Last().Maintenance;
                var lastMaintenance = assessment
                    .MaintenanceHistory.OrderByDescending(m =>
                        DateTime.ParseExact(
                            m.Maintenance?.MaintenanceDate ?? "01-01-1900",
                            SharedEnvironment.GetDateFormatString(false),
                            CultureInfo.InvariantCulture
                        )
                    )
                    .First()
                    .Maintenance;
                if (lastMaintenance != null)
                {
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

    public Dictionary<string, Dictionary<string, string>> GetAssessmentRecap()
    {
        Dictionary<string, Dictionary<string, string>> recap_final = [];
        Dictionary<string, string> recap_heatmap = [];
        Dictionary<string, string> recap_piechart = [];
        Dictionary<string, Dictionary<string, string>> recap_barchart = [];
        Dictionary<string, string> recap_barchart_integritystatus = [];
        Dictionary<string, string> recap_barchart_convert = [];
        recap_barchart_convert["NBU"] = "";
        recap_barchart_convert["SBU"] = "";
        recap_barchart_convert["CBU"] = "";
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
            string risk = AssessmentStaticClass.ColorRiskMap[
                AssessmentStaticClass.GetHeatColor(assessment.TP1Risk)
            ];
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
            if (assessment.Asset != null)
            {
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
                int curr_risk = int.Parse(
                    recap_barchart[assessment.Asset.BusinessArea ?? ""][risk]
                );
                curr_risk++;
                recap_barchart[assessment.Asset.BusinessArea ?? ""][risk] = curr_risk.ToString();
                switch (assessment.IntegrityStatus)
                {
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
        if (recap_barchart.TryGetValue("NBU", out Dictionary<string, string>? value))
        {
            recap_barchart_convert["NBU"] = JsonConvert.SerializeObject(value);
        }
        if (recap_barchart.TryGetValue("SBU", out Dictionary<string, string>? value2))
        {
            recap_barchart_convert["SBU"] = JsonConvert.SerializeObject(value2);
        }
        if (recap_barchart.TryGetValue("CBU", out Dictionary<string, string>? value3))
        {
            recap_barchart_convert["CBU"] = JsonConvert.SerializeObject(value3);
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

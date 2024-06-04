using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Riskvalve.Models;

public class AssessmentContext : DbContext
{
    public DbSet<AssessmentDB> Assessment { get; set; }
    public DbSet<AssetModel> Asset { get; set; }
    public DbSet<PlatformDB> Platform { get; set; }
    public DbSet<CurrentConditionLimitStateModel> CurrentConditionLimitState { get; set; }
    public DbSet<InspectionEffectivenessModel> InspectionEffectiveness { get; set; }
    public DbSet<ImpactEffectModel> ImpactEffect { get; set; }
    public DbSet<UsedWithinOEMSpecificationModel> UsedWithinOEMSpecification { get; set; }
    public DbSet<RepairedModel> Repaired { get; set; }
    public DbSet<HSSEDefinisionModel> HSSEDefinision { get; set; }
    public DbSet<RecommendationActionModel> RecommendationAction { get; set; }
    public DbSet<TimeToLimitStateModel> TimeToLimitState { get; set; }
    public DbSet<UserModel> User { get; set; }
    public DbSet<AssessmentMaintenanceModel> AssessmentMaintenance { get; set; }
    public DbSet<AssessmentInspectionModel> AssessmentInspection { get; set; }
    public DbSet<InspectionDB> Inspection { get; set; }
    public DbSet<InspectionMethodModel> InspectionMethod { get; set; }
    public DbSet<MaintenanceDB> Maintenance { get; set; }
    public DbSet<IsValveRepairedModel> IsValveRepaired { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class AssessmentDB
{
    public int Id { get; set; }
    public int AssetID { get; set; }
    public string? AssessmentNo { get; set; }
    public string? AssessmentDate { get; set; }
    public string? TimePeriode { get; set; }
    public string? TimeToLimitStateLeakageToAtmosphere { get; set; }
    public string? TimeToLimitStateFailureOfFunction { get; set; }
    public string? TimeToLimitStatePassingAccrosValve { get; set; }
    public int? LeakageToAtmosphereID { get; set; }
    public int? FailureOfFunctionID { get; set; }
    public int? PassingAccrosValveID { get; set; }
    public int? LeakageToAtmosphereTP1ID { get; set; }
    public int? LeakageToAtmosphereTP2ID { get; set; }
    public int? LeakageToAtmosphereTP3ID { get; set; }
    public int? FailureOfFunctionTP1ID { get; set; }
    public int? FailureOfFunctionTP2ID { get; set; }
    public int? FailureOfFunctionTP3ID { get; set; }
    public int? PassingAccrosValveTP1ID { get; set; }
    public int? PassingAccrosValveTP2ID { get; set; }
    public int? PassingAccrosValveTP3ID { get; set; }
    public int? InspectionEffectivenessID { get; set; }
    public int? ImpactOfInternalFluidImpuritiesID { get; set; }
    public int? ImpactOfOperatingEnvelopesID { get; set; }
    public int? UsedWithinOEMSpecificationID { get; set; }
    public int? RepairedID { get; set; }
    public string? ProductLossDefinition { get; set; }
    public int? HSSEDefinisionID { get; set; }
    public string? Summary { get; set; }
    public int? RecommendationActionID { get; set; }
    public string? DetailedRecommendation { get; set; }
    public string? ConsequenceOfFailure { get; set; }
    public string? TP1A { get; set; }
    public string? TP2A { get; set; }
    public string? TP3A { get; set; }
    public string? TP1B { get; set; }
    public string? TP2B { get; set; }
    public string? TP3B { get; set; }
    public string? TP1C { get; set; }
    public string? TP2C { get; set; }
    public string? TP3C { get; set; }
    public string? TPTimeToActionA { get; set; }
    public string? TPTimeToActionB { get; set; }
    public string? TPTimeToActionC { get; set; }
    public string? TP1Risk { get; set; }
    public string? TP2Risk { get; set; }
    public string? TP3Risk { get; set; }
    public string? TPTimeToActionRisk { get; set; }
    public double? LoFScoreLeakageToAtmophereTP1 { get; set; }
    public double? LoFScoreLeakageToAtmophereTP2 { get; set; }
    public double? LoFScoreLeakageToAtmophereTP3 { get; set; }
    public double? LoFScoreFailureOfFunctionTP1 { get; set; }
    public double? LoFScoreFailureOfFunctionTP2 { get; set; }
    public double? LoFScoreFailureOfFunctionTP3 { get; set; }
    public double? LoFScorePassingAccrosValveTP1 { get; set; }
    public double? LoFScorePassingAccrosValveTP2 { get; set; }
    public double? LoFScorePassingAccrosValveTP3 { get; set; }
    public double? CoFScore { get; set; }
    public string? IntegrityStatus { get; set; }
    public bool? IsDeleted { get; set; }
    public string? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public string? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
}

public class AssessmentModel : AssessmentDB
{
    public AssetModel? Asset { get; set; }
    public string? LeakageToAtmosphere { get; set; }
    public string? FailureOfFunction { get; set; }
    public string? PassingAccrosValve { get; set; }
    public string? LeakageToAtmosphereTP1 { get; set; }
    public string? LeakageToAtmosphereTP2 { get; set; }
    public string? LeakageToAtmosphereTP3 { get; set; }
    public string? FailureOfFunctionTP1 { get; set; }
    public string? FailureOfFunctionTP2 { get; set; }
    public string? FailureOfFunctionTP3 { get; set; }
    public string? PassingAccrosValveTP1 { get; set; }
    public string? PassingAccrosValveTP2 { get; set; }
    public string? PassingAccrosValveTP3 { get; set; }
    public string? InspectionEffectiveness { get; set; }
    public string? ImpactOfInternalFluidImpurities { get; set; }
    public string? ImpactOfOperatingEnvelopes { get; set; }
    public string? UsedWithinOEMSpecification { get; set; }
    public string? Repaired { get; set; }
    public string? HSSEDefinision { get; set; }
    public string? RecommendationAction { get; set; }
    public List<InspectionFileModel>? AssessmentFiles { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
    public List<InspectionModel>? InspectionHistory { get; set; }
    public List<MaintenanceModel>? MaintenanceHistory { get; set; }
    public string? RiskMax { get; set; }
    public string? LastInspectionDate { get; set; }
    public string? LastMaintenanceDate { get; set; }
    public int? LastInspectionId { get; set; }
    public int? LastMaintenanceId { get; set; }
    public Dictionary<string, string> ColorRiskMap =
        new()
        {
            { "olive", "Very Low" },
            { "green", "Low" },
            { "yellow", "Medium" },
            { "orange", "High" },
            { "red", "Very High" }
        };
    public Dictionary<string, int> ColorRank =
        new()
        {
            { "olive", 1 },
            { "green", 2 },
            { "yellow", 3 },
            { "orange", 4 },
            { "red", 5 }
        };
    public Dictionary<string, int> RiskRank =
        new()
        {
            { "Very Low", 1 },
            { "Low", 2 },
            { "Medium", 3 },
            { "High", 4 },
            { "Very High", 5 }
        };

    public AssessmentDB GetAssessmentDB(int id)
    {
        AssessmentDB assessmentDB = new();
        using (var context = new AssessmentContext())
        {
            assessmentDB = context
                .Assessment.Where(a => a.Id == id)
                .Select(a => new AssessmentDB
                {
                    Id = a.Id,
                    AssetID = a.AssetID,
                    AssessmentNo = a.AssessmentNo,
                    AssessmentDate = a.AssessmentDate,
                    TimePeriode = a.TimePeriode,
                    TimeToLimitStateLeakageToAtmosphere = a.TimeToLimitStateLeakageToAtmosphere,
                    TimeToLimitStateFailureOfFunction = a.TimeToLimitStateFailureOfFunction,
                    TimeToLimitStatePassingAccrosValve = a.TimeToLimitStatePassingAccrosValve,
                    LeakageToAtmosphereID =
                        a.LeakageToAtmosphereID != 0 ? a.LeakageToAtmosphereID : null,
                    FailureOfFunctionID = a.FailureOfFunctionID != 0 ? a.FailureOfFunctionID : null,
                    PassingAccrosValveID =
                        a.PassingAccrosValveID != 0 ? a.PassingAccrosValveID : null,
                    LeakageToAtmosphereTP1ID =
                        a.LeakageToAtmosphereTP1ID != 0 ? a.LeakageToAtmosphereTP1ID : null,
                    LeakageToAtmosphereTP2ID =
                        a.LeakageToAtmosphereTP2ID != 0 ? a.LeakageToAtmosphereTP2ID : null,
                    LeakageToAtmosphereTP3ID =
                        a.LeakageToAtmosphereTP3ID != 0 ? a.LeakageToAtmosphereTP3ID : null,
                    FailureOfFunctionTP1ID =
                        a.FailureOfFunctionTP1ID != 0 ? a.FailureOfFunctionTP1ID : null,
                    FailureOfFunctionTP2ID =
                        a.FailureOfFunctionTP2ID != 0 ? a.FailureOfFunctionTP2ID : null,
                    FailureOfFunctionTP3ID =
                        a.FailureOfFunctionTP3ID != 0 ? a.FailureOfFunctionTP3ID : null,
                    PassingAccrosValveTP1ID =
                        a.PassingAccrosValveTP1ID != 0 ? a.PassingAccrosValveTP1ID : null,
                    PassingAccrosValveTP2ID =
                        a.PassingAccrosValveTP2ID != 0 ? a.PassingAccrosValveTP2ID : null,
                    PassingAccrosValveTP3ID =
                        a.PassingAccrosValveTP3ID != 0 ? a.PassingAccrosValveTP3ID : null,
                    InspectionEffectivenessID =
                        a.InspectionEffectivenessID != 0 ? a.InspectionEffectivenessID : null,
                    ImpactOfInternalFluidImpuritiesID =
                        a.ImpactOfInternalFluidImpuritiesID != 0
                            ? a.ImpactOfInternalFluidImpuritiesID
                            : null,
                    ImpactOfOperatingEnvelopesID =
                        a.ImpactOfOperatingEnvelopesID != 0 ? a.ImpactOfOperatingEnvelopesID : null,
                    UsedWithinOEMSpecificationID =
                        a.UsedWithinOEMSpecificationID != 0 ? a.UsedWithinOEMSpecificationID : null,
                    RepairedID = a.RepairedID != 0 ? a.RepairedID : null,
                    HSSEDefinisionID = a.HSSEDefinisionID != 0 ? a.HSSEDefinisionID : null,
                    RecommendationActionID =
                        a.RecommendationActionID != 0 ? a.RecommendationActionID : null,
                    ProductLossDefinition = a.ProductLossDefinition,
                    Summary = a.Summary,
                    DetailedRecommendation = a.DetailedRecommendation,
                    ConsequenceOfFailure = a.ConsequenceOfFailure,
                    TP1A = a.TP1A,
                    TP2A = a.TP2A,
                    TP3A = a.TP3A,
                    TP1B = a.TP1B,
                    TP2B = a.TP2B,
                    TP3B = a.TP3B,
                    TP1C = a.TP1C,
                    TP2C = a.TP2C,
                    TP3C = a.TP3C,
                    TPTimeToActionA = a.TPTimeToActionA,
                    TPTimeToActionB = a.TPTimeToActionB,
                    TPTimeToActionC = a.TPTimeToActionC,
                    TP1Risk = a.TP1Risk,
                    TP2Risk = a.TP2Risk,
                    TP3Risk = a.TP3Risk,
                    TPTimeToActionRisk = a.TPTimeToActionRisk,
                    LoFScoreLeakageToAtmophereTP1 = a.LoFScoreLeakageToAtmophereTP1,
                    LoFScoreLeakageToAtmophereTP2 = a.LoFScoreLeakageToAtmophereTP2,
                    LoFScoreLeakageToAtmophereTP3 = a.LoFScoreLeakageToAtmophereTP3,
                    LoFScoreFailureOfFunctionTP1 = a.LoFScoreFailureOfFunctionTP1,
                    LoFScoreFailureOfFunctionTP2 = a.LoFScoreFailureOfFunctionTP2,
                    LoFScoreFailureOfFunctionTP3 = a.LoFScoreFailureOfFunctionTP3,
                    LoFScorePassingAccrosValveTP1 = a.LoFScorePassingAccrosValveTP1,
                    LoFScorePassingAccrosValveTP2 = a.LoFScorePassingAccrosValveTP2,
                    LoFScorePassingAccrosValveTP3 = a.LoFScorePassingAccrosValveTP3,
                    IntegrityStatus = a.IntegrityStatus,
                    IsDeleted = a.IsDeleted,
                    CreatedAt = a.CreatedAt,
                    CreatedBy = a.CreatedBy,
                    DeletedAt = a.DeletedAt,
                    DeletedBy = a.DeletedBy
                })
                .FirstOrDefault();
        }
        return assessmentDB;
    }

    public List<AssessmentModel> GetAssessmentList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false,
        bool withHistory = true,
        int AssetID = 0
    )
    {
        List<AssessmentModel> assessmentList = new();
        using (var context = new AssessmentContext())
        {
            assessmentList = (
                from assessment in context.Assessment
                join asset in context.Asset on assessment.AssetID equals asset.Id
                join platform in context.Platform on asset.PlatformID equals platform.Id
                join lta in context.CurrentConditionLimitState
                    on assessment.LeakageToAtmosphereID equals lta.Id
                    into ltaGroup
                from lta in ltaGroup.DefaultIfEmpty()
                join fta in context.CurrentConditionLimitState
                    on assessment.FailureOfFunctionID equals fta.Id
                    into ftaGroup
                from fta in ftaGroup.DefaultIfEmpty()
                join pav in context.CurrentConditionLimitState
                    on assessment.PassingAccrosValveID equals pav.Id
                    into pavGroup
                from pav in pavGroup.DefaultIfEmpty()
                join ltatp1 in context.TimeToLimitState
                    on assessment.LeakageToAtmosphereTP1ID equals ltatp1.Id
                    into ltatp1Group
                from ltatp1 in ltatp1Group.DefaultIfEmpty()
                join ltatp2 in context.TimeToLimitState
                    on assessment.LeakageToAtmosphereTP2ID equals ltatp2.Id
                    into ltatp2Group
                from ltatp2 in ltatp2Group.DefaultIfEmpty()
                join ltatp3 in context.TimeToLimitState
                    on assessment.LeakageToAtmosphereTP3ID equals ltatp3.Id
                    into ltatp3Group
                from ltatp3 in ltatp3Group.DefaultIfEmpty()
                join foftp1 in context.TimeToLimitState
                    on assessment.FailureOfFunctionTP1ID equals foftp1.Id
                    into foftp1Group
                from foftp1 in foftp1Group.DefaultIfEmpty()
                join foftp2 in context.TimeToLimitState
                    on assessment.FailureOfFunctionTP2ID equals foftp2.Id
                    into foftp2Group
                from foftp2 in foftp2Group.DefaultIfEmpty()
                join foftp3 in context.TimeToLimitState
                    on assessment.FailureOfFunctionTP3ID equals foftp3.Id
                    into foftp3Group
                from foftp3 in foftp3Group.DefaultIfEmpty()
                join pavtp1 in context.TimeToLimitState
                    on assessment.PassingAccrosValveTP1ID equals pavtp1.Id
                    into pavtp1Group
                from pavtp1 in pavtp1Group.DefaultIfEmpty()
                join pavtp2 in context.TimeToLimitState
                    on assessment.PassingAccrosValveTP2ID equals pavtp2.Id
                    into pavtp2Group
                from pavtp2 in pavtp2Group.DefaultIfEmpty()
                join pavtp3 in context.TimeToLimitState
                    on assessment.PassingAccrosValveTP3ID equals pavtp3.Id
                    into pavtp3Group
                from pavtp3 in pavtp3Group.DefaultIfEmpty()
                join ie in context.InspectionEffectiveness
                    on assessment.InspectionEffectivenessID equals ie.Id
                    into ieGroup
                from ie in ieGroup.DefaultIfEmpty()
                join iifi in context.ImpactEffect
                    on assessment.ImpactOfInternalFluidImpuritiesID equals iifi.Id
                    into iifiGroup
                from iifi in iifiGroup.DefaultIfEmpty()
                join ioe in context.ImpactEffect
                    on assessment.ImpactOfOperatingEnvelopesID equals ioe.Id
                    into ioeGroup
                from ioe in ioeGroup.DefaultIfEmpty()
                join uos in context.UsedWithinOEMSpecification
                    on assessment.UsedWithinOEMSpecificationID equals uos.Id
                    into uosGroup
                from uos in uosGroup.DefaultIfEmpty()
                join r in context.Repaired on assessment.RepairedID equals r.Id into rGroup
                from r in rGroup.DefaultIfEmpty()
                join hsse in context.HSSEDefinision
                    on assessment.HSSEDefinisionID equals hsse.Id
                    into hsseGroup
                from hsse in hsseGroup.DefaultIfEmpty()
                join ra in context.RecommendationAction
                    on assessment.RecommendationActionID equals ra.Id
                    into raGroup
                from ra in raGroup.DefaultIfEmpty()
                join user in context.User on assessment.CreatedBy equals user.Id into userGroup
                from user in userGroup.DefaultIfEmpty()
                join deletedUser in context.User
                    on assessment.DeletedBy equals deletedUser.Id
                    into deletedUserGroup
                from deletedUser in deletedUserGroup.DefaultIfEmpty()
                where
                    (AreaID == 0 || platform.AreaID == AreaID)
                    && (PlatformID == 0 || asset.PlatformID == PlatformID)
                    && (AssetID == 0 || assessment.AssetID == AssetID)
                    && (assessment.IsDeleted == false || IncludeDeleted == true)
                select new AssessmentModel
                {
                    Id = assessment.Id,
                    AssetID = assessment.AssetID,
                    AssessmentNo = assessment.AssessmentNo,
                    AssessmentDate = assessment.AssessmentDate,
                    TimePeriode = assessment.TimePeriode,
                    TimeToLimitStateLeakageToAtmosphere =
                        assessment.TimeToLimitStateLeakageToAtmosphere,
                    TimeToLimitStateFailureOfFunction =
                        assessment.TimeToLimitStateFailureOfFunction,
                    TimeToLimitStatePassingAccrosValve =
                        assessment.TimeToLimitStatePassingAccrosValve,
                    LeakageToAtmosphereID = assessment.LeakageToAtmosphereID,
                    FailureOfFunctionID = assessment.FailureOfFunctionID,
                    PassingAccrosValveID = assessment.PassingAccrosValveID,
                    LeakageToAtmosphereTP1ID = assessment.LeakageToAtmosphereTP1ID,
                    LeakageToAtmosphereTP2ID = assessment.LeakageToAtmosphereTP2ID,
                    LeakageToAtmosphereTP3ID = assessment.LeakageToAtmosphereTP3ID,
                    FailureOfFunctionTP1ID = assessment.FailureOfFunctionTP1ID,
                    FailureOfFunctionTP2ID = assessment.FailureOfFunctionTP2ID,
                    FailureOfFunctionTP3ID = assessment.FailureOfFunctionTP3ID,
                    PassingAccrosValveTP1ID = assessment.PassingAccrosValveTP1ID,
                    PassingAccrosValveTP2ID = assessment.PassingAccrosValveTP2ID,
                    PassingAccrosValveTP3ID = assessment.PassingAccrosValveTP3ID,
                    InspectionEffectivenessID = assessment.InspectionEffectivenessID,
                    ImpactOfInternalFluidImpuritiesID =
                        assessment.ImpactOfInternalFluidImpuritiesID,
                    ImpactOfOperatingEnvelopesID = assessment.ImpactOfOperatingEnvelopesID,
                    UsedWithinOEMSpecificationID = assessment.UsedWithinOEMSpecificationID,
                    RepairedID = assessment.RepairedID,
                    ProductLossDefinition = assessment.ProductLossDefinition,
                    HSSEDefinisionID = assessment.HSSEDefinisionID,
                    Summary = assessment.Summary,
                    RecommendationActionID = assessment.RecommendationActionID,
                    DetailedRecommendation = assessment.DetailedRecommendation,
                    ConsequenceOfFailure = assessment.ConsequenceOfFailure,
                    TP1A = assessment.TP1A,
                    TP2A = assessment.TP2A,
                    TP3A = assessment.TP3A,
                    TP1B = assessment.TP1B,
                    TP2B = assessment.TP2B,
                    TP3B = assessment.TP3B,
                    TP1C = assessment.TP1C,
                    TP2C = assessment.TP2C,
                    TP3C = assessment.TP3C,
                    TPTimeToActionA = assessment.TPTimeToActionA,
                    TPTimeToActionB = assessment.TPTimeToActionB,
                    TPTimeToActionC = assessment.TPTimeToActionC,
                    TP1Risk = assessment.TP1Risk,
                    TP2Risk = assessment.TP2Risk,
                    TP3Risk = assessment.TP3Risk,
                    TPTimeToActionRisk = assessment.TPTimeToActionRisk,
                    LoFScoreLeakageToAtmophereTP1 = assessment.LoFScoreLeakageToAtmophereTP1,
                    LoFScoreLeakageToAtmophereTP2 = assessment.LoFScoreLeakageToAtmophereTP2,
                    LoFScoreLeakageToAtmophereTP3 = assessment.LoFScoreLeakageToAtmophereTP3,
                    LoFScoreFailureOfFunctionTP1 = assessment.LoFScoreFailureOfFunctionTP1,
                    LoFScoreFailureOfFunctionTP2 = assessment.LoFScoreFailureOfFunctionTP2,
                    LoFScoreFailureOfFunctionTP3 = assessment.LoFScoreFailureOfFunctionTP3,
                    LoFScorePassingAccrosValveTP1 = assessment.LoFScorePassingAccrosValveTP1,
                    LoFScorePassingAccrosValveTP2 = assessment.LoFScorePassingAccrosValveTP2,
                    LoFScorePassingAccrosValveTP3 = assessment.LoFScorePassingAccrosValveTP3,
                    IntegrityStatus = assessment.IntegrityStatus,
                    CoFScore = assessment.CoFScore,
                    IsDeleted = assessment.IsDeleted,
                    CreatedAt = assessment.CreatedAt,
                    CreatedBy = assessment.CreatedBy,
                    DeletedAt = assessment.DeletedAt,
                    DeletedBy = assessment.DeletedBy,
                    Asset = new AssetModel().GetAssetModel(assessment.AssetID),
                    LeakageToAtmosphere = lta.CurrentConditionLimitState,
                    FailureOfFunction = fta.CurrentConditionLimitState,
                    PassingAccrosValve = pav.CurrentConditionLimitState,
                    LeakageToAtmosphereTP1 = ltatp1.TimeToLimitState,
                    LeakageToAtmosphereTP2 = ltatp2.TimeToLimitState,
                    LeakageToAtmosphereTP3 = ltatp3.TimeToLimitState,
                    FailureOfFunctionTP1 = foftp1.TimeToLimitState,
                    FailureOfFunctionTP2 = foftp2.TimeToLimitState,
                    FailureOfFunctionTP3 = foftp3.TimeToLimitState,
                    PassingAccrosValveTP1 = pavtp1.TimeToLimitState,
                    PassingAccrosValveTP2 = pavtp2.TimeToLimitState,
                    PassingAccrosValveTP3 = pavtp3.TimeToLimitState,
                    InspectionEffectiveness = ie.Effectiveness,
                    ImpactOfInternalFluidImpurities = iifi.ImpactEffect,
                    ImpactOfOperatingEnvelopes = ioe.ImpactEffect,
                    UsedWithinOEMSpecification = uos.UsedWithinOEMSpecification,
                    Repaired = r.Repaired,
                    HSSEDefinision = hsse.HSSEDefinision,
                    RecommendationAction = ra.RecommendationAction,
                    CreatedByUser = user.Username,
                    DeletedByUser = deletedUser.Username,
                    InspectionHistory = withHistory
                        ? new AssessmentInspectionModel().GetInspectionList(assessment.Id)
                        : null,
                    MaintenanceHistory = withHistory
                        ? new AssessmentMaintenanceModel().GetMaintenanceList(assessment.Id)
                        : null,
                    LastInspectionDate = new AssessmentInspectionModel().GetLastInspectionDate(
                        assessment.Id
                    ),
                    LastInspectionId = new AssessmentInspectionModel().GetLastInspectionId(
                        assessment.Id
                    ),
                    LastMaintenanceDate = new AssessmentMaintenanceModel().GetLastMaintenanceDate(
                        assessment.Id
                    ),
                    LastMaintenanceId = new AssessmentMaintenanceModel().GetLastMaintenanceId(
                        assessment.Id
                    ),
                }
            ).ToList();
        }
        return assessmentList;
    }

    public List<AssessmentModel> GetAssessmentRecapList(
        int AreaID = 0,
        int PlatformID = 0,
        bool IncludeDeleted = false,
        bool withHistory = true
    )
    {
        List<AssessmentModel> assessmentList = new();
        using (var context = new AssessmentContext())
        {
            assessmentList = (
                from assessment in context.Assessment
                join asset in context.Asset on assessment.AssetID equals asset.Id
                join platform in context.Platform on asset.PlatformID equals platform.Id
                join lta in context.CurrentConditionLimitState
                    on assessment.LeakageToAtmosphereID equals lta.Id
                    into ltaGroup
                from lta in ltaGroup.DefaultIfEmpty()
                join fta in context.CurrentConditionLimitState
                    on assessment.FailureOfFunctionID equals fta.Id
                    into ftaGroup
                from fta in ftaGroup.DefaultIfEmpty()
                join pav in context.CurrentConditionLimitState
                    on assessment.PassingAccrosValveID equals pav.Id
                    into pavGroup
                from pav in pavGroup.DefaultIfEmpty()
                join ltatp1 in context.TimeToLimitState
                    on assessment.LeakageToAtmosphereTP1ID equals ltatp1.Id
                    into ltatp1Group
                from ltatp1 in ltatp1Group.DefaultIfEmpty()
                join ltatp2 in context.TimeToLimitState
                    on assessment.LeakageToAtmosphereTP2ID equals ltatp2.Id
                    into ltatp2Group
                from ltatp2 in ltatp2Group.DefaultIfEmpty()
                join ltatp3 in context.TimeToLimitState
                    on assessment.LeakageToAtmosphereTP3ID equals ltatp3.Id
                    into ltatp3Group
                from ltatp3 in ltatp3Group.DefaultIfEmpty()
                join foftp1 in context.TimeToLimitState
                    on assessment.FailureOfFunctionTP1ID equals foftp1.Id
                    into foftp1Group
                from foftp1 in foftp1Group.DefaultIfEmpty()
                join foftp2 in context.TimeToLimitState
                    on assessment.FailureOfFunctionTP2ID equals foftp2.Id
                    into foftp2Group
                from foftp2 in foftp2Group.DefaultIfEmpty()
                join foftp3 in context.TimeToLimitState
                    on assessment.FailureOfFunctionTP3ID equals foftp3.Id
                    into foftp3Group
                from foftp3 in foftp3Group.DefaultIfEmpty()
                join pavtp1 in context.TimeToLimitState
                    on assessment.PassingAccrosValveTP1ID equals pavtp1.Id
                    into pavtp1Group
                from pavtp1 in pavtp1Group.DefaultIfEmpty()
                join pavtp2 in context.TimeToLimitState
                    on assessment.PassingAccrosValveTP2ID equals pavtp2.Id
                    into pavtp2Group
                from pavtp2 in pavtp2Group.DefaultIfEmpty()
                join pavtp3 in context.TimeToLimitState
                    on assessment.PassingAccrosValveTP3ID equals pavtp3.Id
                    into pavtp3Group
                from pavtp3 in pavtp3Group.DefaultIfEmpty()
                join ie in context.InspectionEffectiveness
                    on assessment.InspectionEffectivenessID equals ie.Id
                    into ieGroup
                from ie in ieGroup.DefaultIfEmpty()
                join iifi in context.ImpactEffect
                    on assessment.ImpactOfInternalFluidImpuritiesID equals iifi.Id
                    into iifiGroup
                from iifi in iifiGroup.DefaultIfEmpty()
                join ioe in context.ImpactEffect
                    on assessment.ImpactOfOperatingEnvelopesID equals ioe.Id
                    into ioeGroup
                from ioe in ioeGroup.DefaultIfEmpty()
                join uos in context.UsedWithinOEMSpecification
                    on assessment.UsedWithinOEMSpecificationID equals uos.Id
                    into uosGroup
                from uos in uosGroup.DefaultIfEmpty()
                join r in context.Repaired on assessment.RepairedID equals r.Id into rGroup
                from r in rGroup.DefaultIfEmpty()
                join hsse in context.HSSEDefinision
                    on assessment.HSSEDefinisionID equals hsse.Id
                    into hsseGroup
                from hsse in hsseGroup.DefaultIfEmpty()
                join ra in context.RecommendationAction
                    on assessment.RecommendationActionID equals ra.Id
                    into raGroup
                from ra in raGroup.DefaultIfEmpty()
                join user in context.User on assessment.CreatedBy equals user.Id into userGroup
                from user in userGroup.DefaultIfEmpty()
                join deletedUser in context.User
                    on assessment.DeletedBy equals deletedUser.Id
                    into deletedUserGroup
                from deletedUser in deletedUserGroup.DefaultIfEmpty()
                where
                    (AreaID == 0 || platform.AreaID == AreaID)
                    && (PlatformID == 0 || asset.PlatformID == PlatformID)
                    && (assessment.IsDeleted == false || IncludeDeleted == true)
                    && assessment.TP1Risk != null
                    && assessment.TP2Risk != null
                    && assessment.TP3Risk != null
                select new AssessmentModel
                {
                    Id = assessment.Id,
                    AssetID = assessment.AssetID,
                    AssessmentNo = assessment.AssessmentNo,
                    AssessmentDate = assessment.AssessmentDate,
                    TimePeriode = assessment.TimePeriode,
                    TimeToLimitStateLeakageToAtmosphere =
                        assessment.TimeToLimitStateLeakageToAtmosphere,
                    TimeToLimitStateFailureOfFunction =
                        assessment.TimeToLimitStateFailureOfFunction,
                    TimeToLimitStatePassingAccrosValve =
                        assessment.TimeToLimitStatePassingAccrosValve,
                    LeakageToAtmosphereID = assessment.LeakageToAtmosphereID,
                    FailureOfFunctionID = assessment.FailureOfFunctionID,
                    PassingAccrosValveID = assessment.PassingAccrosValveID,
                    LeakageToAtmosphereTP1ID = assessment.LeakageToAtmosphereTP1ID,
                    LeakageToAtmosphereTP2ID = assessment.LeakageToAtmosphereTP2ID,
                    LeakageToAtmosphereTP3ID = assessment.LeakageToAtmosphereTP3ID,
                    FailureOfFunctionTP1ID = assessment.FailureOfFunctionTP1ID,
                    FailureOfFunctionTP2ID = assessment.FailureOfFunctionTP2ID,
                    FailureOfFunctionTP3ID = assessment.FailureOfFunctionTP3ID,
                    PassingAccrosValveTP1ID = assessment.PassingAccrosValveTP1ID,
                    PassingAccrosValveTP2ID = assessment.PassingAccrosValveTP2ID,
                    PassingAccrosValveTP3ID = assessment.PassingAccrosValveTP3ID,
                    InspectionEffectivenessID = assessment.InspectionEffectivenessID,
                    ImpactOfInternalFluidImpuritiesID =
                        assessment.ImpactOfInternalFluidImpuritiesID,
                    ImpactOfOperatingEnvelopesID = assessment.ImpactOfOperatingEnvelopesID,
                    UsedWithinOEMSpecificationID = assessment.UsedWithinOEMSpecificationID,
                    RepairedID = assessment.RepairedID,
                    ProductLossDefinition = assessment.ProductLossDefinition,
                    HSSEDefinisionID = assessment.HSSEDefinisionID,
                    Summary = assessment.Summary,
                    RecommendationActionID = assessment.RecommendationActionID,
                    DetailedRecommendation = assessment.DetailedRecommendation,
                    ConsequenceOfFailure = assessment.ConsequenceOfFailure,
                    TP1A = assessment.TP1A,
                    TP2A = assessment.TP2A,
                    TP3A = assessment.TP3A,
                    TP1B = assessment.TP1B,
                    TP2B = assessment.TP2B,
                    TP3B = assessment.TP3B,
                    TP1C = assessment.TP1C,
                    TP2C = assessment.TP2C,
                    TP3C = assessment.TP3C,
                    TPTimeToActionA = assessment.TPTimeToActionA,
                    TPTimeToActionB = assessment.TPTimeToActionB,
                    TPTimeToActionC = assessment.TPTimeToActionC,
                    TP1Risk = assessment.TP1Risk,
                    TP2Risk = assessment.TP2Risk,
                    TP3Risk = assessment.TP3Risk,
                    TPTimeToActionRisk = assessment.TPTimeToActionRisk,
                    LoFScoreLeakageToAtmophereTP1 = assessment.LoFScoreLeakageToAtmophereTP1,
                    LoFScoreLeakageToAtmophereTP2 = assessment.LoFScoreLeakageToAtmophereTP2,
                    LoFScoreLeakageToAtmophereTP3 = assessment.LoFScoreLeakageToAtmophereTP3,
                    LoFScoreFailureOfFunctionTP1 = assessment.LoFScoreFailureOfFunctionTP1,
                    LoFScoreFailureOfFunctionTP2 = assessment.LoFScoreFailureOfFunctionTP2,
                    LoFScoreFailureOfFunctionTP3 = assessment.LoFScoreFailureOfFunctionTP3,
                    LoFScorePassingAccrosValveTP1 = assessment.LoFScorePassingAccrosValveTP1,
                    LoFScorePassingAccrosValveTP2 = assessment.LoFScorePassingAccrosValveTP2,
                    LoFScorePassingAccrosValveTP3 = assessment.LoFScorePassingAccrosValveTP3,
                    IntegrityStatus = assessment.IntegrityStatus,
                    CoFScore = assessment.CoFScore,
                    IsDeleted = assessment.IsDeleted,
                    CreatedAt = assessment.CreatedAt,
                    CreatedBy = assessment.CreatedBy,
                    DeletedAt = assessment.DeletedAt,
                    DeletedBy = assessment.DeletedBy,
                    Asset = new AssetModel().GetAssetModel(assessment.AssetID),
                    LeakageToAtmosphere = lta.CurrentConditionLimitState,
                    FailureOfFunction = fta.CurrentConditionLimitState,
                    PassingAccrosValve = pav.CurrentConditionLimitState,
                    LeakageToAtmosphereTP1 = ltatp1.TimeToLimitState,
                    LeakageToAtmosphereTP2 = ltatp2.TimeToLimitState,
                    LeakageToAtmosphereTP3 = ltatp3.TimeToLimitState,
                    FailureOfFunctionTP1 = foftp1.TimeToLimitState,
                    FailureOfFunctionTP2 = foftp2.TimeToLimitState,
                    FailureOfFunctionTP3 = foftp3.TimeToLimitState,
                    PassingAccrosValveTP1 = pavtp1.TimeToLimitState,
                    PassingAccrosValveTP2 = pavtp2.TimeToLimitState,
                    PassingAccrosValveTP3 = pavtp3.TimeToLimitState,
                    InspectionEffectiveness = ie.Effectiveness,
                    ImpactOfInternalFluidImpurities = iifi.ImpactEffect,
                    ImpactOfOperatingEnvelopes = ioe.ImpactEffect,
                    UsedWithinOEMSpecification = uos.UsedWithinOEMSpecification,
                    Repaired = r.Repaired,
                    HSSEDefinision = hsse.HSSEDefinision,
                    RecommendationAction = ra.RecommendationAction,
                    CreatedByUser = user.Username,
                    DeletedByUser = deletedUser.Username,
                    InspectionHistory = withHistory
                        ? new AssessmentInspectionModel().GetInspectionList(assessment.Id)
                        : null,
                    MaintenanceHistory = withHistory
                        ? new AssessmentMaintenanceModel().GetMaintenanceList(assessment.Id)
                        : null,
                    LastInspectionDate = new AssessmentInspectionModel().GetLastInspectionDate(
                        assessment.Id
                    ),
                    LastInspectionId = new AssessmentInspectionModel().GetLastInspectionId(
                        assessment.Id
                    ),
                    LastMaintenanceDate = new AssessmentMaintenanceModel().GetLastMaintenanceDate(
                        assessment.Id
                    ),
                    LastMaintenanceId = new AssessmentMaintenanceModel().GetLastMaintenanceId(
                        assessment.Id
                    ),
                }
            ).ToList();
        }
        Dictionary<int, AssessmentModel> finalAssessmentList = new();
        foreach (var assessment in assessmentList)
        {
            if (finalAssessmentList.ContainsKey(assessment.AssetID))
            {
                if (
                    DateTime.ParseExact(
                        assessment.AssessmentDate,
                        "dd-MM-yyyy",
                        CultureInfo.InvariantCulture
                    )
                    > DateTime.ParseExact(
                        finalAssessmentList[assessment.AssetID].AssessmentDate,
                        "dd-MM-yyyy",
                        CultureInfo.InvariantCulture
                    )
                )
                {
                    finalAssessmentList[assessment.AssetID] = assessment;
                }
            }
            else
            {
                finalAssessmentList.Add(assessment.AssetID, assessment);
            }
        }
        List<AssessmentModel> finalAssessmentListValues = finalAssessmentList.Values.ToList();

        return finalAssessmentListValues;
    }

    public AssessmentModel GetAssessmentModel(int id)
    {
        AssessmentModel assessmentModel = new();
        using (var context = new AssessmentContext())
        {
            assessmentModel = (
                from assessment in context.Assessment
                join asset in context.Asset on assessment.AssetID equals asset.Id
                where assessment.Id == id
                select new AssessmentModel
                {
                    Id = assessment.Id,
                    AssetID = assessment.AssetID,
                    AssessmentNo = assessment.AssessmentNo,
                    AssessmentDate = assessment.AssessmentDate,
                    TimePeriode = assessment.TimePeriode,
                    TimeToLimitStateLeakageToAtmosphere =
                        assessment.TimeToLimitStateLeakageToAtmosphere,
                    TimeToLimitStateFailureOfFunction =
                        assessment.TimeToLimitStateFailureOfFunction,
                    TimeToLimitStatePassingAccrosValve =
                        assessment.TimeToLimitStatePassingAccrosValve,
                    LeakageToAtmosphereID = assessment.LeakageToAtmosphereID,
                    FailureOfFunctionID = assessment.FailureOfFunctionID,
                    PassingAccrosValveID = assessment.PassingAccrosValveID,
                    LeakageToAtmosphereTP1ID = assessment.LeakageToAtmosphereTP1ID,
                    LeakageToAtmosphereTP2ID = assessment.LeakageToAtmosphereTP2ID,
                    LeakageToAtmosphereTP3ID = assessment.LeakageToAtmosphereTP3ID,
                    FailureOfFunctionTP1ID = assessment.FailureOfFunctionTP1ID,
                    FailureOfFunctionTP2ID = assessment.FailureOfFunctionTP2ID,
                    FailureOfFunctionTP3ID = assessment.FailureOfFunctionTP3ID,
                    PassingAccrosValveTP1ID = assessment.PassingAccrosValveTP1ID,
                    PassingAccrosValveTP2ID = assessment.PassingAccrosValveTP2ID,
                    PassingAccrosValveTP3ID = assessment.PassingAccrosValveTP3ID,
                    InspectionEffectivenessID = assessment.InspectionEffectivenessID,
                    ImpactOfInternalFluidImpuritiesID =
                        assessment.ImpactOfInternalFluidImpuritiesID,
                    ImpactOfOperatingEnvelopesID = assessment.ImpactOfOperatingEnvelopesID,
                    UsedWithinOEMSpecificationID = assessment.UsedWithinOEMSpecificationID,
                    RepairedID = assessment.RepairedID,
                    ProductLossDefinition = assessment.ProductLossDefinition,
                    HSSEDefinisionID = assessment.HSSEDefinisionID,
                    Summary = assessment.Summary,
                    RecommendationActionID = assessment.RecommendationActionID,
                    DetailedRecommendation = assessment.DetailedRecommendation,
                    ConsequenceOfFailure = assessment.ConsequenceOfFailure,
                    TP1A = assessment.TP1A,
                    TP2A = assessment.TP2A,
                    TP3A = assessment.TP3A,
                    TP1B = assessment.TP1B,
                    TP2B = assessment.TP2B,
                    TP3B = assessment.TP3B,
                    TP1C = assessment.TP1C,
                    TP2C = assessment.TP2C,
                    TP3C = assessment.TP3C,
                    TPTimeToActionA = assessment.TPTimeToActionA,
                    TPTimeToActionB = assessment.TPTimeToActionB,
                    TPTimeToActionC = assessment.TPTimeToActionC,
                    TP1Risk = assessment.TP1Risk,
                    TP2Risk = assessment.TP2Risk,
                    TP3Risk = assessment.TP3Risk,
                    TPTimeToActionRisk = assessment.TPTimeToActionRisk,
                    LoFScoreLeakageToAtmophereTP1 = assessment.LoFScoreLeakageToAtmophereTP1,
                    LoFScoreLeakageToAtmophereTP2 = assessment.LoFScoreLeakageToAtmophereTP2,
                    LoFScoreLeakageToAtmophereTP3 = assessment.LoFScoreLeakageToAtmophereTP3,
                    LoFScoreFailureOfFunctionTP1 = assessment.LoFScoreFailureOfFunctionTP1,
                    LoFScoreFailureOfFunctionTP2 = assessment.LoFScoreFailureOfFunctionTP2,
                    LoFScoreFailureOfFunctionTP3 = assessment.LoFScoreFailureOfFunctionTP3,
                    LoFScorePassingAccrosValveTP1 = assessment.LoFScorePassingAccrosValveTP1,
                    LoFScorePassingAccrosValveTP2 = assessment.LoFScorePassingAccrosValveTP2,
                    LoFScorePassingAccrosValveTP3 = assessment.LoFScorePassingAccrosValveTP3,
                    CoFScore = assessment.CoFScore,
                    IntegrityStatus = assessment.IntegrityStatus,
                    IsDeleted = assessment.IsDeleted,
                    CreatedAt = assessment.CreatedAt,
                    CreatedBy = assessment.CreatedBy,
                    DeletedAt = assessment.DeletedAt,
                    DeletedBy = assessment.DeletedBy,
                    Asset = new AssetModel().GetAssetModel(assessment.AssetID),
                    LeakageToAtmosphere = context
                        .CurrentConditionLimitState.Where(cc =>
                            cc.Id == assessment.LeakageToAtmosphereID
                        )
                        .FirstOrDefault()
                        .CurrentConditionLimitState,
                    FailureOfFunction = context
                        .CurrentConditionLimitState.Where(cc =>
                            cc.Id == assessment.FailureOfFunctionID
                        )
                        .FirstOrDefault()
                        .CurrentConditionLimitState,
                    PassingAccrosValve = context
                        .CurrentConditionLimitState.Where(cc =>
                            cc.Id == assessment.PassingAccrosValveID
                        )
                        .FirstOrDefault()
                        .CurrentConditionLimitState,
                    LeakageToAtmosphereTP1 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP1ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    LeakageToAtmosphereTP2 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP2ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    LeakageToAtmosphereTP3 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP3ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    FailureOfFunctionTP1 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.FailureOfFunctionTP1ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    FailureOfFunctionTP2 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.FailureOfFunctionTP2ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    FailureOfFunctionTP3 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.FailureOfFunctionTP3ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    PassingAccrosValveTP1 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.PassingAccrosValveTP1ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    PassingAccrosValveTP2 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.PassingAccrosValveTP2ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    PassingAccrosValveTP3 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.PassingAccrosValveTP3ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    InspectionEffectiveness = context
                        .InspectionEffectiveness.Where(ie =>
                            ie.Id == assessment.InspectionEffectivenessID
                        )
                        .FirstOrDefault()
                        .Effectiveness,
                    ImpactOfInternalFluidImpurities = context
                        .ImpactEffect.Where(ie =>
                            ie.Id == assessment.ImpactOfInternalFluidImpuritiesID
                        )
                        .FirstOrDefault()
                        .ImpactEffect,
                    ImpactOfOperatingEnvelopes = context
                        .ImpactEffect.Where(ie => ie.Id == assessment.ImpactOfOperatingEnvelopesID)
                        .FirstOrDefault()
                        .ImpactEffect,
                    UsedWithinOEMSpecification = context
                        .UsedWithinOEMSpecification.Where(uos =>
                            uos.Id == assessment.UsedWithinOEMSpecificationID
                        )
                        .FirstOrDefault()
                        .UsedWithinOEMSpecification,
                    Repaired = context
                        .Repaired.Where(r => r.Id == assessment.RepairedID)
                        .FirstOrDefault()
                        .Repaired,
                    HSSEDefinision = context
                        .HSSEDefinision.Where(hsse => hsse.Id == assessment.HSSEDefinisionID)
                        .FirstOrDefault()
                        .HSSEDefinision,
                    RecommendationAction = context
                        .RecommendationAction.Where(ra =>
                            ra.Id == assessment.RecommendationActionID
                        )
                        .FirstOrDefault()
                        .RecommendationAction,
                    CreatedByUser = context
                        .User.Where(u => u.Id == assessment.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == assessment.DeletedBy)
                        .FirstOrDefault()
                        .Username,
                    InspectionHistory = new AssessmentInspectionModel().GetInspectionList(
                        assessment.Id
                    ),
                    MaintenanceHistory = new AssessmentMaintenanceModel().GetMaintenanceList(
                        assessment.Id
                    )
                }
            ).FirstOrDefault();
        }
        return assessmentModel;
    }

    public int AddAssessment(AssessmentDB assessmentDB)
    {
        int assessmentID = 0;
        using (var context = new AssessmentContext())
        {
            assessmentDB.IsDeleted = false;
            if (assessmentDB.LeakageToAtmosphereID == 0)
            {
                assessmentDB.LeakageToAtmosphereID = null;
            }
            if (assessmentDB.FailureOfFunctionID == 0)
            {
                assessmentDB.FailureOfFunctionID = null;
            }
            if (assessmentDB.PassingAccrosValveID == 0)
            {
                assessmentDB.PassingAccrosValveID = null;
            }
            if (assessmentDB.LeakageToAtmosphereTP1ID == 0)
            {
                assessmentDB.LeakageToAtmosphereTP1ID = null;
            }
            if (assessmentDB.LeakageToAtmosphereTP2ID == 0)
            {
                assessmentDB.LeakageToAtmosphereTP2ID = null;
            }
            if (assessmentDB.LeakageToAtmosphereTP3ID == 0)
            {
                assessmentDB.LeakageToAtmosphereTP3ID = null;
            }
            if (assessmentDB.FailureOfFunctionTP1ID == 0)
            {
                assessmentDB.FailureOfFunctionTP1ID = null;
            }
            if (assessmentDB.FailureOfFunctionTP2ID == 0)
            {
                assessmentDB.FailureOfFunctionTP2ID = null;
            }
            if (assessmentDB.FailureOfFunctionTP3ID == 0)
            {
                assessmentDB.FailureOfFunctionTP3ID = null;
            }
            if (assessmentDB.PassingAccrosValveTP1ID == 0)
            {
                assessmentDB.PassingAccrosValveTP1ID = null;
            }
            if (assessmentDB.PassingAccrosValveTP2ID == 0)
            {
                assessmentDB.PassingAccrosValveTP2ID = null;
            }
            if (assessmentDB.PassingAccrosValveTP3ID == 0)
            {
                assessmentDB.PassingAccrosValveTP3ID = null;
            }
            if (assessmentDB.InspectionEffectivenessID == 0)
            {
                assessmentDB.InspectionEffectivenessID = null;
            }
            if (assessmentDB.ImpactOfInternalFluidImpuritiesID == 0)
            {
                assessmentDB.ImpactOfInternalFluidImpuritiesID = null;
            }
            if (assessmentDB.ImpactOfOperatingEnvelopesID == 0)
            {
                assessmentDB.ImpactOfOperatingEnvelopesID = null;
            }
            if (assessmentDB.UsedWithinOEMSpecificationID == 0)
            {
                assessmentDB.UsedWithinOEMSpecificationID = null;
            }
            if (assessmentDB.RepairedID == 0)
            {
                assessmentDB.RepairedID = null;
            }
            if (assessmentDB.RecommendationActionID == 0)
            {
                assessmentDB.RecommendationActionID = null;
            }
            if (assessmentDB.HSSEDefinisionID == 0)
            {
                assessmentDB.HSSEDefinisionID = null;
            }
            // check if there is already an assessment with the same assetid and assessment date
            if (
                context
                    .Assessment.Select(a => new { a.AssetID, a.AssessmentDate })
                    .Where(a => a.AssetID == assessmentDB.AssetID)
                    .Where(a => a.AssessmentDate == assessmentDB.AssessmentDate)
                    .Count() > 0
            )
            {
                Exception e = new Exception(
                    "An assessment with the same asset and date already exists"
                );
                throw e;
            }
            context.Assessment.Add(assessmentDB);
            context.SaveChanges();
            assessmentID = assessmentDB.Id;
        }
        using (var context = new AssessmentContext())
        {
            AssessmentDB assessmentNo = GetAssessmentDB(assessmentID);
            if (
                string.IsNullOrEmpty(assessmentNo.AssessmentNo)
                || assessmentNo.AssessmentNo == "IMPORT"
            )
            {
                int assId = assessmentDB.Id;
                // AssessmentDB assessmentNo = GetAssessmentDB(assId);
                assessmentNo.AssessmentNo = "ASSESSMENT" + assessmentDB.Id;
                context.Assessment.Update(assessmentNo);
                context.SaveChanges();
            }
            assessmentID = assessmentDB.Id;
            return assessmentID;
        }
    }

    public void AddMaintenanceToAssessment(
        int assessmentID,
        List<int> maintenanceIDs,
        bool update = false
    )
    {
        if (update)
        {
            using (var context = new AssessmentContext())
            {
                var assessmentMaintenances = context
                    .AssessmentMaintenance.Where(am => am.AssessmentID == assessmentID)
                    .ToList();
                context.RemoveRange(assessmentMaintenances);
                context.SaveChanges();
            }
        }
        using (var context = new AssessmentContext())
        {
            foreach (var maintenanceID in maintenanceIDs)
            {
                AssessmentMaintenanceModel assessmentMaintenance =
                    new() { AssessmentID = assessmentID, MaintenanceID = maintenanceID };
                context.Add(assessmentMaintenance);
            }
            context.SaveChanges();
        }
    }

    public void AddInspectionToAssessment(
        int assessmentID,
        List<int> inspectionIDs,
        bool update = false
    )
    {
        if (update)
        {
            using (var context = new AssessmentContext())
            {
                var assessmentInspections = context
                    .AssessmentInspection.Where(ai => ai.AssessmentID == assessmentID)
                    .ToList();
                context.RemoveRange(assessmentInspections);
                context.SaveChanges();
            }
        }
        using (var context = new AssessmentContext())
        {
            foreach (var inspectionID in inspectionIDs)
            {
                AssessmentInspectionModel assessmentInspection =
                    new() { AssessmentID = assessmentID, InspectionID = inspectionID };
                context.Add(assessmentInspection);
            }
            context.SaveChanges();
        }
    }

    public void UpdateAssessment(AssessmentDB assessment)
    {
        int id = assessment.Id;
        AssessmentDB oldAssessment = new AssessmentDB() { Id = 0, AssetID = 0, };
        using (var context = new AssessmentContext())
        {
            oldAssessment = GetAssessmentDB(id);
            oldAssessment.AssetID = assessment.AssetID;
            oldAssessment.AssessmentNo = assessment.AssessmentNo;
            oldAssessment.AssessmentDate = assessment.AssessmentDate;
            oldAssessment.TimePeriode = assessment.TimePeriode;
            oldAssessment.TimeToLimitStateLeakageToAtmosphere =
                assessment.TimeToLimitStateLeakageToAtmosphere;
            oldAssessment.TimeToLimitStateFailureOfFunction =
                assessment.TimeToLimitStateFailureOfFunction;
            oldAssessment.TimeToLimitStatePassingAccrosValve =
                assessment.TimeToLimitStatePassingAccrosValve;
            if (assessment.LeakageToAtmosphereID != 0)
            {
                oldAssessment.LeakageToAtmosphereID = assessment.LeakageToAtmosphereID;
            }
            else
            {
                oldAssessment.LeakageToAtmosphereID = null;
            }
            if (assessment.FailureOfFunctionID != 0)
            {
                oldAssessment.FailureOfFunctionID = assessment.FailureOfFunctionID;
            }
            else
            {
                oldAssessment.FailureOfFunctionID = null;
            }
            if (assessment.PassingAccrosValveID != 0)
            {
                oldAssessment.PassingAccrosValveID = assessment.PassingAccrosValveID;
            }
            else
            {
                oldAssessment.PassingAccrosValveID = null;
            }
            if (assessment.LeakageToAtmosphereTP1ID != 0)
            {
                oldAssessment.LeakageToAtmosphereTP1ID = assessment.LeakageToAtmosphereTP1ID;
            }
            else
            {
                oldAssessment.LeakageToAtmosphereTP1ID = null;
            }
            if (assessment.LeakageToAtmosphereTP2ID != 0)
            {
                oldAssessment.LeakageToAtmosphereTP2ID = assessment.LeakageToAtmosphereTP2ID;
            }
            else
            {
                oldAssessment.LeakageToAtmosphereTP2ID = null;
            }
            if (assessment.LeakageToAtmosphereTP3ID != 0)
            {
                oldAssessment.LeakageToAtmosphereTP3ID = assessment.LeakageToAtmosphereTP3ID;
            }
            else
            {
                oldAssessment.LeakageToAtmosphereTP3ID = null;
            }
            if (assessment.FailureOfFunctionTP1ID != 0)
            {
                oldAssessment.FailureOfFunctionTP1ID = assessment.FailureOfFunctionTP1ID;
            }
            else
            {
                oldAssessment.FailureOfFunctionTP1ID = null;
            }
            if (assessment.FailureOfFunctionTP2ID != 0)
            {
                oldAssessment.FailureOfFunctionTP2ID = assessment.FailureOfFunctionTP2ID;
            }
            else
            {
                oldAssessment.FailureOfFunctionTP2ID = null;
            }
            if (assessment.FailureOfFunctionTP3ID != 0)
            {
                oldAssessment.FailureOfFunctionTP3ID = assessment.FailureOfFunctionTP3ID;
            }
            else
            {
                oldAssessment.FailureOfFunctionTP3ID = null;
            }
            if (assessment.PassingAccrosValveTP1ID != 0)
            {
                oldAssessment.PassingAccrosValveTP1ID = assessment.PassingAccrosValveTP1ID;
            }
            else
            {
                oldAssessment.PassingAccrosValveTP1ID = null;
            }
            if (assessment.PassingAccrosValveTP2ID != 0)
            {
                oldAssessment.PassingAccrosValveTP2ID = assessment.PassingAccrosValveTP2ID;
            }
            else
            {
                oldAssessment.PassingAccrosValveTP2ID = null;
            }
            if (assessment.PassingAccrosValveTP3ID != 0)
            {
                oldAssessment.PassingAccrosValveTP3ID = assessment.PassingAccrosValveTP3ID;
            }
            else
            {
                oldAssessment.PassingAccrosValveTP3ID = null;
            }
            if (assessment.InspectionEffectivenessID != 0)
            {
                oldAssessment.InspectionEffectivenessID = assessment.InspectionEffectivenessID;
            }
            else
            {
                oldAssessment.InspectionEffectivenessID = null;
            }
            if (assessment.ImpactOfInternalFluidImpuritiesID != 0)
            {
                oldAssessment.ImpactOfInternalFluidImpuritiesID =
                    assessment.ImpactOfInternalFluidImpuritiesID;
            }
            else
            {
                oldAssessment.ImpactOfInternalFluidImpuritiesID = null;
            }
            if (assessment.ImpactOfOperatingEnvelopesID != 0)
            {
                oldAssessment.ImpactOfOperatingEnvelopesID =
                    assessment.ImpactOfOperatingEnvelopesID;
            }
            else
            {
                oldAssessment.ImpactOfOperatingEnvelopesID = null;
            }
            if (assessment.UsedWithinOEMSpecificationID != 0)
            {
                oldAssessment.UsedWithinOEMSpecificationID =
                    assessment.UsedWithinOEMSpecificationID;
            }
            else
            {
                oldAssessment.UsedWithinOEMSpecificationID = null;
            }
            if (assessment.RepairedID != 0)
            {
                oldAssessment.RepairedID = assessment.RepairedID;
            }
            else
            {
                oldAssessment.RepairedID = null;
            }
            oldAssessment.ProductLossDefinition = assessment.ProductLossDefinition;
            if (assessment.HSSEDefinisionID != 0)
            {
                oldAssessment.HSSEDefinisionID = assessment.HSSEDefinisionID;
            }
            else
            {
                oldAssessment.HSSEDefinisionID = null;
            }
            oldAssessment.Summary = assessment.Summary;
            if (assessment.RecommendationActionID != 0)
            {
                oldAssessment.RecommendationActionID = assessment.RecommendationActionID;
            }
            else
            {
                oldAssessment.RecommendationActionID = null;
            }
            oldAssessment.DetailedRecommendation = assessment.DetailedRecommendation;
            oldAssessment.ConsequenceOfFailure = assessment.ConsequenceOfFailure;
            oldAssessment.TP1A = assessment.TP1A;
            oldAssessment.TP2A = assessment.TP2A;
            oldAssessment.TP3A = assessment.TP3A;
            oldAssessment.TP1B = assessment.TP1B;
            oldAssessment.TP2B = assessment.TP2B;
            oldAssessment.TP3B = assessment.TP3B;
            oldAssessment.TP1C = assessment.TP1C;
            oldAssessment.TP2C = assessment.TP2C;
            oldAssessment.TP3C = assessment.TP3C;
            oldAssessment.TPTimeToActionA = assessment.TPTimeToActionA;
            oldAssessment.TPTimeToActionB = assessment.TPTimeToActionB;
            oldAssessment.TPTimeToActionC = assessment.TPTimeToActionC;
            oldAssessment.TP1Risk = assessment.TP1Risk;
            oldAssessment.TP2Risk = assessment.TP2Risk;
            oldAssessment.TP3Risk = assessment.TP3Risk;
            oldAssessment.TPTimeToActionRisk = assessment.TPTimeToActionRisk;
            oldAssessment.LoFScoreLeakageToAtmophereTP1 = assessment.LoFScoreLeakageToAtmophereTP1;
            oldAssessment.LoFScoreLeakageToAtmophereTP2 = assessment.LoFScoreLeakageToAtmophereTP2;
            oldAssessment.LoFScoreLeakageToAtmophereTP3 = assessment.LoFScoreLeakageToAtmophereTP3;
            oldAssessment.LoFScoreFailureOfFunctionTP1 = assessment.LoFScoreFailureOfFunctionTP1;
            oldAssessment.LoFScoreFailureOfFunctionTP2 = assessment.LoFScoreFailureOfFunctionTP2;
            oldAssessment.LoFScoreFailureOfFunctionTP3 = assessment.LoFScoreFailureOfFunctionTP3;
            oldAssessment.LoFScorePassingAccrosValveTP1 = assessment.LoFScorePassingAccrosValveTP1;
            oldAssessment.LoFScorePassingAccrosValveTP2 = assessment.LoFScorePassingAccrosValveTP2;
            oldAssessment.LoFScorePassingAccrosValveTP3 = assessment.LoFScorePassingAccrosValveTP3;
            oldAssessment.CoFScore = assessment.CoFScore;
            oldAssessment.IntegrityStatus = assessment.IntegrityStatus;
            context.Assessment.Update(oldAssessment);
            context.SaveChanges();
        }
    }

    public void DeleteAssessment(AssessmentDB assessment)
    {
        int id = assessment.Id;
        AssessmentDB oldAssessment = GetAssessmentDB(id);
        using (var context = new AssessmentContext())
        {
            oldAssessment.IsDeleted = true;
            oldAssessment.DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString());
            oldAssessment.DeletedBy = assessment.DeletedBy;
            context.Assessment.Update(oldAssessment);
            context.SaveChanges();
        }
    }

    public ToolImportModel MapAssessment(List<Dictionary<string, string>> data)
    {
        ToolImportModel toolImport = new();
        List<string> failedRecords = new();
        List<AssetModel> assetList = new AssetModel().GetAssetList(0, 0, true);
        List<CurrentConditionLimitStateModel> currentConditionLimitStateList =
            new CurrentConditionLimitStateModel().GetConditionLimitStates();
        List<TimeToLimitStateModel> timeToLimitStateList =
            new TimeToLimitStateModel().GetTimeToLimitStates();
        List<InspectionEffectivenessModel> inspectionEffectivenessList =
            new InspectionEffectivenessModel().GetInspectionEffectivenessStates();
        List<ImpactEffectModel> impactEffectList = new ImpactEffectModel().GetImpactEffectStates();
        List<UsedWithinOEMSpecificationModel> usedWithinOEMSpecificationList =
            new UsedWithinOEMSpecificationModel().GetUsedWithinOEMSpecifications();
        List<RepairedModel> repairedList = new RepairedModel().GetRepairedStates();
        List<HSSEDefinisionModel> hsseDefinisionList =
            new HSSEDefinisionModel().GetHSSEDefinisions();
        List<Dictionary<string, string>> finalResult = new();
        foreach (var records in data)
        {
            int timeperiod = 0;
            Dictionary<string, string> result = new();
            foreach (var record in records)
            {
                string key = record.Key;
                string value = record.Value.Trim().ToLower();
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
                            if (asset.TagNo.Trim().ToLower().Equals(value))
                            {
                                mappedValue = asset.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("AssessmentDate"))
                    {
                        // mappedValue = DateTime
                        //     .FromOADate(Convert.ToDouble(value))
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
                    else if (
                        mappedKey.Equals("ImpactOfInternalFluidImpuritiesID")
                        || mappedKey.Equals("ImpactOfOperatingEnvelopesID")
                    )
                    {
                        foreach (var impactEffect in impactEffectList)
                        {
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
                            if (repaired.Repaired.Trim().ToLower().Equals(value))
                            {
                                mappedValue = repaired.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("HSSEDefinisionID"))
                    {
                        foreach (var hsseDefinision in hsseDefinisionList)
                        {
                            if (hsseDefinision.HSSEDefinision.Trim().ToLower().Equals(value))
                            {
                                mappedValue = hsseDefinision.Id.ToString();
                                break;
                            }
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
            // result.Remove("TimeToLimitStateLeakageToAtmosphere");
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
            // result.Remove("TimeToLimitStateFailureOfFunction");
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
            // result.Remove("TimeToLimitStatePassingAccrosValve");
            result.Add("PassingAccrosValveTP1ID", PassingAccrosValveTP1ID.ToString());
            result.Add("PassingAccrosValveTP2ID", PassingAccrosValveTP2ID.ToString());
            result.Add("PassingAccrosValveTP3ID", PassingAccrosValveTP3ID.ToString());
            result.Add("AssessmentNo", "IMPORT");
            finalResult.Add(result);
        }
        toolImport.mappedRecords = finalResult;
        toolImport.failedRecords = failedRecords;
        // return finalResult;
        return toolImport;
    }

    private string MapHeader(string header)
    {
        switch (header.ToLower())
        {
            case "valve tag no.":
                return "AssetID";
            case "assessment date\n(dd/mm/yyyy)":
                return "AssessmentDate";
            case "time periode\n(month)":
                return "TimePeriode";
            case "lf2 - time to limit state leakage to atmosphere \n(month)":
                return "TimeToLimitStateLeakageToAtmosphere";
            case "lf2 - time to limit state failure of function (month)":
                return "TimeToLimitStateFailureOfFunction";
            case "lf2 - time to limit state passing accros valve (month)":
                return "TimeToLimitStatePassingAccrosValve";
            case "lf4 - impact of internal fluid impurities":
                return "ImpactOfInternalFluidImpuritiesID";
            case "lf5 - impact of operating envelopes":
                return "ImpactOfOperatingEnvelopesID";
            case "lf6 - used within oem specification":
                return "UsedWithinOEMSpecificationID";
            case "lf7 - repaired":
                return "RepairedID";
            case "cf1 - product loss definition (bbls)":
                return "ProductLossDefinition";
            case "cf2 - hsse definision":
                return "HSSEDefinisionID";
            default:
                return "";
        }
    }

    public Dictionary<string, Dictionary<string, string>> GetAssessmentRecap()
    {
        Dictionary<string, Dictionary<string, string>> recap_final = new();
        Dictionary<string, string> recap_heatmap = new();
        Dictionary<string, string> recap_piechart = new();
        Dictionary<string, Dictionary<string, string>> recap_barchart = new();
        Dictionary<string, string> recap_barchart_convert = new();
        using (var context = new AssessmentContext())
        {
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
            List<AssessmentModel> assessmentList = GetAssessmentRecapList(0, 0, false, false);
            Console.WriteLine("== START DEBUG ==");
            Console.WriteLine(JsonConvert.SerializeObject(assessmentList));
            Console.WriteLine("== END DEBUG ==");
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
                string heat_risk = decide_risk_full(
                    assessment.TP1Risk,
                    assessment.TP2Risk,
                    assessment.TP3Risk
                );
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
                string risk = decide_risk(
                    assessment.TP1Risk,
                    assessment.TP2Risk,
                    assessment.TP3Risk
                );
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
                // string assessmentjson = JsonConvert.SerializeObject(assessment.TP1Risk);
                // Console.WriteLine(assessmentjson);
                if (!recap_barchart.ContainsKey(assessment.Asset.BusinessArea))
                {
                    recap_barchart.Add(
                        assessment.Asset.BusinessArea,
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
                int curr_risk = int.Parse(recap_barchart[assessment.Asset.BusinessArea][risk]);
                curr_risk++;
                recap_barchart[assessment.Asset.BusinessArea][risk] = curr_risk.ToString();
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
        }
        recap_final.Add("heatmap", recap_heatmap);
        recap_final.Add("piechart", recap_piechart);
        foreach (var item in recap_barchart)
        {
            recap_barchart_convert.Add(item.Key, JsonConvert.SerializeObject(item.Value));
        }
        recap_final.Add("barchart", recap_barchart_convert);
        return recap_final;
    }

    private List<string> split_risk(string risk)
    {
        if (risk.Length < 2)
        {
            return new List<string> { "0", "0" };
        }
        string lof = risk.Substring(0, 1);
        string cos = risk.Substring(1, 1);
        return new List<string> { lof, cos };
    }

    private string decide_risk_full(string tp1, string tp2, string tp3)
    {
        List<string> tp1_split = split_risk(tp1);
        List<string> tp2_split = split_risk(tp2);
        List<string> tp3_split = split_risk(tp3);
        string cos = tp1_split[1];

        int tp1_value = int.Parse(tp1_split[0]);
        int tp2_value = int.Parse(tp2_split[0]);
        int tp3_value = int.Parse(tp3_split[0]);
        int max_value = Math.Max(tp1_value, Math.Max(tp2_value, tp3_value));
        string lof = max_value.ToString();
        string risk = max_value + cos;
        return risk;
    }

    private string decide_risk(string tp1, string tp2, string tp3)
    {
        string tp1_color = ColorRiskMap[GetHeatColor(tp1)];
        int tp1_riskrank = RiskRank[tp1_color];
        string tp2_color = ColorRiskMap[GetHeatColor(tp2)];
        int tp2_riskrank = RiskRank[tp2_color];
        string tp3_color = ColorRiskMap[GetHeatColor(tp3)];
        int tp3_riskrank = RiskRank[tp3_color];
        int max_risk = Math.Max(tp1_riskrank, Math.Max(tp2_riskrank, tp3_riskrank));
        int risk_rank = RiskRank.FirstOrDefault(x => x.Value == max_risk).Value;
        string risk_max = RiskRank.FirstOrDefault(x => x.Value == max_risk).Key;
        return risk_max;
        // string color_risk = ColorRank.FirstOrDefault(x => x.Value == max_risk).Key;
        // string risk_max = ColorRiskMap.FirstOrDefault(x => x.Value == color_risk).Key;
        // return risk_max;
    }

    private int LofToInt(string lof)
    {
        if (lof == "A")
        {
            return 1;
        }
        else if (lof == "B")
        {
            return 2;
        }
        else if (lof == "C")
        {
            return 3;
        }
        else if (lof == "D")
        {
            return 4;
        }
        else if (lof == "E")
        {
            return 5;
        }
        else
        {
            return 0;
        }
    }

    public string GetHeatColor(string pos)
    {
        List<string> olivePositions = new List<string> { "1A", "2A", "1B" };
        List<string> greenPositions = new List<string> { "3A", "2B", "1C" };
        List<string> yellowPositions = new List<string> { "4A", "3B", "3C", "2C", "1D" };
        List<string> orangePositions = new List<string>
        {
            "5A",
            "5B",
            "4B",
            "4C",
            "3D",
            "2D",
            "2E",
            "1E"
        };

        if (olivePositions.Contains(pos))
        {
            return "olive";
        }
        else if (greenPositions.Contains(pos))
        {
            return "green";
        }
        else if (yellowPositions.Contains(pos))
        {
            return "yellow";
        }
        else if (orangePositions.Contains(pos))
        {
            return "orange";
        }
        else
        {
            return "red";
        }
    }

    public Dictionary<string, int> GetHeatXYpos(string pos)
    {
        List<string> pos_split = split_risk(pos);
        string lof = pos_split[0];
        string cos = pos_split[1];
        int ypos = (int.Parse(lof) * 20) - 10;
        int xpos = (LofToInt(cos) * 20) - 10;
        return new Dictionary<string, int> { { "xpos", xpos }, { "ypos", ypos } };
    }

    public AssessmentModel GetLastAssetAssessment(int assetID)
    {
        AssessmentModel assessmentModel = new();
        using (var context = new AssessmentContext())
        {
            assessmentModel = (
                from assessment in context.Assessment
                join asset in context.Asset on assessment.AssetID equals asset.Id
                where asset.Id == assetID
                where assessment.TP1Risk != null
                where assessment.TP2Risk != null
                where assessment.TP3Risk != null
                orderby assessment.AssessmentDate descending
                orderby assessment.Id descending
                select new AssessmentModel
                {
                    Id = assessment.Id,
                    AssetID = assessment.AssetID,
                    AssessmentNo = assessment.AssessmentNo,
                    AssessmentDate = assessment.AssessmentDate,
                    TimePeriode = assessment.TimePeriode,
                    TimeToLimitStateLeakageToAtmosphere =
                        assessment.TimeToLimitStateLeakageToAtmosphere,
                    TimeToLimitStateFailureOfFunction =
                        assessment.TimeToLimitStateFailureOfFunction,
                    TimeToLimitStatePassingAccrosValve =
                        assessment.TimeToLimitStatePassingAccrosValve,
                    LeakageToAtmosphereID = assessment.LeakageToAtmosphereID,
                    FailureOfFunctionID = assessment.FailureOfFunctionID,
                    PassingAccrosValveID = assessment.PassingAccrosValveID,
                    LeakageToAtmosphereTP1ID = assessment.LeakageToAtmosphereTP1ID,
                    LeakageToAtmosphereTP2ID = assessment.LeakageToAtmosphereTP2ID,
                    LeakageToAtmosphereTP3ID = assessment.LeakageToAtmosphereTP3ID,
                    FailureOfFunctionTP1ID = assessment.FailureOfFunctionTP1ID,
                    FailureOfFunctionTP2ID = assessment.FailureOfFunctionTP2ID,
                    FailureOfFunctionTP3ID = assessment.FailureOfFunctionTP3ID,
                    PassingAccrosValveTP1ID = assessment.PassingAccrosValveTP1ID,
                    PassingAccrosValveTP2ID = assessment.PassingAccrosValveTP2ID,
                    PassingAccrosValveTP3ID = assessment.PassingAccrosValveTP3ID,
                    InspectionEffectivenessID = assessment.InspectionEffectivenessID,
                    ImpactOfInternalFluidImpuritiesID =
                        assessment.ImpactOfInternalFluidImpuritiesID,
                    ImpactOfOperatingEnvelopesID = assessment.ImpactOfOperatingEnvelopesID,
                    UsedWithinOEMSpecificationID = assessment.UsedWithinOEMSpecificationID,
                    RepairedID = assessment.RepairedID,
                    ProductLossDefinition = assessment.ProductLossDefinition,
                    HSSEDefinisionID = assessment.HSSEDefinisionID,
                    Summary = assessment.Summary,
                    RecommendationActionID = assessment.RecommendationActionID,
                    DetailedRecommendation = assessment.DetailedRecommendation,
                    ConsequenceOfFailure = assessment.ConsequenceOfFailure,
                    TP1A = assessment.TP1A,
                    TP2A = assessment.TP2A,
                    TP3A = assessment.TP3A,
                    TP1B = assessment.TP1B,
                    TP2B = assessment.TP2B,
                    TP3B = assessment.TP3B,
                    TP1C = assessment.TP1C,
                    TP2C = assessment.TP2C,
                    TP3C = assessment.TP3C,
                    TPTimeToActionA = assessment.TPTimeToActionA,
                    TPTimeToActionB = assessment.TPTimeToActionB,
                    TPTimeToActionC = assessment.TPTimeToActionC,
                    TP1Risk = assessment.TP1Risk,
                    TP2Risk = assessment.TP2Risk,
                    TP3Risk = assessment.TP3Risk,
                    TPTimeToActionRisk = assessment.TPTimeToActionRisk,
                    IsDeleted = assessment.IsDeleted,
                    CreatedAt = assessment.CreatedAt,
                    CreatedBy = assessment.CreatedBy,
                    DeletedAt = assessment.DeletedAt,
                    DeletedBy = assessment.DeletedBy,
                    Asset = new AssetModel().GetAssetModel(assessment.AssetID),
                    LeakageToAtmosphere = context
                        .CurrentConditionLimitState.Where(cc =>
                            cc.Id == assessment.LeakageToAtmosphereID
                        )
                        .FirstOrDefault()
                        .CurrentConditionLimitState,
                    FailureOfFunction = context
                        .CurrentConditionLimitState.Where(cc =>
                            cc.Id == assessment.FailureOfFunctionID
                        )
                        .FirstOrDefault()
                        .CurrentConditionLimitState,
                    PassingAccrosValve = context
                        .CurrentConditionLimitState.Where(cc =>
                            cc.Id == assessment.PassingAccrosValveID
                        )
                        .FirstOrDefault()
                        .CurrentConditionLimitState,
                    LeakageToAtmosphereTP1 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP1ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    LeakageToAtmosphereTP2 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP2ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    LeakageToAtmosphereTP3 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP3ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    FailureOfFunctionTP1 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.FailureOfFunctionTP1ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    FailureOfFunctionTP2 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.FailureOfFunctionTP2ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    FailureOfFunctionTP3 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.FailureOfFunctionTP3ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    PassingAccrosValveTP1 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.PassingAccrosValveTP1ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    PassingAccrosValveTP2 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.PassingAccrosValveTP2ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    PassingAccrosValveTP3 = context
                        .TimeToLimitState.Where(tp => tp.Id == assessment.PassingAccrosValveTP3ID)
                        .FirstOrDefault()
                        .TimeToLimitState,
                    InspectionEffectiveness = context
                        .InspectionEffectiveness.Where(ie =>
                            ie.Id == assessment.InspectionEffectivenessID
                        )
                        .FirstOrDefault()
                        .Effectiveness,
                    ImpactOfInternalFluidImpurities = context
                        .ImpactEffect.Where(ie =>
                            ie.Id == assessment.ImpactOfInternalFluidImpuritiesID
                        )
                        .FirstOrDefault()
                        .ImpactEffect,
                    ImpactOfOperatingEnvelopes = context
                        .ImpactEffect.Where(ie => ie.Id == assessment.ImpactOfOperatingEnvelopesID)
                        .FirstOrDefault()
                        .ImpactEffect,
                    UsedWithinOEMSpecification = context
                        .UsedWithinOEMSpecification.Where(uos =>
                            uos.Id == assessment.UsedWithinOEMSpecificationID
                        )
                        .FirstOrDefault()
                        .UsedWithinOEMSpecification,
                    Repaired = context
                        .Repaired.Where(r => r.Id == assessment.RepairedID)
                        .FirstOrDefault()
                        .Repaired,
                    HSSEDefinision = context
                        .HSSEDefinision.Where(hsse => hsse.Id == assessment.HSSEDefinisionID)
                        .FirstOrDefault()
                        .HSSEDefinision,
                    RecommendationAction = context
                        .RecommendationAction.Where(ra =>
                            ra.Id == assessment.RecommendationActionID
                        )
                        .FirstOrDefault()
                        .RecommendationAction,
                    CreatedByUser = context
                        .User.Where(u => u.Id == assessment.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == assessment.DeletedBy)
                        .FirstOrDefault()
                        .Username,
                    InspectionHistory = new AssessmentInspectionModel().GetInspectionList(
                        assessment.Id
                    ),
                    MaintenanceHistory = new AssessmentMaintenanceModel().GetMaintenanceList(
                        assessment.Id
                    ),
                }
            ).FirstOrDefault();
        }
        if (assessmentModel == null)
        {
            return null;
        }
        assessmentModel.RiskMax = decide_risk_full(
            assessmentModel.TP1Risk,
            assessmentModel.TP2Risk,
            assessmentModel.TP3Risk
        );
        return assessmentModel;
    }

    public List<InspectionSidebarModel> GetSidebarAssessment(int assetID)
    {
        List<InspectionSidebarModel> inspectionSidebarList = new();
        using (var context = new AssessmentContext())
        {
            inspectionSidebarList = (
                from assessment in context.Assessment
                join asset in context.Asset on assessment.AssetID equals asset.Id
                where asset.Id == assetID && assessment.IsDeleted == false
                select new InspectionSidebarModel
                {
                    Id = assessment.Id,
                    Name = assessment.AssessmentDate
                }
            ).ToList();
            inspectionSidebarList = inspectionSidebarList
                .OrderByDescending(i =>
                    DateTime.ParseExact(i.Name, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                )
                .ToList();
        }
        return inspectionSidebarList;
    }
}

public class ImpactEffectModel
{
    public int Id { get; set; }
    public string? ImpactEffect { get; set; }
    public double? ImpactEffectValue { get; set; }
    public double? Weighting { get; set; }
    public double? Weighting2 { get; set; }

    public List<ImpactEffectModel> GetImpactEffectStates()
    {
        List<ImpactEffectModel> impactEffectList = new();
        using (var context = new AssessmentContext())
        {
            impactEffectList = context.ImpactEffect.ToList();
        }
        return impactEffectList;
    }
}

public class UsedWithinOEMSpecificationModel
{
    public int Id { get; set; }
    public string? UsedWithinOEMSpecification { get; set; }
    public double? UsedWithinOEMSpecificationValue { get; set; }
    public double? Weighting { get; set; }

    public List<UsedWithinOEMSpecificationModel> GetUsedWithinOEMSpecifications()
    {
        List<UsedWithinOEMSpecificationModel> usedWithinOEMSpecificationList = new();
        using (var context = new AssessmentContext())
        {
            usedWithinOEMSpecificationList = context.UsedWithinOEMSpecification.ToList();
        }
        return usedWithinOEMSpecificationList;
    }
}

public class RepairedModel
{
    public int Id { get; set; }
    public string? Repaired { get; set; }
    public double? RepairedValue { get; set; }
    public double? Weighting { get; set; }

    public List<RepairedModel> GetRepairedStates()
    {
        List<RepairedModel> repairedList = new();
        using (var context = new AssessmentContext())
        {
            repairedList = context.Repaired.ToList();
        }
        return repairedList;
    }
}

public class HSSEDefinisionModel
{
    public int Id { get; set; }
    public string? HSSEDefinision { get; set; }
    public double? MinBBSValue { get; set; }
    public string? CoFCategory { get; set; }
    public double? Score { get; set; }

    public List<HSSEDefinisionModel> GetHSSEDefinisions()
    {
        List<HSSEDefinisionModel> hsseDefinisionList = new();
        using (var context = new AssessmentContext())
        {
            hsseDefinisionList = context.HSSEDefinision.OrderBy(h => h.MinBBSValue).ToList();
        }
        return hsseDefinisionList;
    }
}

public class RecommendationActionModel
{
    public int Id { get; set; }
    public string? RecommendationAction { get; set; }
}

public class TimeToLimitStateModel
{
    public int Id { get; set; }
    public string? TimeToLimitState { get; set; }
    public double? LimitStateValue { get; set; }
    public double? Weighting { get; set; }

    public List<TimeToLimitStateModel> GetTimeToLimitStates()
    {
        List<TimeToLimitStateModel> timeToLimitStateList = new();
        using (var context = new AssessmentContext())
        {
            timeToLimitStateList = context.TimeToLimitState.ToList();
        }
        return timeToLimitStateList;
    }
}

public class AssessmentMaintenanceModel
{
    public int Id { get; set; }
    public int? AssessmentID { get; set; }
    public int? MaintenanceID { get; set; }
    public MaintenanceModel? Maintenance { get; set; }

    public List<AssessmentMaintenanceModel> GetAssessmentMaintenanceList(int assessmentID)
    {
        List<AssessmentMaintenanceModel> assessmentMaintenanceList = new();
        using (var context = new AssessmentContext())
        {
            assessmentMaintenanceList = (
                from assessmentMaintenance in context.AssessmentMaintenance
                where assessmentMaintenance.AssessmentID == assessmentID
                select new AssessmentMaintenanceModel
                {
                    Id = assessmentMaintenance.Id,
                    AssessmentID = assessmentMaintenance.AssessmentID,
                    MaintenanceID = assessmentMaintenance.MaintenanceID,
                    Maintenance = assessmentMaintenance.MaintenanceID.HasValue
                        ? new MaintenanceModel().GetMaintenanceModel(
                            assessmentMaintenance.MaintenanceID.Value
                        )
                        : null
                }
            ).ToList();
        }
        return assessmentMaintenanceList;
    }

    public List<MaintenanceModel> GetMaintenanceList(int AssessmentID)
    {
        List<MaintenanceModel> maintenanceList = new();
        using (var context = new AssessmentContext())
        {
            maintenanceList = (
                from m in context.Maintenance
                join ivr in context.IsValveRepaired on m.IsValveRepairedID equals ivr.Id
                join assessmentMaintenance in context.AssessmentMaintenance
                    on m.Id equals assessmentMaintenance.MaintenanceID
                where assessmentMaintenance.AssessmentID == AssessmentID
                select new MaintenanceModel
                {
                    Id = m.Id,
                    AssetID = m.AssetID,
                    IsValveRepairedID = m.IsValveRepairedID,
                    MaintenanceDate = m.MaintenanceDate,
                    MaintenanceDescription = m.MaintenanceDescription,
                    IsDeleted = m.IsDeleted,
                    CreatedBy = m.CreatedBy,
                    CreatedAt = m.CreatedAt,
                    DeletedBy = m.DeletedBy,
                    DeletedAt = m.DeletedAt,
                    Asset = new AssetModel().GetAssetModel(m.AssetID),
                    IsValveRepaired = ivr.IsValveRepaired,
                    CreatedByUser = context
                        .User.Where(u => u.Id == m.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == m.DeletedBy)
                        .FirstOrDefault()
                        .Username,
                    MaintenanceFiles = new InspectionFileModel().GetMaintenanceFiles(m.Id)
                }
            ).ToList();
        }
        return maintenanceList;
    }

    public string GetLastMaintenanceDate(int AssessmentID)
    {
        string lastMaintenanceDate = "";
        try
        {
            using (var context = new AssessmentContext())
            {
                lastMaintenanceDate = (
                    from m in context.Maintenance
                    join assessmentMaintenance in context.AssessmentMaintenance
                        on m.Id equals assessmentMaintenance.MaintenanceID
                    where assessmentMaintenance.AssessmentID == AssessmentID
                    orderby m.MaintenanceDate descending
                    select m.MaintenanceDate
                )
                    .FirstOrDefault()
                    .ToString();
            }
        }
        catch (Exception e) { }
        return lastMaintenanceDate;
    }

    public int GetLastMaintenanceId(int AssessmentID)
    {
        int lastMaintenanceId = 0;
        try
        {
            string slastMaintenanceId = "";
            using (var context = new AssessmentContext())
            {
                slastMaintenanceId = (
                    from m in context.Maintenance
                    join assessmentMaintenance in context.AssessmentMaintenance
                        on m.Id equals assessmentMaintenance.MaintenanceID
                    where assessmentMaintenance.AssessmentID == AssessmentID
                    orderby m.MaintenanceDate descending
                    select m.Id
                )
                    .FirstOrDefault()
                    .ToString();
            }
            lastMaintenanceId = int.Parse(slastMaintenanceId);
        }
        catch (Exception e) { }
        return lastMaintenanceId;
    }
}

public class AssessmentInspectionModel
{
    public int Id { get; set; }
    public int? AssessmentID { get; set; }
    public int? InspectionID { get; set; }
    public InspectionModel? Inspection { get; set; }

    public List<AssessmentInspectionModel> GetAssessmentInspectionList(int assessmentID)
    {
        List<AssessmentInspectionModel> assessmentInspectionList = new();
        using (var context = new AssessmentContext())
        {
            assessmentInspectionList = (
                from assessmentInspection in context.AssessmentInspection
                where assessmentInspection.AssessmentID == assessmentID
                select new AssessmentInspectionModel
                {
                    Id = assessmentInspection.Id,
                    AssessmentID = assessmentInspection.AssessmentID,
                    InspectionID = assessmentInspection.InspectionID,
                    Inspection = assessmentInspection.InspectionID.HasValue
                        ? new InspectionModel().GetInspectionModel(
                            assessmentInspection.InspectionID.Value
                        )
                        : null
                }
            ).ToList();
        }
        return assessmentInspectionList;
    }

    public List<InspectionModel> GetInspectionList(int AssessmentID)
    {
        List<InspectionModel> inspectionList = new();
        using (var context = new AssessmentContext())
        {
            inspectionList = (
                from inspection in context.Inspection
                join asset in context.Asset on inspection.AssetID equals asset.Id
                join inspectionMethod in context.InspectionMethod
                    on inspection.InspectionMethodID equals inspectionMethod.Id
                join inspectionEffectiveness in context.InspectionEffectiveness
                    on inspection.InspectionEffectivenessID equals inspectionEffectiveness.Id
                join currentConditionLimitStateA in context.CurrentConditionLimitState
                    on inspection.CurrentConditionLeakeageToAtmosphereID equals currentConditionLimitStateA.Id
                join currentConditionLimitStateB in context.CurrentConditionLimitState
                    on inspection.CurrentConditionFailureOfFunctionID equals currentConditionLimitStateB.Id
                join currentConditionLimitStateC in context.CurrentConditionLimitState
                    on inspection.CurrentConditionPassingAcrossValveID equals currentConditionLimitStateC.Id
                join assessmentInspection in context.AssessmentInspection
                    on inspection.Id equals assessmentInspection.InspectionID
                where assessmentInspection.AssessmentID == AssessmentID
                select new InspectionModel
                {
                    Id = inspection.Id,
                    AssetID = inspection.AssetID,
                    InspectionDate = inspection.InspectionDate,
                    InspectionMethodID = inspection.InspectionMethodID,
                    InspectionEffectivenessID = inspection.InspectionEffectivenessID,
                    InspectionDescription = inspection.InspectionDescription,
                    CurrentConditionLeakeageToAtmosphereID =
                        inspection.CurrentConditionLeakeageToAtmosphereID,
                    CurrentConditionFailureOfFunctionID =
                        inspection.CurrentConditionFailureOfFunctionID,
                    CurrentConditionPassingAcrossValveID =
                        inspection.CurrentConditionPassingAcrossValveID,
                    FunctionCondition = inspection.FunctionCondition,
                    TestPressureIfAny = inspection.TestPressureIfAny,
                    Asset = new AssetModel().GetAssetModel(inspection.AssetID),
                    InspectionMethod = inspectionMethod.InspectionMethod,
                    InspectionEffectiveness = inspectionEffectiveness.Effectiveness,
                    CurrentConditionLeakeageToAtmosphere =
                        currentConditionLimitStateA.CurrentConditionLimitState,
                    CurrentConditionFailureOfFunction =
                        currentConditionLimitStateB.CurrentConditionLimitState,
                    CurrentConditionPassingAcrossValve =
                        currentConditionLimitStateC.CurrentConditionLimitState,
                    InspectionFiles = new InspectionFileModel().GetInspectionFiles(inspection.Id),
                    IsDeleted = inspection.IsDeleted,
                    CreatedBy = inspection.CreatedBy,
                    CreatedAt = inspection.CreatedAt,
                    DeletedBy = inspection.DeletedBy,
                    DeletedAt = inspection.DeletedAt,
                    CreatedByUser = context
                        .User.Where(u => u.Id == inspection.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == inspection.DeletedBy)
                        .FirstOrDefault()
                        .Username
                }
            ).ToList();
        }
        return inspectionList;
    }

    public string GetLastInspectionDate(int AssessmentID)
    {
        string lastAssessmentDate = "";
        try
        {
            using (var context = new AssessmentContext())
            {
                lastAssessmentDate = (
                    from inspection in context.Inspection
                    join assessmentInspection in context.AssessmentInspection
                        on inspection.Id equals assessmentInspection.InspectionID
                    where assessmentInspection.AssessmentID == AssessmentID
                    orderby inspection.InspectionDate descending
                    select inspection.InspectionDate
                )
                    .FirstOrDefault()
                    .ToString();
            }
        }
        catch (Exception e) { }
        return lastAssessmentDate;
    }

    public int GetLastInspectionId(int AssessmentID)
    {
        int lastAssessmentId = 0;
        try
        {
            string slastAssessmentId = "";
            using (var context = new AssessmentContext())
            {
                slastAssessmentId = (
                    from inspection in context.Inspection
                    join assessmentInspection in context.AssessmentInspection
                        on inspection.Id equals assessmentInspection.InspectionID
                    where assessmentInspection.AssessmentID == AssessmentID
                    orderby inspection.InspectionDate descending
                    select inspection.Id
                )
                    .FirstOrDefault()
                    .ToString();
            }
            lastAssessmentId = int.Parse(slastAssessmentId);
        }
        catch (Exception e) { }
        return lastAssessmentId;
    }
}

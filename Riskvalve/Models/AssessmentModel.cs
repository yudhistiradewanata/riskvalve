using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Riskvalve.Models;

public class AssessmentContext : DbContext
{
    public DbSet<AssessmentDB> Assessment { get; set; }
    public DbSet<AssetModel> Asset { get; set; }
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

    // public string? LastInspectionDate { get; set; }
    // public string? MaxRisk { get; set; }
    // public string? MaxRiskColor { get; set; }
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

    public List<AssessmentModel> GetAssessmentList(bool IncludeDeleted = false)
    {
        List<AssessmentModel> assessmentList = new();
        using (var context = new AssessmentContext())
        {
            assessmentList = (
                from assessment in context.Assessment
                join asset in context.Asset on assessment.AssetID equals asset.Id
                where assessment.IsDeleted == false || IncludeDeleted == true
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
            ).ToList();
        }
        return assessmentList;
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
                context.Assessment.Select(a => new { a.AssetID, a.AssessmentDate })
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
            if (
                string.IsNullOrEmpty(assessmentDB.AssessmentNo)
                || assessmentDB.AssessmentNo == "IMPORT"
            )
            {
                AssessmentDB assessmentNo = context.Assessment.Find(assessmentDB.Id);
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
        using (var context = new AssessmentContext())
        {
            AssessmentDB oldAssessment = context.Assessment.Find(assessment.Id);
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
            context.Update(oldAssessment);
            context.SaveChanges();
        }
    }

    public void DeleteAssessment(AssessmentDB assessment)
    {
        using (var context = new AssessmentContext())
        {
            AssessmentDB oldAssessment = context.Assessment.Find(assessment.Id);
            oldAssessment.IsDeleted = true;
            oldAssessment.DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString());
            oldAssessment.DeletedBy = assessment.DeletedBy;
            context.Update(oldAssessment);
            context.SaveChanges();
        }
    }

    public List<Dictionary<string, string>> MapAssessment(List<Dictionary<string, string>> data)
    {
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
            Dictionary<string, string> result = new();
            foreach (var record in records)
            {
                string key = record.Key;
                string value = record.Value;
                string mappedKey = MapHeader(key);
                string mappedValue = "";
                if (mappedKey.Equals(""))
                {
                    continue;
                }
                if (
                    mappedKey.Equals("AssetID")
                    || mappedKey.Equals("AssessmentDate")
                    || mappedKey.Equals("ImpactOfInternalFluidImpuritiesID")
                    || mappedKey.Equals("ImpactOfOperatingEnvelopesID")
                    || mappedKey.Equals("UsedWithinOEMSpecificationID")
                    || mappedKey.Equals("RepairedID")
                )
                {
                    if (mappedKey.Equals("AssetID"))
                    {
                        foreach (var asset in assetList)
                        {
                            if (asset.TagNo.Equals(value))
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
                            if (impactEffect.ImpactEffect.Equals(value))
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
                            if (usedWithinOEMSpecification.UsedWithinOEMSpecification.Equals(value))
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
                            if (repaired.Repaired.Equals(value))
                            {
                                mappedValue = repaired.Id.ToString();
                                break;
                            }
                        }
                    }
                    if (mappedValue == "")
                    {
                        Exception e =
                            new(
                                "Value '"
                                    + record.Value
                                    + "' on field '"
                                    + key
                                    + "' is not match with the database value"
                            );
                        throw e;
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
            double tp_limit_1 = 1.5 * 12;
            double tp_limit_2 = 3 * 12;
            double tp_limit_3 = 4.5 * 12;
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
                if (timeToLimitStateLeakage > tp_limit_1 * 2)
                {
                    LeakageToAtmosphereTP1ID = idImprobable;
                }
                else if (timeToLimitStateLeakage > tp_limit_1)
                {
                    LeakageToAtmosphereTP1ID = idDoubtful;
                }
                else if (timeToLimitStateLeakage <= tp_limit_1)
                {
                    LeakageToAtmosphereTP1ID = idExpected;
                }
                if (timeToLimitStateLeakage > tp_limit_2 * 2)
                {
                    LeakageToAtmosphereTP2ID = idImprobable;
                }
                else if (timeToLimitStateLeakage > tp_limit_2)
                {
                    LeakageToAtmosphereTP2ID = idDoubtful;
                }
                else if (timeToLimitStateLeakage <= tp_limit_2)
                {
                    LeakageToAtmosphereTP2ID = idExpected;
                }
                if (timeToLimitStateLeakage > tp_limit_3 * 2)
                {
                    LeakageToAtmosphereTP3ID = idImprobable;
                }
                else if (timeToLimitStateLeakage > tp_limit_3)
                {
                    LeakageToAtmosphereTP3ID = idDoubtful;
                }
                else if (timeToLimitStateLeakage <= tp_limit_3)
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
                if (timeToLimitStateFailure > tp_limit_1 * 2)
                {
                    FailureOfFunctionTP1ID = idImprobable;
                }
                else if (timeToLimitStateFailure > tp_limit_1)
                {
                    FailureOfFunctionTP1ID = idDoubtful;
                }
                else if (timeToLimitStateFailure <= tp_limit_1)
                {
                    FailureOfFunctionTP1ID = idExpected;
                }
                if (timeToLimitStateFailure > tp_limit_2 * 2)
                {
                    FailureOfFunctionTP2ID = idImprobable;
                }
                else if (timeToLimitStateFailure > tp_limit_2)
                {
                    FailureOfFunctionTP2ID = idDoubtful;
                }
                else if (timeToLimitStateFailure <= tp_limit_2)
                {
                    FailureOfFunctionTP2ID = idExpected;
                }
                if (timeToLimitStateFailure > tp_limit_3 * 2)
                {
                    FailureOfFunctionTP3ID = idImprobable;
                }
                else if (timeToLimitStateFailure > tp_limit_3)
                {
                    FailureOfFunctionTP3ID = idDoubtful;
                }
                else if (timeToLimitStateFailure <= tp_limit_3)
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
                if (timeToLimitStatePassing > tp_limit_1 * 2)
                {
                    PassingAccrosValveTP1ID = idImprobable;
                }
                else if (timeToLimitStatePassing > tp_limit_1)
                {
                    PassingAccrosValveTP1ID = idDoubtful;
                }
                else if (timeToLimitStatePassing <= tp_limit_1)
                {
                    PassingAccrosValveTP1ID = idExpected;
                }
                if (timeToLimitStatePassing > tp_limit_2 * 2)
                {
                    PassingAccrosValveTP2ID = idImprobable;
                }
                else if (timeToLimitStatePassing > tp_limit_2)
                {
                    PassingAccrosValveTP2ID = idDoubtful;
                }
                else if (timeToLimitStatePassing <= tp_limit_2)
                {
                    PassingAccrosValveTP2ID = idExpected;
                }
                if (timeToLimitStatePassing > tp_limit_3 * 2)
                {
                    PassingAccrosValveTP3ID = idImprobable;
                }
                else if (timeToLimitStatePassing > tp_limit_3)
                {
                    PassingAccrosValveTP3ID = idDoubtful;
                }
                else if (timeToLimitStatePassing <= tp_limit_3)
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
        return finalResult;
    }

    private string MapHeader(string header)
    {
        switch (header)
        {
            case "Valve Tag No.":
                return "AssetID";
            case "Assessment Date\n(dd/mm/yyyy)":
                return "AssessmentDate";
            case "Time Periode\n(Month)":
                return "TimePeriode";
            case "LF2 - Time to Limit State Leakage to atmosphere \n(Month)":
                return "TimeToLimitStateLeakageToAtmosphere";
            case "LF2 - Time to Limit State Failure of function (Month)":
                return "TimeToLimitStateFailureOfFunction";
            case "LF2 - Time to Limit State Passing accros valve (Month)":
                return "TimeToLimitStatePassingAccrosValve";
            case "LF4 - Impact of Internal Fluid Impurities":
                return "ImpactOfInternalFluidImpuritiesID";
            case "LF5 - Impact of Operating Envelopes":
                return "ImpactOfOperatingEnvelopesID";
            case "LF6 - Used within OEM Specification":
                return "UsedWithinOEMSpecificationID";
            case "LF7 - Repaired":
                return "RepairedID";
            case "CF1 - Product loss definition (bbls)":
                return "ProductLossDefinition";
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
        Dictionary<string, string> bartemplate = new();
        bartemplate.Add("Very Low", "0");
        bartemplate.Add("Low", "0");
        bartemplate.Add("Medium", "0");
        bartemplate.Add("High", "0");
        bartemplate.Add("Very High", "0");
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
            List<AssessmentModel> assessmentList = GetAssessmentList();
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
                    recap_barchart.Add(assessment.Asset.BusinessArea, bartemplate);
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
}

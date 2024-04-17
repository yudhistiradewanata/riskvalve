using Microsoft.EntityFrameworkCore;

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

    public List<AssessmentModel> GetAssessmentList(bool IncludeDeleted = false)
    {
        List<AssessmentModel> assessmentList = new();
        using (var context = new AssessmentContext())
        {
            assessmentList = (
                from assessment in context.Assessment
                join asset in context.Asset on assessment.AssetID equals asset.Id
                where assessment.IsDeleted == IncludeDeleted
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
                    InspectionHistory = new InspectionModel().GetInspectionList(
                        false,
                        assessment.AssetID
                    ),
                    MaintenanceHistory = new MaintenanceModel().GetMaintenanceList(
                        false,
                        assessment.AssetID
                    )
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
                    InspectionHistory = new InspectionModel().GetInspectionList(
                        false,
                        assessment.AssetID
                    ),
                    MaintenanceHistory = new MaintenanceModel().GetMaintenanceList(
                        false,
                        assessment.AssetID
                    )
                }
            ).FirstOrDefault();
        }
        return assessmentModel;
    }

    public int AddAssessment(AssessmentDB assessment)
    {
        using (var context = new AssessmentContext())
        {
            assessment.IsDeleted = false;
            if (assessment.LeakageToAtmosphereID == 0)
            {
                assessment.LeakageToAtmosphereID = null;
            }
            if (assessment.FailureOfFunctionID == 0)
            {
                assessment.FailureOfFunctionID = null;
            }
            if (assessment.PassingAccrosValveID == 0)
            {
                assessment.PassingAccrosValveID = null;
            }
            if (assessment.LeakageToAtmosphereTP1ID == 0)
            {
                assessment.LeakageToAtmosphereTP1ID = null;
            }
            if (assessment.LeakageToAtmosphereTP2ID == 0)
            {
                assessment.LeakageToAtmosphereTP2ID = null;
            }
            if (assessment.LeakageToAtmosphereTP3ID == 0)
            {
                assessment.LeakageToAtmosphereTP3ID = null;
            }
            if (assessment.FailureOfFunctionTP1ID == 0)
            {
                assessment.FailureOfFunctionTP1ID = null;
            }
            if (assessment.FailureOfFunctionTP2ID == 0)
            {
                assessment.FailureOfFunctionTP2ID = null;
            }
            if (assessment.FailureOfFunctionTP3ID == 0)
            {
                assessment.FailureOfFunctionTP3ID = null;
            }
            if (assessment.PassingAccrosValveTP1ID == 0)
            {
                assessment.PassingAccrosValveTP1ID = null;
            }
            if (assessment.PassingAccrosValveTP2ID == 0)
            {
                assessment.PassingAccrosValveTP2ID = null;
            }
            if (assessment.PassingAccrosValveTP3ID == 0)
            {
                assessment.PassingAccrosValveTP3ID = null;
            }
            if (assessment.InspectionEffectivenessID == 0)
            {
                assessment.InspectionEffectivenessID = null;
            }
            if (assessment.ImpactOfInternalFluidImpuritiesID == 0)
            {
                assessment.ImpactOfInternalFluidImpuritiesID = null;
            }
            if (assessment.ImpactOfOperatingEnvelopesID == 0)
            {
                assessment.ImpactOfOperatingEnvelopesID = null;
            }
            if (assessment.UsedWithinOEMSpecificationID == 0)
            {
                assessment.UsedWithinOEMSpecificationID = null;
            }
            if (assessment.RepairedID == 0)
            {
                assessment.RepairedID = null;
            }
            if (assessment.RecommendationActionID == 0)
            {
                assessment.RecommendationActionID = null;
            }
            if (assessment.HSSEDefinisionID == 0)
            {
                assessment.HSSEDefinisionID = null;
            }
            context.Add(assessment);
            context.SaveChanges();
            return assessment.Id;
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
                        mappedValue = DateTime
                            .FromOADate(Convert.ToDouble(value))
                            .ToString(Environment.GetDateFormatString());
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
            result.Remove("TimeToLimitStateLeakageToAtmosphere");
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
            result.Remove("TimeToLimitStateFailureOfFunction");
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
            result.Remove("TimeToLimitStatePassingAccrosValve");
            result.Add("PassingAccrosValveTP1ID", PassingAccrosValveTP1ID.ToString());
            result.Add("PassingAccrosValveTP2ID", PassingAccrosValveTP2ID.ToString());
            result.Add("PassingAccrosValveTP3ID", PassingAccrosValveTP3ID.ToString());
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

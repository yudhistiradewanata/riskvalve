using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class AssessmentContext : DbContext{
    public DbSet<AssessmentDB> Assessment { get; set; }
    public DbSet<AssetModel> Asset { get; set; }
    public DbSet<LeakageToAtmosphereModel> LeakageToAtmosphere { get; set; }
    public DbSet<FailureOfFunctionModel> FailureOfFunction { get; set; }
    public DbSet<PassingAccrosValveModel> PassingAccrosValve { get; set; }
    public DbSet<InspectionEffectivenessModel> InspectionEffectiveness { get; set; }
    public DbSet<ImpactOfInternalFluidImpuritiesModel> ImpactOfInternalFluidImpurities { get; set; }
    public DbSet<ImpactOfOperatingEnvelopesModel> ImpactOfOperatingEnvelopes { get; set; }
    public DbSet<UsedWithinOEMSpecificationModel> UsedWithinOEMSpecification { get; set; }
    public DbSet<RepairedModel> Repaired { get; set; }
    public DbSet<HSSEDefinisionModel> HSSEDefinision { get; set; }
    public DbSet<RecommendationActionModel> RecommendationAction { get; set; }
    public DbSet<TimePeriodModel> TimePeriod { get; set; }
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

}

public class AssessmentDB {
    public int Id { get; set; }
    public int AssetID { get; set; }
    public string? AssessmentNo { get; set; }
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

public class AssessmentModel : AssessmentDB {
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

    public List<AssessmentModel> GetAssessmentList(bool IncludeDeleted = false) {
        List<AssessmentModel> assessmentList = new();
        using (var context = new AssessmentContext()) {
            assessmentList = (
                from assessment in context.Assessment
                join asset in context.Asset on assessment.AssetID equals asset.Id
                join lat in context.LeakageToAtmosphere on assessment.LeakageToAtmosphereID equals lat.Id
                join fof in context.FailureOfFunction on assessment.FailureOfFunctionID equals fof.Id
                join pav in context.PassingAccrosValve on assessment.PassingAccrosValveID equals pav.Id
                join ief in context.InspectionEffectiveness on assessment.InspectionEffectivenessID equals ief.Id
                join iif in context.ImpactOfInternalFluidImpurities on assessment.ImpactOfInternalFluidImpuritiesID equals iif.Id
                join ioe in context.ImpactOfOperatingEnvelopes on assessment.ImpactOfOperatingEnvelopesID equals ioe.Id
                join uos in context.UsedWithinOEMSpecification on assessment.UsedWithinOEMSpecificationID equals uos.Id
                join rep in context.Repaired on assessment.RepairedID equals rep.Id
                join hsse in context.HSSEDefinision on assessment.HSSEDefinisionID equals hsse.Id
                join ra in context.RecommendationAction on assessment.RecommendationActionID equals ra.Id
                where assessment.IsDeleted == IncludeDeleted
                select new AssessmentModel
                {
                    Id = assessment.Id,
                    AssetID = assessment.AssetID,
                    AssessmentNo = assessment.AssessmentNo,
                    TimePeriode = assessment.TimePeriode,
                    TimeToLimitStateLeakageToAtmosphere = assessment.TimeToLimitStateLeakageToAtmosphere,
                    TimeToLimitStateFailureOfFunction = assessment.TimeToLimitStateFailureOfFunction,
                    TimeToLimitStatePassingAccrosValve = assessment.TimeToLimitStatePassingAccrosValve,
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
                    ImpactOfInternalFluidImpuritiesID = assessment.ImpactOfInternalFluidImpuritiesID,
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
                    LeakageToAtmosphere = lat.LeakageToAtmosphere,
                    FailureOfFunction = fof.FailureOfFunction,
                    PassingAccrosValve = pav.PassingAccrosValve,
                    LeakageToAtmosphereTP1 = context.TimePeriod.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP1ID).FirstOrDefault().TimePeriod,
                    LeakageToAtmosphereTP2 = context.TimePeriod.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP2ID).FirstOrDefault().TimePeriod,
                    LeakageToAtmosphereTP3 = context.TimePeriod.Where(tp => tp.Id == assessment.LeakageToAtmosphereTP3ID).FirstOrDefault().TimePeriod,
                    FailureOfFunctionTP1 = context.TimePeriod.Where(tp => tp.Id == assessment.FailureOfFunctionTP1ID).FirstOrDefault().TimePeriod,
                    FailureOfFunctionTP2 = context.TimePeriod.Where(tp => tp.Id == assessment.FailureOfFunctionTP2ID).FirstOrDefault().TimePeriod,
                    FailureOfFunctionTP3 = context.TimePeriod.Where(tp => tp.Id == assessment.FailureOfFunctionTP3ID).FirstOrDefault().TimePeriod,
                    PassingAccrosValveTP1 = context.TimePeriod.Where(tp => tp.Id == assessment.PassingAccrosValveTP1ID).FirstOrDefault().TimePeriod,
                    PassingAccrosValveTP2 = context.TimePeriod.Where(tp => tp.Id == assessment.PassingAccrosValveTP2ID).FirstOrDefault().TimePeriod,
                    PassingAccrosValveTP3 = context.TimePeriod.Where(tp => tp.Id == assessment.PassingAccrosValveTP3ID).FirstOrDefault().TimePeriod,
                    InspectionEffectiveness = ief.Effectiveness,
                    ImpactOfInternalFluidImpurities = iif.ImpactOfInternalFluidImpurities,
                    ImpactOfOperatingEnvelopes = ioe.ImpactOfOperatingEnvelopes,
                    UsedWithinOEMSpecification = uos.UsedWithinOEMSpecification,
                    Repaired = rep.Repaired,
                    HSSEDefinision = hsse.HSSEDefinision,
                    RecommendationAction = ra.RecommendationAction,
                    CreatedByUser = context.User.Where(u => u.Id == assessment.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == assessment.DeletedBy).FirstOrDefault().Username,
                    InspectionHistory = new InspectionModel().GetInspectionList(false, assessment.AssetID),
                    MaintenanceHistory = new MaintenanceModel().GetMaintenanceList(false,assessment.AssetID)
                }
            ).ToList();
        }
        return assessmentList;
    }
}

public class LeakageToAtmosphereModel {
    public int Id { get; set; }
    public string? LeakageToAtmosphere { get; set; }
}

public class FailureOfFunctionModel {
    public int Id { get; set; }
    public string? FailureOfFunction { get; set; }
}

public class PassingAccrosValveModel {
    public int Id { get; set; }
    public string? PassingAccrosValve { get; set; }
}

public class ImpactOfInternalFluidImpuritiesModel {
    public int Id { get; set; }
    public string? ImpactOfInternalFluidImpurities { get; set; }
}

public class ImpactOfOperatingEnvelopesModel {
    public int Id { get; set; }
    public string? ImpactOfOperatingEnvelopes { get; set; }
}

public class UsedWithinOEMSpecificationModel {
    public int Id { get; set; }
    public string? UsedWithinOEMSpecification { get; set; }
}

public class RepairedModel {
    public int Id { get; set; }
    public string? Repaired { get; set; }
}

public class HSSEDefinisionModel {
    public int Id { get; set; }
    public string? HSSEDefinision { get; set; }
}

public class RecommendationActionModel {
    public int Id { get; set; }
    public string? RecommendationAction { get; set; }
}

public class TimePeriodModel {
    public int Id { get; set; }
    public string? TimePeriod { get; set; }
}
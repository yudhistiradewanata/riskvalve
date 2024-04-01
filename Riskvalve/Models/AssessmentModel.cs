using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class AssessmentDB {
    public int Id { get; set; }
    public int? AssetID { get; set; }
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
    public bool IsDeleted { get; set; }
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
}
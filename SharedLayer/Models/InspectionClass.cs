namespace SharedLayer;

public class InspectionClass
{
    public int Id { get; set; }
    public int AssetID { get; set; } //FK
    public string? InspectionDate { get; set; }
    public int InspectionMethodID { get; set; } //FK
    public int InspectionEffectivenessID { get; set; } //FK
    public string? InspectionDescription { get; set; }
    public int CurrentConditionLeakeageToAtmosphereID { get; set; } //FK
    public int CurrentConditionFailureOfFunctionID { get; set; } //FK
    public int CurrentConditionPassingAcrossValveID { get; set; } //FK
    public string? FunctionCondition { get; set; }
    public string? TestPressureIfAny { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class InspectionData : InspectionClass
{
    public AssetData? Asset { get; set; }
    public string? InspectionMethod { get; set; }
    public string? InspectionEffectiveness { get; set; }
    public string? CurrentConditionLeakeageToAtmosphere { get; set; }
    public string? CurrentConditionFailureOfFunction { get; set; }
    public string? CurrentConditionPassingAcrossValve { get; set; }
    public List<InspectionFileData>? InspectionFiles { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
}
namespace SharedLayer;

public class AssetClass
{
    public int Id { get; set; }
    public string? TagNo { get; set; }
    public string? AssetName { get; set; }
    public int? PlatformID { get; set; } // FK
    public int? ValveTypeID { get; set; } // FK
    public string? Size { get; set; }
    public string? ClassRating { get; set; }
    public string? ParentEquipmentNo { get; set; }
    public string? ParentEquipmentDescription { get; set; }
    public string? InstallationDate { get; set; }
    public string? PIDNo { get; set; }
    public string? Manufacturer { get; set; }
    public string? BodyModel { get; set; }
    public string? BodyMaterial { get; set; }
    public string? EndConnection { get; set; }
    public string? SerialNo { get; set; }
    public int? ManualOverrideID { get; set; } // FK
    public string? ActuatorMfg { get; set; }
    public string? ActuatorSerialNo { get; set; }
    public string? ActuatorTypeModel { get; set; }
    public string? ActuatorPower { get; set; }
    public string? OperatingTemperature { get; set; }
    public string? OperatingPressure { get; set; }
    public string? FlowRate { get; set; }
    public string? ServiceFluid { get; set; }
    public int? FluidPhaseID { get; set; } // FK
    public int? ToxicOrFlamableFluidID { get; set; } // FK
    public string? UsageType { get; set; }
    public string? CostOfReplacementAndRepair { get; set; }
    public string? Actuation { get; set; }
    public string? Status { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class AssetData : AssetClass
{
    public string? Platform { get; set; }
    public PlatformData? PlatformData { get; set; }
    public string? BusinessArea { get; set; }
    public string? ValveType { get; set; }
    public string? ManualOverride { get; set; }
    public string? FluidPhase { get; set; }
    public string? ToxicOrFlamableFluid { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
    public InspectionData? LastInspection { get; set; }
    public MaintenanceData? LastMaintenance { get; set; }
    public AssessmentData? LastAssessment { get; set; }
}
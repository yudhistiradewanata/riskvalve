namespace Riskvalve.Models;

public class AssetModel
{
    public int Id { get; set; }
    public string? TagNo { get; set; }
    public PlatformModel? Platform { get; set; }
    public ValveTypeModel? ValveType { get; set; }
    public int Size { get; set; }
    public int ClassRating { get; set; }
    public string? ParentEquitpmentNo { get; set; }
    public string? ParentEquipmentDescription { get; set; }
    public DateTime InstallationDate { get; set; }
    public string? PIDNo { get; set; }
    public string? Manufacturer { get; set; }
    public string? BodyModel { get; set; }
    public string? BodyMaterial { get; set; }
    public string? EndConnection { get; set; }
    public string? SerialNo { get; set; }
    public ManualOverrideModel? ManualOverride { get; set; }
    public string? ActuatorMfg { get; set; }
    public string? ActuatorSerialNo { get; set; }
    public string? ActuatorTypeModel { get; set; }
    public string? ActuatorPower { get; set; }
    public string? OperatingTemperature { get; set; }
    public string? OperatingPressure { get; set; }
    public string? FlowRate { get; set; }
    public string? ServiceFluid { get; set; }
    public FluidPhaseModel? FluidPhase { get; set; }
    public ToxicOrFlamableFluidModel? ToxicOrFlamableFluid { get; set; }
}

public class ValveTypeModel
{
    public int Id { get; set; }
    public string? ValveType { get; set; }
}

public class ManualOverrideModel
{
    public int Id { get; set; }
    public string? Text { get; set; }

    public static List<ManualOverrideModel> GetManualOverrideList()
    {
        List<ManualOverrideModel> manualOverrideList =
            new()
            {
                new() { Id = 1, Text = "Yes" },
                new() { Id = 2, Text = "No" },
                new() { Id = 2, Text = "Normally Open or Close" }
            };
        return manualOverrideList;
    }
}

public class FluidPhaseModel
{
    public int Id { get; set; }
    public string? FluidPhase { get; set; }

    public static List<FluidPhaseModel> GetFluidPhaseList()
    {
        List<FluidPhaseModel> fluidPhaseList =
            new()
            {
                new() { Id = 1, FluidPhase = "Liquid" },
                new() { Id = 2, FluidPhase = "Gas" }
            };
        return fluidPhaseList;
    }
}

public class ToxicOrFlamableFluidModel
{
    public int Id { get; set; }
    public string? ToxicOrFlamableFluid { get; set; }

    public static List<ToxicOrFlamableFluidModel> GetToxicOrFlamableFluidList()
    {
        List<ToxicOrFlamableFluidModel> toxicOrFlamableFluidList =
            new()
            {
                new() { Id = 1, ToxicOrFlamableFluid = "Yes" },
                new() { Id = 2, ToxicOrFlamableFluid = "No" }
            };
        return toxicOrFlamableFluidList;
    }
}

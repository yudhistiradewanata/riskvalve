namespace SharedLayer;

public class InspectionEffectivenessClass
{
    public int Id { get; set; }
    public string? Effectiveness { get; set; }
    public double? EffectivenessValue { get; set; }
    public double? Weighting { get; set; }
}

public class InspectionEffectivenessData : InspectionEffectivenessClass
{
}
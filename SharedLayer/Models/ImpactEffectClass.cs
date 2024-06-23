namespace SharedLayer;

public class ImpactEffectClass
{
    public int Id { get; set; }
    public string? ImpactEffect { get; set; }
    public double? ImpactEffectValue { get; set; }
    public double? Weighting { get; set; }
    public double? Weighting2 { get; set; }
}

public class ImpactEffectData : ImpactEffectClass
{
}
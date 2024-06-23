namespace SharedLayer;

public class RepairedClass
{
    public int Id { get; set; }
    public string? Repaired { get; set; }
    public double? RepairedValue { get; set; }
    public double? Weighting { get; set; }
}

public class RepairedData : RepairedClass
{
}
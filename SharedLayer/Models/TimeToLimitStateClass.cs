namespace SharedLayer;

public class TimeToLimitStateClass
{
    public int Id { get; set; }
    public string? TimeToLimitState { get; set; }
    public double? LimitStateValue { get; set; }
    public double? Weighting { get; set; }
}

public class TimeToLimitStateData : TimeToLimitStateClass
{
}
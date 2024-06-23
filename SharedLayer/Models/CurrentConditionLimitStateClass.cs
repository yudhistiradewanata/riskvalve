namespace SharedLayer;

public class CurrentConditionLimitStateClass
{
    public int Id { get; set; }
    public string? CurrentConditionLimitState { get; set; }
    public double? LimitStateValue { get; set; }
    public double? Weighting { get; set; }
}

public class CurrentConditionLimitStateData : CurrentConditionLimitStateClass
{
}
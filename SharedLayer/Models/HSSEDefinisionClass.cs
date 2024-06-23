namespace SharedLayer;

public class HSSEDefinisionClass
{
    public int Id { get; set; }
    public string? HSSEDefinision { get; set; }
    public double? MinBBSValue { get; set; }
    public string? CoFCategory { get; set; }
    public double? Score { get; set; }
}

public class HSSEDefinisionData : HSSEDefinisionClass
{
}
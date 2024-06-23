namespace SharedLayer;

public class UsedWithinOEMSpecificationClass
{
    public int Id { get; set; }
    public string? UsedWithinOEMSpecification { get; set; }
    public double? UsedWithinOEMSpecificationValue { get; set; }
    public double? Weighting { get; set; }
}

public class UsedWithinOEMSpecificationData : UsedWithinOEMSpecificationClass
{
}
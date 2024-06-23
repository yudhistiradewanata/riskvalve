namespace SharedLayer;

public class AreaClass
{
    public int Id { get; set; }
    public string? BusinessArea { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class AreaData : AreaClass
{
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
}
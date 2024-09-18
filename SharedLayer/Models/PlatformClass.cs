namespace SharedLayer;

public class PlatformClass
{
    public int Id { get; set; }
    public int? AreaID { get; set; }
    public string? Platform { get; set; }
    public string? Code { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public string? UpdatedAt { get; set; }
}

public class PlatformData : PlatformClass
{
    public string? BusinessArea { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
    public string? UpdatedByUser { get; set; }
}
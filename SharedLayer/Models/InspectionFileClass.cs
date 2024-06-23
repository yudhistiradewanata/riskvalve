namespace SharedLayer;

public class InspectionFileClass
{
    public int Id { get; set; }
    public int? InspectionID { get; set; } //FK
    public int? MaintenanceID { get; set; } //FK
    public string? FileName { get; set; }
    public long FileSize { get; set; }
    public string? FileType { get; set; }
    public string? FilePath { get; set; }
    public bool? IsDeleted { get; set; }
    public string? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public string? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
}

public class InspectionFileData : InspectionFileClass
{
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
}
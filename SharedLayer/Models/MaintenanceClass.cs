namespace SharedLayer;

public class MaintenanceClass
{
    public int Id { get; set; }
    public int AssetID { get; set; }
    public int? IsValveRepairedID { get; set; }
    public string? MaintenanceDate { get; set; }
    public string? MaintenanceDescription { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class MaintenanceData : MaintenanceClass
{
    public AssetData? Asset { get; set; }
    public string? IsValveRepaired { get; set; }
    public List<InspectionFileData>? MaintenanceFiles { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
}
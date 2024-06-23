namespace SharedLayer;

public class AssessmentMaintenanceClass
{
    public int Id { get; set; }
    public int? AssessmentID { get; set; }
    public int? MaintenanceID { get; set; }
}

public class AssessmentMaintenanceData : AssessmentMaintenanceClass
{
    public MaintenanceData? Maintenance { get; set; }
}
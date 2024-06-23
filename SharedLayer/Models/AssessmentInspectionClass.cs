namespace SharedLayer;

public class AssessmentInspectionClass
{
    public int Id { get; set; }
    public int? AssessmentID { get; set; }
    public int? InspectionID { get; set; }
}

public class AssessmentInspectionData : AssessmentInspectionClass
{
    public InspectionData? Inspection { get; set; }
}
using SharedLayer;

namespace DataAccessLayer;

public interface IAssessmentInspectionRepository
{
    List<AssessmentInspectionData> GetAssessmentInspectionList(int assessmentId);
    List<AssessmentInspectionData> GetAssessmentInspectionListByInspectionID(int inspectionId);
    void DeleteAssessmentInspection(int assessmentId);
    void AddInspectionToAssessment(int assessmentId, List<int> inspectionIds);
}

public class AssessmentInspectionRepository(ApplicationDbContext context) : IAssessmentInspectionRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<AssessmentInspectionData> GetAssessmentInspectionList(int assessmentId)
    {
        List<AssessmentInspectionData> assessmentInspectionList = [];
        var result =
            from assessmentInspection in _context.AssessmentInspection
            where assessmentInspection.AssessmentID == assessmentId
            select new AssessmentInspectionData
            {
                Id = assessmentInspection.Id,
                AssessmentID = assessmentInspection.AssessmentID,
                InspectionID = assessmentInspection.InspectionID,
            };
        assessmentInspectionList = [.. result];
        return assessmentInspectionList;
    }

    public List<AssessmentInspectionData> GetAssessmentInspectionListByInspectionID(int inspectionId)
    {
        List<AssessmentInspectionData> assessmentInspectionList = [];
        var result =
            from assessmentInspection in _context.AssessmentInspection
            join assessment in _context.Assessment on assessmentInspection.AssessmentID equals assessment.Id
            where assessmentInspection.InspectionID == inspectionId && assessment.IsDeleted == false
            select new AssessmentInspectionData
            {
                Id = assessmentInspection.Id,
                AssessmentID = assessmentInspection.AssessmentID,
                InspectionID = assessmentInspection.InspectionID,
            };
        assessmentInspectionList = [.. result];
        return assessmentInspectionList;
    }

    public void DeleteAssessmentInspection(int assessmentId)
    {
        var assessmentInspectionList = GetAssessmentInspectionList(assessmentId);
        _context.AssessmentInspection.RemoveRange(assessmentInspectionList);
        _context.SaveChanges();
    }

    public void AddInspectionToAssessment(int assessmentId, List<int> inspectionIds)
    {
        foreach (var inspectionId in inspectionIds)
        {
            _context.AssessmentInspection.Add(new AssessmentInspectionClass
            {
                AssessmentID = assessmentId,
                InspectionID = inspectionId,
            });
        }
        _context.SaveChanges();
    }
}
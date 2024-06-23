using SharedLayer;

namespace DataAccessLayer;

public interface IAssessmentMaintenanceRepository
{
    List<AssessmentMaintenanceData> GetAssessmentMaintenanceList(int assessmentId);
    void DeleteAssessmentMaintenance(int assessmentId);
    void AddMaintenanceToAssessment(int assessmentId, List<int> maintenanceIds);
}

public class AssessmentMaintenanceRepository(ApplicationDbContext context) : IAssessmentMaintenanceRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<AssessmentMaintenanceData> GetAssessmentMaintenanceList(int assessmentId)
    {
        List<AssessmentMaintenanceData> assessmentMaintenanceList = [];
        var result =
            from assessmentMaintenance in _context.AssessmentMaintenance
            where assessmentMaintenance.AssessmentID == assessmentId
            select new AssessmentMaintenanceData
            {
                Id = assessmentMaintenance.Id,
                AssessmentID = assessmentMaintenance.AssessmentID,
                MaintenanceID = assessmentMaintenance.MaintenanceID,
            };
        assessmentMaintenanceList = [.. result];
        return assessmentMaintenanceList;
    }

    public void DeleteAssessmentMaintenance(int assessmentId)
    {
        var assessmentMaintenanceList = GetAssessmentMaintenanceList(assessmentId);
        _context.AssessmentMaintenance.RemoveRange(assessmentMaintenanceList);
        _context.SaveChanges();
    }

    public void AddMaintenanceToAssessment(int assessmentId, List<int> maintenanceIds)
    {
        foreach (var maintenanceId in maintenanceIds)
        {
            _context.AssessmentMaintenance.Add(new AssessmentMaintenanceClass
            {
                AssessmentID = assessmentId,
                MaintenanceID = maintenanceId,
            });
        }
        _context.SaveChanges();
    }
}
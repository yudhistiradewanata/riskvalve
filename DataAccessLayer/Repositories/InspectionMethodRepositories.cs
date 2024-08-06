using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IInspectionMethodRepository{
    List<InspectionMethodData> GetInspectionMethodList();
}

public class InspectionMethodRepository(ApplicationDbContext context) : IInspectionMethodRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<InspectionMethodData> GetInspectionMethodList()
    {
        List<InspectionMethodData> inspectionMethodDataList;
        var result =
            from inspectionMethod in _context.InspectionMethod
            select new InspectionMethodData
            {
                Id = inspectionMethod.Id,
                InspectionMethod = HttpUtility.HtmlEncode(inspectionMethod.InspectionMethod)
            };
        inspectionMethodDataList = [.. result];
        return inspectionMethodDataList;
    }
}
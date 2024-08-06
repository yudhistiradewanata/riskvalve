using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IRepairedRepository
{
    List<RepairedData> GetRepairedList();
}

public class RepairedRepository(ApplicationDbContext context) : IRepairedRepository
{
    private readonly ApplicationDbContext _context = context;
    public List<RepairedData> GetRepairedList()
    {
        List<RepairedData> repairedList = [];
        var result =
            from repaired in _context.Repaired
            select new RepairedData
            {
                Id = repaired.Id,
                Repaired = HttpUtility.HtmlEncode(repaired.Repaired),
                RepairedValue = repaired.RepairedValue,
                Weighting = repaired.Weighting,
            };
        repairedList = [.. result];
        return repairedList;
    }
}
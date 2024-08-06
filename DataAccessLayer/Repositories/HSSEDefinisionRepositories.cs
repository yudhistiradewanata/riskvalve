using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IHSSEDefinisionRepository
{
    List<HSSEDefinisionData> GetHSSEDefinisionList();
}

public class HSSEDefinisionRepository(ApplicationDbContext context) : IHSSEDefinisionRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<HSSEDefinisionData> GetHSSEDefinisionList()
    {
        List<HSSEDefinisionData> hsseDefinisionList = [];
        var result =
            from hsseDefinision in _context.HSSEDefinision
            orderby hsseDefinision.MinBBSValue ascending
            select new HSSEDefinisionData
            {
                Id = hsseDefinision.Id,
                HSSEDefinision = HttpUtility.HtmlEncode(hsseDefinision.HSSEDefinision),
                MinBBSValue = hsseDefinision.MinBBSValue,
                CoFCategory = HttpUtility.HtmlEncode(hsseDefinision.CoFCategory),
                Score = hsseDefinision.Score,
            };
        hsseDefinisionList = [.. result];
        return hsseDefinisionList;
    }

}
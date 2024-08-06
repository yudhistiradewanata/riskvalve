using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IRecomendationActionRepository
{
    List<RecommendationActionData> GetRecomendationActionList();
}

public class RecomendationActionRepository(ApplicationDbContext context) : IRecomendationActionRepository
{
    private readonly ApplicationDbContext _context = context;
    public List<RecommendationActionData> GetRecomendationActionList()
    {
        List<RecommendationActionData> recomendationActionList = [];
        var result =
            from recomendationAction in _context.RecommendationAction
            select new RecommendationActionData
            {
                Id = recomendationAction.Id,
                RecommendationAction = HttpUtility.HtmlEncode(recomendationAction.RecommendationAction),
            };
        recomendationActionList = [.. result];
        return recomendationActionList;
    }
}
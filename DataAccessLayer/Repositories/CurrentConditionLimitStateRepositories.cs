using SharedLayer;

namespace DataAccessLayer;

public interface ICurrentConditionLimitStateRepository
{
    List<CurrentConditionLimitStateData> GetCurrentConditionLimitStateList();
}

public class CurrentConditionLimitStateRepository(ApplicationDbContext context) : ICurrentConditionLimitStateRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<CurrentConditionLimitStateData> GetCurrentConditionLimitStateList()
    {
        List<CurrentConditionLimitStateData> currentConditionLimitStateDataList;
        var result =
            from currentConditionLimitState in _context.CurrentConditionLimitState
            select new CurrentConditionLimitStateData
            {
                Id = currentConditionLimitState.Id,
                CurrentConditionLimitState = currentConditionLimitState.CurrentConditionLimitState,
                LimitStateValue = currentConditionLimitState.LimitStateValue,
                Weighting = currentConditionLimitState.Weighting
            };
        currentConditionLimitStateDataList = [.. result];
        return currentConditionLimitStateDataList;
    }
}
using SharedLayer;

namespace DataAccessLayer;

public interface ITimeToLimitStateRepository
{
    List<TimeToLimitStateData> GetTimeToLimitStateList();
}

public class TimeToLimitStateRepository(ApplicationDbContext context) : ITimeToLimitStateRepository
{
    private readonly ApplicationDbContext _context = context;
    public List<TimeToLimitStateData> GetTimeToLimitStateList()
    {
        List<TimeToLimitStateData> timeToLimitStateList = [];
        var result =
            from timeToLimitState in _context.TimeToLimitState
            select new TimeToLimitStateData
            {
                Id = timeToLimitState.Id,
                TimeToLimitState = timeToLimitState.TimeToLimitState,
                LimitStateValue = timeToLimitState.LimitStateValue,
                Weighting = timeToLimitState.Weighting,
            };
        timeToLimitStateList = [.. result];
        return timeToLimitStateList;
    }
}
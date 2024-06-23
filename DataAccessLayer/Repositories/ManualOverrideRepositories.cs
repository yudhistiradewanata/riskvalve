using SharedLayer;

namespace DataAccessLayer;

public interface IManualOverrideRepository
{
    List<ManualOverrideData> GetManualOverrideList();
}

public class ManualOverrideRepository(ApplicationDbContext context) : IManualOverrideRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<ManualOverrideData> GetManualOverrideList()
    {
        List<ManualOverrideData> manualoverridelist;
        var result =
            from manualoverride in _context.ManualOverride
            select new ManualOverrideData
            {
                Id = manualoverride.Id,
                ManualOverride = manualoverride.ManualOverride
            };
        manualoverridelist = [.. result];
        return manualoverridelist;
    }
}
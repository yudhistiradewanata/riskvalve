using SharedLayer;

namespace DataAccessLayer;

public interface ILogRepository
{
    void AddLog(LogClass log);
}

public class LogRepository(ApplicationDbContext context) : ILogRepository
{
    private readonly ApplicationDbContext _context = context;

    public void AddLog(LogClass log)
    {
        _context.Log.Add(log);
        _context.SaveChanges();
    }
}
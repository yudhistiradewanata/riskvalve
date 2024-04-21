using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class LogContext : DbContext
{
    public DbSet<LogDB> Log { get; set; }
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class LogDB
{
    public int Id { get; set; }
    public string? Module { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public string? Message { get; set; }
    public string? Data { get; set; }
}

public class LogModel : LogDB {
    public string? CreatedByUser { get; set; }
    public LogModel GetLogModel(int id)
    {
        LogModel log = new();
        using (var context = new LogContext())
        {
            log = (
                from l in context.Log
                where l.Id == id
                select new LogModel
                {
                    Id = l.Id,
                    Module = l.Module,
                    CreatedBy = l.CreatedBy,
                    CreatedAt = l.CreatedAt,
                    Message = l.Message,
                    Data = l.Data,
                    CreatedByUser = context.User.Where(u => u.Id == l.CreatedBy).FirstOrDefault().Username
                }
            ).ToList().FirstOrDefault();
        }
        return log;
    }

    public List<LogModel> GetLogList(bool IncludeDeleted = false)
    {
        List<LogModel> logList = new();
        using (var context = new LogContext())
        {
            logList = (
                from l in context.Log
                select new LogModel
                {
                    Id = l.Id,
                    Module = l.Module,
                    CreatedBy = l.CreatedBy,
                    CreatedAt = l.CreatedAt,
                    Message = l.Message,
                    Data = l.Data,
                    CreatedByUser = context.User.Where(u => u.Id == l.CreatedBy).FirstOrDefault().Username
                }
            ).ToList();
        }
        return logList;
    }

    public void AddLog(LogDB log)
    {
        using (var context = new LogContext())
        {
            context.Log.Add(log);
            context.SaveChanges();
        }
    }
}

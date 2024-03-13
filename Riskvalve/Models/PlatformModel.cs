using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Riskvalve.Models;

public class PlatformContext : DbContext
{
    public DbSet<PlatformDB> Platform { get; set; }
    public DbSet<AreaModel> Area { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(
                "Server=127.0.0.1,1433;Database=Riskvalve;User Id=SA;Password=DB_Password;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;"
            )
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class PlatformDB
{
    public int Id { get; set; }
    public int? AreaID { get; set; }
    public string? Platform { get; set; }
    public string? Code { get; set; }
}

public class PlatformModel : PlatformDB
{
    // public int Id { get; set; }
    // public int? AreaID { get; set; }
    // public string? Platform { get; set; }
    // public string? Code { get; set; }
    public string? BusinessArea { get; set; }

    public PlatformModel GetPlatformModel(int id)
    {
        PlatformModel platform = new();
        using (var context = new PlatformContext())
        {
            List<PlatformModel> platformList = (
                from p in context.Platform
                join a in context.Area on p.AreaID equals a.Id
                where p.Id == id
                select new PlatformModel
                {
                    Id = p.Id,
                    AreaID = p.AreaID,
                    Platform = p.Platform,
                    Code = p.Code,
                    BusinessArea = a!.BusinessArea
                }
            ).ToList();
            platform = platformList[0];
        }
        return platform;
    }

    public List<PlatformModel> GetPlatformList()
    {
        List<PlatformModel> platformList = new();
        using (var context = new PlatformContext())
        {
            platformList = (
                from p in context.Platform
                join a in context.Area on p.AreaID equals a.Id
                select new PlatformModel
                {
                    Id = p.Id,
                    AreaID = p.AreaID,
                    Platform = p.Platform,
                    Code = p.Code,
                    BusinessArea = a!.BusinessArea
                }
            ).ToList();
        }
        return platformList;
    }

    public void AddPlatform(PlatformDB platform)
    {
        using (var context = new PlatformContext())
        {
            context.Platform.Add(platform);
            context.SaveChanges();
        }
    }

    public void UpdatePlatform(PlatformDB platform)
    {
        using (var context = new PlatformContext())
        {
            context.Platform.Update(platform);
            context.SaveChanges();
        }
    }

    public void DeletePlatform(int id)
    {
        using (var context = new PlatformContext())
        {
            PlatformDB platform = context.Platform.Find(id);
            context.Platform.Remove(platform);
            context.SaveChanges();
        }
    }
}

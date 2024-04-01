using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Riskvalve.Models;

public class PlatformContext : DbContext
{
    public DbSet<PlatformDB> Platform { get; set; }
    public DbSet<AreaModel> Area { get; set; }
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class PlatformDB
{
    public int Id { get; set; }
    public int? AreaID { get; set; }
    public string? Platform { get; set; }
    public string? Code { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class PlatformModel : PlatformDB
{
    public string? BusinessArea { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }

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
                    BusinessArea = a.BusinessArea,
                    CreatedByUser = context
                        .User.Where(u => u.Id == a.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == a.DeletedBy)
                        .FirstOrDefault()
                        .Username
                }
            ).ToList();
            platform = platformList.FirstOrDefault();
        }
        return platform;
    }

    public List<PlatformModel> GetPlatformList(int AreaID = 0, bool IncludeDeleted = false)
    {
        List<PlatformModel> platformList = new();
        using (var context = new PlatformContext())
        {
            platformList = (
                from p in context.Platform
                join a in context.Area on p.AreaID equals a.Id
                where (p.AreaID == AreaID || AreaID == 0) && (IncludeDeleted == true || p.IsDeleted == false)
                select new PlatformModel
                {
                    Id = p.Id,
                    AreaID = p.AreaID,
                    Platform = p.Platform,
                    Code = p.Code,
                    BusinessArea = a.BusinessArea,
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    DeletedBy = a.DeletedBy,
                    DeletedAt = a.DeletedAt,
                    CreatedByUser = context
                        .User.Where(u => u.Id == a.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == a.DeletedBy)
                        .FirstOrDefault()
                        .Username
                }
            ).ToList();
        }
        return platformList;
    }

    public void AddPlatform(PlatformDB platform)
    {
        using (var context = new PlatformContext())
        {
            platform.IsDeleted = false;
            context.Platform.Add(platform);
            context.SaveChanges();
        }
    }

    public void UpdatePlatform(PlatformDB platform)
    {
        using (var context = new PlatformContext())
        {
            PlatformDB platformOld = context.Platform.Find(platform.Id);
            platformOld.IsDeleted = false;
            platformOld.AreaID = platform.AreaID;
            platformOld.Platform = platform.Platform;
            platformOld.Code = platform.Code;
            context.Platform.Update(platformOld);
            context.SaveChanges();
        }
    }

    public void DeletePlatform(PlatformDB platform)
    {
        using (var context = new PlatformContext())
        {
            PlatformDB platformOld = context.Platform.Find(platform.Id);
            platformOld.IsDeleted = true;
            platformOld.DeletedBy = platform.DeletedBy;
            platformOld.DeletedAt = platform.DeletedAt;
            context.Platform.Update(platformOld);
            context.SaveChanges();
        }
    }
}

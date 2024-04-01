using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class AreaContext : DbContext
{
    public DbSet<AreaDB> Area { get; set; }
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class AreaDB {
    public int Id { get; set; }
    public string? BusinessArea { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class AreaModel : AreaDB
{
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
    public AreaModel GetAreaModel(int id)
    {
        AreaModel area = new();
        using (var context = new AreaContext())
        {
            area = (
                from a in context.Area
                where a.Id == id
                select new AreaModel
                {
                    Id = a.Id,
                    BusinessArea = a.BusinessArea,
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    DeletedBy = a.DeletedBy,
                    DeletedAt = a.DeletedAt,
                    CreatedByUser = context.User.Where(u => u.Id == a.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == a.DeletedBy).FirstOrDefault().Username
                }
            ).ToList().FirstOrDefault();
        }
        return area;
    }

    public List<AreaModel> GetAreaList(bool IncludeDeleted = false)
    {
        List<AreaModel> areaList = new();
        using (var context = new AreaContext())
        {
            areaList = (
                from a in context.Area
                where IncludeDeleted == true || a.IsDeleted == false
                select new AreaModel
                {
                    Id = a.Id,
                    BusinessArea = a.BusinessArea,
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    DeletedBy = a.DeletedBy,
                    DeletedAt = a.DeletedAt,
                    CreatedByUser = context.User.Where(u => u.Id == a.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == a.DeletedBy).FirstOrDefault().Username
                }
            ).ToList();
        }
        return areaList;
    }

    public void AddArea(AreaDB area)
    {
        using (var context = new AreaContext())
        {
            area.IsDeleted = false;
            context.Area.Add(area);
            context.SaveChanges();
        }
    }

    public void UpdateArea(AreaDB area)
    {
        using (var context = new AreaContext())
        {
            AreaDB areaOld = context.Area.Find(area.Id);
            areaOld.BusinessArea = area.BusinessArea;
            area.IsDeleted = false;
            context.Area.Update(areaOld);
            context.SaveChanges();
        }
    }

    public void DeleteArea(AreaDB area)
    {
        using (var context = new AreaContext())
        {
            AreaDB areaOld = context.Area.Find(area.Id);
            areaOld.IsDeleted = true;
            areaOld.DeletedBy = area.DeletedBy;
            areaOld.DeletedAt = area.DeletedAt;
            context.Area.Update(areaOld);
            context.SaveChanges();
        }
    }
}

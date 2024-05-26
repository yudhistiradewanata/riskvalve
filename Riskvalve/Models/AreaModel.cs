using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class AreaContext : DbContext
{
    public DbSet<AreaDB> Area { get; set; }
    public DbSet<PlatformDB> Platform { get; set; }
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class AreaDB
{
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
                    CreatedByUser = context
                        .User.Where(u => u.Id == a.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == a.DeletedBy)
                        .FirstOrDefault()
                        .Username
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
        return areaList;
    }

    public ResultModel AddArea(AreaDB area)
    {
        using (var context = new AreaContext())
        {
            AreaDB areaCheck = context
                .Area.Where(a => a.BusinessArea == area.BusinessArea && a.IsDeleted == false)
                .FirstOrDefault();
            if (areaCheck != null)
            {
                return new ResultModel
                {
                    Result = 400,
                    Message = "Area already exists"
                };
            }
            area.IsDeleted = false;
            context.Area.Add(area);
            context.SaveChanges();
            return new ResultModel
            {
                Result = 200,
                Message = "Area added successfully"
            };
        }
    }

    public ResultModel UpdateArea(AreaDB area)
    {
        using (var context = new AreaContext())
        {
            AreaDB areaCheck = context
                .Area.Where(a => a.BusinessArea == area.BusinessArea && a.IsDeleted == false)
                .FirstOrDefault();
            if (areaCheck != null)
            {
                return new ResultModel
                {
                    Result = 400,
                    Message = "Area already exists"
                };
            }
            AreaDB areaOld = context.Area.Find(area.Id);
            areaOld.BusinessArea = area.BusinessArea;
            area.IsDeleted = false;
            context.Area.Update(areaOld);
            context.SaveChanges();
            return new ResultModel
            {
                Result = 200,
                Message = "Area updated successfully"
            };
        }
    }

    public ResultModel DeleteArea(AreaDB area)
    {
        using (var context = new AreaContext())
        {
            int platformCount = context.Platform
                .Where(p => p.AreaID == area.Id && p.IsDeleted == false)
                .Count();
            if (platformCount > 0) {
                return new ResultModel
                {
                    Result = 400,
                    Message = "Area is used by " + platformCount + " platform(s)"
                };
            }
            AreaDB areaOld = context.Area.Find(area.Id);
            areaOld.IsDeleted = true;
            areaOld.DeletedBy = area.DeletedBy;
            areaOld.DeletedAt = area.DeletedAt;
            context.Area.Update(areaOld);
            context.SaveChanges();
            return new ResultModel
            {
                Result = 200,
                Message = "Area deleted successfully"
            };
        }
    }
}

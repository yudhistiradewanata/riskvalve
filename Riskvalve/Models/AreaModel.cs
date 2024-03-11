using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class AreaContext : DbContext
{
    public DbSet<AreaModel> Area { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=127.0.0.1,1433;Database=Riskvalve;User Id=SA;Password=DB_Password;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;")
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class AreaModel {
    public int Id { get; set; }
    public string? BusinessArea { get; set; }

    public AreaModel GetAreaModel(int id)
    {
        AreaModel area = new();
        using (var context = new AreaContext())
        {
            area = context.Area.Find(id);
        }
        return area;
    }

    public List<AreaModel> GetAreaList()
    {
        List<AreaModel> areaList = new();
        using (var context = new AreaContext())
        {
            areaList = context.Area.ToList();
        }
        return areaList;
    }

    public void AddArea(AreaModel area)
    {
        using (var context = new AreaContext())
        {
            context.Area.Add(area);
            context.SaveChanges();
        }
    }

    public void UpdateArea(AreaModel area)
    {
        using (var context = new AreaContext())
        {
            context.Area.Update(area);
            context.SaveChanges();
        }
    }

    public void DeleteArea(int id)
    {
        using (var context = new AreaContext())
        {
            AreaModel area = context.Area.Find(id);
            context.Area.Remove(area);
            context.SaveChanges();
        }
    }
}
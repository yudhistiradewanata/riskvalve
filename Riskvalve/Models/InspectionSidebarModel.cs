using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class InspectionSidebarContext : DbContext
{
    public DbSet<AssetDB> Asset { get; set; }
    public DbSet<PlatformDB> Platform { get; set; }
    public DbSet<AreaModel> Area { get; set; }
    public DbSet<InspectionDB> Inspection { get; set; }
    public DbSet<MaintenanceDB> Maintenance { get; set; }
    public DbSet<AssessmentDB> Assessment { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class InspectionSidebarModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<InspectionSidebarModel>? Child { get; set; }
}

public class InspectionSidebarHistory
{
    public List<InspectionSidebarModel> GetInspectionSidebarHistory(string dataFor)
    {
        List<InspectionSidebarModel> inspectionSidebar = new();
        using (var context = new InspectionSidebarContext())
        {
            inspectionSidebar = context
                .Area.Where(a => a.IsDeleted == false)
                .Select(a => new InspectionSidebarModel
                {
                    Id = a.Id,
                    Name = a.BusinessArea,
                    Child = context
                        .Platform.Where(p => p.AreaID == a.Id && p.IsDeleted == false)
                        .Select(p => new InspectionSidebarModel
                        {
                            Id = p.Id,
                            Name = p.Platform,
                            Child = context
                                .Asset.Where(asset => asset.PlatformID == p.Id)
                                .Select(asset => new InspectionSidebarModel
                                {
                                    Id = asset.Id,
                                    Name = asset.TagNo,
                                    Child = (
                                        dataFor.Equals("Inspection")
                                            ? context
                                                .Inspection.Where(i =>
                                                    i.AssetID == asset.Id && i.IsDeleted == false
                                                )
                                                .Select(data => new InspectionSidebarModel
                                                {
                                                    Id = data.Id,
                                                    Name = data.InspectionDate,
                                                })
                                                .ToList()
                                            : (
                                                dataFor.Equals("Maintenance")
                                                    ? context
                                                        .Maintenance.Where(m =>
                                                            m.AssetID == asset.Id
                                                            && m.IsDeleted == false
                                                        )
                                                        .Select(data => new InspectionSidebarModel
                                                        {
                                                            Id = data.Id,
                                                            Name = data.MaintenanceDate,
                                                        })
                                                        .ToList()
                                                    : context
                                                        .Assessment.Where(m =>
                                                            m.AssetID == asset.Id
                                                            && m.IsDeleted == false
                                                        )
                                                        .Select(data => new InspectionSidebarModel
                                                        {
                                                            Id = data.Id,
                                                            Name = data.AssessmentNo,
                                                        })
                                                        .ToList()
                                            )
                                    )
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();
        }
        return inspectionSidebar;
    }
}

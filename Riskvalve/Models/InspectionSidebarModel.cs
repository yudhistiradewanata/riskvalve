using System.Globalization;
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
                            Child = null
                            // context
                            //     .Asset.Where(asset =>
                            //         asset.PlatformID == p.Id && asset.IsDeleted == false
                            //     )
                            //     .Select(asset => new InspectionSidebarModel
                            //     {
                            //         Id = asset.Id,
                            //         Name = asset.TagNo,
                            //         Child = (
                            //             dataFor.Equals("Inspection")
                            //                 ? new InspectionSidebarHistory().GetInspectionSidebar(asset.Id)
                            //                 : dataFor.Equals("Maintenance")
                            //                     ? new InspectionSidebarHistory().GetMaintenanceSidebar(asset.Id)
                            //                     : new InspectionSidebarHistory().GetAssessmentSidebar(asset.Id)
                            //         )
                            //     })
                            //     .ToList()
                        })
                        .ToList()
                })
                .ToList();
        }
        return inspectionSidebar;
    }

    public List<InspectionSidebarModel> GetInspectionSidebar(int assetid)
    {
        return new InspectionModel().GetSidebarInspection(assetid);
    }

    public List<InspectionSidebarModel> GetMaintenanceSidebar(int assetid)
    {
        return new MaintenanceModel().GetSidebarMaintenance(assetid);
    }

    public List<InspectionSidebarModel> GetAssessmentSidebar(int assetid)
    {
        return new AssessmentModel().GetSidebarAssessment(assetid);
    }
}

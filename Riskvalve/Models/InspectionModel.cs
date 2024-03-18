using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models
{
    public class InspectionContext : DbContext
    {
        public DbSet<AssetDB> Asset { get; set; }
        public DbSet<PlatformDB> Platform { get; set; }
        public DbSet<AreaModel> Area { get; set; }

        // public DbSet<InspectionDB> Inspection { get; set; }
        public DbSet<InspectionEffectivenessModel> InspectionEffectiveness { get; set; }

        // public DbSet<ConditionLimitStateDB> ConditionLimitState { get; set; }
        // public DbSet<IsValveRepairedDB> IsValveRepaired { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options
                .UseSqlServer(
                    "Server=127.0.0.1,1433;Database=Riskvalve;User Id=SA;Password=DB_Password;Encrypt=False;Connection Timeout=30;"
                )
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    }

    public class InspectionModel
    {
        public int Id { get; set; }
        public string? ValveTagNo { get; set; }
        public string? InspectionDate { get; set; }
        public string? InspectionMethod { get; set; }
        public int? InspectionEffectivenessID { get; set; }
        public string? InspectionDescription { get; set; }
        public int? InspectionCurrentLimitStateAID { get; set; }
        public int? InspectionCurrentLimitStateBID { get; set; }
        public int? InspectionCurrentLimitStateCID { get; set; }
        public string? FunctionCondition { get; set; }
        public string? TestPressureIfAny { get; set; }
    }

    public class InspectionSidebarModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<InspectionSidebarModel>? Child { get; set; }
    }

    public class InspectionSidebarHistory
    {
        public List<InspectionSidebarModel> GetInspectionSidebarHistory()
        {
            List<InspectionSidebarModel> inspectionSidebar = new();
            using (var context = new InspectionContext())
            {
                inspectionSidebar = context.Area.Select(a => new InspectionSidebarModel
                {
                    Id = a.Id,
                    Name = a.BusinessArea,
                    Child = context.Platform.Where(p => p.AreaID == a.Id).Select(p => new InspectionSidebarModel
                    {
                        Id = p.Id,
                        Name = p.Platform,
                        Child = context.Asset.Where(asset => asset.PlatformID == p.Id).Select(asset => new InspectionSidebarModel
                        {
                            Id = asset.Id,
                            Name = asset.TagNo,
                            // Child = context.Inspection.Where(inspection => inspection.ValveTagNo == asset.TagNo).Select(inspection => new InspectionSidebarModel
                            // {
                            //     Id = inspection.Id,
                            //     Name = inspection.InspectionDate
                            // }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList();
            }
            return inspectionSidebar;
        }
        // Area -> Platform -> Asset -> Inspection
    }

    public class ConditionLimitStateModel
    {
        public int Id { get; set; }
        public string? LimitState { get; set; }

        public List<ConditionLimitStateModel> GetConditionLimitStates()
        {
            List<ConditionLimitStateModel> conditionLimitStates =
                new()
                {
                    new ConditionLimitStateModel { Id = 1, LimitState = "Good" },
                    new ConditionLimitStateModel { Id = 2, LimitState = "Fair" },
                    new ConditionLimitStateModel { Id = 3, LimitState = "Poor" }
                };
            return conditionLimitStates;
        }
    }

    public class InspectionEffectivenessModel
    {
        public int Id { get; set; }
        public string? Effectiveness { get; set; }

        public List<InspectionEffectivenessModel> GetInspectionEffectivenessStates()
        {
            List<InspectionEffectivenessModel> inspectionEffectiveness = new();
            using (var context = new InspectionContext())
            {
                inspectionEffectiveness = context.InspectionEffectiveness.ToList();
            }
            return inspectionEffectiveness;
        }
    }

    public class IsValveRepairedModel
    {
        public int Id { get; set; }
        public string? IsValveRepaired { get; set; }

        public List<IsValveRepairedModel> GetIsValveRepairedStates()
        {
            List<IsValveRepairedModel> isValveRepaired =
                new()
                {
                    new IsValveRepairedModel { Id = 1, IsValveRepaired = "Yes" },
                    new IsValveRepairedModel { Id = 2, IsValveRepaired = "No" }
                };
            return isValveRepaired;
        }
    }
}

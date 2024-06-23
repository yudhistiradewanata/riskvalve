using Microsoft.EntityFrameworkCore;
using SharedLayer;

namespace DataAccessLayer;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<UserClass> User { get; set; }
    public DbSet<LogClass> Log { get; set; }
    // Area, Platform & Asset
    public DbSet<AreaClass> Area { get; set; }
    public DbSet<PlatformClass> Platform { get; set; }
    public DbSet<AssetClass> Asset { get; set; }
    public DbSet<FluidPhaseClass> FluidPhase { get; set; }
    public DbSet<ManualOverrideClass> ManualOverride { get; set; }
    public DbSet<ToxicOrFlamableFluidClass> ToxicOrFlamableFluid { get; set; }
    public DbSet<ValveTypeClass> ValveType { get; set; }
    // Maintenance & Inspection
    public DbSet<MaintenanceClass> Maintenance { get; set; }
    public DbSet<InspectionFileClass> InspectionFile { get; set; }
    public DbSet<IsValveRepairedClass> IsValveRepaired { get; set; }
    public DbSet<InspectionClass> Inspection {get; set;}
    public DbSet<InspectionMethodClass> InspectionMethod { get; set; }
    public DbSet<CurrentConditionLimitStateClass> CurrentConditionLimitState { get; set; }
    public DbSet<InspectionEffectivenessClass> InspectionEffectiveness { get; set; }
    // Assessment
    public DbSet<AssessmentClass> Assessment { get; set; }
    public DbSet<AssessmentMaintenanceClass> AssessmentMaintenance { get; set; }
    public DbSet<AssessmentInspectionClass> AssessmentInspection { get; set; }
    public DbSet<HSSEDefinisionClass> HSSEDefinision { get; set; }
    public DbSet<ImpactEffectClass> ImpactEffect { get; set; }
    public DbSet<RecommendationActionClass> RecommendationAction { get; set; }
    public DbSet<RepairedClass> Repaired { get; set; }
    public DbSet<TimeToLimitStateClass> TimeToLimitState { get; set; }
    public DbSet<UsedWithinOEMSpecificationClass> UsedWithinOEMSpecification { get; set; }
}

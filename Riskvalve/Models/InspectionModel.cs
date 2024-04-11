using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class InspectionContext : DbContext
{
    public DbSet<AssetDB> Asset { get; set; }
    public DbSet<InspectionDB> Inspection { get; set; }
    public DbSet<InspectionEffectivenessModel> InspectionEffectiveness { get; set; }
    public DbSet<CurrentConditionLimitStateModel> CurrentConditionLimitState { get; set; }
    public DbSet<InspectionMethodModel> InspectionMethod { get; set; }
    public DbSet<UserModel> User { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class InspectionDB
{
    public int Id { get; set; }
    public int AssetID { get; set; } //FK
    public string? InspectionDate { get; set; }
    public int InspectionMethodID { get; set; } //FK
    public int InspectionEffectivenessID { get; set; } //FK
    public string? InspectionDescription { get; set; }
    public int CurrentConditionLeakeageToAtmosphereID { get; set; } //FK
    public int CurrentConditionFailureOfFunctionID { get; set; } //FK
    public int CurrentConditionPassingAcrossValveID { get; set; } //FK
    public string? FunctionCondition { get; set; }
    public string? TestPressureIfAny { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class InspectionModel : InspectionDB
{
    public AssetModel? Asset { get; set; }
    public string? InspectionMethod { get; set; }
    public string? InspectionEffectiveness { get; set; }
    public string? CurrentConditionLeakeageToAtmosphere { get; set; }
    public string? CurrentConditionFailureOfFunction { get; set; }
    public string? CurrentConditionPassingAcrossValve { get; set; }
    public List<InspectionFileModel>? InspectionFiles { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }

    public InspectionModel GetInspectionModel(int id)
    {
        InspectionModel inspectionData = new();
        using (var context = new InspectionContext())
        {
            List<InspectionModel> inspecionList = (
                from inspection in context.Inspection
                join asset in context.Asset on inspection.AssetID equals asset.Id
                join inspectionMethod in context.InspectionMethod
                    on inspection.InspectionMethodID equals inspectionMethod.Id
                join inspectionEffectiveness in context.InspectionEffectiveness
                    on inspection.InspectionEffectivenessID equals inspectionEffectiveness.Id
                join currentConditionLimitStateA in context.CurrentConditionLimitState
                    on inspection.CurrentConditionLeakeageToAtmosphereID equals currentConditionLimitStateA.Id
                join currentConditionLimitStateB in context.CurrentConditionLimitState
                    on inspection.CurrentConditionFailureOfFunctionID equals currentConditionLimitStateB.Id
                join currentConditionLimitStateC in context.CurrentConditionLimitState
                    on inspection.CurrentConditionPassingAcrossValveID equals currentConditionLimitStateC.Id
                where inspection.Id == id
                select new InspectionModel
                {
                    Id = inspection.Id,
                    AssetID = inspection.AssetID,
                    InspectionDate = inspection.InspectionDate,
                    InspectionMethodID = inspection.InspectionMethodID,
                    InspectionEffectivenessID = inspection.InspectionEffectivenessID,
                    InspectionDescription = inspection.InspectionDescription,
                    CurrentConditionLeakeageToAtmosphereID =
                        inspection.CurrentConditionLeakeageToAtmosphereID,
                    CurrentConditionFailureOfFunctionID =
                        inspection.CurrentConditionFailureOfFunctionID,
                    CurrentConditionPassingAcrossValveID =
                        inspection.CurrentConditionPassingAcrossValveID,
                    FunctionCondition = inspection.FunctionCondition,
                    TestPressureIfAny = inspection.TestPressureIfAny,
                    Asset = new AssetModel().GetAssetModel(inspection.AssetID),
                    InspectionMethod = inspectionMethod.InspectionMethod,
                    InspectionEffectiveness = inspectionEffectiveness.Effectiveness,
                    CurrentConditionLeakeageToAtmosphere =
                        currentConditionLimitStateA.CurrentConditionLimitState,
                    CurrentConditionFailureOfFunction =
                        currentConditionLimitStateB.CurrentConditionLimitState,
                    CurrentConditionPassingAcrossValve =
                        currentConditionLimitStateC.CurrentConditionLimitState,
                    InspectionFiles = new InspectionFileModel().GetInspectionFiles(inspection.Id),
                    IsDeleted = inspection.IsDeleted,
                    CreatedBy = inspection.CreatedBy,
                    CreatedAt = inspection.CreatedAt,
                    DeletedBy = inspection.DeletedBy,
                    DeletedAt = inspection.DeletedAt,
                    CreatedByUser = context.User.Where(u => u.Id == inspection.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == inspection.DeletedBy).FirstOrDefault().Username
                }
            ).ToList();
            inspectionData = inspecionList.FirstOrDefault();
        }
        return inspectionData;
    }

    public List<InspectionModel> GetInspectionList(bool IncludeDeleted = false, int AssetID = 0)
    {
        List<InspectionModel> inspectionData = new();
        using (var context = new InspectionContext())
        {
            inspectionData = (
                from inspection in context.Inspection
                join asset in context.Asset on inspection.AssetID equals asset.Id
                join inspectionMethod in context.InspectionMethod
                    on inspection.InspectionMethodID equals inspectionMethod.Id
                join inspectionEffectiveness in context.InspectionEffectiveness
                    on inspection.InspectionEffectivenessID equals inspectionEffectiveness.Id
                join currentConditionLimitStateA in context.CurrentConditionLimitState
                    on inspection.CurrentConditionLeakeageToAtmosphereID equals currentConditionLimitStateA.Id
                join currentConditionLimitStateB in context.CurrentConditionLimitState
                    on inspection.CurrentConditionFailureOfFunctionID equals currentConditionLimitStateB.Id
                join currentConditionLimitStateC in context.CurrentConditionLimitState
                    on inspection.CurrentConditionPassingAcrossValveID equals currentConditionLimitStateC.Id
                where (IncludeDeleted == true || inspection.IsDeleted == false) && (AssetID == 0 || inspection.AssetID == AssetID)
                select new InspectionModel
                {
                    Id = inspection.Id,
                    AssetID = inspection.AssetID,
                    InspectionDate = inspection.InspectionDate,
                    InspectionMethodID = inspection.InspectionMethodID,
                    InspectionEffectivenessID = inspection.InspectionEffectivenessID,
                    InspectionDescription = inspection.InspectionDescription,
                    CurrentConditionLeakeageToAtmosphereID =
                        inspection.CurrentConditionLeakeageToAtmosphereID,
                    CurrentConditionFailureOfFunctionID =
                        inspection.CurrentConditionFailureOfFunctionID,
                    CurrentConditionPassingAcrossValveID =
                        inspection.CurrentConditionPassingAcrossValveID,
                    FunctionCondition = inspection.FunctionCondition,
                    TestPressureIfAny = inspection.TestPressureIfAny,
                    Asset = new AssetModel().GetAssetModel(inspection.AssetID),
                    InspectionMethod = inspectionMethod.InspectionMethod,
                    InspectionEffectiveness = inspectionEffectiveness.Effectiveness,
                    CurrentConditionLeakeageToAtmosphere =
                        currentConditionLimitStateA.CurrentConditionLimitState,
                    CurrentConditionFailureOfFunction =
                        currentConditionLimitStateB.CurrentConditionLimitState,
                    CurrentConditionPassingAcrossValve =
                        currentConditionLimitStateC.CurrentConditionLimitState,
                    InspectionFiles = new InspectionFileModel().GetInspectionFiles(inspection.Id),
                    IsDeleted = inspection.IsDeleted,
                    CreatedBy = inspection.CreatedBy,
                    CreatedAt = inspection.CreatedAt,
                    DeletedBy = inspection.DeletedBy,
                    DeletedAt = inspection.DeletedAt,
                    CreatedByUser = context.User.Where(u => u.Id == inspection.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == inspection.DeletedBy).FirstOrDefault().Username
                }
            ).ToList();
        }
        return inspectionData;
    }

    public int AddInspection(InspectionDB inspection)
    {
        using (var context = new InspectionContext())
        {
            inspection.IsDeleted = false;
            context.Inspection.Add(inspection);
            context.SaveChanges();
            return inspection.Id;
        }
    }

    public bool UpdateInspection(InspectionDB inspection)
    {
        using (var context = new InspectionContext())
        {
            InspectionDB oldInspection = context.Inspection.Find(inspection.Id);
            oldInspection.AssetID = inspection.AssetID;
            oldInspection.InspectionDate = inspection.InspectionDate;
            oldInspection.InspectionMethodID = inspection.InspectionMethodID;
            oldInspection.InspectionEffectivenessID = inspection.InspectionEffectivenessID;
            oldInspection.InspectionDescription = inspection.InspectionDescription;
            oldInspection.CurrentConditionLeakeageToAtmosphereID =
                inspection.CurrentConditionLeakeageToAtmosphereID;
            oldInspection.CurrentConditionFailureOfFunctionID =
                inspection.CurrentConditionFailureOfFunctionID;
            oldInspection.CurrentConditionPassingAcrossValveID =
                inspection.CurrentConditionPassingAcrossValveID;
            oldInspection.FunctionCondition = inspection.FunctionCondition;
            oldInspection.TestPressureIfAny = inspection.TestPressureIfAny;
            context.Inspection.Update(oldInspection);
            context.SaveChanges();
        }
        return true;
    }

    public bool DeleteInspection(InspectionDB inspection)
    {
        using (var context = new InspectionContext())
        {
            InspectionDB oldInspection = context.Inspection.Find(inspection.Id);
            oldInspection.IsDeleted = true;
            oldInspection.DeletedAt = inspection.DeletedAt;
            oldInspection.DeletedBy = inspection.DeletedBy;
            context.Inspection.Update(oldInspection);
            context.SaveChanges();
        }
        return true;
    }
}

// Helper class is below
public class CurrentConditionLimitStateModel
{
    public int Id { get; set; }
    public string? CurrentConditionLimitState { get; set; }
    public double? LimitStateValue { get; set; }
    public double? Weighting { get; set; }
    public List<CurrentConditionLimitStateModel> GetConditionLimitStates()
    {
        List<CurrentConditionLimitStateModel> currentConditionLimitStates = new();
        using (var context = new InspectionContext())
        {
            currentConditionLimitStates = context.CurrentConditionLimitState.ToList();
        }
        return currentConditionLimitStates;
    }
}

public class InspectionEffectivenessModel
{
    public int Id { get; set; }
    public string? Effectiveness { get; set; }
    public double? EffectivenessValue { get; set; }
    public double? Weighting { get; set; }

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

public class InspectionMethodModel
{
    public int Id { get; set; }
    public string? InspectionMethod { get; set; }

    public List<InspectionMethodModel> GetInspectionMethods()
    {
        List<InspectionMethodModel> inspectionMethods = new();
        using (var context = new InspectionContext())
        {
            inspectionMethods = context.InspectionMethod.ToList();
        }
        return inspectionMethods;
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

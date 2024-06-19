using System.Globalization;
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
                    CreatedByUser = context
                        .User.Where(u => u.Id == inspection.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == inspection.DeletedBy)
                        .FirstOrDefault()
                        .Username
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
                where
                    (IncludeDeleted == true || inspection.IsDeleted == false)
                    && (AssetID == 0 || inspection.AssetID == AssetID)
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
                    CreatedByUser = context
                        .User.Where(u => u.Id == inspection.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == inspection.DeletedBy)
                        .FirstOrDefault()
                        .Username
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
            // check if there is already an inspection with the same assetid and inspection date
            if (
                context
                    .Inspection.Select(i => new
                    {
                        i.AssetID,
                        i.InspectionDate,
                        i.IsDeleted
                    })
                    .Where(i =>
                        i.AssetID == inspection.AssetID
                        && i.InspectionDate == inspection.InspectionDate
                        && i.IsDeleted == false
                    )
                    .Count() > 0
            )
            {
                Exception e = new("Inspection already exists for this asset on this date");
                throw e;
            }
            context.Inspection.Add(inspection);
            context.SaveChanges();
            return inspection.Id;
        }
    }

    public ResultModel UpdateInspection(InspectionDB inspection)
    {
        using (var context = new InspectionContext())
        {
            InspectionDB checkInspection = context
                .Inspection.Where(i =>
                    i.InspectionDate == inspection.InspectionDate
                    && i.AssetID == inspection.AssetID
                    && i.Id != inspection.Id
                )
                .FirstOrDefault();
            if (checkInspection != null)
            {
                return new ResultModel { Result = 400, Message = "Inspection date already exists" };
            }
        }
        try
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
            return new ResultModel { Result = 200, Message = "Inspection updated successfully" };
        }
        catch (Exception e)
        {
            return new ResultModel { Result = 400, Message = "Inspection date already exists" };
        }
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

    public ToolImportModel MapInspection(List<Dictionary<string, string>> data)
    {
        ToolImportModel toolImport = new();
        List<string> failedRecords = new();
        List<AssetModel> assetList = new AssetModel().GetAssetList(0, 0, true);
        List<InspectionMethodModel> inspectionMethodList =
            new InspectionMethodModel().GetInspectionMethods();
        List<InspectionEffectivenessModel> inspectionEffectivenessList =
            new InspectionEffectivenessModel().GetInspectionEffectivenessStates();
        List<CurrentConditionLimitStateModel> currentConditionLimitStateList =
            new CurrentConditionLimitStateModel().GetConditionLimitStates();
        List<Dictionary<string, string>> finalResult = new();
        foreach (var records in data)
        {
            Dictionary<string, string> result = new();
            foreach (var record in records)
            {
                string key = record.Key;
                string value = record.Value.Trim().ToLower();
                string valuereal = record.Value.Trim();
                string mappedKey = MapHeader(key);
                string mappedValue = "";
                if (mappedKey.Equals(""))
                {
                    continue;
                }
                if (
                    mappedKey.Equals("AssetID")
                    || mappedKey.Equals("InspectionMethodID")
                    || mappedKey.Equals("InspectionDate")
                    || mappedKey.Equals("InspectionEffectivenessID")
                    || mappedKey.Equals("CurrentConditionLeakeageToAtmosphereID")
                    || mappedKey.Equals("CurrentConditionFailureOfFunctionID")
                    || mappedKey.Equals("CurrentConditionPassingAcrossValveID")
                )
                {
                    if (mappedKey.Equals("AssetID"))
                    {
                        foreach (var asset in assetList)
                        {
                            if (asset.TagNo.Trim().ToLower().Equals(value))
                            {
                                mappedValue = asset.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("InspectionDate"))
                    {
                        if (value.Contains("/"))
                        {
                            string date = value.Split(" ")[0];
                            List<string> dateParts = date.Split("/").ToList();
                            if (dateParts[0].Length == 1)
                            {
                                dateParts[0] = "0" + dateParts[0];
                            }
                            if (dateParts[1].Length == 1)
                            {
                                dateParts[1] = "0" + dateParts[1];
                            }
                            string newDate = dateParts[0] + "-" + dateParts[1] + "-" + dateParts[2];
                            mappedValue = newDate;
                        }
                        else
                        {
                            mappedValue = DateTime
                                .FromOADate(Convert.ToDouble(value))
                                .ToString(Environment.GetDateFormatString(false));
                        }
                    }
                    else if (mappedKey.Equals("InspectionMethodID"))
                    {
                        foreach (var inspectionMethod in inspectionMethodList)
                        {
                            if (inspectionMethod.InspectionMethod.Trim().ToLower().Equals(value))
                            {
                                mappedValue = inspectionMethod.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (mappedKey.Equals("InspectionEffectivenessID"))
                    {
                        foreach (var inspectionEffectiveness in inspectionEffectivenessList)
                        {
                            if (
                                inspectionEffectiveness.Effectiveness.Trim().ToLower().Equals(value)
                            )
                            {
                                mappedValue = inspectionEffectiveness.Id.ToString();
                                break;
                            }
                        }
                    }
                    else if (
                        mappedKey.Equals("CurrentConditionLeakeageToAtmosphereID")
                        || mappedKey.Equals("CurrentConditionFailureOfFunctionID")
                        || mappedKey.Equals("CurrentConditionPassingAcrossValveID")
                    )
                    {
                        foreach (var currentConditionLimitState in currentConditionLimitStateList)
                        {
                            if (
                                currentConditionLimitState
                                    .CurrentConditionLimitState.Trim()
                                    .ToLower()
                                    .Equals(value)
                            )
                            {
                                mappedValue = currentConditionLimitState.Id.ToString();
                                break;
                            }
                        }
                    }
                    if (mappedValue == "")
                    {
                        // Exception e =
                        //     new(
                        //         "Value '"
                        //             + record.Value
                        //             + "' on field '"
                        //             + key
                        //             + "' is not match with the database value"
                        //     );
                        // throw e;
                        failedRecords.Add(
                            "Value '"
                                + record.Value
                                + "' on field '"
                                + key
                                + "' is not match with the database value"
                        );
                    }
                    else
                    {
                        value = mappedValue;
                    }
                }
                else
                {
                    value = valuereal;
                }
                result.Add(mappedKey, value);
            }
            finalResult.Add(result);
        }
        toolImport.failedRecords = failedRecords;
        toolImport.mappedRecords = finalResult;
        return toolImport;
        // return finalResult;
    }

    private string MapHeader(string header)
    {
        switch (header.ToLower())
        {
            case "valve tag no.":
                return "AssetID";
            case "inspection date\n(dd/mm/yyyy)":
                return "InspectionDate";
            case "inspection method":
                return "InspectionMethodID";
            case "inspection effectiveness":
                return "InspectionEffectivenessID";
            case "inspection description":
                return "InspectionDescription";
            case "current condition leakage to atmosphere":
                return "CurrentConditionLeakeageToAtmosphereID";
            case "current condition failure of function":
                return "CurrentConditionFailureOfFunctionID";
            case "current condition passing across valve":
                return "CurrentConditionPassingAcrossValveID";
            case "function condition":
                return "FunctionCondition";
            case "test pressure if any":
                return "TestPressureIfAny";
            default:
                return "";
        }
    }

    public InspectionModel GetLastAssetInspection(int assetID)
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
                where assetID == inspection.AssetID
                orderby inspection.InspectionDate descending
                orderby inspection.Id descending
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
                    CreatedByUser = context
                        .User.Where(u => u.Id == inspection.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == inspection.DeletedBy)
                        .FirstOrDefault()
                        .Username
                }
            ).ToList();
            inspectionData = inspecionList.FirstOrDefault();
        }
        return inspectionData;
    }

    public List<InspectionSidebarModel> GetSidebarInspection(int assetID)
    {
        List<InspectionSidebarModel> inspectionSidebarList = new();
        using (var context = new InspectionContext())
        {
            inspectionSidebarList = (
                from inspection in context.Inspection
                join asset in context.Asset on inspection.AssetID equals asset.Id
                where assetID == inspection.AssetID && inspection.IsDeleted == false
                select new InspectionSidebarModel
                {
                    Id = inspection.Id,
                    Name = inspection.InspectionDate
                }
            ).ToList();
            inspectionSidebarList = inspectionSidebarList
                .OrderByDescending(i =>
                    DateTime.ParseExact(i.Name, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                )
                .ToList();
        }
        return inspectionSidebarList;
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

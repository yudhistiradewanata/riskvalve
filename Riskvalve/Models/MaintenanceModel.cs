using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class MaintenanceContext : DbContext
{
    public DbSet<AssetDB> Asset { get; set; }
    public DbSet<MaintenanceDB> Maintenance { get; set; }
    public DbSet<IsValveRepairedModel> IsValveRepaired { get; set; }
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class MaintenanceDB
{
    public int Id { get; set; }
    public int AssetID { get; set; }
    public int? IsValveRepairedID { get; set; }
    public string? MaintenanceDate { get; set; }
    public string? MaintenanceDescription { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public string? DeletedAt { get; set; }
}

public class MaintenanceModel : MaintenanceDB
{
    public AssetModel? Asset { get; set; }
    public string? IsValveRepaired { get; set; }
    public List<InspectionFileModel>? MaintenanceFiles { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }

    public MaintenanceModel GetMaintenanceModel(int id)
    {
        MaintenanceModel maintenance = new();
        using (var context = new MaintenanceContext())
        {
            maintenance = (
                from m in context.Maintenance
                join ivr in context.IsValveRepaired on m.IsValveRepairedID equals ivr.Id
                where m.Id == id
                select new MaintenanceModel
                {
                    Id = m.Id,
                    AssetID = m.AssetID,
                    IsValveRepairedID = m.IsValveRepairedID,
                    MaintenanceDate = m.MaintenanceDate,
                    MaintenanceDescription = m.MaintenanceDescription,
                    IsDeleted = m.IsDeleted,
                    CreatedBy = m.CreatedBy,
                    CreatedAt = m.CreatedAt,
                    DeletedBy = m.DeletedBy,
                    DeletedAt = m.DeletedAt,
                    Asset = new AssetModel().GetAssetModel(m.AssetID),
                    IsValveRepaired = ivr.IsValveRepaired,
                    CreatedByUser = context
                        .User.Where(u => u.Id == m.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == m.DeletedBy)
                        .FirstOrDefault()
                        .Username,
                    MaintenanceFiles = new InspectionFileModel().GetMaintenanceFiles(m.Id)
                }
            ).ToList().FirstOrDefault();
        }
        return maintenance;
    }

    public List<MaintenanceModel> GetMaintenanceList(bool IncludeDeleted = false, int AssetID = 0)
    {
        List<MaintenanceModel> maintenanceList = new();
        using (var context = new MaintenanceContext())
        {
            maintenanceList = (
                from m in context.Maintenance
                join ivr in context.IsValveRepaired on m.IsValveRepairedID equals ivr.Id
                where
                    (IncludeDeleted == true || m.IsDeleted == false)
                    && (AssetID == 0 || m.AssetID == AssetID)
                select new MaintenanceModel
                {
                    Id = m.Id,
                    AssetID = m.AssetID,
                    IsValveRepairedID = m.IsValveRepairedID,
                    MaintenanceDate = m.MaintenanceDate,
                    MaintenanceDescription = m.MaintenanceDescription,
                    IsDeleted = m.IsDeleted,
                    CreatedBy = m.CreatedBy,
                    CreatedAt = m.CreatedAt,
                    DeletedBy = m.DeletedBy,
                    DeletedAt = m.DeletedAt,
                    Asset = new AssetModel().GetAssetModel(m.AssetID),
                    IsValveRepaired = ivr.IsValveRepaired,
                    CreatedByUser = context
                        .User.Where(u => u.Id == m.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == m.DeletedBy)
                        .FirstOrDefault()
                        .Username,
                    MaintenanceFiles = new InspectionFileModel().GetMaintenanceFiles(m.Id)
                }
            ).ToList();
        }
        return maintenanceList;
    }

    public int AddMaintenance(MaintenanceDB maintenanceDB)
    {
        int maintenanceID = 0;
        using (var context = new MaintenanceContext())
        {
            maintenanceDB.IsDeleted = false;
            // check if there is already an maintenance with the same assetid and maintenance date
            if (
                context
                    .Maintenance.Where(m =>
                        m.AssetID == maintenanceDB.AssetID
                        && m.MaintenanceDate == maintenanceDB.MaintenanceDate
                        && m.IsDeleted == false
                    )
                    .FirstOrDefault() != null
            )
            {
                Exception e = new("Maintenance with the same asset and date already exist");
                throw e;
            }
            context.Maintenance.Add(maintenanceDB);
            context.SaveChanges();
            maintenanceID = maintenanceDB.Id;
        }
        return maintenanceID;
    }

    public ResultModel UpdateMaintenance(MaintenanceDB maintenanceDB)
    {
        using (var context = new MaintenanceContext())
        {
            MaintenanceDB checkMaintenance = context
                .Maintenance.Where(m =>
                    m.Id != maintenanceDB.Id
                    && m.MaintenanceDate == maintenanceDB.MaintenanceDate
                    && m.AssetID == maintenanceDB.AssetID
                    && m.IsDeleted == false
                )
                .FirstOrDefault();
            if (checkMaintenance != null)
            {
                return new ResultModel { Result = 400, Message = "Maintenance with the same asset and date already exist" };
            }
        }
        using (var context = new MaintenanceContext())
        {
            MaintenanceDB oldMaintenance = context.Maintenance.Find(maintenanceDB.Id);
            oldMaintenance.IsDeleted = false;
            oldMaintenance.AssetID = maintenanceDB.AssetID;
            oldMaintenance.IsValveRepairedID = maintenanceDB.IsValveRepairedID;
            oldMaintenance.MaintenanceDate = maintenanceDB.MaintenanceDate;
            oldMaintenance.MaintenanceDescription = maintenanceDB.MaintenanceDescription;
            context.Maintenance.Update(oldMaintenance);
            context.SaveChanges();
            return new ResultModel { Result = 200, Message = "Inspection updated successfully" };
        }
    }

    public void DeleteMaintenance(MaintenanceDB maintenanceDB)
    {
        using (var context = new MaintenanceContext())
        {
            MaintenanceDB oldMaintenance = context.Maintenance.Find(maintenanceDB.Id);
            oldMaintenance.IsDeleted = true;
            oldMaintenance.DeletedBy = maintenanceDB.DeletedBy;
            oldMaintenance.DeletedAt = maintenanceDB.DeletedAt;
            context.Maintenance.Update(oldMaintenance);
            context.SaveChanges();
        }
    }

    public ToolImportModel MapMaintenanceRegister(List<Dictionary<string, string>> data)
    {
        ToolImportModel toolImport = new();
        List<string> failedRecords = new();
        List<AssetModel> assetList = new AssetModel().GetAssetList(0, 0, true);
        List<IsValveRepairedModel> isValveRepairedList =
            new IsValveRepairedModel().GetIsValveRepairedList();
        List<Dictionary<string, string>> finalResult = new();
        foreach (var records in data)
        {
            Dictionary<string, string> result = new();
            foreach (var record in records)
            {
                string key = record.Key;
                string value = record.Value.Trim().ToLower();
                string mappedKey = MapHeader(key);
                string mappedValue = "";
                if (mappedKey.Equals(""))
                {
                    continue;
                }
                if (
                    mappedKey.Equals("AssetID")
                    || mappedKey.Equals("MaintenanceDate")
                    || mappedKey.Equals("IsValveRepairedID")
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
                    else if (mappedKey.Equals("MaintenanceDate"))
                    {
                        // mappedValue = DateTime
                        //     .FromOADate(Convert.ToDouble(value))
                        //     .ToString(Environment.GetDateFormatString(false));
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
                    else if (mappedKey.Equals("IsValveRepairedID"))
                    {
                        foreach (var ivr in isValveRepairedList)
                        {
                            if (ivr.IsValveRepaired.Trim().ToLower().Equals(value))
                            {
                                mappedValue = ivr.Id.ToString();
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
                result.Add(mappedKey, value);
            }
            finalResult.Add(result);
        }
        toolImport.failedRecords = failedRecords;
        toolImport.mappedRecords = finalResult;
        // return finalResult;
        return toolImport;
    }

    private string MapHeader(string header)
    {
        switch (header.ToLower())
        {
            case "valve tag no.":
                return "AssetID";
            case "is valve repaired?\n(y/n)":
                return "IsValveRepairedID";
            case "maintenance date\n(dd/mm/yyyy)":
                return "MaintenanceDate";
            case "maintenance description":
                return "MaintenanceDescription";
            default:
                return "";
        }
    }

    public MaintenanceModel GetLastAssetMaintenance(int assetID)
    {
        MaintenanceModel maintenance = new();
        using (var context = new MaintenanceContext())
        {
            maintenance = (
                from m in context.Maintenance
                join a in context.Asset on m.AssetID equals a.Id
                join ivr in context.IsValveRepaired on m.IsValveRepairedID equals ivr.Id
                where a.Id == assetID
                orderby m.MaintenanceDate descending
                orderby m.Id descending
                select new MaintenanceModel
                {
                    Id = m.Id,
                    AssetID = m.AssetID,
                    IsValveRepairedID = m.IsValveRepairedID,
                    MaintenanceDate = m.MaintenanceDate,
                    MaintenanceDescription = m.MaintenanceDescription,
                    IsDeleted = m.IsDeleted,
                    CreatedBy = m.CreatedBy,
                    CreatedAt = m.CreatedAt,
                    DeletedBy = m.DeletedBy,
                    DeletedAt = m.DeletedAt,
                    Asset = new AssetModel().GetAssetModel(m.AssetID),
                    IsValveRepaired = ivr.IsValveRepaired,
                    CreatedByUser = context
                        .User.Where(u => u.Id == m.CreatedBy)
                        .FirstOrDefault()
                        .Username,
                    DeletedByUser = context
                        .User.Where(u => u.Id == m.DeletedBy)
                        .FirstOrDefault()
                        .Username,
                    MaintenanceFiles = new InspectionFileModel().GetMaintenanceFiles(m.Id)
                }
            ).ToList().FirstOrDefault();
        }
        return maintenance;
    }

    public List<InspectionSidebarModel> GetSidebarMaintenance(int assetID)
    {
        List<InspectionSidebarModel> inspectionSidebar = new();
        using (var context = new MaintenanceContext())
        {
            inspectionSidebar = (
                from m in context.Maintenance
                where m.AssetID == assetID && m.IsDeleted == false
                select new InspectionSidebarModel { Id = m.Id, Name = m.MaintenanceDate }
            ).ToList();
            inspectionSidebar = inspectionSidebar
                .OrderByDescending(i =>
                    DateTime.ParseExact(i.Name, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                )
                .ToList();
        }
        return inspectionSidebar;
    }
}

public class IsValveRepairedModel
{
    public int Id { get; set; }
    public string? IsValveRepaired { get; set; }

    public List<IsValveRepairedModel> GetIsValveRepairedList()
    {
        List<IsValveRepairedModel> isValveRepairedList = new();
        using (var context = new MaintenanceContext())
        {
            isValveRepairedList = (
                from ivr in context.IsValveRepaired
                select new IsValveRepairedModel
                {
                    Id = ivr.Id,
                    IsValveRepaired = ivr.IsValveRepaired
                }
            ).ToList();
        }
        return isValveRepairedList;
    }
}

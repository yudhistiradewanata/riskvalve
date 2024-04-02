using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class MaintenanceContext : DbContext
{
    public DbSet<MaintenanceDB> Maintenance { get; set; }
    public DbSet<IsValveRepairedDB> IsValveRepaired { get; set; }
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
                    CreatedByUser = context.User.Where(u => u.Id == m.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == m.DeletedBy).FirstOrDefault().Username,
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
                where (IncludeDeleted == true || m.IsDeleted == false) && (AssetID == 0 || m.AssetID == AssetID)
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
                    CreatedByUser = context.User.Where(u => u.Id == m.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == m.DeletedBy).FirstOrDefault().Username,
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
            context.Maintenance.Add(maintenanceDB);
            context.SaveChanges();
            maintenanceID = maintenanceDB.Id;
        }
        return maintenanceID;
    }

    public void UpdateMaintenance(MaintenanceDB maintenanceDB)
    {
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
}

public class IsValveRepairedDB
{
    public int Id { get; set; }
    public string? IsValveRepaired { get; set; }

    public List<IsValveRepairedDB> GetIsValveRepairedList()
    {
        List<IsValveRepairedDB> isValveRepairedList = new();
        using (var context = new MaintenanceContext())
        {
            isValveRepairedList = (
                from ivr in context.IsValveRepaired
                select new IsValveRepairedDB { Id = ivr.Id, IsValveRepaired = ivr.IsValveRepaired }
            ).ToList();
        }
        return isValveRepairedList;
    }
}

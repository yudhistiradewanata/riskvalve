using Microsoft.EntityFrameworkCore;

namespace Riskvalve.Models;

public class InspectionFileModelContext : DbContext
{
    public DbSet<InspectionFileDB> InspectionFile { get; set; }
    public DbSet<UserModel> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options
            .UseSqlServer(Environment.GetConnectionStringDB())
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

public class InspectionFileDB
{
    public int Id { get; set; }
    public int? InspectionID { get; set; } //FK
    public int? MaintenanceID { get; set; } //FK
    public string? FileName { get; set; }
    public long FileSize { get; set; }
    public string? FileType { get; set; }
    public string? FilePath { get; set; }
    public bool IsDeleted { get; set; }
    public string? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public string? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
}

public class InspectionFileModel : InspectionFileDB
{
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
    public List<InspectionFileModel> GetInspectionFiles(int inspectionID)
    {
        List<InspectionFileModel> inspectionFiles = new();
        using (var context = new InspectionFileModelContext())
        {
            inspectionFiles = (
                from f in context.InspectionFile
                where f.InspectionID == inspectionID && f.IsDeleted == false
                select new InspectionFileModel
                {
                    Id = f.Id,
                    InspectionID = f.InspectionID,
                    MaintenanceID = f.MaintenanceID,
                    FileName = f.FileName,
                    FileSize = f.FileSize,
                    FileType = f.FileType,
                    FilePath = f.FilePath,
                    IsDeleted = f.IsDeleted,
                    CreatedAt = f.CreatedAt,
                    CreatedBy = f.CreatedBy,
                    DeletedAt = f.DeletedAt,
                    DeletedBy = f.DeletedBy,
                    CreatedByUser = context.User.Where(u => u.Id == f.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == f.DeletedBy).FirstOrDefault().Username
                }
            ).ToList();
        }
        return inspectionFiles;
    }

    public List<InspectionFileModel> GetMaintenanceFiles(int maintenanceID)
    {
        List<InspectionFileModel> maintenanceFiles = new();
        using (var context = new InspectionFileModelContext())
        {
            maintenanceFiles = (
                from f in context.InspectionFile
                where f.MaintenanceID == maintenanceID && f.IsDeleted == false
                select new InspectionFileModel
                {
                    Id = f.Id,
                    InspectionID = f.InspectionID,
                    MaintenanceID = f.MaintenanceID,
                    FileName = f.FileName,
                    FileSize = f.FileSize,
                    FileType = f.FileType,
                    FilePath = f.FilePath,
                    IsDeleted = f.IsDeleted,
                    CreatedAt = f.CreatedAt,
                    CreatedBy = f.CreatedBy,
                    DeletedAt = f.DeletedAt,
                    DeletedBy = f.DeletedBy,
                    CreatedByUser = context.User.Where(u => u.Id == f.CreatedBy).FirstOrDefault().Username,
                    DeletedByUser = context.User.Where(u => u.Id == f.DeletedBy).FirstOrDefault().Username
                }
            ).ToList();
        }
        return maintenanceFiles;
    }

    public bool AddInspectionFile(InspectionFileDB inspectionFile)
    {
        using (var context = new InspectionFileModelContext())
        {
            context.InspectionFile.Add(inspectionFile);
            context.SaveChanges();
        }
        return true;
    }

    public bool DeleteInspectionFile(InspectionFileDB inspectionDB)
    {
        using (var context = new InspectionFileModelContext())
        {
            InspectionFileDB oldInspectionFile = context.InspectionFile.Find(inspectionDB.Id);
            oldInspectionFile.IsDeleted = true;
            oldInspectionFile.DeletedAt = DateTime.Now.ToString(Environment.GetDateFormatString());
            oldInspectionFile.DeletedBy = inspectionDB.DeletedBy;
            context.InspectionFile.Update(oldInspectionFile);
            context.SaveChanges();
        }
        return true;
    }
}

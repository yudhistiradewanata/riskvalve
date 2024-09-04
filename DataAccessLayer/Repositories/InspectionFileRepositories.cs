using System.Web;
using DataAccessLayer;
using SharedLayer;

namespace BusinessLogicLayer;

public interface IInspectionFileRepository
{
    List<InspectionFileData> GetInspectionFiles(int inspectionID);
    List<InspectionFileData> GetMaintenanceFiles(int maintenanceID);
    bool AddInspectionFile(InspectionFileClass inspectionFile);
    bool DeleteInspectionFiles(List<InspectionFileClass> inspectionFiles);
}

public class InspectionFileRepository(ApplicationDbContext context) : IInspectionFileRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<InspectionFileData> GetInspectionFiles(int inspectionID)
    {
        List<InspectionFileData> inspectionFiles = [];
        inspectionFiles = 
        [
            ..(
                from inspectionfile in _context.InspectionFile
                join createby in _context.User
                    on inspectionfile.CreatedBy equals createby.Id
                    into sc
                from subcreateby in sc.DefaultIfEmpty()
                join deleteby in _context.User
                    on inspectionfile.DeletedBy equals deleteby.Id
                    into dc
                from subdeleteby in dc.DefaultIfEmpty()
                where
                    inspectionfile.InspectionID == inspectionID
                    && inspectionfile.IsDeleted == false
                select new InspectionFileData
                {
                    Id = inspectionfile.Id,
                    InspectionID = inspectionfile.InspectionID,
                    MaintenanceID = inspectionfile.MaintenanceID,
                    FileName = SharedEnvironment.HtmlEncode(inspectionfile.FileName),
                    FileSize = inspectionfile.FileSize,
                    FileType = SharedEnvironment.HtmlEncode(inspectionfile.FileType),
                    FilePath = SharedEnvironment.HtmlEncode(inspectionfile.FilePath),
                    IsDeleted = inspectionfile.IsDeleted,
                    CreatedAt = inspectionfile.CreatedAt,
                    CreatedBy = inspectionfile.CreatedBy,
                    DeletedAt = inspectionfile.DeletedAt,
                    DeletedBy = inspectionfile.DeletedBy,
                    CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                    DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? "")
                }
            )
        ];
        return inspectionFiles;
    }

    public List<InspectionFileData> GetMaintenanceFiles(int maintenanceID)
    {
        List<InspectionFileData> maintenanceFiles = [];
        maintenanceFiles =
        [
            .. (
                from inspectionfile in _context.InspectionFile
                join createby in _context.User
                    on inspectionfile.CreatedBy equals createby.Id
                    into sc
                from subcreateby in sc.DefaultIfEmpty()
                join deleteby in _context.User
                    on inspectionfile.DeletedBy equals deleteby.Id
                    into dc
                from subdeleteby in dc.DefaultIfEmpty()
                where
                    inspectionfile.MaintenanceID == maintenanceID
                    && inspectionfile.IsDeleted == false
                select new InspectionFileData
                {
                    Id = inspectionfile.Id,
                    InspectionID = inspectionfile.InspectionID,
                    MaintenanceID = inspectionfile.MaintenanceID,
                    FileName = SharedEnvironment.HtmlEncode(inspectionfile.FileName),
                    FileSize = inspectionfile.FileSize,
                    FileType = SharedEnvironment.HtmlEncode(inspectionfile.FileType),
                    FilePath = SharedEnvironment.HtmlEncode(inspectionfile.FilePath),
                    IsDeleted = inspectionfile.IsDeleted,
                    CreatedAt = inspectionfile.CreatedAt,
                    CreatedBy = inspectionfile.CreatedBy,
                    DeletedAt = inspectionfile.DeletedAt,
                    DeletedBy = inspectionfile.DeletedBy,
                    CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                    DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? "")
                }
            )
        ];
        return maintenanceFiles;
    }

    public bool AddInspectionFile(InspectionFileClass inspectionFile)
    {
        inspectionFile.IsDeleted = false;
        _context.InspectionFile.Add(inspectionFile);
        _context.SaveChanges();
        return true;
    }

    public bool DeleteInspectionFiles(List<InspectionFileClass> inspectionFiles)
    {
        foreach (InspectionFileClass inspectionFile in inspectionFiles)
        {
            InspectionFileClass? oldiInspectionFile = _context.InspectionFile.Find(
                inspectionFile.Id
            );
            if (oldiInspectionFile != null)
            {
                oldiInspectionFile.IsDeleted = true;
                oldiInspectionFile.DeletedAt = inspectionFile.DeletedAt;
                oldiInspectionFile.DeletedBy = inspectionFile.DeletedBy;
                _context.InspectionFile.Update(oldiInspectionFile);
            }
        }
        _context.SaveChanges();
        return true;
    }
}

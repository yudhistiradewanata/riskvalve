using System.Globalization;
using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IMaintenanceRepository
{
    MaintenanceData GetMaintenance(int id);
    List<MaintenanceData> GetMaintenanceList(bool IncludeDeleted = false, int AssetID = 0);
    MaintenanceData AddMaintenance(MaintenanceClass maintenance);
    MaintenanceData UpdateMaintenance(MaintenanceClass maintenance);
    MaintenanceData DeleteMaintenance(MaintenanceClass maintenance);
}

public class MaintenanceRepository(ApplicationDbContext context) : IMaintenanceRepository
{
    private readonly ApplicationDbContext _context = context;

    public MaintenanceData GetMaintenance(int id)
    {
        MaintenanceData? maintenanceData;
        var result =
            from maintenance in _context.Maintenance
            join isValveRepaired in _context.IsValveRepaired
                on maintenance.IsValveRepairedID equals isValveRepaired.Id
                into ivr
            from subIsValveRepaired in ivr.DefaultIfEmpty()
            join createby in _context.User on maintenance.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on maintenance.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where maintenance.Id == id
            select new MaintenanceData
            {
                Id = maintenance.Id,
                AssetID = maintenance.AssetID,
                IsValveRepairedID = maintenance.IsValveRepairedID,
                MaintenanceDate = maintenance.MaintenanceDate,
                MaintenanceDescription = HttpUtility.HtmlEncode(maintenance.MaintenanceDescription),
                IsDeleted = maintenance.IsDeleted,
                CreatedBy = maintenance.CreatedBy,
                CreatedAt = maintenance.CreatedAt,
                DeletedBy = maintenance.DeletedBy,
                DeletedAt = maintenance.DeletedAt,
                CreatedByUser = HttpUtility.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = HttpUtility.HtmlEncode(subdeleteby.Username ?? ""),
                IsValveRepaired = HttpUtility.HtmlEncode(subIsValveRepaired.IsValveRepaired)
            };
        maintenanceData = result.FirstOrDefault();
        if (maintenanceData == null)
        {
            throw new Exception("Maintenance not found");
        }
        return maintenanceData;
    }

    public List<MaintenanceData> GetMaintenanceList(bool IncludeDeleted = false, int AssetID = 0)
    {
        List<MaintenanceData> maintenanceDataList;
        var result =
            from maintenance in _context.Maintenance
            join isValveRepaired in _context.IsValveRepaired
                on maintenance.IsValveRepairedID equals isValveRepaired.Id
                into ivr
            from subIsValveRepaired in ivr.DefaultIfEmpty()
            join createby in _context.User on maintenance.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on maintenance.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where (IncludeDeleted || maintenance.IsDeleted == false)
                && (AssetID == 0 || maintenance.AssetID == AssetID)
            select new MaintenanceData
            {
                Id = maintenance.Id,
                AssetID = maintenance.AssetID,
                IsValveRepairedID = maintenance.IsValveRepairedID,
                MaintenanceDate = maintenance.MaintenanceDate,
                MaintenanceDescription = HttpUtility.HtmlEncode(maintenance.MaintenanceDescription),
                IsDeleted = maintenance.IsDeleted,
                CreatedBy = maintenance.CreatedBy,
                CreatedAt = maintenance.CreatedAt,
                DeletedBy = maintenance.DeletedBy,
                DeletedAt = maintenance.DeletedAt,
                CreatedByUser = HttpUtility.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = HttpUtility.HtmlEncode(subdeleteby.Username ?? ""),
                IsValveRepaired = HttpUtility.HtmlEncode(subIsValveRepaired.IsValveRepaired)
            };
        maintenanceDataList = [.. result];
        return maintenanceDataList;
    }

    public MaintenanceData AddMaintenance(MaintenanceClass maintenance)
    {
        if (
            !DateTime.TryParseExact(
                maintenance.MaintenanceDate,
                SharedEnvironment.GetDateFormatString(false),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
        {
            throw new FormatException("Maintenance Date is not in the correct format (dd-MM-yyyy)");
        }
        MaintenanceClass? maintenanceClass = _context
            .Maintenance.Where(m =>
                m.MaintenanceDate == maintenance.MaintenanceDate
                && m.AssetID == maintenance.AssetID
                && m.IsDeleted == false
            )
            .FirstOrDefault();
        if (maintenanceClass != null)
        {
            throw new Exception(
                "Maintenance on " + maintenance.MaintenanceDate + " already exists"
            );
        }
        _context.Maintenance.Add(maintenance);
        _context.SaveChanges();
        return GetMaintenance(maintenance.Id);
    }

    public MaintenanceData UpdateMaintenance(MaintenanceClass maintenance)
    {
        if (
            !DateTime.TryParseExact(
                maintenance.MaintenanceDate,
                SharedEnvironment.GetDateFormatString(false),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
        {
            throw new FormatException("Maintenance Date is not in the correct format (dd-MM-yyyy)");
        }
        MaintenanceClass? oldMaintenance =
            _context
                .Maintenance.Where(m => m.Id == maintenance.Id && m.IsDeleted == false)
                .FirstOrDefault() ?? throw new Exception("Maintenance not found");
        MaintenanceClass? searchMaintenance = _context
            .Maintenance.Where(m =>
                m.MaintenanceDate == maintenance.MaintenanceDate
                && m.AssetID == maintenance.AssetID
                && m.Id != maintenance.Id
                && m.IsDeleted == false
            )
            .FirstOrDefault();
        if(searchMaintenance != null)
        {
            throw new Exception(
                "Maintenance on " + maintenance.MaintenanceDate + " already exists"
            );
        }
        oldMaintenance.IsDeleted = false;
        oldMaintenance.AssetID = maintenance.AssetID;
        oldMaintenance.MaintenanceDate = maintenance.MaintenanceDate;
        oldMaintenance.MaintenanceDescription = maintenance.MaintenanceDescription;
        oldMaintenance.IsValveRepairedID = maintenance.IsValveRepairedID;
        _context.Maintenance.Update(oldMaintenance);
        _context.SaveChanges();
        return GetMaintenance(maintenance.Id);
    }

    public MaintenanceData DeleteMaintenance(MaintenanceClass maintenance)
    {
        MaintenanceClass searchMaintenance = _context
            .Maintenance.Where(m => m.Id == maintenance.Id && m.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("Maintenance not found");
        searchMaintenance.IsDeleted = true;
        searchMaintenance.DeletedBy = maintenance.DeletedBy;
        searchMaintenance.DeletedAt = maintenance.DeletedAt;
        _context.Maintenance.Update(searchMaintenance);
        _context.SaveChanges();
        return GetMaintenance(maintenance.Id);
    }
}

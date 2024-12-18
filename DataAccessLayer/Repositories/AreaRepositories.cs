using System.Web;
using DataAccessLayer;
using SharedLayer;

namespace DataAccessLayer;

public interface IAreaRepository
{
    AreaData GetArea(int id);
    List<AreaData> GetAreaList(bool IncludeDeleted = false);
    AreaData AddArea(AreaClass area);
    AreaData UpdateArea(AreaClass area);
    AreaData DeleteArea(AreaClass area);
}

public class AreaRepository(ApplicationDbContext context) : IAreaRepository
{
    private readonly ApplicationDbContext _context = context;

    public AreaData GetArea(int id)
    {
        AreaData? areadata;
        var result =
            from area in _context.Area
            join createby in _context.User on area.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on area.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            join updateby in _context.User on area.UpdatedBy equals updateby.Id into uc
            from subupdateby in uc.DefaultIfEmpty()
            where area.Id == id
            select new AreaData
            {
                Id = area.Id,
                BusinessArea = SharedEnvironment.HtmlEncode(area.BusinessArea),
                IsDeleted = area.IsDeleted,
                CreatedBy = area.CreatedBy,
                CreatedAt = area.CreatedAt,
                DeletedBy = area.DeletedBy,
                DeletedAt = area.DeletedAt,
                UpdatedBy = area.UpdatedBy,
                UpdatedAt = area.UpdatedAt,
                CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? ""),
                UpdatedByUser = SharedEnvironment.HtmlEncode(subupdateby.Username ?? "")
            };
        areadata = result.FirstOrDefault();
        if (areadata == null)
        {
            throw new Exception("Area not found");
        }
        return areadata;
    }

    public List<AreaData> GetAreaList(bool IncludeDeleted = false)
    {
        List<AreaData> arealist;
        var result =
            from area in _context.Area
            join createby in _context.User on area.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on area.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            join updateby in _context.User on area.UpdatedBy equals updateby.Id into uc
            from subupdateby in uc.DefaultIfEmpty()
            where IncludeDeleted == true || area.IsDeleted == false
            select new AreaData
            {
                Id = area.Id,
                BusinessArea = SharedEnvironment.HtmlEncode(area.BusinessArea),
                IsDeleted = area.IsDeleted,
                CreatedBy = area.CreatedBy,
                CreatedAt = area.CreatedAt,
                DeletedBy = area.DeletedBy,
                DeletedAt = area.DeletedAt,
                UpdatedBy = area.UpdatedBy,
                UpdatedAt = area.UpdatedAt,
                CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? ""),
                UpdatedByUser = SharedEnvironment.HtmlEncode(subupdateby.Username ?? "")
            };
        arealist = [.. result];
        return arealist;
    }

    public AreaData AddArea(AreaClass area)
    {
        AreaClass? searcharea = _context
            .Area.Where(a => a.BusinessArea == area.BusinessArea && a.IsDeleted == false)
            .FirstOrDefault();
        if (searcharea != null)
        {
            throw new Exception("Area named " + area.BusinessArea + " already exists.");
        }
        area.UpdatedBy = area.CreatedBy;
        area.UpdatedAt = area.CreatedAt;
        _context.Area.Add(area);
        _context.SaveChanges();
        return GetArea(area.Id);
    }

    public AreaData UpdateArea(AreaClass area)
    {
        AreaClass? oldArea = _context
            .Area.Where(a => a.Id == area.Id && a.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("Area not found");
        AreaClass? searcharea = _context
            .Area.Where(a => a.BusinessArea == area.BusinessArea && a.IsDeleted == false && a.Id != area.Id)
            .FirstOrDefault();
        if (searcharea != null)
        {
            throw new Exception("Area named " + area.BusinessArea + " already exists.");
        }
        oldArea.BusinessArea = area.BusinessArea;
        oldArea.UpdatedBy = area.UpdatedBy;
        oldArea.UpdatedAt = area.UpdatedAt;
        _context.Area.Update(oldArea);
        _context.SaveChanges();
        return GetArea(area.Id);
    }

    public AreaData DeleteArea(AreaClass area)
    {
        AreaClass searcharea = _context
            .Area.Where(a => a.Id == area.Id && a.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("Area not found");
        searcharea.IsDeleted = true;
        searcharea.DeletedBy = area.DeletedBy;
        searcharea.DeletedAt = area.DeletedAt;
        searcharea.UpdatedBy = area.DeletedBy;
        searcharea.UpdatedAt = area.DeletedAt;
        _context.Area.Update(searcharea);
        _context.SaveChanges();
        return GetArea(area.Id);
    }
}

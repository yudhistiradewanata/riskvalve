using SharedLayer;

namespace DataAccessLayer;

public interface IPlatformRepository
{
    PlatformData GetPlatform(int id);
    List<PlatformData> GetPlatformList(int AreaID = 0, bool IncludeDeleted = false);
    PlatformData AddPlatform(PlatformClass platform);
    PlatformData UpdatePlatform(PlatformClass platform);
    PlatformData DeletePlatform(PlatformClass platform);
}

public class PlatformRepository(ApplicationDbContext context) : IPlatformRepository
{
    private readonly ApplicationDbContext _context = context;

    public PlatformData GetPlatform(int id)
    {
        PlatformData? platformdata;
        var result =
            from platform in _context.Platform
            join area in _context.Area on platform.AreaID equals area.Id into sc
            from subarea in sc.DefaultIfEmpty()
            join createby in _context.User on subarea.CreatedBy equals createby.Id into cc
            from subcreateby in cc.DefaultIfEmpty()
            join deleteby in _context.User on subarea.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where platform.Id == id
            select new PlatformData
            {
                Id = platform.Id,
                AreaID = platform.AreaID,
                Platform = platform.Platform,
                Code = platform.Code,
                BusinessArea = subarea.BusinessArea,
                IsDeleted = subarea.IsDeleted,
                CreatedBy = subarea.CreatedBy,
                CreatedAt = subarea.CreatedAt,
                DeletedBy = subarea.DeletedBy,
                DeletedAt = subarea.DeletedAt,
                CreatedByUser = subcreateby.Username ?? "",
                DeletedByUser = subdeleteby.Username ?? ""
            };
        platformdata = result.FirstOrDefault();
        if (platformdata == null)
        {
            throw new Exception("Platform not found");
        }
        return platformdata;
    }

    public List<PlatformData> GetPlatformList(int AreaID = 0, bool IncludeDeleted = false)
    {
        var result =
            from platform in _context.Platform
            join area in _context.Area on platform.AreaID equals area.Id into sc
            from subarea in sc.DefaultIfEmpty()
            join createby in _context.User on subarea.CreatedBy equals createby.Id into cc
            from subcreateby in cc.DefaultIfEmpty()
            join deleteby in _context.User on subarea.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where
                (platform.AreaID == AreaID || AreaID == 0)
                && (IncludeDeleted == true || subarea.IsDeleted == false)
            select new PlatformData
            {
                Id = platform.Id,
                AreaID = platform.AreaID,
                Platform = platform.Platform,
                Code = platform.Code,
                BusinessArea = subarea.BusinessArea,
                IsDeleted = subarea.IsDeleted,
                CreatedBy = subarea.CreatedBy,
                CreatedAt = subarea.CreatedAt,
                DeletedBy = subarea.DeletedBy,
                DeletedAt = subarea.DeletedAt,
                CreatedByUser = subcreateby.Username ?? "",
                DeletedByUser = subdeleteby.Username ?? ""
            };
        return [.. result];
    }

    public PlatformData AddPlatform(PlatformClass platform)
    {
        PlatformClass? searchplatform = _context
            .Platform.Where(p =>
                p.Platform == platform.Platform
                // && p.AreaID == platform.AreaID
                && p.IsDeleted == false
            )
            .FirstOrDefault();
        if (searchplatform != null)
        {
            throw new Exception(
                "Platform named " + platform.Platform + " already exists."
            );
        }
        _context.Platform.Add(platform);
        _context.SaveChanges();
        return GetPlatform(platform.Id);
    }

    public PlatformData UpdatePlatform(PlatformClass platform)
    {
        PlatformClass? oldPlatform =
            _context
                .Platform.Where(p => p.Id == platform.Id && p.IsDeleted == false)
                .FirstOrDefault() ?? throw new Exception("Platform not found.");
        PlatformClass? searchplatform = _context
            .Platform.Where(p =>
                p.Platform == platform.Platform
                // && p.AreaID == platform.AreaID
                && p.IsDeleted == false
                && p.Id != platform.Id
            )
            .FirstOrDefault();
        if (searchplatform != null)
        {
            throw new Exception(
                "Platform named " + platform.Platform + " already exists."
            );
        }
        oldPlatform.AreaID = platform.AreaID;
        oldPlatform.Platform = platform.Platform;
        oldPlatform.Code = platform.Code;
        _context.Platform.Update(oldPlatform);
        _context.SaveChanges();
        return GetPlatform(platform.Id);
    }

    public PlatformData DeletePlatform(PlatformClass platform)
    {
        PlatformClass searchplatform =
            _context
                .Platform.Where(p => p.Id == platform.Id && p.IsDeleted == false)
                .FirstOrDefault() ?? throw new Exception("Platform not found.");
        searchplatform.IsDeleted = true;
        searchplatform.DeletedBy = platform.DeletedBy;
        searchplatform.DeletedAt = platform.DeletedAt;
        _context.Platform.Update(searchplatform);
        _context.SaveChanges();
        return GetPlatform(platform.Id);
    }
}

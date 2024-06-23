using SharedLayer;
using DataAccessLayer;

namespace BusinessLogicLayer;

public interface IAreaService
{
    AreaData GetArea(int id);
    List<AreaData> GetAreaList(bool IncludeDeleted = false);
    AreaData AddArea(AreaClass area);
    AreaData UpdateArea(AreaClass area);
    AreaData DeleteArea(AreaClass area);
    List<SidebarData> GetSidebarData();
}

public class AreaService(
    IAreaRepository areaRepository,
    IPlatformRepository platformRepository    
) : IAreaService
{
    private readonly IAreaRepository _areaRepository = areaRepository;
    private readonly IPlatformRepository _platformRepository = platformRepository;

    public AreaData GetArea(int id)
    {
        try{
            return _areaRepository.GetArea(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<AreaData> GetAreaList(bool IncludeDeleted = false)
    {
        return _areaRepository.GetAreaList(IncludeDeleted);
    }

    public AreaData AddArea(AreaClass area)
    {
        try
        {
            if (area.BusinessArea == null || area.BusinessArea == "")
            {
                throw new Exception("Business Area is required.");
            }
            return _areaRepository.AddArea(area);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public AreaData UpdateArea(AreaClass area)
    {
        try
        {
            if (area.BusinessArea == null || area.BusinessArea == "")
            {
                throw new Exception("Business Area is required.");
            }
            return _areaRepository.UpdateArea(area);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public AreaData DeleteArea(AreaClass area)
    {
        try
        {
            return _areaRepository.DeleteArea(area);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<SidebarData> GetSidebarData()
    {
        try
        {
            List<SidebarData> sidebarData = [];
            List<AreaData> areaData = GetAreaList();
            foreach (var area in areaData)
            {
                List<PlatformData> platformData = _platformRepository.GetPlatformList(area.Id);
                List<SidebarData> platformSidebar = [];
                foreach (var platform in platformData)
                {
                    SidebarData sidebarPlatform = new()
                    {
                        Id = platform.Id,
                        Name = platform.Platform,
                        Child = []
                    };
                    platformSidebar.Add(sidebarPlatform);
                }
                SidebarData sidebar = new()
                {
                    Id = area.Id,
                    Name = area.BusinessArea,
                    Child = platformSidebar
                };
                sidebarData.Add(sidebar);
            }
            return sidebarData;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

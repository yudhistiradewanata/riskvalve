using SharedLayer;
using DataAccessLayer;

namespace BusinessLogicLayer;

public interface IPlatformService
{
    PlatformData GetPlatform(int id);
    List<PlatformData> GetPlatformList(int AreaID = 0, bool IncludeDeleted = false);
    PlatformData AddPlatform(PlatformClass platform);
    PlatformData UpdatePlatform(PlatformClass platform);
    PlatformData DeletePlatform(PlatformClass platform);
}

public class PlatformService(IPlatformRepository platformRepository) : IPlatformService
{
    private readonly IPlatformRepository _platformRepository = platformRepository;

    public PlatformData GetPlatform(int id)
    {
        try
        {
            return _platformRepository.GetPlatform(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<PlatformData> GetPlatformList(int AreaID = 0, bool IncludeDeleted = false)
    {

        return _platformRepository.GetPlatformList(AreaID, IncludeDeleted);
    }

    public PlatformData AddPlatform(PlatformClass platform)
    {
        try
        {
            if (platform.Platform == null || platform.Platform == "")
            {
                throw new Exception("Platform is required.");
            }
            return _platformRepository.AddPlatform(platform);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public PlatformData UpdatePlatform(PlatformClass platform)
    {
        try
        {
            if (platform.Platform == null || platform.Platform == "")
            {
                throw new Exception("Platform is required.");
            }
            return _platformRepository.UpdatePlatform(platform);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public PlatformData DeletePlatform(PlatformClass platform)
    {
        try
        {
            return _platformRepository.DeletePlatform(platform);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
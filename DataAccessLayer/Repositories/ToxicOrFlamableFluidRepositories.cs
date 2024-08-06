using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IToxicOrFlamableFluidRepository
{
    List<ToxicOrFlamableFluidData> GetToxicOrFlamableFluidList();
}

public class ToxicOrFlamableFluidRepository(ApplicationDbContext context) : IToxicOrFlamableFluidRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<ToxicOrFlamableFluidData> GetToxicOrFlamableFluidList()
    {
        List<ToxicOrFlamableFluidData> toxicorflamablefluidlist;
        var result =
            from toxicorflamablefluid in _context.ToxicOrFlamableFluid
            select new ToxicOrFlamableFluidData
            {
                Id = toxicorflamablefluid.Id,
                ToxicOrFlamableFluid = HttpUtility.HtmlEncode(toxicorflamablefluid.ToxicOrFlamableFluid)
            };
        toxicorflamablefluidlist = [.. result];
        return toxicorflamablefluidlist;
    }
}
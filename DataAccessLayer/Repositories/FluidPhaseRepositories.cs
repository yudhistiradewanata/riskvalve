using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IFluidPhaseRepository
{
    List<FluidPhaseData> GetFluidPhaseList();
}

public class FluidPhaseRepository(ApplicationDbContext context) : IFluidPhaseRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<FluidPhaseData> GetFluidPhaseList()
    {
        List<FluidPhaseData> fluidphaselist;
        var result =
            from fluidphase in _context.FluidPhase
            select new FluidPhaseData
            {
                Id = fluidphase.Id,
                FluidPhase = HttpUtility.HtmlEncode(fluidphase.FluidPhase)
            };
        fluidphaselist = [.. result];
        return fluidphaselist;
    }
}
using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IImpactEffectRepository
{
    List<ImpactEffectData> GetImpactEffectList();
}

public class ImpactEffectRepository(ApplicationDbContext context) : IImpactEffectRepository
{
    private readonly ApplicationDbContext _context = context;
    public List<ImpactEffectData> GetImpactEffectList()
    {
        List<ImpactEffectData> impactEffectList = [];
        var result =
            from impactEffect in _context.ImpactEffect
            select new ImpactEffectData
            {
                Id = impactEffect.Id,
                ImpactEffect = SharedEnvironment.HtmlEncode(impactEffect.ImpactEffect),
                ImpactEffectValue = impactEffect.ImpactEffectValue,
                Weighting = impactEffect.Weighting,
                Weighting2 = impactEffect.Weighting2,
            };
        impactEffectList = [.. result];
        return impactEffectList;
    }
}
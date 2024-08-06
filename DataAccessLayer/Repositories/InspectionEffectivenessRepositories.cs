using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IInspectionEffectivenessRepository
{
    List<InspectionEffectivenessData> GetInspectionEffectivenessList();
}

public class InspectionEffectivenessRepository(ApplicationDbContext context) : IInspectionEffectivenessRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<InspectionEffectivenessData> GetInspectionEffectivenessList()
    {
        List<InspectionEffectivenessData> inspectionEffectivenessDataList;
        var result =
            from inspectionEffectiveness in _context.InspectionEffectiveness
            select new InspectionEffectivenessData
            {
                Id = inspectionEffectiveness.Id,
                Effectiveness = HttpUtility.HtmlEncode(inspectionEffectiveness.Effectiveness),
                EffectivenessValue = inspectionEffectiveness.EffectivenessValue,
                Weighting = inspectionEffectiveness.Weighting
            };
        inspectionEffectivenessDataList = [.. result];
        return inspectionEffectivenessDataList;
    }
}
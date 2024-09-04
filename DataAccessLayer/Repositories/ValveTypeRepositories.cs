using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IValveTypeRepository
{
    List<ValveTypeData> GetValveTypeList();
}

public class ValveTypeRepository(ApplicationDbContext context) : IValveTypeRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<ValveTypeData> GetValveTypeList()
    {
        List<ValveTypeData> valvetypelist;
        var result =
            from valvetype in _context.ValveType
            select new ValveTypeData
            {
                Id = valvetype.Id,
                ValveType = SharedEnvironment.HtmlEncode(valvetype.ValveType)
            };
        valvetypelist = [.. result];
        return valvetypelist;
    }
}
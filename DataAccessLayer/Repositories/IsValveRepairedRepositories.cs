using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IIsValveRepairedRepository
{
    List<IsValveRepairedData> GetIsValveRepairedList();
}

public class IsValveRepairedRepository(ApplicationDbContext context) : IIsValveRepairedRepository
{
    private readonly ApplicationDbContext _context = context;

    public List<IsValveRepairedData> GetIsValveRepairedList()
    {
        List<IsValveRepairedData> isvalverepairedlist;
        var result =
            from isvalverepaired in _context.IsValveRepaired
            select new IsValveRepairedData
            {
                Id = isvalverepaired.Id,
                IsValveRepaired = SharedEnvironment.HtmlEncode(isvalverepaired.IsValveRepaired)
            };
        isvalverepairedlist = [.. result];
        return isvalverepairedlist;
    }
}
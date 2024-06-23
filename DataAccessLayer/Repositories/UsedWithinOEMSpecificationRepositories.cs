using SharedLayer;

namespace DataAccessLayer;

public interface IUsedWithinOEMSpecificationRepository
{
    List<UsedWithinOEMSpecificationData> GetUsedWithinOEMSpecificationList();
}

public class UsedWithinOEMSpecificationRepository(ApplicationDbContext context) : IUsedWithinOEMSpecificationRepository
{
    private readonly ApplicationDbContext _context = context;
    public List<UsedWithinOEMSpecificationData> GetUsedWithinOEMSpecificationList()
    {
        List<UsedWithinOEMSpecificationData> usedWithinOEMSpecificationList = [];
        var result =
            from usedWithinOEMSpecification in _context.UsedWithinOEMSpecification
            select new UsedWithinOEMSpecificationData
            {
                Id = usedWithinOEMSpecification.Id,
                UsedWithinOEMSpecification = usedWithinOEMSpecification.UsedWithinOEMSpecification,
                UsedWithinOEMSpecificationValue = usedWithinOEMSpecification.UsedWithinOEMSpecificationValue,
                Weighting = usedWithinOEMSpecification.Weighting,
            };
        usedWithinOEMSpecificationList = [.. result];
        return usedWithinOEMSpecificationList;
    }
}
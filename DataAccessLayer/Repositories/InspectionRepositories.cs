using System.Globalization;
using SharedLayer;

namespace DataAccessLayer;

public interface IInspectionRepository
{
    InspectionData GetInspection(int id);
    List<InspectionData> GetInspectionList(bool IncludeDeleted = false, int AssetID = 0);
    InspectionData AddInspection(InspectionClass inspection);
    InspectionData UpdateInspection(InspectionClass inspection);
    InspectionData DeleteInspection(InspectionClass inspection);
}

public class InspectionRepository(ApplicationDbContext context) : IInspectionRepository
{
    private readonly ApplicationDbContext _context = context;

    public InspectionData GetInspection(int id)
    {
        InspectionData? inspectionData;
        var result = 
            from inspection in _context.Inspection
            join inspectionMethod in _context.InspectionMethod on inspection.InspectionMethodID equals inspectionMethod.Id into im
            from subInspectionMethod in im.DefaultIfEmpty()
            join inspectioneffectiveness in _context.InspectionEffectiveness on inspection.InspectionEffectivenessID equals inspectioneffectiveness.Id into ie
            from subInspectionEffectiveness in ie.DefaultIfEmpty()
            join currentConditionLimitStateA in _context.CurrentConditionLimitState on inspection.CurrentConditionLeakeageToAtmosphereID equals currentConditionLimitStateA.Id into cclta
            from subCurrentConditionLimitStateA in cclta.DefaultIfEmpty()
            join currentConditionLimitStateB in _context.CurrentConditionLimitState on inspection.CurrentConditionFailureOfFunctionID equals currentConditionLimitStateB.Id into ccfof
            from subCurrentConditionLimitStateB in ccfof.DefaultIfEmpty()
            join currentConditionLimitStateC in _context.CurrentConditionLimitState on inspection.CurrentConditionPassingAcrossValveID equals currentConditionLimitStateC.Id into ccpav
            from subCurrentConditionLimitStateC in ccpav.DefaultIfEmpty()
            join createby in _context.User on inspection.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on inspection.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where inspection.Id == id
            select new InspectionData
            {
                Id = inspection.Id,
                AssetID = inspection.AssetID,
                InspectionMethodID = inspection.InspectionMethodID,
                InspectionEffectivenessID = inspection.InspectionEffectivenessID,
                CurrentConditionLeakeageToAtmosphereID = inspection.CurrentConditionLeakeageToAtmosphereID,
                CurrentConditionFailureOfFunctionID = inspection.CurrentConditionFailureOfFunctionID,
                CurrentConditionPassingAcrossValveID = inspection.CurrentConditionPassingAcrossValveID,
                InspectionDate = inspection.InspectionDate,
                InspectionDescription = inspection.InspectionDescription,
                FunctionCondition = inspection.FunctionCondition,
                TestPressureIfAny = inspection.TestPressureIfAny,
                IsDeleted = inspection.IsDeleted,
                CreatedBy = inspection.CreatedBy,
                CreatedAt = inspection.CreatedAt,
                DeletedBy = inspection.DeletedBy,
                DeletedAt = inspection.DeletedAt,
                InspectionMethod = subInspectionMethod.InspectionMethod ?? "",
                InspectionEffectiveness = subInspectionEffectiveness.Effectiveness ?? "",
                CurrentConditionLeakeageToAtmosphere = subCurrentConditionLimitStateA.CurrentConditionLimitState ?? "",
                CurrentConditionFailureOfFunction = subCurrentConditionLimitStateB.CurrentConditionLimitState ?? "",
                CurrentConditionPassingAcrossValve = subCurrentConditionLimitStateC.CurrentConditionLimitState ?? "",
                CreatedByUser = subcreateby.Username ?? "",
                DeletedByUser = subdeleteby.Username ?? ""
            };
        inspectionData = result.FirstOrDefault();
        if (inspectionData == null)
        {
            throw new Exception("Inspection not found");
        }
        return inspectionData;
    }

    public List<InspectionData> GetInspectionList(bool IncludeDeleted = false, int AssetID = 0)
    {
        
        List<InspectionData> inspectionDataList;
        var result = 
            from inspection in _context.Inspection
            join inspectionMethod in _context.InspectionMethod on inspection.InspectionMethodID equals inspectionMethod.Id into im
            from subInspectionMethod in im.DefaultIfEmpty()
            join inspectioneffectiveness in _context.InspectionEffectiveness on inspection.InspectionEffectivenessID equals inspectioneffectiveness.Id into ie
            from subInspectionEffectiveness in ie.DefaultIfEmpty()
            join currentConditionLimitStateA in _context.CurrentConditionLimitState on inspection.CurrentConditionLeakeageToAtmosphereID equals currentConditionLimitStateA.Id into cclta
            from subCurrentConditionLimitStateA in cclta.DefaultIfEmpty()
            join currentConditionLimitStateB in _context.CurrentConditionLimitState on inspection.CurrentConditionFailureOfFunctionID equals currentConditionLimitStateB.Id into ccfof
            from subCurrentConditionLimitStateB in ccfof.DefaultIfEmpty()
            join currentConditionLimitStateC in _context.CurrentConditionLimitState on inspection.CurrentConditionPassingAcrossValveID equals currentConditionLimitStateC.Id into ccpav
            from subCurrentConditionLimitStateC in ccpav.DefaultIfEmpty()
            join createby in _context.User on inspection.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on inspection.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where (IncludeDeleted || inspection.IsDeleted == false)
                && (AssetID == 0 || inspection.AssetID == AssetID)
            select new InspectionData
            {
                Id = inspection.Id,
                AssetID = inspection.AssetID,
                InspectionMethodID = inspection.InspectionMethodID,
                InspectionEffectivenessID = inspection.InspectionEffectivenessID,
                CurrentConditionLeakeageToAtmosphereID = inspection.CurrentConditionLeakeageToAtmosphereID,
                CurrentConditionFailureOfFunctionID = inspection.CurrentConditionFailureOfFunctionID,
                CurrentConditionPassingAcrossValveID = inspection.CurrentConditionPassingAcrossValveID,
                InspectionDate = inspection.InspectionDate,
                InspectionDescription = inspection.InspectionDescription,
                FunctionCondition = inspection.FunctionCondition,
                TestPressureIfAny = inspection.TestPressureIfAny,
                IsDeleted = inspection.IsDeleted,
                CreatedBy = inspection.CreatedBy,
                CreatedAt = inspection.CreatedAt,
                DeletedBy = inspection.DeletedBy,
                DeletedAt = inspection.DeletedAt,
                InspectionMethod = subInspectionMethod.InspectionMethod ?? "",
                InspectionEffectiveness = subInspectionEffectiveness.Effectiveness ?? "",
                CurrentConditionLeakeageToAtmosphere = subCurrentConditionLimitStateA.CurrentConditionLimitState ?? "",
                CurrentConditionFailureOfFunction = subCurrentConditionLimitStateB.CurrentConditionLimitState ?? "",
                CurrentConditionPassingAcrossValve = subCurrentConditionLimitStateC.CurrentConditionLimitState ?? "",
                CreatedByUser = subcreateby.Username ?? "",
                DeletedByUser = subdeleteby.Username ?? ""
            };
        inspectionDataList = [.. result];
        return inspectionDataList;
    }

    public InspectionData AddInspection(InspectionClass inspection)
    {
        if (
            !DateTime.TryParseExact(
                inspection.InspectionDate,
                SharedEnvironment.GetDateFormatString(false),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
        {
            throw new FormatException("Inspection Date is not in the correct format (dd-MM-yyyy)");
        }
        InspectionClass? inspectionClass = _context
            .Inspection.Where(i => 
                i.InspectionDate == inspection.InspectionDate
                && i.AssetID == inspection.AssetID
                && i.IsDeleted == false
            )
            .FirstOrDefault();
        if (inspectionClass != null)
        {
            throw new Exception("Inspection on "+inspection.InspectionDate+" already exists");
        }
        inspection.IsDeleted = false;
        _context.Inspection.Add(inspection);
        _context.SaveChanges();
        return GetInspection(inspection.Id);
    }

    public InspectionData UpdateInspection(InspectionClass inspection)
    {
        if (
            !DateTime.TryParseExact(
                inspection.InspectionDate,
                SharedEnvironment.GetDateFormatString(false),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
        {
            throw new FormatException("Inspection Date is not in the correct format (dd-MM-yyyy)");
        }
        InspectionClass? oldInspection = 
            _context
                .Inspection.Where(i => i.Id == inspection.Id && i.IsDeleted == false)
                .FirstOrDefault() ?? throw new Exception("Inspection not found");
        InspectionClass? searchInspection = _context
            .Inspection.Where(i => 
                i.InspectionDate == inspection.InspectionDate
                && i.AssetID == inspection.AssetID
                && i.Id != inspection.Id
                && i.IsDeleted == false
            )
            .FirstOrDefault();
        if(searchInspection != null)
        {
            throw new Exception("Inspection on "+inspection.InspectionDate+" already exists");
        }
        oldInspection.IsDeleted = false;
        oldInspection.AssetID = inspection.AssetID;
        oldInspection.InspectionDate = inspection.InspectionDate;
        oldInspection.InspectionMethodID = inspection.InspectionMethodID;
        oldInspection.InspectionEffectivenessID = inspection.InspectionEffectivenessID;
        oldInspection.InspectionDescription = inspection.InspectionDescription;
        oldInspection.CurrentConditionLeakeageToAtmosphereID = inspection.CurrentConditionLeakeageToAtmosphereID;
        oldInspection.CurrentConditionFailureOfFunctionID = inspection.CurrentConditionFailureOfFunctionID;
        oldInspection.CurrentConditionPassingAcrossValveID = inspection.CurrentConditionPassingAcrossValveID;
        oldInspection.FunctionCondition = inspection.FunctionCondition;
        oldInspection.TestPressureIfAny = inspection.TestPressureIfAny;
        _context.Inspection.Update(oldInspection);
        _context.SaveChanges();
        return GetInspection(inspection.Id);
    }

    public InspectionData DeleteInspection(InspectionClass inspection)
    {
        InspectionClass searchInspection = _context
            .Inspection.Where(i => i.Id == inspection.Id && i.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("Inspection not found");
        searchInspection.IsDeleted = true;
        searchInspection.DeletedBy = inspection.DeletedBy;
        searchInspection.DeletedAt = inspection.DeletedAt;
        _context.Inspection.Update(searchInspection);
        _context.SaveChanges();
        return GetInspection(inspection.Id);
    }
}

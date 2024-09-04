using System.Globalization;
using System.Web;
using SharedLayer;

namespace DataAccessLayer;

public interface IAssessmentRepository
{
    AssessmentData GetAssessment(int id);
    List<AssessmentData> GetAssessmentList(bool IncludeDeleted = false, int AssetID = 0);
    AssessmentData AddAssessment(AssessmentClass assessment);
    AssessmentData UpdateAssessment(AssessmentClass assessment);
    AssessmentData DeleteAssessment(AssessmentClass assessment);
}

public class AssessmentRepository(ApplicationDbContext context) : IAssessmentRepository
{
    private readonly ApplicationDbContext _context = context;
    public AssessmentData GetAssessment(int id)
    {
        AssessmentData? assessmentdata;
        var result = 
            from assessment in _context.Assessment
            join asset in _context.Asset on assessment.AssetID equals asset.Id into ac
            from subasset in ac.DefaultIfEmpty()
            join leakagetoatmosphere in _context.CurrentConditionLimitState on assessment.LeakageToAtmosphereID equals leakagetoatmosphere.Id into lta
            from subleakagetoatmosphere in lta.DefaultIfEmpty()
            join failureoffunction in _context.CurrentConditionLimitState on assessment.FailureOfFunctionID equals failureoffunction.Id into fof
            from subfailureoffunction in fof.DefaultIfEmpty()
            join passingacrossvalve in _context.CurrentConditionLimitState on assessment.PassingAccrosValveID equals passingacrossvalve.Id into pav
            from subpassingacrossvalve in pav.DefaultIfEmpty()
            join leakeagetoatmospheretp1 in _context.TimeToLimitState on assessment.LeakageToAtmosphereTP1ID equals leakeagetoatmospheretp1.Id into lta1
            from subleakeagetoatmospheretp1 in lta1.DefaultIfEmpty()
            join leakeagetoatmospheretp2 in _context.TimeToLimitState on assessment.LeakageToAtmosphereTP2ID equals leakeagetoatmospheretp2.Id into lta2
            from subleakeagetoatmospheretp2 in lta2.DefaultIfEmpty()
            join leakeagetoatmospheretp3 in _context.TimeToLimitState on assessment.LeakageToAtmosphereTP3ID equals leakeagetoatmospheretp3.Id into lta3
            from subleakeagetoatmospheretp3 in lta3.DefaultIfEmpty()
            join failureoffunctiontp1 in _context.TimeToLimitState on assessment.FailureOfFunctionTP1ID equals failureoffunctiontp1.Id into fof1
            from subfailureoffunctiontp1 in fof1.DefaultIfEmpty()
            join failureoffunctiontp2 in _context.TimeToLimitState on assessment.FailureOfFunctionTP2ID equals failureoffunctiontp2.Id into fof2
            from subfailureoffunctiontp2 in fof2.DefaultIfEmpty()
            join failureoffunctiontp3 in _context.TimeToLimitState on assessment.FailureOfFunctionTP3ID equals failureoffunctiontp3.Id into fof3
            from subfailureoffunctiontp3 in fof3.DefaultIfEmpty()
            join passingacrossvalvetp1 in _context.TimeToLimitState on assessment.PassingAccrosValveTP1ID equals passingacrossvalvetp1.Id into pav1
            from subpassingacrossvalvetp1 in pav1.DefaultIfEmpty()
            join passingacrossvalvetp2 in _context.TimeToLimitState on assessment.PassingAccrosValveTP2ID equals passingacrossvalvetp2.Id into pav2
            from subpassingacrossvalvetp2 in pav2.DefaultIfEmpty()
            join passingacrossvalvetp3 in _context.TimeToLimitState on assessment.PassingAccrosValveTP3ID equals passingacrossvalvetp3.Id into pav3
            from subpassingacrossvalvetp3 in pav3.DefaultIfEmpty()
            join inspectioneffectiveness in _context.InspectionEffectiveness on assessment.InspectionEffectivenessID equals inspectioneffectiveness.Id into ie
            from subinspectioneffectiveness in ie.DefaultIfEmpty()
            join impactofinternalfluidimpurities in _context.ImpactEffect on assessment.ImpactOfInternalFluidImpuritiesID equals impactofinternalfluidimpurities.Id into iifi
            from subimpactofinternalfluidimpurities in iifi.DefaultIfEmpty()
            join impactofoperatingenvelope in _context.ImpactEffect on assessment.ImpactOfOperatingEnvelopesID equals impactofoperatingenvelope.Id into ioe
            from subimpactofoperatingenvelope in ioe.DefaultIfEmpty()
            join usedwithinoemspecifications in _context.UsedWithinOEMSpecification on assessment.UsedWithinOEMSpecificationID equals usedwithinoemspecifications.Id into uwoem
            from subusedwithinoemspecifications in uwoem.DefaultIfEmpty()
            join repaired in _context.Repaired on assessment.RepairedID equals repaired.Id into r
            from subrepaired in r.DefaultIfEmpty()
            join hssedefinision in _context.HSSEDefinision on assessment.HSSEDefinisionID equals hssedefinision.Id into hsse
            from subhssedefinision in hsse.DefaultIfEmpty()
            join recomendationaction in _context.RecommendationAction on assessment.RecommendationActionID equals recomendationaction.Id into ra
            from subrecomendationaction in ra.DefaultIfEmpty()
            join createby in _context.User on assessment.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on assessment.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where assessment.Id == id
            select new AssessmentData
            {
                Id = assessment.Id,
                AssetID = assessment.AssetID,
                AssessmentNo = SharedEnvironment.HtmlEncode(assessment.AssessmentNo),
                AssessmentDate = assessment.AssessmentDate,
                TimePeriode = SharedEnvironment.HtmlEncode(assessment.TimePeriode),
                TimeToLimitStateLeakageToAtmosphere = SharedEnvironment.HtmlEncode(assessment.TimeToLimitStateLeakageToAtmosphere),
                TimeToLimitStateFailureOfFunction = SharedEnvironment.HtmlEncode(assessment.TimeToLimitStateFailureOfFunction),
                TimeToLimitStatePassingAccrosValve = SharedEnvironment.HtmlEncode(assessment.TimeToLimitStatePassingAccrosValve),
                LeakageToAtmosphereID = assessment.LeakageToAtmosphereID,
                FailureOfFunctionID = assessment.FailureOfFunctionID,
                PassingAccrosValveID = assessment.PassingAccrosValveID,
                LeakageToAtmosphereTP1ID = assessment.LeakageToAtmosphereTP1ID,
                LeakageToAtmosphereTP2ID = assessment.LeakageToAtmosphereTP2ID,
                LeakageToAtmosphereTP3ID = assessment.LeakageToAtmosphereTP3ID,
                FailureOfFunctionTP1ID = assessment.FailureOfFunctionTP1ID,
                FailureOfFunctionTP2ID = assessment.FailureOfFunctionTP2ID,
                FailureOfFunctionTP3ID = assessment.FailureOfFunctionTP3ID,
                PassingAccrosValveTP1ID = assessment.PassingAccrosValveTP1ID,
                PassingAccrosValveTP2ID = assessment.PassingAccrosValveTP2ID,
                PassingAccrosValveTP3ID = assessment.PassingAccrosValveTP3ID,
                InspectionEffectivenessID = assessment.InspectionEffectivenessID,
                ImpactOfInternalFluidImpuritiesID = assessment.ImpactOfInternalFluidImpuritiesID,
                ImpactOfOperatingEnvelopesID = assessment.ImpactOfOperatingEnvelopesID,
                UsedWithinOEMSpecificationID = assessment.UsedWithinOEMSpecificationID,
                RepairedID = assessment.RepairedID,
                ProductLossDefinition = SharedEnvironment.HtmlEncode(assessment.ProductLossDefinition),
                HSSEDefinisionID = assessment.HSSEDefinisionID,
                Summary = SharedEnvironment.HtmlEncode(assessment.Summary),
                RecommendationActionID = assessment.RecommendationActionID,
                DetailedRecommendation = SharedEnvironment.HtmlEncode(assessment.DetailedRecommendation),
                ConsequenceOfFailure = SharedEnvironment.HtmlEncode(assessment.ConsequenceOfFailure),
                TP1A = SharedEnvironment.HtmlEncode(assessment.TP1A),
                TP1B = SharedEnvironment.HtmlEncode(assessment.TP1B),
                TP1C = SharedEnvironment.HtmlEncode(assessment.TP1C),
                TP2A = SharedEnvironment.HtmlEncode(assessment.TP2A),
                TP2B = SharedEnvironment.HtmlEncode(assessment.TP2B),
                TP2C = SharedEnvironment.HtmlEncode(assessment.TP2C),
                TP3A = SharedEnvironment.HtmlEncode(assessment.TP3A),
                TP3B = SharedEnvironment.HtmlEncode(assessment.TP3B),
                TP3C = SharedEnvironment.HtmlEncode(assessment.TP3C),
                TPTimeToActionA = SharedEnvironment.HtmlEncode(assessment.TPTimeToActionA),
                TPTimeToActionB = SharedEnvironment.HtmlEncode(assessment.TPTimeToActionB),
                TPTimeToActionC = SharedEnvironment.HtmlEncode(assessment.TPTimeToActionC),
                TP1Risk = SharedEnvironment.HtmlEncode(assessment.TP1Risk),
                TP2Risk = SharedEnvironment.HtmlEncode(assessment.TP2Risk),
                TP3Risk = SharedEnvironment.HtmlEncode(assessment.TP3Risk),
                TPTimeToActionRisk = SharedEnvironment.HtmlEncode(assessment.TPTimeToActionRisk),
                LoFScoreLeakageToAtmophereTP1 = assessment.LoFScoreLeakageToAtmophereTP1,
                LoFScoreLeakageToAtmophereTP2 = assessment.LoFScoreLeakageToAtmophereTP2,
                LoFScoreLeakageToAtmophereTP3 = assessment.LoFScoreLeakageToAtmophereTP3,
                LoFScoreFailureOfFunctionTP1 = assessment.LoFScoreFailureOfFunctionTP1,
                LoFScoreFailureOfFunctionTP2 = assessment.LoFScoreFailureOfFunctionTP2,
                LoFScoreFailureOfFunctionTP3 = assessment.LoFScoreFailureOfFunctionTP3,
                LoFScorePassingAccrosValveTP1 = assessment.LoFScorePassingAccrosValveTP1,
                LoFScorePassingAccrosValveTP2 = assessment.LoFScorePassingAccrosValveTP2,
                LoFScorePassingAccrosValveTP3 = assessment.LoFScorePassingAccrosValveTP3,
                CoFScore = assessment.CoFScore,
                IntegrityStatus = SharedEnvironment.HtmlEncode(assessment.IntegrityStatus),
                IsDeleted = assessment.IsDeleted,
                CreatedBy = assessment.CreatedBy,
                CreatedAt = assessment.CreatedAt,
                DeletedBy = assessment.DeletedBy,
                DeletedAt = assessment.DeletedAt,
                LeakageToAtmosphere = SharedEnvironment.HtmlEncode(subleakagetoatmosphere.CurrentConditionLimitState ?? ""),
                FailureOfFunction = SharedEnvironment.HtmlEncode(subfailureoffunction.CurrentConditionLimitState ?? ""),
                PassingAccrosValve = SharedEnvironment.HtmlEncode(subpassingacrossvalve.CurrentConditionLimitState ?? ""),
                LeakageToAtmosphereTP1 = SharedEnvironment.HtmlEncode(subleakeagetoatmospheretp1.TimeToLimitState ?? ""),
                LeakageToAtmosphereTP2 = SharedEnvironment.HtmlEncode(subleakeagetoatmospheretp2.TimeToLimitState ?? ""),
                LeakageToAtmosphereTP3 = SharedEnvironment.HtmlEncode(subleakeagetoatmospheretp3.TimeToLimitState ?? ""),
                FailureOfFunctionTP1 = SharedEnvironment.HtmlEncode(subfailureoffunctiontp1.TimeToLimitState ?? ""),
                FailureOfFunctionTP2 = SharedEnvironment.HtmlEncode(subfailureoffunctiontp2.TimeToLimitState ?? ""),
                FailureOfFunctionTP3 = SharedEnvironment.HtmlEncode(subfailureoffunctiontp3.TimeToLimitState ?? ""),
                PassingAccrosValveTP1 = SharedEnvironment.HtmlEncode(subpassingacrossvalvetp1.TimeToLimitState ?? ""),
                PassingAccrosValveTP2 = SharedEnvironment.HtmlEncode(subpassingacrossvalvetp2.TimeToLimitState ?? ""),
                PassingAccrosValveTP3 = SharedEnvironment.HtmlEncode(subpassingacrossvalvetp3.TimeToLimitState ?? ""),
                InspectionEffectiveness = SharedEnvironment.HtmlEncode(subinspectioneffectiveness.Effectiveness ?? ""),
                ImpactOfInternalFluidImpurities = SharedEnvironment.HtmlEncode(subimpactofinternalfluidimpurities.ImpactEffect ?? ""),
                ImpactOfOperatingEnvelopes = SharedEnvironment.HtmlEncode(subimpactofoperatingenvelope.ImpactEffect ?? ""),
                UsedWithinOEMSpecification = SharedEnvironment.HtmlEncode(subusedwithinoemspecifications.UsedWithinOEMSpecification ?? ""),
                Repaired = SharedEnvironment.HtmlEncode(subrepaired.Repaired ?? ""),
                HSSEDefinision = SharedEnvironment.HtmlEncode(subhssedefinision.HSSEDefinision ?? ""),
                RecommendationAction = SharedEnvironment.HtmlEncode(subrecomendationaction.RecommendationAction ?? ""),
                CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? "")
            };
        assessmentdata = result.FirstOrDefault();
        if(assessmentdata == null)
        {
            throw new Exception("Assessment not found");
        }
        return assessmentdata;
    }
    public List<AssessmentData> GetAssessmentList(bool IncludeDeleted = false, int AssetID = 0)
    {
        List<AssessmentData> assessmentlist;
        var result = 
            from assessment in _context.Assessment
            join asset in _context.Asset on assessment.AssetID equals asset.Id into ac
            from subasset in ac.DefaultIfEmpty()
            join leakagetoatmosphere in _context.CurrentConditionLimitState on assessment.LeakageToAtmosphereID equals leakagetoatmosphere.Id into lta
            from subleakagetoatmosphere in lta.DefaultIfEmpty()
            join failureoffunction in _context.CurrentConditionLimitState on assessment.FailureOfFunctionID equals failureoffunction.Id into fof
            from subfailureoffunction in fof.DefaultIfEmpty()
            join passingacrossvalve in _context.CurrentConditionLimitState on assessment.PassingAccrosValveID equals passingacrossvalve.Id into pav
            from subpassingacrossvalve in pav.DefaultIfEmpty()
            join leakeagetoatmospheretp1 in _context.TimeToLimitState on assessment.LeakageToAtmosphereTP1ID equals leakeagetoatmospheretp1.Id into lta1
            from subleakeagetoatmospheretp1 in lta1.DefaultIfEmpty()
            join leakeagetoatmospheretp2 in _context.TimeToLimitState on assessment.LeakageToAtmosphereTP2ID equals leakeagetoatmospheretp2.Id into lta2
            from subleakeagetoatmospheretp2 in lta2.DefaultIfEmpty()
            join leakeagetoatmospheretp3 in _context.TimeToLimitState on assessment.LeakageToAtmosphereTP3ID equals leakeagetoatmospheretp3.Id into lta3
            from subleakeagetoatmospheretp3 in lta3.DefaultIfEmpty()
            join failureoffunctiontp1 in _context.TimeToLimitState on assessment.FailureOfFunctionTP1ID equals failureoffunctiontp1.Id into fof1
            from subfailureoffunctiontp1 in fof1.DefaultIfEmpty()
            join failureoffunctiontp2 in _context.TimeToLimitState on assessment.FailureOfFunctionTP2ID equals failureoffunctiontp2.Id into fof2
            from subfailureoffunctiontp2 in fof2.DefaultIfEmpty()
            join failureoffunctiontp3 in _context.TimeToLimitState on assessment.FailureOfFunctionTP3ID equals failureoffunctiontp3.Id into fof3
            from subfailureoffunctiontp3 in fof3.DefaultIfEmpty()
            join passingacrossvalvetp1 in _context.TimeToLimitState on assessment.PassingAccrosValveTP1ID equals passingacrossvalvetp1.Id into pav1
            from subpassingacrossvalvetp1 in pav1.DefaultIfEmpty()
            join passingacrossvalvetp2 in _context.TimeToLimitState on assessment.PassingAccrosValveTP2ID equals passingacrossvalvetp2.Id into pav2
            from subpassingacrossvalvetp2 in pav2.DefaultIfEmpty()
            join passingacrossvalvetp3 in _context.TimeToLimitState on assessment.PassingAccrosValveTP3ID equals passingacrossvalvetp3.Id into pav3
            from subpassingacrossvalvetp3 in pav3.DefaultIfEmpty()
            join inspectioneffectiveness in _context.InspectionEffectiveness on assessment.InspectionEffectivenessID equals inspectioneffectiveness.Id into ie
            from subinspectioneffectiveness in ie.DefaultIfEmpty()
            join impactofinternalfluidimpurities in _context.ImpactEffect on assessment.ImpactOfInternalFluidImpuritiesID equals impactofinternalfluidimpurities.Id into iifi
            from subimpactofinternalfluidimpurities in iifi.DefaultIfEmpty()
            join impactofoperatingenvelope in _context.ImpactEffect on assessment.ImpactOfOperatingEnvelopesID equals impactofoperatingenvelope.Id into ioe
            from subimpactofoperatingenvelope in ioe.DefaultIfEmpty()
            join usedwithinoemspecifications in _context.UsedWithinOEMSpecification on assessment.UsedWithinOEMSpecificationID equals usedwithinoemspecifications.Id into uwoem
            from subusedwithinoemspecifications in uwoem.DefaultIfEmpty()
            join repaired in _context.Repaired on assessment.RepairedID equals repaired.Id into r
            from subrepaired in r.DefaultIfEmpty()
            join hssedefinision in _context.HSSEDefinision on assessment.HSSEDefinisionID equals hssedefinision.Id into hsse
            from subhssedefinision in hsse.DefaultIfEmpty()
            join recomendationaction in _context.RecommendationAction on assessment.RecommendationActionID equals recomendationaction.Id into ra
            from subrecomendationaction in ra.DefaultIfEmpty()
            join createby in _context.User on assessment.CreatedBy equals createby.Id into sc
            from subcreateby in sc.DefaultIfEmpty()
            join deleteby in _context.User on assessment.DeletedBy equals deleteby.Id into dc
            from subdeleteby in dc.DefaultIfEmpty()
            where  (IncludeDeleted || assessment.IsDeleted == false)
                && (AssetID == 0 || assessment.AssetID == AssetID)
            select new AssessmentData
            {
                Id = assessment.Id,
                AssetID = assessment.AssetID,
                AssessmentNo = assessment.AssessmentNo,
                AssessmentDate = assessment.AssessmentDate,
                TimePeriode = assessment.TimePeriode,
                TimeToLimitStateLeakageToAtmosphere = assessment.TimeToLimitStateLeakageToAtmosphere,
                TimeToLimitStateFailureOfFunction = assessment.TimeToLimitStateFailureOfFunction,
                TimeToLimitStatePassingAccrosValve = assessment.TimeToLimitStatePassingAccrosValve,
                LeakageToAtmosphereID = assessment.LeakageToAtmosphereID,
                FailureOfFunctionID = assessment.FailureOfFunctionID,
                PassingAccrosValveID = assessment.PassingAccrosValveID,
                LeakageToAtmosphereTP1ID = assessment.LeakageToAtmosphereTP1ID,
                LeakageToAtmosphereTP2ID = assessment.LeakageToAtmosphereTP2ID,
                LeakageToAtmosphereTP3ID = assessment.LeakageToAtmosphereTP3ID,
                FailureOfFunctionTP1ID = assessment.FailureOfFunctionTP1ID,
                FailureOfFunctionTP2ID = assessment.FailureOfFunctionTP2ID,
                FailureOfFunctionTP3ID = assessment.FailureOfFunctionTP3ID,
                PassingAccrosValveTP1ID = assessment.PassingAccrosValveTP1ID,
                PassingAccrosValveTP2ID = assessment.PassingAccrosValveTP2ID,
                PassingAccrosValveTP3ID = assessment.PassingAccrosValveTP3ID,
                InspectionEffectivenessID = assessment.InspectionEffectivenessID,
                ImpactOfInternalFluidImpuritiesID = assessment.ImpactOfInternalFluidImpuritiesID,
                ImpactOfOperatingEnvelopesID = assessment.ImpactOfOperatingEnvelopesID,
                UsedWithinOEMSpecificationID = assessment.UsedWithinOEMSpecificationID,
                RepairedID = assessment.RepairedID,
                ProductLossDefinition = SharedEnvironment.HtmlEncode(assessment.ProductLossDefinition),
                HSSEDefinisionID = assessment.HSSEDefinisionID,
                Summary = SharedEnvironment.HtmlEncode(assessment.Summary),
                RecommendationActionID = assessment.RecommendationActionID,
                DetailedRecommendation = SharedEnvironment.HtmlEncode(assessment.DetailedRecommendation),
                ConsequenceOfFailure = SharedEnvironment.HtmlEncode(assessment.ConsequenceOfFailure),
                TP1A = assessment.TP1A,
                TP1B = assessment.TP1B,
                TP1C = assessment.TP1C,
                TP2A = assessment.TP2A,
                TP2B = assessment.TP2B,
                TP2C = assessment.TP2C,
                TP3A = assessment.TP3A,
                TP3B = assessment.TP3B,
                TP3C = assessment.TP3C,
                TPTimeToActionA = assessment.TPTimeToActionA,
                TPTimeToActionB = assessment.TPTimeToActionB,
                TPTimeToActionC = assessment.TPTimeToActionC,
                TP1Risk = assessment.TP1Risk,
                TP2Risk = assessment.TP2Risk,
                TP3Risk = assessment.TP3Risk,
                TPTimeToActionRisk = assessment.TPTimeToActionRisk,
                LoFScoreLeakageToAtmophereTP1 = assessment.LoFScoreLeakageToAtmophereTP1,
                LoFScoreLeakageToAtmophereTP2 = assessment.LoFScoreLeakageToAtmophereTP2,
                LoFScoreLeakageToAtmophereTP3 = assessment.LoFScoreLeakageToAtmophereTP3,
                LoFScoreFailureOfFunctionTP1 = assessment.LoFScoreFailureOfFunctionTP1,
                LoFScoreFailureOfFunctionTP2 = assessment.LoFScoreFailureOfFunctionTP2,
                LoFScoreFailureOfFunctionTP3 = assessment.LoFScoreFailureOfFunctionTP3,
                LoFScorePassingAccrosValveTP1 = assessment.LoFScorePassingAccrosValveTP1,
                LoFScorePassingAccrosValveTP2 = assessment.LoFScorePassingAccrosValveTP2,
                LoFScorePassingAccrosValveTP3 = assessment.LoFScorePassingAccrosValveTP3,
                CoFScore = assessment.CoFScore,
                IntegrityStatus = SharedEnvironment.HtmlEncode(assessment.IntegrityStatus),
                IsDeleted = assessment.IsDeleted,
                CreatedBy = assessment.CreatedBy,
                CreatedAt = assessment.CreatedAt,
                DeletedBy = assessment.DeletedBy,
                DeletedAt = assessment.DeletedAt,
                LeakageToAtmosphere = SharedEnvironment.HtmlEncode(subleakagetoatmosphere.CurrentConditionLimitState ?? ""),
                FailureOfFunction = SharedEnvironment.HtmlEncode(subfailureoffunction.CurrentConditionLimitState ?? ""),
                PassingAccrosValve = SharedEnvironment.HtmlEncode(subpassingacrossvalve.CurrentConditionLimitState ?? ""),
                LeakageToAtmosphereTP1 = SharedEnvironment.HtmlEncode(subleakeagetoatmospheretp1.TimeToLimitState ?? ""),
                LeakageToAtmosphereTP2 = SharedEnvironment.HtmlEncode(subleakeagetoatmospheretp2.TimeToLimitState ?? ""),
                LeakageToAtmosphereTP3 = SharedEnvironment.HtmlEncode(subleakeagetoatmospheretp3.TimeToLimitState ?? ""),
                FailureOfFunctionTP1 = SharedEnvironment.HtmlEncode(subfailureoffunctiontp1.TimeToLimitState ?? ""),
                FailureOfFunctionTP2 = SharedEnvironment.HtmlEncode(subfailureoffunctiontp2.TimeToLimitState ?? ""),
                FailureOfFunctionTP3 = SharedEnvironment.HtmlEncode(subfailureoffunctiontp3.TimeToLimitState ?? ""),
                PassingAccrosValveTP1 = SharedEnvironment.HtmlEncode(subpassingacrossvalvetp1.TimeToLimitState ?? ""),
                PassingAccrosValveTP2 = SharedEnvironment.HtmlEncode(subpassingacrossvalvetp2.TimeToLimitState ?? ""),
                PassingAccrosValveTP3 = SharedEnvironment.HtmlEncode(subpassingacrossvalvetp3.TimeToLimitState ?? ""),
                InspectionEffectiveness = SharedEnvironment.HtmlEncode(subinspectioneffectiveness.Effectiveness ?? ""),
                ImpactOfInternalFluidImpurities = SharedEnvironment.HtmlEncode(subimpactofinternalfluidimpurities.ImpactEffect ?? ""),
                ImpactOfOperatingEnvelopes = SharedEnvironment.HtmlEncode(subimpactofoperatingenvelope.ImpactEffect ?? ""),
                UsedWithinOEMSpecification = SharedEnvironment.HtmlEncode(subusedwithinoemspecifications.UsedWithinOEMSpecification ?? ""),
                Repaired = SharedEnvironment.HtmlEncode(subrepaired.Repaired ?? ""),
                HSSEDefinision = SharedEnvironment.HtmlEncode(subhssedefinision.HSSEDefinision ?? ""),
                RecommendationAction = SharedEnvironment.HtmlEncode(subrecomendationaction.RecommendationAction ?? ""),
                CreatedByUser = SharedEnvironment.HtmlEncode(subcreateby.Username ?? ""),
                DeletedByUser = SharedEnvironment.HtmlEncode(subdeleteby.Username ?? "")
            };
        assessmentlist = [.. result];
        return assessmentlist;
    }
    public AssessmentData AddAssessment(AssessmentClass assessment)
    {
        if (
            !DateTime.TryParseExact(
                assessment.AssessmentDate,
                SharedEnvironment.GetDateFormatString(false),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
        {
            throw new FormatException("Assessment Date is not in the correct format (dd-MM-yyyy)");
        }
        AssessmentClass? searchassessment = _context
            .Assessment.Where(a => 
                a.AssessmentDate == assessment.AssessmentDate &&
                a.AssetID == assessment.AssetID &&
                a.IsDeleted == false)
            .FirstOrDefault();
        if (searchassessment != null)
        {
            throw new Exception("Assessment on " + assessment.AssessmentDate + " already exists.");
        }
        assessment.AssessmentNo = "IMPORT";
        assessment.IsDeleted = false;
        _context.Assessment.Add(assessment);
        _context.SaveChanges();

        AssetClass? asset = _context.Asset.Where(a => a.Id == assessment.AssetID).FirstOrDefault();
        string assettagno = asset?.TagNo ?? "";
        string lastAssessmentNo = _context.Assessment.Where(a => a.AssetID == assessment.AssetID && a.AssessmentNo.EndsWith(assettagno)).OrderByDescending(a => a.Id).Select(a => a.AssessmentNo).FirstOrDefault() ?? "";
        if(String.IsNullOrEmpty(lastAssessmentNo))
        {
            lastAssessmentNo = "ASSESSMENT-1-" + assettagno;
        } else {
            string[] splitAssessmentNo = lastAssessmentNo.Split("-");
            int countAssessmentSameAsset = Int32.Parse(splitAssessmentNo[1]);
            lastAssessmentNo = "ASSESSMENT-" + (countAssessmentSameAsset + 1) + "-" + assettagno;
        }
        assessment.AssessmentNo = lastAssessmentNo;
        _context.Assessment.Update(assessment);
        _context.SaveChanges();
        return GetAssessment(assessment.Id);
    }
    public AssessmentData UpdateAssessment(AssessmentClass assessment)
    {
        if (
            !DateTime.TryParseExact(
                assessment.AssessmentDate,
                SharedEnvironment.GetDateFormatString(false),
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
        {
            throw new FormatException("Assessment Date is not in the correct format (dd-MM-yyyy)");
        }
        AssessmentClass? oldAssessment = _context
            .Assessment.Where(a => a.Id == assessment.Id && a.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("Assessment not found");
        AssessmentClass? searchassessment = _context
            .Assessment.Where(a => 
                a.AssessmentDate == assessment.AssessmentDate &&
                a.AssetID == assessment.AssetID &&
                a.IsDeleted == false &&
                a.Id != assessment.Id)
            .FirstOrDefault();
        if (searchassessment != null)
        {
            throw new Exception("Assessment on " + assessment.AssessmentDate + " already exists.");
        }
        oldAssessment.AssetID = assessment.AssetID;
        oldAssessment.AssessmentNo = assessment.AssessmentNo;
        oldAssessment.AssessmentDate = assessment.AssessmentDate;
        oldAssessment.TimePeriode = assessment.TimePeriode;
        oldAssessment.TimeToLimitStateLeakageToAtmosphere = assessment.TimeToLimitStateLeakageToAtmosphere;
        oldAssessment.TimeToLimitStateFailureOfFunction = assessment.TimeToLimitStateFailureOfFunction;
        oldAssessment.TimeToLimitStatePassingAccrosValve = assessment.TimeToLimitStatePassingAccrosValve;
        oldAssessment.LeakageToAtmosphereID = assessment.LeakageToAtmosphereID;
        oldAssessment.FailureOfFunctionID = assessment.FailureOfFunctionID;
        oldAssessment.PassingAccrosValveID = assessment.PassingAccrosValveID;
        oldAssessment.LeakageToAtmosphereTP1ID = assessment.LeakageToAtmosphereTP1ID;
        oldAssessment.LeakageToAtmosphereTP2ID = assessment.LeakageToAtmosphereTP2ID;
        oldAssessment.LeakageToAtmosphereTP3ID = assessment.LeakageToAtmosphereTP3ID;
        oldAssessment.FailureOfFunctionTP1ID = assessment.FailureOfFunctionTP1ID;
        oldAssessment.FailureOfFunctionTP2ID = assessment.FailureOfFunctionTP2ID;
        oldAssessment.FailureOfFunctionTP3ID = assessment.FailureOfFunctionTP3ID;
        oldAssessment.PassingAccrosValveTP1ID = assessment.PassingAccrosValveTP1ID;
        oldAssessment.PassingAccrosValveTP2ID = assessment.PassingAccrosValveTP2ID;
        oldAssessment.PassingAccrosValveTP3ID = assessment.PassingAccrosValveTP3ID;
        oldAssessment.InspectionEffectivenessID = assessment.InspectionEffectivenessID;
        oldAssessment.ImpactOfInternalFluidImpuritiesID = assessment.ImpactOfInternalFluidImpuritiesID;
        oldAssessment.ImpactOfOperatingEnvelopesID = assessment.ImpactOfOperatingEnvelopesID;
        oldAssessment.UsedWithinOEMSpecificationID = assessment.UsedWithinOEMSpecificationID;
        oldAssessment.RepairedID = assessment.RepairedID;
        oldAssessment.ProductLossDefinition = assessment.ProductLossDefinition;
        oldAssessment.HSSEDefinisionID = assessment.HSSEDefinisionID;
        oldAssessment.Summary = assessment.Summary;
        oldAssessment.RecommendationActionID = assessment.RecommendationActionID;
        oldAssessment.DetailedRecommendation = assessment.DetailedRecommendation;
        oldAssessment.ConsequenceOfFailure = assessment.ConsequenceOfFailure;
        oldAssessment.TP1A = assessment.TP1A;
        oldAssessment.TP1B = assessment.TP1B;
        oldAssessment.TP1C = assessment.TP1C;
        oldAssessment.TP2A = assessment.TP2A;
        oldAssessment.TP2B = assessment.TP2B;
        oldAssessment.TP2C = assessment.TP2C;
        oldAssessment.TP3A = assessment.TP3A;
        oldAssessment.TP3B = assessment.TP3B;
        oldAssessment.TP3C = assessment.TP3C;
        oldAssessment.TPTimeToActionA = assessment.TPTimeToActionA;
        oldAssessment.TPTimeToActionB = assessment.TPTimeToActionB;
        oldAssessment.TPTimeToActionC = assessment.TPTimeToActionC;
        oldAssessment.TP1Risk = assessment.TP1Risk;
        oldAssessment.TP2Risk = assessment.TP2Risk;
        oldAssessment.TP3Risk = assessment.TP3Risk;
        oldAssessment.TPTimeToActionRisk = assessment.TPTimeToActionRisk;
        oldAssessment.LoFScoreLeakageToAtmophereTP1 = assessment.LoFScoreLeakageToAtmophereTP1;
        oldAssessment.LoFScoreLeakageToAtmophereTP2 = assessment.LoFScoreLeakageToAtmophereTP2;
        oldAssessment.LoFScoreLeakageToAtmophereTP3 = assessment.LoFScoreLeakageToAtmophereTP3;
        oldAssessment.LoFScoreFailureOfFunctionTP1 = assessment.LoFScoreFailureOfFunctionTP1;
        oldAssessment.LoFScoreFailureOfFunctionTP2 = assessment.LoFScoreFailureOfFunctionTP2;
        oldAssessment.LoFScoreFailureOfFunctionTP3 = assessment.LoFScoreFailureOfFunctionTP3;
        oldAssessment.LoFScorePassingAccrosValveTP1 = assessment.LoFScorePassingAccrosValveTP1;
        oldAssessment.LoFScorePassingAccrosValveTP2 = assessment.LoFScorePassingAccrosValveTP2;
        oldAssessment.LoFScorePassingAccrosValveTP3 = assessment.LoFScorePassingAccrosValveTP3;
        oldAssessment.CoFScore = assessment.CoFScore;
        oldAssessment.IntegrityStatus = assessment.IntegrityStatus;
        _context.Assessment.Update(oldAssessment);
        _context.SaveChanges();
        return GetAssessment(assessment.Id);
    }
    public AssessmentData DeleteAssessment(AssessmentClass assessment)
    {
        AssessmentClass? oldAssessment = _context
            .Assessment.Where(a => a.Id == assessment.Id && a.IsDeleted == false)
            .FirstOrDefault() ?? throw new Exception("Assessment not found");
        oldAssessment.IsDeleted = true;
        oldAssessment.DeletedAt = assessment.DeletedAt;
        oldAssessment.DeletedBy = assessment.DeletedBy;
        _context.Assessment.Update(oldAssessment);
        _context.SaveChanges();
        return GetAssessment(assessment.Id);
    }

}
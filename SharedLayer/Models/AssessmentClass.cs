namespace SharedLayer;

public class AssessmentClass
{
    public int Id { get; set; }
    public int AssetID { get; set; }
    public string? AssessmentNo { get; set; }
    public string? AssessmentDate { get; set; }
    public string? TimePeriode { get; set; }
    public string? TimeToLimitStateLeakageToAtmosphere { get; set; }
    public string? TimeToLimitStateFailureOfFunction { get; set; }
    public string? TimeToLimitStatePassingAccrosValve { get; set; }
    public int? LeakageToAtmosphereID { get; set; }
    public int? FailureOfFunctionID { get; set; }
    public int? PassingAccrosValveID { get; set; }
    public int? LeakageToAtmosphereTP1ID { get; set; }
    public int? LeakageToAtmosphereTP2ID { get; set; }
    public int? LeakageToAtmosphereTP3ID { get; set; }
    public int? FailureOfFunctionTP1ID { get; set; }
    public int? FailureOfFunctionTP2ID { get; set; }
    public int? FailureOfFunctionTP3ID { get; set; }
    public int? PassingAccrosValveTP1ID { get; set; }
    public int? PassingAccrosValveTP2ID { get; set; }
    public int? PassingAccrosValveTP3ID { get; set; }
    public int? InspectionEffectivenessID { get; set; }
    public int? ImpactOfInternalFluidImpuritiesID { get; set; }
    public int? ImpactOfOperatingEnvelopesID { get; set; }
    public int? UsedWithinOEMSpecificationID { get; set; }
    public int? RepairedID { get; set; }
    public string? ProductLossDefinition { get; set; }
    public int? HSSEDefinisionID { get; set; }
    public string? Summary { get; set; }
    public int? RecommendationActionID { get; set; }
    public string? DetailedRecommendation { get; set; }
    public string? ConsequenceOfFailure { get; set; }
    public string? TP1A { get; set; }
    public string? TP2A { get; set; }
    public string? TP3A { get; set; }
    public string? TP1B { get; set; }
    public string? TP2B { get; set; }
    public string? TP3B { get; set; }
    public string? TP1C { get; set; }
    public string? TP2C { get; set; }
    public string? TP3C { get; set; }
    public string? TPTimeToActionA { get; set; }
    public string? TPTimeToActionB { get; set; }
    public string? TPTimeToActionC { get; set; }
    public string? TP1Risk { get; set; }
    public string? TP2Risk { get; set; }
    public string? TP3Risk { get; set; }
    public string? TPTimeToActionRisk { get; set; }
    public double? LoFScoreLeakageToAtmophereTP1 { get; set; }
    public double? LoFScoreLeakageToAtmophereTP2 { get; set; }
    public double? LoFScoreLeakageToAtmophereTP3 { get; set; }
    public double? LoFScoreFailureOfFunctionTP1 { get; set; }
    public double? LoFScoreFailureOfFunctionTP2 { get; set; }
    public double? LoFScoreFailureOfFunctionTP3 { get; set; }
    public double? LoFScorePassingAccrosValveTP1 { get; set; }
    public double? LoFScorePassingAccrosValveTP2 { get; set; }
    public double? LoFScorePassingAccrosValveTP3 { get; set; }
    public double? CoFScore { get; set; }
    public string? IntegrityStatus { get; set; }
    public string? TimeToAction { get; set; }
    public bool? IsDeleted { get; set; }
    public string? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public string? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public string? UpdatedAt { get; set; }
}

public class AssessmentData : AssessmentClass
{
    public AssetData? Asset { get; set; }
    public string? LeakageToAtmosphere { get; set; }
    public string? FailureOfFunction { get; set; }
    public string? PassingAccrosValve { get; set; }
    public string? LeakageToAtmosphereTP1 { get; set; }
    public string? LeakageToAtmosphereTP2 { get; set; }
    public string? LeakageToAtmosphereTP3 { get; set; }
    public string? FailureOfFunctionTP1 { get; set; }
    public string? FailureOfFunctionTP2 { get; set; }
    public string? FailureOfFunctionTP3 { get; set; }
    public string? PassingAccrosValveTP1 { get; set; }
    public string? PassingAccrosValveTP2 { get; set; }
    public string? PassingAccrosValveTP3 { get; set; }
    public string? InspectionEffectiveness { get; set; }
    public string? ImpactOfInternalFluidImpurities { get; set; }
    public string? ImpactOfOperatingEnvelopes { get; set; }
    public string? UsedWithinOEMSpecification { get; set; }
    public string? Repaired { get; set; }
    public string? HSSEDefinision { get; set; }
    public string? RecommendationAction { get; set; }
    public string? CreatedByUser { get; set; }
    public string? DeletedByUser { get; set; }
    public string? UpdatedByUser { get; set; }
    public List<AssessmentInspectionData>? InspectionHistory { get; set; }
    public List<AssessmentMaintenanceData>? MaintenanceHistory { get; set; }
    public string? RiskMax { get; set; }
    public string? LastInspectionDate { get; set; }
    public string? LastMaintenanceDate { get; set; }
    public int? LastInspectionId { get; set; }
    public int? LastMaintenanceId { get; set; }
}

public class AssessmentStaticClass
{
    public static readonly Dictionary<string, string> ColorRiskMap =
        new()
        {
            { "olive", "Very Low" },
            { "green", "Low" },
            { "yellow", "Medium" },
            { "orange", "High" },
            { "red", "Very High" }
        };
    public static string GetHeatColor(string pos)
    {
        List<string> olivePositions = ["1A", "2A", "1B"];
        List<string> greenPositions = ["3A", "2B", "1C"];
        List<string> yellowPositions = ["4A", "3B", "3C", "2C", "1D"];
        List<string> orangePositions =
        [
            "5A",
            "5B",
            "4B",
            "4C",
            "3D",
            "2D",
            "2E",
            "1E"
        ];

        if (olivePositions.Contains(pos))
        {
            return "olive";
        }
        else if (greenPositions.Contains(pos))
        {
            return "green";
        }
        else if (yellowPositions.Contains(pos))
        {
            return "yellow";
        }
        else if (orangePositions.Contains(pos))
        {
            return "orange";
        }
        else
        {
            return "red";
        }
    }

    public static Dictionary<string, int> GetHeatXYpos(string pos)
    {
        List<string> pos_split = Split_risk(pos);
        string lof = pos_split[0];
        string cos = pos_split[1];
        int ypos = (int.Parse(lof) * 20) - 10;
        int xpos = (LofToInt(cos) * 20) - 10;
        return new Dictionary<string, int> { { "xpos", xpos }, { "ypos", ypos } };
    }

    public static List<string> Split_risk(string risk)
    {
        if (risk.Length < 2)
        {
            return ["0", "0"];
        }
        string lof = risk.Substring(0, 1);
        string cos = risk.Substring(1, 1);
        return [lof, cos];
    }

    public static int LofToInt(string lof)
    {
        return lof switch
        {
            "A" => 1,
            "B" => 2,
            "C" => 3,
            "D" => 4,
            "E" => 5,
            _ => 0,
        };
    }
}
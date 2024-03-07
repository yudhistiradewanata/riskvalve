namespace Riskvalve.Models
{
    public class InspectionModel
    {
        public int Id { get; set; }
        public string? ValveTagNo { get; set; }
        public string? InspectionDate { get; set; }
        public string? InspectionMethod { get; set; }
        public int? InspectionEffectivenessID { get; set; }
        public string? InspectionDescription { get; set; }
        public int? InspectionCurrentLimitStateAID { get; set; }
        public int? InspectionCurrentLimitStateBID { get; set; }
        public int? InspectionCurrentLimitStateCID { get; set; }
        public string? FunctionCondition { get; set; }
        public string? TestPressureIfAny { get; set; }
    }

    public class InspectionSidebarModel {
        public int Id { get; set; }
        public string? Name { get; set; }
        public InspectionSidebarModel? Child { get; set; }
    }

    public class ConditionLimitStateModel
    {
        public int Id { get; set; }
        public string? LimitState { get; set; }

        public List<ConditionLimitStateModel> GetConditionLimitStates()
        {
            List<ConditionLimitStateModel> conditionLimitStates =
                new()
                {
                    new ConditionLimitStateModel { Id = 1, LimitState = "Good" },
                    new ConditionLimitStateModel { Id = 2, LimitState = "Fair" },
                    new ConditionLimitStateModel { Id = 3, LimitState = "Poor" }
                };
            return conditionLimitStates;
        }
    }

    public class InspectionEffectivenessModel
    {
        public int Id { get; set; }
        public string? Effectiveness { get; set; }

        public List<InspectionEffectivenessModel> GetInspectionEffectivenessStates()
        {
            List<InspectionEffectivenessModel> inspectionEffectiveness =
                new()
                {
                    new InspectionEffectivenessModel
                    {
                        Id = 1,
                        Effectiveness = "Hightly Effective"
                    },
                    new InspectionEffectivenessModel
                    {
                        Id = 2,
                        Effectiveness = "Ussualy Effective"
                    },
                    new InspectionEffectivenessModel { Id = 3, Effectiveness = "Fairly Effective" },
                    new InspectionEffectivenessModel { Id = 4, Effectiveness = "Ineffective" }
                };
            return inspectionEffectiveness;
        }
    }

    public class IsValveRepairedModel
    {
        public int Id { get; set; }
        public string? IsValveRepaired { get; set; }

        public List<IsValveRepairedModel> GetIsValveRepairedStates()
        {
            List<IsValveRepairedModel> isValveRepaired =
                new()
                {
                    new IsValveRepairedModel { Id = 1, IsValveRepaired = "Yes" },
                    new IsValveRepairedModel { Id = 2, IsValveRepaired = "No" }
                };
            return isValveRepaired;
        }
    }
}

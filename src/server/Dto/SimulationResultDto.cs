namespace FinanceServices.Dto
{
    public sealed class SimulationResultDto : IEquatable<SimulationResultDto>
    {
        public decimal LiquidAmount { get; set; }
        public decimal BruteAmount { get; set; }
        public decimal TributePercent { get; set; }
        public decimal TributeAmount { get; set; }

        public bool Equals(SimulationResultDto? other)
        {
            if (other == null)
                return false;

            return LiquidAmount == other.LiquidAmount &&
                   BruteAmount == other.BruteAmount &&
                   TributeAmount == other.TributeAmount &&
                   TributePercent == other.TributePercent;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as SimulationResultDto);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}

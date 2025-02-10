namespace FinanceServices.Exceptions
{
    public class SimulationException(string? message) : Exception(message)
    {
        public bool IsBusinessException { get; set; } = true;

        public const string AmountValidationMessage = "O valor inicial deve ser maior que 0.";

        public const string MonthQuantityValidationMessage = "A quantidade de meses deve ser maior do que 1 (um mês).";

        public const string MonthIntegerValidationMessage = "O número de meses deve ser um número inteiro.";
    }
}

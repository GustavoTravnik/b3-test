using FinanceServices.Exceptions;

namespace FinanceServices.Extentions
{
    public static class ObjectExtensions
    {
        public static bool IsInternalExeption(this object obj)
        {
            return obj != null && obj.GetType() == typeof(SimulationException) && ((SimulationException)obj).IsBusinessException;
        }
    }
}

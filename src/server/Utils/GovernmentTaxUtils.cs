namespace FinanceServices.Utils
{
    public abstract class GovernmentTaxUtils
    {
        public static decimal GetTribute(int months)
        {
            if (months <= 6)
            {
                return 0.225m;
            }
            else if (months <= 12)
            {
                return 0.2m;
            }
            else if (months <= 24)
            {
                return 0.175m;
            }
            else
            {
                return 0.15m;
            }
        }
    }
}

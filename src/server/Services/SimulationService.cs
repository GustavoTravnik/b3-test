using FinanceServices.Dto;
using FinanceServices.Exceptions;
using FinanceServices.Utils;

namespace FinanceServices.Services
{
    public class SimulationService : ISimulationService
    {
        public SimulationService()
        {

        }

        public Task<SimulationResultDto> GetSimulation(decimal initialAmount, decimal mounthQuantity)
        {
            ValidadeInputs(initialAmount, mounthQuantity);

            var cdiPercentageValue = 0.9m;
            var tbPercentageValue = 108m;

            decimal payedCdiTaxByBank = GetBankYield(cdiPercentageValue, tbPercentageValue);

            var bruteValue = initialAmount;

            var governmentTaxValue = GovernmentTaxUtils.GetTribute(Convert.ToInt32(mounthQuantity));

            return Task.Run(() =>
            {
                try
                {
                    for (var i = 0; i < mounthQuantity; i++)
                    {
                        ApplyCdiTax(ref bruteValue, payedCdiTaxByBank);
                    }

                    var rawProfit = bruteValue - initialAmount;
                    var texDiscont = rawProfit * governmentTaxValue;

                    var result = new SimulationResultDto
                    {
                        BruteAmount = Math.Round(bruteValue, 2),
                        LiquidAmount = Math.Round(bruteValue - texDiscont, 2),
                        TributePercent = Math.Round(governmentTaxValue * 100, 2),
                        TributeAmount = Math.Round(texDiscont, 2)
                    };

                    return result;
                }
                catch (Exception ex)
                {
                    throw new SimulationException(ex.Message);
                }
            });
        }

        public static decimal GetBankYield(decimal cdiPercentageValue, decimal tbPercentageValue)
        {
            return cdiPercentageValue * (tbPercentageValue / 100);
        }

        public static void ValidadeInputs(decimal initialAmount, decimal mounthQuantity)
        {
            if (initialAmount <= 0)
            {
                throw new SimulationException(SimulationException.AmountValidationMessage);
            }
            else if (mounthQuantity < 1)
            {
                throw new SimulationException(SimulationException.MonthQuantityValidationMessage);
            }
            else if (mounthQuantity % 1 != 0)
            {
                throw new SimulationException(SimulationException.MonthIntegerValidationMessage);
            }
        }

        public static void ApplyCdiTax(ref decimal bruteValue, decimal payedCdiTaxByBank)
        {
            bruteValue *= 1 + (payedCdiTaxByBank / 100);
        }
    }
}

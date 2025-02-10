using FinanceServices.Dto;
using FinanceServices.Exceptions;
using FinanceServices.Services;
using FinanceServices.Utils;

namespace FinanceServices
{
    public class UnitTests
    {
        private readonly SimulationService _service;

        public UnitTests()
        {
            _service = new SimulationService();
        }

        [Theory]
        [InlineData(100, 10, 108.13, 110.16, 2.03, 20.0)]
        [InlineData(100, 20, 117.61, 121.34, 3.74, 17.50)]
        [InlineData(123, 321, 2351.00, 2744.18, 393.18, 15.00)]
        [InlineData(221, 4, 227.76, 229.72, 1.96, 22.50)]
        public async Task Given_ValidArgumentsParams_When_CallTheSimulationMethod_Then_GiveTheCorrectSimulatedValues(
            decimal initialDeposit, int numberOfMounths, decimal expectedLiquidAmount, decimal expectedBruteAmount,
            decimal expectedAppliedTax, decimal texPercentage)
        {
            var simulationReslt = await _service.GetSimulation(initialDeposit, numberOfMounths);

            var expectedSimulationResult = new SimulationResultDto
            {
                LiquidAmount = expectedLiquidAmount + 0.00m,
                BruteAmount = expectedBruteAmount + 0.00m,
                TributeAmount = expectedAppliedTax + 0.00m,
                TributePercent = texPercentage + 0.00m
            };

            Assert.Equal(simulationReslt, expectedSimulationResult);
        }

        [Theory]
        [InlineData(100, 0, SimulationException.MonthQuantityValidationMessage)]
        [InlineData(100, -5, SimulationException.MonthQuantityValidationMessage)]
        [InlineData(100, 1.4, SimulationException.MonthIntegerValidationMessage)]
        [InlineData(-100, 0, SimulationException.AmountValidationMessage)]
        [InlineData(0, -5, SimulationException.AmountValidationMessage)]
        public async Task Given_NotAllowedArgumentsParams_When_CallTheSimulationMethod_Then_ThrowTheCorrectErrorsExceptions(decimal initialDeposit, decimal numberOfMounths, string message)
        {
            var SimulationException = await Assert.ThrowsAsync<SimulationException>(() => _service.GetSimulation(initialDeposit, numberOfMounths));

            Assert.Equal(message, SimulationException?.Message);
        }

        [Theory]
        [InlineData(0.9, 108, 0.972)]
        [InlineData(0.86, 200, 1.72)]
        [InlineData(0.563, 45, 0.25335)]
        public void Given_CdiAndTb_When_GetTaxYield_Then_GetCorrectValues(decimal cdiPercentageValue, decimal tbPercentageValue, decimal expectedTax)
        {
            expectedTax = Math.Round(expectedTax, 3);
            var taxValue = Math.Round(SimulationService.GetBankYield(cdiPercentageValue, tbPercentageValue), 3);

            Assert.Equal(taxValue, expectedTax);
        }

        [Theory]
        [InlineData(100, 0.9, 1, 100.90)]
        [InlineData(100, 0.9, 6, 105.52)]
        [InlineData(100, 0.9, 212, 668.23)]
        [InlineData(3241, 1.52, 33, 5331.90)]

        public void Given_BruteValueAndTax_When_ApplyCdiTaxNTimes_Then_GetCorrectYield(decimal initialValue, decimal taxValue, int times, decimal expectedNewValue)
        {
            for (var i = 0; i < times; i++)
            {
                SimulationService.ApplyCdiTax(ref initialValue, taxValue);
            }

            Assert.Equal(Math.Round(initialValue, 2), Math.Round(expectedNewValue, 2));
        }

        [Theory]
        [InlineData(-7, 0, SimulationException.AmountValidationMessage)]
        [InlineData(100, -5, SimulationException.MonthQuantityValidationMessage)]
        [InlineData(100, 1.4, SimulationException.MonthIntegerValidationMessage)]
        public async Task Given_InvalidMountOrAmountParams_When_CallValidationCheck_Then_ThrowTheCorrectErrorsExceptions(decimal initialDeposit, decimal numberOfMounths, string message)
        {
            var SimulationException = await Assert.ThrowsAsync<SimulationException>(() => Task.Run(() => SimulationService.ValidadeInputs(initialDeposit, numberOfMounths)));

            Assert.Equal(message, SimulationException?.Message);
        }

        [Theory]
        [InlineData(4, 0.225)]
        [InlineData(8, 0.2)]
        [InlineData(16, 0.175)]
        [InlineData(28, 0.15)]
        public void Given_NumberOfMounths_When_GetTaxValue_Then_ReturnTheDefaultTaxes(int numberOfMounths, decimal expectedNewValue)
        {
            Assert.Equal(expectedNewValue, GovernmentTaxUtils.GetTribute(numberOfMounths));
        }
    }
}
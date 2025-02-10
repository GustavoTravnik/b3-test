using FinanceServices.Dto;

namespace FinanceServices.Services
{
    public interface ISimulationService
    {
        Task<SimulationResultDto> GetSimulation(decimal initialAmount, decimal mounthQuantity);
    }
}

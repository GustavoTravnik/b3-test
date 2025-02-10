using FinanceServices.Extentions;
using FinanceServices.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinanceServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Simulation : ControllerBase
    {
        private readonly ISimulationService _FinanceServices;

        public Simulation(ISimulationService FinanceServices)
        {
            _FinanceServices = FinanceServices;
        }

        [HttpGet("getCalculation")]
        [SwaggerOperation(Summary = "Make the simulation of investment")]
        public async Task<ActionResult> GetCalculation([FromQuery] decimal initialAmount, [FromQuery] decimal mounthQuantity)
        {
            try
            {
                var result = await _FinanceServices.GetSimulation(initialAmount, mounthQuantity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.IsInternalExeption())
                {
                    return ValidationProblem(ex.Message);
                }

                return Problem(ex.Message);
            }
        }
    }
}

using ElectricityTariffTest.Server.Interfaces;
using ElectricityTariffTest.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityTariffTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffsController : ControllerBase
    {
        private readonly ITariffService _tariffService;
        private readonly ILogger<TariffsController> _logger;
        public TariffsController(ITariffService tariffService, ILogger<TariffsController> logger)
        {
            _tariffService = tariffService;
            _logger = logger;
        }
        public class ConsumptionRequest
        {
            public decimal Consumption { get; set; }
        }
        [HttpPost]
        public IActionResult CalculateCosts([FromBody] ConsumptionRequest request)
        {
            try
            {
                if (request.Consumption <= 0)
                {
                    return BadRequest("Consumption must be greater than 0.");
                }
                var result = _tariffService.CalculateCosts(request.Consumption);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Handle specific exceptions
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid Input",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred during tariff calculation.");

                // Return generic error response
                return StatusCode(500, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                    Detail = "An unexpected error occurred. Please try again later."
                });
            }
        }

        //[HttpPost]
        //public IActionResult CalculateCosts([FromBody] ConsumptionRequest request)
        //{
        //    var tariffs = new List<Tariff>
        //{
        //    new Tariff { Name = "Product A", Type = 1, BaseCost = 5, AdditionalKwhCost = 22 },
        //    new Tariff { Name = "Product B", Type = 2, IncludedKwh = 4000, BaseCost = 800, AdditionalKwhCost = 30 }
        //};

        //    var results = TariffCalculator.CalculateAnnualCosts(tariffs, request.Consumption);

        //    return Ok(results);
        //}
    }
}

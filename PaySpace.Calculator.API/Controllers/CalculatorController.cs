using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Abstractions;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Borders.Shared;
using PaySpace.Calculator.Services.Abstractions.UseCases;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class CalculatorController(
        IActionResultConverter actionResultConverter,
        ICalculateTaxUseCase calculatorUseCase,
        IGetCalculatorHistoryUseCase historyUseCase) : ControllerBase
    {
        [HttpPost("calculate-tax")]
        [ProducesResponseType(typeof(CalculateResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage[]), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMessage[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Calculate(CalculateRequest request)
        {
            var result = await calculatorUseCase.Execute(request);
            return actionResultConverter.Convert(result);
        }

        [HttpGet("history")]
        [ProducesResponseType(typeof(List<CalculatorHistoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage[]), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMessage[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> History()
        {
            var result = await historyUseCase.Execute();
            return actionResultConverter.Convert(result);
        }
    }
}
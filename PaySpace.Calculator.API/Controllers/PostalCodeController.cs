using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Abstractions;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Borders.Shared;
using PaySpace.Calculator.Services.Abstractions.UseCases;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class PostalCodeController(
        IActionResultConverter actionResultConverter,
        IGetPostalCodesUseCase postalCodesUseCase) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<PostalCodeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage[]), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMessage[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> List()
        {
            var result = await postalCodesUseCase.Execute();
            return actionResultConverter.Convert(result);
        }
    }
}
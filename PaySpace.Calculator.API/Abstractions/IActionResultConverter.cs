using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.Borders.Shared;

namespace PaySpace.Calculator.API.Abstractions;

public interface IActionResultConverter
{
    IActionResult Convert<Tin, Tout>(UseCaseResponse<Tin> response, Func<Tin?, Tout?>? converter = null) where Tin : class where Tout : class;

    IActionResult Convert<Tin>(UseCaseResponse<Tin> response) where Tin : class;
}

using PaySpace.Calculator.Borders.Models;

namespace PaySpace.Calculator.Services.Abstractions.UseCases;

public interface IGetCalculatorHistoryUseCase : IUseCaseOnlyResponse<List<CalculatorHistoryDto>>
{
}
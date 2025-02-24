using MapsterMapper;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Borders.Shared;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Abstractions.UseCases;

namespace PaySpace.Calculator.Services.UseCases;

internal sealed class GetCalculatorHistoryUseCase(
    IMapper _mapper,
    IHistoryService _historyService) : IGetCalculatorHistoryUseCase
{
    public async Task<UseCaseResponse<List<CalculatorHistoryDto>>> Execute()
    {
        var result = await _historyService.GetHistoryAsync();
        return UseCaseResponse<List<CalculatorHistoryDto>>.Success(_mapper.Map<List<CalculatorHistoryDto>>(result));
    }
}
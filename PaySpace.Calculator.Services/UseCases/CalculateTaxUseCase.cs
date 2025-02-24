using FluentValidation;
using MapsterMapper;
using PaySpace.Calculator.Borders.Constants;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Borders.Shared;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Abstractions.Strategies;
using PaySpace.Calculator.Services.Abstractions.UseCases;

namespace PaySpace.Calculator.Services.UseCases;

internal sealed class CalculateTaxUseCase(
    IMapper _mapper,
    IValidator<CalculateRequest> _validator,
    IPostalCodeService _postalCodeService,
    IHistoryService _historyService,
    ITaxCalculatorStrategy _calculatorStrategy) : ICalculateTaxUseCase
{
    public async Task<UseCaseResponse<CalculateResultDto>> Execute(CalculateRequest request)
    {
        _validator.ValidateAndThrow(request);

        var calculatorType = await _postalCodeService.GetCalculatorTypeAsync(request.PostalCode);

        if (calculatorType == null)
            return UseCaseResponse<CalculateResultDto>.NotFound(ErrorMessages.InvalidPostalCode);

        var result = await _calculatorStrategy.GetCalculator(calculatorType.Value).CalculateAsync(request.Income);

        await _historyService.AddAsync(_mapper.Map<CalculatorHistory>((request, result)));

        return UseCaseResponse<CalculateResultDto>.Success(_mapper.Map<CalculateResultDto>(result));
    }
}
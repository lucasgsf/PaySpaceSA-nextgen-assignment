using Mapster;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Borders.Adapters;

internal sealed class CalculatorAdapters : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CalculateRequest request, CalculateResult result), CalculatorHistory>()
            .MapWith(src => AdaptCalculatorHistory(src.request, src.result));

        config.NewConfig<CalculateResult, CalculateResultDto>()
            .Map(dest => dest.Calculator, src => src.Calculator.ToString());

        config.NewConfig<CalculatorHistory, CalculatorHistoryDto>()
            .Map(dest => dest.Calculator, src => src.Calculator.ToString())
            .TwoWays();

        config.NewConfig<PostalCode, PostalCodeDto>()
            .TwoWays();
    }

    private CalculatorHistory AdaptCalculatorHistory(CalculateRequest request, CalculateResult result)
    {
        return new()
        {
            Income = request.Income,
            PostalCode = request.PostalCode!,
            Calculator = result.Calculator,
            Tax = result.Tax,
        };
    }
}
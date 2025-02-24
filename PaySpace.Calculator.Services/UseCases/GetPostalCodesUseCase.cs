using MapsterMapper;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Borders.Shared;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Abstractions.UseCases;

namespace PaySpace.Calculator.Services.UseCases;

internal sealed class GetPostalCodesUseCase(
    IMapper _mapper,
    IPostalCodeService _postalCodeService) : IGetPostalCodesUseCase
{
    public async Task<UseCaseResponse<List<PostalCodeDto>>> Execute()
    {
        var result = await _postalCodeService.GetPostalCodesAsync();
        return UseCaseResponse<List<PostalCodeDto>>.Success(_mapper.Map<List<PostalCodeDto>>(result));
    }
}
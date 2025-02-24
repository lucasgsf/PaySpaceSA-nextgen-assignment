using PaySpace.Calculator.Borders.Shared;

namespace PaySpace.Calculator.Services.Abstractions;

public interface IUseCase<TRequest, TResponse>
{
    Task<UseCaseResponse<TResponse>> Execute(TRequest request);
}
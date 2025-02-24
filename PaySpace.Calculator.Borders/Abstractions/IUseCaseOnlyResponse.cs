using PaySpace.Calculator.Borders.Shared;

namespace PaySpace.Calculator.Services.Abstractions;

public interface IUseCaseOnlyResponse<TResponse>
{
    Task<UseCaseResponse<TResponse>> Execute();
}
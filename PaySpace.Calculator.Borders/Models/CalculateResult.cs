using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Borders.Models
{
    public sealed record CalculateResult
    {
        public CalculatorType Calculator { get; init; }

        public decimal Tax { get; init; }
    }
}
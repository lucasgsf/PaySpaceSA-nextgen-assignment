using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Strategies;

namespace PaySpace.Calculator.Tests.Strategies;

[TestFixture]
internal sealed class TaxCalculatorStrategyTest
{
    private MockRepository _repositoryMock;

    private Mock<ITaxCalculator> _mockProgressiveCalculator;
    private Mock<ITaxCalculator> _mockFlatValueCalculator;
    private Mock<ITaxCalculator> _mockFlatRateCalculator;

    private TaxCalculatorStrategy _service;

    private List<ITaxCalculator> _calculators;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new MockRepository(MockBehavior.Default);

        _mockProgressiveCalculator = _repositoryMock.Create<ITaxCalculator>();
        _mockFlatValueCalculator = _repositoryMock.Create<ITaxCalculator>();
        _mockFlatRateCalculator = _repositoryMock.Create<ITaxCalculator>();

        _mockProgressiveCalculator.Setup(c => c.CalculatorType).Returns(CalculatorType.Progressive);
        _mockFlatValueCalculator.Setup(c => c.CalculatorType).Returns(CalculatorType.FlatValue);
        _mockFlatRateCalculator.Setup(c => c.CalculatorType).Returns(CalculatorType.FlatRate);

        _calculators =
        [
            _mockProgressiveCalculator.Object,
            _mockFlatValueCalculator.Object,
            _mockFlatRateCalculator.Object
        ];

        _service = new TaxCalculatorStrategy(_calculators);
    }

    [TestCase(CalculatorType.Progressive)]
    [TestCase(CalculatorType.FlatRate)]
    [TestCase(CalculatorType.FlatValue)]
    public async Task Verify_Get_TaxCalculator(CalculatorType rateType)
    {
        // Act
        var result = _service.GetCalculator(rateType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.CalculatorType, Is.EqualTo(rateType));
    }
}
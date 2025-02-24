using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Abstractions.Strategies;
using PaySpace.Calculator.Services.Builders;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Services;
using PaySpace.Calculator.Tests.Builders;

namespace PaySpace.Calculator.Tests.Calculators;

[TestFixture]
internal sealed class FlatRateCalculatorTests
{
    private MockRepository _repositoryMock;
    private Mock<ICalculatorSettingsService> _mockCalculatorSettingsService;
    private Mock<ICalculationServiceStrategy> _mockCalculationServiceStrategy;

    private CalculatorType _calculatorType = CalculatorType.FlatRate;
    private FlatRateCalculator _service;

    private PercentageValueCalculationService _percentageCalculationService;
    private AmountValueCalculationService _amountCalculationService;
    private CalculateResultBuilder _calculateResultBuilder;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new MockRepository(MockBehavior.Default);
        _mockCalculatorSettingsService = _repositoryMock.Create<ICalculatorSettingsService>();
        _mockCalculationServiceStrategy = _repositoryMock.Create<ICalculationServiceStrategy>();

        _calculateResultBuilder = new CalculateResultBuilder();
        _percentageCalculationService = new PercentageValueCalculationService(_calculateResultBuilder);
        _amountCalculationService = new AmountValueCalculationService(_calculateResultBuilder);

        _service = new FlatRateCalculator(
            _mockCalculatorSettingsService.Object,
            _mockCalculationServiceStrategy.Object);
    }

    [TestCase(-1, 0)]
    [TestCase(999999, 174999.825)]
    [TestCase(1000, 175)]
    [TestCase(5, 0.875)]
    public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
    {
        // Arrange
        var setting = new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Percentage).WithRate(17.5M).WithFrom(0).WithTo(null).Build();

        _mockCalculatorSettingsService
           .Setup(service => service.GetSettingAsync(_calculatorType, income))
           .ReturnsAsync(setting);

        _mockCalculationServiceStrategy
            .Setup(service => service.GetCalculationService(RateType.Percentage))
            .Returns(_percentageCalculationService);

        _mockCalculationServiceStrategy
            .Setup(service => service.GetCalculationService(RateType.Amount))
            .Returns(_amountCalculationService);

        // Act
        var result = await _service.CalculateAsync(income);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Tax, Is.EqualTo(expectedTax));
    }
}
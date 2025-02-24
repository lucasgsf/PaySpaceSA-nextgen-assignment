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
internal sealed class ProgressiveCalculatorTests
{
    private MockRepository _repositoryMock;
    private Mock<ICalculatorSettingsService> _mockCalculatorSettingsService;
    private Mock<ICalculationServiceStrategy> _mockCalculationServiceStrategy;
    
    private CalculatorType _calculatorType = CalculatorType.Progressive;
    private ProgressiveCalculator _service;

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

        _service = new ProgressiveCalculator(
            _mockCalculatorSettingsService.Object,
            _mockCalculationServiceStrategy.Object,
            _calculateResultBuilder);
    }

    [TestCase(-1, 0)]
    [TestCase(50, 5)]
    [TestCase(8350, 835)]
    [TestCase(8351, 835.15)]
    [TestCase(33951, 4675.25)]
    [TestCase(82251, 16750.28)]
    [TestCase(171550, 41754)]
    [TestCase(999999, 327683.15)]
    public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
    {
        // Arrange
        var lstSettings = new List<CalculatorSetting>
        {
            new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Percentage).WithRate(10).WithFrom(0).WithTo(8350).Build(),
            new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Percentage).WithRate(15).WithFrom(8350).WithTo(33950).Build(),
            new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Percentage).WithRate(25).WithFrom(33950).WithTo(82250).Build(),
            new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Percentage).WithRate(28).WithFrom(82250).WithTo(171550).Build(),
            new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Percentage).WithRate(33).WithFrom(171550).WithTo(372950).Build(),
            new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Percentage).WithRate(35).WithFrom(372950).WithTo(null).Build(),
        };

        _mockCalculatorSettingsService
            .Setup(service => service.GetSettingsAsync(_calculatorType))
            .ReturnsAsync(lstSettings);

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
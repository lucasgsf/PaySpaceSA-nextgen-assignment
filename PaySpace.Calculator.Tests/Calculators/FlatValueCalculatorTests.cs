using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Strategies;
using PaySpace.Calculator.Services.Builders;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Services;
using PaySpace.Calculator.Tests.Builders;
using PaySpace.Calculator.Tests.Helpers;

namespace PaySpace.Calculator.Tests.Calculators;

[TestFixture]
internal sealed class FlatValueCalculatorTests
{
    private MockRepository _repositoryMock;
    private Mock<ICalculationServiceStrategy> _mockCalculationServiceStrategy;
    private Mock<ICalculatorSettingsRepository> _mockCalculatorSettingsRepository;

    private CalculatorType _calculatorType = CalculatorType.FlatValue;
    private FlatValueCalculator _service;

    private CalculatorSettingsService _calculatorSettingsService;
    private PercentageValueCalculationService _percentageCalculationService;
    private AmountValueCalculationService _amountCalculationService;
    private CalculateResultBuilder _calculateResultBuilder;
    private FakeMemoryCache _mockMemoryCache;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new MockRepository(MockBehavior.Default);
        _mockCalculationServiceStrategy = _repositoryMock.Create<ICalculationServiceStrategy>();
        _mockCalculatorSettingsRepository = _repositoryMock.Create<ICalculatorSettingsRepository>();

        _calculateResultBuilder = new CalculateResultBuilder();
        _percentageCalculationService = new PercentageValueCalculationService(_calculateResultBuilder);
        _amountCalculationService = new AmountValueCalculationService(_calculateResultBuilder);
        _mockMemoryCache = new FakeMemoryCache();
        _calculatorSettingsService = new CalculatorSettingsService(_mockCalculatorSettingsRepository.Object, _mockMemoryCache);

        _service = new FlatValueCalculator(
            _calculatorSettingsService,
            _mockCalculationServiceStrategy.Object);
    }

    [TestCase(-1, 0)]
    [TestCase(199999, 9999.95)]
    [TestCase(100, 5)]
    [TestCase(200000, 10000)]
    [TestCase(6000000, 10000)]
    public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
    {
        // Arrange
        var lstSettings = new List<CalculatorSetting>
        {
            new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Percentage).WithRate(5).WithFrom(0).WithTo(199999).Build(),
            new CalculatorSettingBuilder().WithCalculator(_calculatorType).WithRateType(RateType.Amount).WithRate(10000).WithFrom(200000).WithTo(null).Build(),
        };

        _mockCalculatorSettingsRepository
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
using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Strategies;

namespace PaySpace.Calculator.Tests.Strategies;

[TestFixture]
internal sealed class CalculationServiceStrategyTest
{
    private MockRepository _repositoryMock;
    
    private Mock<ICalculationService> _mockPercentageCalculationService;
    private Mock<ICalculationService> _mockAmountCalculationService;

    private CalculationServiceStrategy _service;

    private List<ICalculationService> _services;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new MockRepository(MockBehavior.Default);

        _mockPercentageCalculationService = _repositoryMock.Create<ICalculationService>();
        _mockAmountCalculationService = _repositoryMock.Create<ICalculationService>();

        _mockPercentageCalculationService.Setup(c => c.RateType).Returns(RateType.Percentage);
        _mockAmountCalculationService.Setup(c => c.RateType).Returns(RateType.Amount);

        _services =
        [
            _mockPercentageCalculationService.Object,
            _mockAmountCalculationService.Object
        ];

        _service = new CalculationServiceStrategy(_services);
    }

    [TestCase(RateType.Percentage)]
    [TestCase(RateType.Amount)]
    public async Task Verify_Get_CalculationService(RateType rateType)
    {
        // Act
        var result = _service.GetCalculationService(rateType);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.RateType, Is.EqualTo(rateType));
    }
}
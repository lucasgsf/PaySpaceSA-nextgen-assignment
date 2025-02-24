using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Borders.Constants;
using PaySpace.Calculator.Borders.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Calculators;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.Abstractions.Strategies;
using PaySpace.Calculator.Services.Builders;
using PaySpace.Calculator.Services.UseCases;
using PaySpace.Calculator.Tests.Builders;
using PaySpace.Calculator.Tests.Utils;
using System.Net;

namespace PaySpace.Calculator.Tests.UseCases;

[TestFixture]
internal sealed class CalculateTaxUseCaseTest
{
    private MockRepository _repositoryMock;
    private IMapper _mapper;
    private Mock<IValidator<CalculateRequest>> _mockValidator;
    private Mock<IPostalCodeService> _mockPostalCodeService;
    private Mock<IHistoryService> _mockHistoryService;
    private Mock<ITaxCalculatorStrategy> _mockCalculatorStrategy;
    private Mock<ITaxCalculator> _mockProgressiveCalculator;

    private CalculateTaxUseCase _service;

    [SetUp]
    public void Setup()
    {
        _mapper = MapperSingleton.Instance();

        _repositoryMock = new MockRepository(MockBehavior.Default);
        _mockValidator = _repositoryMock.Create<IValidator<CalculateRequest>>();
        _mockPostalCodeService = _repositoryMock.Create<IPostalCodeService>();
        _mockHistoryService = _repositoryMock.Create<IHistoryService>();
        _mockCalculatorStrategy = _repositoryMock.Create<ITaxCalculatorStrategy>();
        _mockProgressiveCalculator = _repositoryMock.Create<ITaxCalculator>();

        _mockProgressiveCalculator.Setup(c => c.CalculatorType).Returns(CalculatorType.Progressive);

        _service = new CalculateTaxUseCase(
            _mapper, 
            _mockValidator.Object, 
            _mockPostalCodeService.Object, 
            _mockHistoryService.Object, 
            _mockCalculatorStrategy.Object);
    }

    [TestCase]
    public async Task Verify_CalculateTax_Success()
    {
        // Arrange
        var request = new CalculateRequestBuilder().Build();
        var resultService = new CalculateResultBuilder().Build();

        _mockValidator
            .Setup(validator => validator.ValidateAsync(request, default))
            .ReturnsAsync(new ValidationResult());

        _mockPostalCodeService
            .Setup(service => service.GetCalculatorTypeAsync(request.PostalCode))
            .ReturnsAsync(CalculatorType.FlatValue);

        _mockCalculatorStrategy
            .Setup(service => service.GetCalculator(CalculatorType.FlatValue))
            .Returns(_mockProgressiveCalculator.Object);

        _mockProgressiveCalculator
            .Setup(service => service.CalculateAsync(request.Income))
            .ReturnsAsync(resultService);

        _mockHistoryService
            .Setup(service => service.AddAsync(_mapper.Map<CalculatorHistory>(resultService)));

        // Act
        var result = await _service.Execute(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.Not.Null);
        Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.Result.Tax, Is.EqualTo(resultService.Tax));
        Assert.That(result.Result.Calculator, Is.EqualTo(resultService.Calculator.ToString()));
    }

    [TestCase]
    public async Task Verify_Tax_Calculator_PostalCodeNotFound_NotFound()
    {
        // Arrange
        var request = new CalculateRequestBuilder().Build();

        _mockValidator
            .Setup(validator => validator.ValidateAsync(request, default))
            .ReturnsAsync(new ValidationResult());

        _mockPostalCodeService
            .Setup(service => service.GetCalculatorTypeAsync(request.PostalCode))
            .ReturnsAsync(() => null);

        // Act
        var result = await _service.Execute(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Status, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(result.Errors, Is.Not.Null);
        Assert.That(result.Errors.Count, Is.AtLeast(1));
        Assert.That(result.Errors.Any(_ => _.Message == ErrorMessages.InvalidPostalCode), Is.True);
    }
}
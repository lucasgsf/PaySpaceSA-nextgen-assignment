using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Services;
using PaySpace.Calculator.Tests.Builders;
using PaySpace.Calculator.Tests.Helpers;

namespace PaySpace.Calculator.Tests.Service;

[TestFixture]
internal sealed class PostalCodeServiceTest
{
    private MockRepository _repositoryMock;
    private Mock<IPostalCodeRepository> _repository;

    private PostalCodeService _service;

    private FakeMemoryCache _mockMemoryCache;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new MockRepository(MockBehavior.Default);
        _repository = _repositoryMock.Create<IPostalCodeRepository>();
        _mockMemoryCache = new FakeMemoryCache();

        _service = new PostalCodeService(_repository.Object, _mockMemoryCache);
    }

    [TestCase]
    public async Task Verify_Get_PostalCode()
    {
        // Arrange
        var lstHistory = new List<PostalCode> {
            new PostalCodeBuilder().Build(),
            new PostalCodeBuilder().Build(),
        };

        _repository
           .Setup(service => service.GetPostalCodesAsync())
           .ReturnsAsync(lstHistory);

        // Act
        var result = await _service.GetPostalCodesAsync();

        Assert.IsNotNull(result);
    }

    [TestCase("7441", CalculatorType.Progressive)]
    [TestCase("1000", CalculatorType.Progressive)]
    [TestCase("A100", CalculatorType.FlatValue)]
    [TestCase("7000", CalculatorType.FlatRate)]
    public async Task Verify_Get_Calculator(string postalCode, CalculatorType calculatorType)
    {
        // Arrange
        var lstHistory = new List<PostalCode> {
            new PostalCodeBuilder().WithCalculator(CalculatorType.Progressive).WithCode("7441").Build(),
            new PostalCodeBuilder().WithCalculator(CalculatorType.Progressive).WithCode("1000").Build(),
            new PostalCodeBuilder().WithCalculator(CalculatorType.FlatValue).WithCode("A100").Build(),
            new PostalCodeBuilder().WithCalculator(CalculatorType.FlatRate).WithCode("7000").Build(),
        };

        _repository
           .Setup(service => service.GetPostalCodesAsync())
           .ReturnsAsync(lstHistory);

        // Act
        var result = await _service.GetCalculatorTypeAsync(postalCode);

        Assert.IsNotNull(result);
        Assert.That(result, Is.EqualTo(calculatorType));
    }
}
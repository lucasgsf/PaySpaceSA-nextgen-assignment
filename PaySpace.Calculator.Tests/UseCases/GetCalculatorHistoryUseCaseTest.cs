using MapsterMapper;
using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions.Services;
using PaySpace.Calculator.Services.UseCases;
using PaySpace.Calculator.Tests.Builders;
using PaySpace.Calculator.Tests.Utils;
using System.Net;

namespace PaySpace.Calculator.Tests.UseCases;

[TestFixture]
internal sealed class GetCalculatorHistoryUseCaseTest
{
    private MockRepository _repositoryMock;
    private IMapper _mapper;
    private Mock<IHistoryService> _mockHistoryService;

    private GetCalculatorHistoryUseCase _service;

    [SetUp]
    public void Setup()
    {
        _mapper = MapperSingleton.Instance();

        _repositoryMock = new MockRepository(MockBehavior.Default);
        _mockHistoryService = _repositoryMock.Create<IHistoryService>();

        _service = new GetCalculatorHistoryUseCase(
            _mapper,
            _mockHistoryService.Object);
    }

    [TestCase]
    public async Task Verify_Get_TaxHistory_Success()
    {
        // Arrange
        var resultService = new List<CalculatorHistory>() {
            new CalculatorHistoryBuilder().Build(),
            new CalculatorHistoryBuilder().Build(),
            new CalculatorHistoryBuilder().Build(),
        };

        _mockHistoryService
            .Setup(service => service.GetHistoryAsync())
            .ReturnsAsync(resultService);

        // Act
        var result = await _service.Execute();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Result, Is.Not.Null);
        Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.Result.Count, Is.EqualTo(resultService.Count));
    }
}
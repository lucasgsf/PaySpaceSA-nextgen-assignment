using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Abstractions;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Services;
using PaySpace.Calculator.Tests.Builders;

namespace PaySpace.Calculator.Tests.Service;

[TestFixture]
internal sealed class HistoryServiceTest
{
    private MockRepository _repositoryMock;
    private Mock<IHistoryRepository> _repository;

    private HistoryService _service;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new MockRepository(MockBehavior.Default);
        _repository = _repositoryMock.Create<IHistoryRepository>();

        _service = new HistoryService(_repository.Object);
    }

    [TestCase]
    public async Task Verify_Add_History()
    {
        // Arrange
        var history = new CalculatorHistoryBuilder().Build();

        _repository
           .Setup(service => service.AddAsync(It.IsAny<CalculatorHistory>()));

        // Act
        await _service.AddAsync(history);
    }

    [TestCase]
    public async Task Verify_Get_History()
    {
        // Arrange
        var lstHistory = new List<CalculatorHistory> {
            new CalculatorHistoryBuilder().Build(),
            new CalculatorHistoryBuilder().Build(),
            new CalculatorHistoryBuilder().Build(),
        };

        _repository
           .Setup(service => service.GetHistoryAsync())
           .ReturnsAsync(lstHistory);

        // Act
        var result = await _service.GetHistoryAsync();

        Assert.IsNotNull(result);
    }
}
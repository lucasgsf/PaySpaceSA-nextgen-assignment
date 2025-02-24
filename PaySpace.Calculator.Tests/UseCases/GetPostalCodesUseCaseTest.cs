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
internal sealed class GetPostalCodesUseCaseTest
{
    private MockRepository _repositoryMock;
    private IMapper _mapper;
    private Mock<IPostalCodeService> _mockPostalCodeService;

    private GetPostalCodesUseCase _service;

    [SetUp]
    public void Setup()
    {
        _mapper = MapperSingleton.Instance();

        _repositoryMock = new MockRepository(MockBehavior.Default);
        _mockPostalCodeService = _repositoryMock.Create<IPostalCodeService>();

        _service = new GetPostalCodesUseCase(
            _mapper,
            _mockPostalCodeService.Object);
    }

    [TestCase]
    public async Task Verify_Get_PostalCode_Success()
    {
        // Arrange
        var resultService = new List<PostalCode>() {
            new PostalCodeBuilder().Build(),
            new PostalCodeBuilder().Build(),
            new PostalCodeBuilder().Build(),
        };

        _mockPostalCodeService
            .Setup(service => service.GetPostalCodesAsync())
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
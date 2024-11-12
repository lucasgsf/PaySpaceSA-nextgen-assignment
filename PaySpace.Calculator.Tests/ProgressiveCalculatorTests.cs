using NUnit.Framework;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class ProgressiveCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(-1, 0)]
        [TestCase(50, 5)]
        [TestCase(8350, 835)]
        [TestCase(8351, 835.15)]
        [TestCase(33951, 4675.25)]
        [TestCase(82251, 16750.28)]
        [TestCase(171550, 41734)]
        [TestCase(999999, 327683.15)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange

            // Act

            // Assert
        }
    }
}


namespace ProbTool.Tests;

public class ProbCalcServiceTests
{
  [Fact]
  public void ProbCalcCombinedService_Calculate_ReturnsProductRounded()
  {
    // Arrange
    var service = new ProbCalcCombinedService();
    decimal prob1 = 0.5m;
    decimal prob2 = 0.4m;

    // Act
    var result = service.Calculate(prob1, prob2);

    // Assert
    Assert.Equal(0.2000m, result);
  }

  [Fact]
  public void ProbCalcEitherService_Calculate_ReturnsEitherRounded()
  {
    // Arrange
    var service = new ProbCalcEitherService();
    decimal prob1 = 0.5m;
    decimal prob2 = 0.4m;

    // Act
    var result = service.Calculate(prob1, prob2);

    // Assert
    result.ShouldBe(0.7m);
  }

  [Theory]
  [InlineData(1.0, 1.0, 1.0000)]
  [InlineData(0.5, 0.5, 0.25)]
  [InlineData(0.25, 0.75, 0.1875)]
  public void ProbCalcCombinedService_Calculate_VariousInputs(decimal prob1, decimal prob2, decimal expected)
  {
    var service = new ProbCalcCombinedService();
    var result = service.Calculate(prob1, prob2);
    result.ShouldBe(decimal.Round(expected, 4));
  }

  [Theory]
  [InlineData(1.0, 1.0, 1.0000)]
  [InlineData(0.5, 0.5, 0.75)]
  [InlineData(0.25, 0.75, 0.8125)]
  public void ProbCalcEitherService_Calculate_VariousInputs(decimal prob1, decimal prob2, decimal expected)
  {
    var service = new ProbCalcEitherService();
    var result = service.Calculate(prob1, prob2);
    result.ShouldBe(decimal.Round(expected, 4));
  }
}
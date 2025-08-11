namespace ProbTool.Tests;

public class ProbCalcServiceTests
{
  [Theory]
  [InlineData(1.0, 1.0, 1.0)]
  [InlineData(0.5, 0.5, 0.25)]
  [InlineData(0.25, 0.75, 0.1875)]
  public void ProbCalcCombinedService_Calculate_returns_expected(decimal prob1, decimal prob2, decimal expected)
  {
    var service = new ProbCalcCombinedService();
    var result = service.Calculate(prob1, prob2);
    result.ShouldBe(decimal.Round(expected, 4));
  }

  [Theory]
  [InlineData(0, 1.0)]
  [InlineData(1.0, 0)]
  [InlineData(0.5, 2.0)]
  [InlineData(2.0, 0.5)]
  [InlineData(-0.25, 0.75)]
  [InlineData(0.25, -0.75)]
  public void ProbCalcCombinedService_Calculate_throws_exception_when_arguments_invalid(decimal prob1, decimal prob2)
  {
    var service = new ProbCalcCombinedService();
    Should.Throw<ArgumentException>(() => service.Calculate(prob1, prob2));
  }

  [Theory]
  [InlineData(1.0, 1.0, 1.0000)]
  [InlineData(0.5, 0.5, 0.75)]
  [InlineData(0.25, 0.75, 0.8125)]
  public void ProbCalcEitherService_Calculate_returns_expected(decimal prob1, decimal prob2, decimal expected)
  {
    var service = new ProbCalcEitherService();
    var result = service.Calculate(prob1, prob2);
    result.ShouldBe(decimal.Round(expected, 4));
  }

  [Theory]
  [InlineData(0, 1.0)]
  [InlineData(1.0, 0)]
  [InlineData(0.5, 2.0)]
  [InlineData(2.0, 0.5)]
  [InlineData(-0.25, 0.75)]
  [InlineData(0.25, -0.75)]
  public void ProbCalcEitherService_Calculate_throws_exception_when_arguments_invalid(decimal prob1, decimal prob2)
  {
    var service = new ProbCalcEitherService();
    Should.Throw<ArgumentException>(() => service.Calculate(prob1, prob2));
  }
}
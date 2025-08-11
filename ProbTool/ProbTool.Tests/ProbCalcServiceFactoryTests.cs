namespace ProbTool.Tests;

public class ProbCalcServiceFactoryTests
{
  [Fact]
  public void CreateProbCalcService_Combined_returns_ProbCalcCombinedService()
  {
    // Arrange
    var factory = new ProbCalcServiceFactory();

    // Act
    var service = factory.CreateProbCalcService(ProbFunType.Combined);

    // Assert
    service.ShouldBeOfType<ProbCalcCombinedService>();
  }

  [Fact]
  public void CreateProbCalcService_Either_returns_ProbCalcEitherService()
  {
    // Arrange
    var factory = new ProbCalcServiceFactory();

    // Act
    var service = factory.CreateProbCalcService(ProbFunType.Either);

    // Assert
    service.ShouldBeOfType<ProbCalcEitherService>();
  }

  [Fact]
  public void CreateProbCalcService_with_invalid_type_throws_exception()
  {
    // Arrange
    var factory = new ProbCalcServiceFactory();
    var invalidType = (ProbFunType)999;

    // Act & Assert
    Should
      .Throw<Exception>(() =>
        factory.CreateProbCalcService(invalidType));
  }
}

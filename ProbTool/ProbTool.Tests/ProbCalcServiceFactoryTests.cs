namespace ProbTool.Tests;

public class ProbCalcServiceFactoryTests
{
  [Fact]
  public void CreateProbCalcService_WithCombined_ReturnsProbCalcCombinedService()
  {
    // Arrange
    var factory = new ProbCalcServiceFactory();

    // Act
    var service = factory.CreateProbCalcService(ProbFunType.Combined);

    // Assert
    service.ShouldBeOfType<ProbCalcCombinedService>();
  }

  [Fact]
  public void CreateProbCalcService_WithEither_ReturnsProbCalcEitherService()
  {
    // Arrange
    var factory = new ProbCalcServiceFactory();

    // Act
    var service = factory.CreateProbCalcService(ProbFunType.Either);

    // Assert
    service.ShouldBeOfType<ProbCalcEitherService>();
  }

  [Fact]
  public void CreateProbCalcService_WithInvalidType_ThrowsException()
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

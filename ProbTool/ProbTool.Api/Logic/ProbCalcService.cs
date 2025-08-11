namespace ProbTool.Api;

public interface IProbCalcService
{
  public const int DECIMAL_ROUNDING_DP = 4;

  decimal Calculate(decimal prob1, decimal prob2);
}

public class ProbCalcCombinedService : IProbCalcService
{
  public decimal Calculate(decimal prob1, decimal prob2)
  {
    return decimal.Round(
      prob1 * prob2,
      IProbCalcService.DECIMAL_ROUNDING_DP);
  }
}

public class ProbCalcEitherService : IProbCalcService
{
  public decimal Calculate(decimal prob1, decimal prob2)
  {
    return decimal
      .Round(
        (prob1 + prob2) - (prob1 * prob2),
        IProbCalcService.DECIMAL_ROUNDING_DP);
  }
}

public interface IProbCalcServiceFactory
{
  IProbCalcService CreateProbCalcService(ProbFunType probFunType);
}

public class ProbCalcServiceFactory : IProbCalcServiceFactory
{
  public IProbCalcService CreateProbCalcService(ProbFunType probFunType) =>
    probFunType switch
    {
      ProbFunType.Combined => new ProbCalcCombinedService(),
      ProbFunType.Either => new ProbCalcEitherService(),
      _ => throw new Exception($"Can't create service for Product Function {probFunType}")
    };
}
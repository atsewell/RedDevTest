using System.Text.Json.Serialization;

namespace ProbTool.Api;

public enum ProbFunType
{
  Combined = 1,
  Either = 2,
}

public record ProbRequest(string ProbFun, decimal prob1, decimal prob2)
{
  [JsonIgnore]
  public ProbFunType ProbFunType => ParseProbFun();
  [JsonIgnore]
  public bool ProbFunIsValid => CanParseProbFun();

  private ProbFunType ParseProbFun()
  {
    if (Enum.TryParse<ProbFunType>(ProbFun, out var funType))
    {
      return funType;
    }

    throw new InvalidOperationException(
      $"Cannot use function type {ProbFun}");
  }

  private bool CanParseProbFun() =>
    Enum.TryParse<ProbFunType>(ProbFun, out _);
}

public record ProbResponse(decimal result);

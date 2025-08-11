namespace ProbTool.Api;

public class ProbRequestValidator : AbstractValidator<ProbRequest>
{
  public ProbRequestValidator()
  {
    RuleFor(r => r.ProbFun).NotNull();
    RuleFor(r => r)
      .Must(r => r.ProbFun is null || // This avoids 2 messages for a null
        r.ProbFunIsValid)
      .WithMessage(
        $"The Probability Function name should be in the list; {ProbFunValues()}");
    RuleFor(r => r.prob1).GreaterThan(0);
    RuleFor(r => r.prob1).LessThanOrEqualTo(1);
    RuleFor(r => r.prob2).GreaterThan(0);
    RuleFor(r => r.prob2).LessThanOrEqualTo(1);
  }

  private string ProbFunValues() =>
    string.Join(
      ", ",
      Enum
        .GetValues<ProbFunType>()
        .Select(v => v.ToString()).ToList());
}
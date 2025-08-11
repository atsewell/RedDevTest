using Serilog;

const string devOriginPolicy = "Allow localhost";

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
  configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: devOriginPolicy,
    policy =>
    {
      policy
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProbCalcServiceFactory, ProbCalcServiceFactory>();
builder.Services.AddScoped<IValidator<ProbRequest>, ProbRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseCors(devOriginPolicy);
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calc", async (
  ProbRequest request,
  IValidator<ProbRequest> validator,
  IProbCalcServiceFactory factory,
  ILogger<Program> logger) =>
{
  var validationResult = await validator.ValidateAsync(request);
  if (!validationResult.IsValid)
  {
    logger.LogWarning("Request failed validation: ProbFunType: {probFunType}, prob1: {prob1}, prob2: {prob2}",
      request.ProbFun,
      request.prob1,
      request.prob2);
    return Results
      .ValidationProblem(validationResult.ToDictionary());
  }

  var probCalcService = factory
    .CreateProbCalcService(request.ProbFunType);

  var res = probCalcService
    .Calculate(request.prob1, request.prob2);

  logger.LogInformation("Request succeeded: ProbFunType: {probFunType}, prob1: {prob1}, prob2: {prob2}, result: {res}",
    request.ProbFun,
    request.prob1,
    request.prob2,
    res);

  return Results.Ok(new ProbResponse(res));
})
.WithName("CalculateCombinedProbability")
.WithOpenApi();

app.Run();
public partial class Program { }
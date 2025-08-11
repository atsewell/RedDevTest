using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ProbTool.Tests;

public class ApiEndpointTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory = factory;

  [Theory]
  [InlineData("Combined", 0.5, 0.4, 0.2000)]
  [InlineData("Either", 0.5, 0.4, 0.7000)]
  public async Task Calc_endpoint_returns_expected_result(string probFun, decimal prob1, decimal prob2, decimal expected)
  {
    var client = _factory.CreateClient();
    var request = new ProbRequest(probFun, prob1, prob2);

    var response = await client.PostAsJsonAsync("/calc", request);

    response.EnsureSuccessStatusCode();
    var result = await response.Content.ReadFromJsonAsync<ProbResponse>();

    result.ShouldNotBeNull();
    result.result.ShouldBe(expected);
  }

  [Theory]
  [InlineData(null, 0.5, 0.4)]
  [InlineData("InvalidFun", 0.5, 0.4)]
  [InlineData("Combined", -1, 0.4)]
  [InlineData("Combined", 0.5, 2)]
  public async Task Calc_endpoint_returns_ValidationProblem_when_request_is_invalid(string probFun, decimal prob1, decimal prob2)
  {
    var client = _factory.CreateClient();
    var request = new ProbRequest(probFun, prob1, prob2);

    var response = await client.PostAsJsonAsync("/calc", request);

    response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    problem.ShouldNotBeNull();
    problem.Errors.ShouldNotBeEmpty();
  }
}
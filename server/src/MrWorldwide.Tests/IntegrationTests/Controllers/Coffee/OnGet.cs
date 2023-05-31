using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MrWorldwide.Tests.Utilities;
using MrWorldwide.WebApi.Infrastructure.WebApi.Common;
using MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;
using MrWorldwide.WebApi.Infrastructure.WebApi.Exceptions;
using Shouldly;

namespace MrWorldwide.Tests.IntegrationTests.Controllers.Coffee;

[TestFixture]
public class OnGet : HttpIntegrationTest
{
    [Test]
    public async Task ProducesProblem()
    {
        var response = await Client.GetAsync("coffee");
        await response.ShouldProduceProblemAsync(details =>
        {
            details.Status.ShouldBe(StatusCodes.Status418ImATeapot);
            details.Type.ShouldBe("https://tools.ietf.org/html/rfc2324#section-2.3.2");
            details.Title.ShouldBe("ImATeapot");
            details.Detail.ShouldBe(new ServerIsTeapotException().Message);
        });
    }

    [Test]
    public async Task RepliesWithVerbosityHeader_False_If_Not_Provided()
    {
        var response = await Client.GetAsync("coffee");
        response.ShouldSatisfyAllConditions(
            x => x.Headers.Contains(ExceptionHandlingDefaults.VerboseExceptionHeader).ShouldBeTrue(),
            x => x.Headers.GetValues(ExceptionHandlingDefaults.VerboseExceptionHeader).ShouldHaveSingleItem(),
            x => x.Headers.GetValues(ExceptionHandlingDefaults.VerboseExceptionHeader).Single()
                .ShouldBe(HttpConstants.False));
    }

    [TestCase(HttpConstants.False)]
    [TestCase(HttpConstants.True)]
    public async Task RepliesWithVerbosityHeader_That_Matches_Request(string getVerboseErrors)
    {
        var response =
            await Client.WithDefaultHeader(ExceptionHandlingDefaults.VerboseExceptionHeader, getVerboseErrors)
                .GetAsync("coffee");
        response.ShouldSatisfyAllConditions(
            x => x.Headers.Contains(ExceptionHandlingDefaults.VerboseExceptionHeader).ShouldBeTrue(),
            x => x.Headers.GetValues(ExceptionHandlingDefaults.VerboseExceptionHeader).ShouldHaveSingleItem(),
            x => x.Headers.GetValues(ExceptionHandlingDefaults.VerboseExceptionHeader).Single()
                .ShouldBe(getVerboseErrors));
    }

    [Test]
    public async Task ProvidesExceptionDetails_WithRequest()
    {
        var response = await Client.WithExceptionDetails().GetAsync("coffee");
        await response.ShouldProduceProblemAsync(details =>
        {
            details.TryGetErrorDetails(out var errorDetails).ShouldBeTrue();
            errorDetails.Name.ShouldBe(nameof(ServerIsTeapotException));
            errorDetails.Message.ShouldBe(new ServerIsTeapotException().Message);
        });
    }
    
    [Test]
    public async Task DoesNotProvideErrorDetails_WithoutRequest()
    {
        var response = await Client.WithoutExceptionDetails().GetAsync("coffee");
        await response.ShouldProduceProblemAsync(details =>
        {
            details.TryGetErrorDetails(out _).ShouldBeFalse();
        });
    }
}
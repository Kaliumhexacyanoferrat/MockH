using GenHTTP.Api.Protocol;
using System.Net;

namespace MockH.Tests;

[TestClass]
public class RespondTests : ServerTest
{

    [TestMethod]
    public async Task BasicResponse()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get().Respond(ResponseStatus.InternalServerError)
        );

        using var response = await GetAsync(server);

        Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
    }

}
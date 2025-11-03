using GenHTTP.Api.Protocol;
using GenHTTP.Modules.DirectoryBrowsing;
using GenHTTP.Modules.IO;
using System.Net;

namespace MockH.Tests;

[TestClass]
public class RunTests : ServerTest
{

    [TestMethod]
    public async Task TestConstant()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get().Run(() => 42)
        );

        using var response = await GetAsync(server);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("42",  await response.Content.ReadAsStringAsync());
    }

    private record MyClass(int IntValue, string StringValue);

    [TestMethod]
    public async Task TestJson()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get().Run(() => new MyClass(42, "The answer"))
        );

        using var response = await GetAsync(server);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("{\"intValue\":42,\"stringValue\":\"The answer\"}", await response.Content.ReadAsStringAsync());
    }

    [TestMethod]
    public async Task TestQuery()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get().Run((int i) => i + 1)
        );

        using var response = await GetAsync(server, "/?i=1");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("2", await response.Content.ReadAsStringAsync());
    }

    [TestMethod]
    public async Task TestPath()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get("/increment/:i").Run((int i) => i + 1)
        );

        using var response = await GetAsync(server, "/increment/1");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("2", await response.Content.ReadAsStringAsync());
    }

    [TestMethod]
    public async Task TestPost()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Post().Run((MyClass body) => body)
        );

        using var response = await PostAsync(server, "{\"intValue\":42,\"stringValue\":\"The answer\"}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("{\"intValue\":42,\"stringValue\":\"The answer\"}", await response.Content.ReadAsStringAsync());
    }

    [TestMethod]
    public async Task TestStream()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Post().Run((Stream body) => body.Length)
        );

        using var response = await PostAsync(server, "{\"intValue\":42,\"stringValue\":\"The answer\"}");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual("42" /* :D */, await response.Content.ReadAsStringAsync());
    }

    [TestMethod]
    public async Task TestRequest()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get().Run((IRequest request) => request.Respond().Status(ResponseStatus.BadRequest))
        );

        using var response = await GetAsync(server);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    public async Task TestHandler()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get().Run(() => Listing.From(ResourceTree.FromDirectory("./")))
        );

        using var response = await GetAsync(server);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

}
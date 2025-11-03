namespace MockH.Tests;

[TestClass]
public class ServerTests : ServerTest
{

    [TestMethod]
    public async Task TestRelativeUrl()
    {
        await using var server = await MockServer.RunAsync();

        var url = server.Url("/api/users");

        Assert.StartsWith("http://localhost:", url);
        Assert.EndsWith("/api/users", url);
    }

    [TestMethod]
    public async Task TestRelativeUrlWithoutSlash()
    {
        await using var server = await MockServer.RunAsync();

        var url = server.Url("api/users");

        Assert.StartsWith("http://localhost:", url);
        Assert.EndsWith("/api/users", url);
    }

    [TestMethod]
    public async Task TestAbsoluteUrl()
    {
        await using var server = await MockServer.RunAsync();

        var url = server.Url("https://google.de");

        Assert.AreEqual("https://google.de", url);
    }

}

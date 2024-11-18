using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MockH.Tests;

[TestClass]
public class ServerTests : ServerTest
{

    [TestMethod]
    public async Task TestRelativeUrl()
    {
        await using var server = await MockServer.RunAsync();

        var url = server.Url("/api/users");

        Assert.IsTrue(url.StartsWith("http://localhost:"));
        Assert.IsTrue(url.EndsWith("/api/users"));
    }

    [TestMethod]
    public async Task TestRelativeUrlWithoutSlash()
    {
        await using var server = await MockServer.RunAsync();

        var url = server.Url("api/users");

        Assert.IsTrue(url.StartsWith("http://localhost:"));
        Assert.IsTrue(url.EndsWith("/api/users"));
    }

    [TestMethod]
    public async Task TestAbsoluteUrl()
    {
        await using var server = await MockServer.RunAsync();

        var url = server.Url("https://google.de");

        Assert.AreEqual("https://google.de", url);
    }

}
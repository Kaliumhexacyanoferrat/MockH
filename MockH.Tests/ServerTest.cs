using MockH.Environment;

namespace MockH.Tests;

public abstract class ServerTest
{
    protected HttpClient Client = new(new HttpClientHandler()
    {
        AllowAutoRedirect = false
    });

    protected async ValueTask<string> GetStringAsync(Server server, string? path = null) => await Client.GetStringAsync(server.Url(path));

    protected async ValueTask<HttpResponseMessage> GetAsync(Server server, string? path = null) => await Client.GetAsync(server.Url(path));

    protected async ValueTask<HttpResponseMessage> PostAsync(Server server, string value, string? path = null)
    {
        var content = new StringContent(value);

        content.Headers.ContentType = new("application/json");

        return await Client.PostAsync(server.Url(path), content);
    }

    [TestCleanup]
    public void Cleanup()
    {
        Client.Dispose();
    }

}

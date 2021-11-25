using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockH.Environment;

namespace MockH.Tests
{
    
    public abstract class ServerTest
    {
        protected HttpClient _Client = new();

        protected async ValueTask<string> GetStringAsync(Server server, string? path = null) => await _Client.GetStringAsync(server.Url(path));

        [TestCleanup]
        public void Cleanup()
        {
            _Client.Dispose();
        }

    }

}

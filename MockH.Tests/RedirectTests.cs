using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace MockH.Tests
{

    [TestClass]
    public class RedirectTests : ServerTest
    {

        [TestMethod]
        public async Task RulesCanRedirectTemporarily()
        {
            using var server = MockServer.Run
            (
                On.Get().Redirect("https://www.google.de")
            );

            using var response = await GetAsync(server);

            Assert.AreEqual(HttpStatusCode.TemporaryRedirect, response.StatusCode);
            Assert.AreEqual(new Uri("https://www.google.de"), response.Headers.Location);
        }

        [TestMethod]
        public async Task RulesCanRedirectPermanently()
        {
            using var server = MockServer.Run
            (
                On.Get().Redirect("https://www.google.de", temporary: false)
            );

            using var response = await GetAsync(server);

            Assert.AreEqual(HttpStatusCode.Moved, response.StatusCode);
            Assert.AreEqual(new Uri("https://www.google.de"), response.Headers.Location);
        }

    }

}

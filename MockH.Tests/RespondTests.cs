using GenHTTP.Api.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace MockH.Tests
{

    [TestClass]
    public class RespondTests : ServerTest
    {

        [TestMethod]
        public async Task BasicResponse()
        {
            using var server = MockServer.Run
            (
                On.Get().Respond(ResponseStatus.InternalServerError)
            );

            using var response = await GetAsync(server);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

    }

}

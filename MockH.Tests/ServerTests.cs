using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MockH.Tests
{

    [TestClass]
    public class ServerTests : ServerTest
    {

        [TestMethod]
        public void TestRelativeUrl()
        {
            using var server = MockServer.Run();

            var url = server.Url("/api/users");

            Assert.IsTrue(url.StartsWith("http://localhost:"));
            Assert.IsTrue(url.EndsWith("/api/users"));
        }

        [TestMethod]
        public void TestRelativeUrlWithoutSlash()
        {
            using var server = MockServer.Run();

            var url = server.Url("api/users");

            Assert.IsTrue(url.StartsWith("http://localhost:"));
            Assert.IsTrue(url.EndsWith("/api/users"));
        }

        [TestMethod]
        public void TestAbsoluteUrl()
        {
            using var server = MockServer.Run();

            var url = server.Url("https://google.de");

            Assert.AreEqual("https://google.de", url);
        }

    }

}

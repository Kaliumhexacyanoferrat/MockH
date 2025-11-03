namespace MockH.Tests;

[TestClass]
public class ReturnTests : ServerTest
{

        #region Supporting data structures

    public record Data(string Value);

        #endregion

    [TestMethod]
    public async Task RulesCanReturnPrimitiveTypes()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get().Return(42)
        );

        Assert.AreEqual("42", await GetStringAsync(server));
    }

    [TestMethod]
    public async Task RulesCanReturnComplexTypes()
    {
        await using var server = await MockServer.RunAsync
        (
            On.Get().Return(new Data("Hello World"))
        );

        Assert.AreEqual("{\"value\":\"Hello World\"}", await GetStringAsync(server));
    }

}
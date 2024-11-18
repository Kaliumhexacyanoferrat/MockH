using MockH.Builder;
using MockH.Environment;

namespace MockH;

/// <summary>
/// Main entry point to create a new mock server instance.
/// </summary>
public static class MockServer
{

    /// <summary>
    /// Creates and starts a server instance that will handle incoming
    /// HTTP requests as specified by the given ruleset.
    /// </summary>
    /// <param name="rules">The responses to be provided by the server</param>
    /// <returns>The newly created server instance</returns>
    /// <remarks>
    /// Typical rules can be created using the <c cref="On">On</c> class.
    /// </remarks>
    public static ValueTask<Server> RunAsync(params Rule[] rules) => Create(rules).RunAsync();

    /// <summary>
    /// Creates a server builder for the specified ruleset. 
    /// </summary>
    /// <param name="rules">The responses to be provided by the server</param>
    /// <returns>The newly created server instance</returns>
    /// <remarks>
    /// Typical rules can be created using the <c cref="On">On</c> class.
    /// 
    /// This method will not actually launch a server instance, which allows to 
    /// perform additional modifications such as overriding the default port strategy.
    /// </remarks>
    public static ServerBuilder Create(params Rule[] rules) => new ServerBuilder().Add(rules);

}
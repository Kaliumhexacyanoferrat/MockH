using MockH.Builder;

using GenHTTP.Api.Infrastructure;

using GenHTTP.Modules.Functional;
using GenHTTP.Modules.Functional.Provider;

namespace MockH.Environment;

/// <summary>
/// A mock server instance serving incoming HTTP requests by
/// evaluating a given rule set.
/// </summary>
public class Server : IAsyncDisposable
{
    private bool _Disposed;

    #region Get-/Setters

    internal IServerHost Host { get; }

    /// <summary>
    /// The port the HTTP server is listening to.
    /// </summary>
    public ushort Port { get; }

        #endregion

        #region Initialization

    /// <summary>
    /// Creates and starts a new server instance with the given ruleset.
    /// </summary>
    /// <param name="portProvider">The strategy to obtain a free port with</param>
    /// <param name="rules">The ruleset to be evaluated by the server instance</param>
    public Server(IPortProvider portProvider, List<Rule> rules)
    {
        Port = portProvider.GetAvailable();

        Host = GenHTTP.Engine.Internal.Host.Create()
                      .Port(Port)
                      .Handler(SetupHandler(rules));
    }

    private static InlineBuilder SetupHandler(List<Rule> rules)
    {
        var builder = Inline.Create();

        foreach (var rule in rules)
        {
            rule.AddTo(builder);
        }

        return builder;
    }

    #endregion

    #region Functionality

    /// <summary>
    /// Returns a fully qualified URL that can be used in a HTTP
    /// request to fetch the specified, relative path.
    /// </summary>
    /// <param name="path">The requested path, e.g. "/api/users"</param>
    /// <returns>The fully qualified URL to access the specified path</returns>
    public string Url(string? path)
    {
        string actualPath;

        if (path != null)
        {
            if (path.StartsWith("http"))
            {
                return path;
            }

            if (path.StartsWith("/"))
            {
                actualPath = path;
            }
            else
            {
                actualPath = $"/{path}";
            }
        }
        else
        {
            actualPath = "";
        }

        return $"http://localhost:{Port}{actualPath}";
    }

    #endregion

    #region Disposal

    /// <summary>
    /// Releases all resources held by the server instance by stopping
    /// the running server instance.
    /// </summary>
    /// <param name="disposing">true, if managed resources should be disposed</param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_Disposed)
        {
            if (disposing)
            {
                await Host.StopAsync();
            }

            _Disposed = true;
        }
    }

    /// <summary>
    /// Releases all resources held by the server instance by stopping
    /// the running server instance.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion

}
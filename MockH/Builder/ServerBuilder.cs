using MockH.Environment;

namespace MockH.Builder;

/// <summary>
/// Allows to configure and build a new mock server instance.
/// </summary>
public class ServerBuilder
{
    private static readonly IPortProvider DefaultPortProvider = new StaticPortProvider();

    private readonly List<Rule> _rules = new();

    private IPortProvider? _portProvider;

    #region Functionality

    /// <summary>
    /// Adds the given rule to the ruleset to be evaluated on request.
    /// </summary>
    /// <param name="rule">The rule to be added</param>
    /// <returns>The builder instance</returns>
    public ServerBuilder Add(Rule rule)
    {
        _rules.Add(rule);
        return this;
    }

    /// <summary>
    /// Adds the given rules to the ruleset to be evaluated on request.
    /// </summary>
    /// <param name="rules">The rule to be added</param>
    /// <returns>The builder instance</returns>
    public ServerBuilder Add(IEnumerable<Rule> rules)
    {
        _rules.AddRange(rules);
        return this;
    }

    /// <summary>
    /// Sets the strategy used by the server to obtain a free port
    /// to listen on. 
    /// </summary>
    /// <param name="portProvider">The strategy used to determine a free port to listen on</param>
    /// <returns>The builder instance</returns>
    public ServerBuilder Ports(IPortProvider portProvider)
    {
        _portProvider = portProvider;
        return this;
    }

    /// <summary>
    /// Creates and starts the configured mock server instance.
    /// </summary>
    /// <returns>The configured and started mock server instance</returns>
    /// <remarks>
    /// Can be called multiple times if needed.
    /// </remarks>
    public async ValueTask<Server> RunAsync()
    {
        var portProvider = _portProvider ?? DefaultPortProvider;

        var server = new Server(portProvider, _rules);

        await server.Host.StartAsync();

        return server;
    }

    #endregion

}
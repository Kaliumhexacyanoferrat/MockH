using MockH.Environment;

namespace MockH.Builder
{

    /// <summary>
    /// Allows to configure and build a new mock server instance.
    /// </summary>
    public class ServerBuilder
    {
        private static readonly IPortProvider _DefaultPortProvider = new StaticPortProvider();

        private readonly List<Rule> _Rules = new();

        private IPortProvider? _PortProvider;

        #region Functionality

        /// <summary>
        /// Adds the given rule to the ruleset to be evaluated on request.
        /// </summary>
        /// <param name="rule">The rule to be added</param>
        /// <returns>The builder instance</returns>
        public ServerBuilder Add(Rule rule)
        {
            _Rules.Add(rule);
            return this;
        }

        /// <summary>
        /// Adds the given rules to the ruleset to be evaluated on request.
        /// </summary>
        /// <param name="rules">The rule to be added</param>
        /// <returns>The builder instance</returns>
        public ServerBuilder Add(IEnumerable<Rule> rules)
        {
            _Rules.AddRange(rules);
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
            _PortProvider = portProvider;
            return this;
        }

        /// <summary>
        /// Creates and starts the configured mock server instance.
        /// </summary>
        /// <returns>The configured and started mock server instance</returns>
        /// <remarks>
        /// Can be called multiple times if needed.
        /// </remarks>
        public Server Run()
        {
            var portProvider = _PortProvider ?? _DefaultPortProvider;

            return new Server(portProvider, _Rules);
        }

        #endregion

    }

}

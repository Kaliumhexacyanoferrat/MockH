using MockH.Environment;

namespace MockH.Builder
{

    public class ServerBuilder
    {
        private static readonly IPortProvider _DefaultPortProvider = new StaticPortProvider();

        private readonly List<Rule> _Rules = new();

        private IPortProvider? _PortProvider;

        #region Functionality

        public ServerBuilder Add(Rule rule)
        {
            _Rules.Add(rule);
            return this;
        }

        public ServerBuilder Add(IEnumerable<Rule> rules)
        {
            _Rules.AddRange(rules);
            return this;
        }

        public ServerBuilder Ports(IPortProvider portProvider)
        {
            _PortProvider = portProvider;
            return this;
        }

        public Server Run()
        {
            var portProvider = _PortProvider ?? _DefaultPortProvider;

            return new Server(portProvider, _Rules);
        }

        #endregion

    }

}

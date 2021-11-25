using MockH.Builder;

using GenHTTP.Api.Infrastructure;

using GenHTTP.Modules.Functional;
using GenHTTP.Modules.Functional.Provider;

namespace MockH.Environment
{

    public class Server : IDisposable
    {
        private bool _Disposed;

        #region Get-/Setters

        private IServerHost Host { get; }

        public ushort Port { get; }

        #endregion

        #region Initialization

        public Server(IPortProvider portProvider, List<Rule> rules)
        {
            Port = portProvider.GetAvailable();

            Host = GenHTTP.Engine.Host.Create()
                                      .Port(Port)
                                      .Handler(SetupHandler(rules))
                                      .Start();
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

        public string Url(string? path)
        {
            string actualPath;

            if (path != null)
            {
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    Host.Stop();
                }

                _Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }

}

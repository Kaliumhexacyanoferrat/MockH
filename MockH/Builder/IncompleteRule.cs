using GenHTTP.Api.Protocol;

using Basics = GenHTTP.Modules.Basics;

namespace MockH.Builder
{

    public class IncompleteRule
    {

        #region Get-/Setters

        private HashSet<FlexibleRequestMethod> Methods { get; }

        private string? Path { get; }

        #endregion

        #region Initialization

        public IncompleteRule(RequestMethod method, string? path)
            : this(new HashSet<FlexibleRequestMethod>() { new(method) }, path) { }

        public IncompleteRule(HashSet<FlexibleRequestMethod> methods, string? path)
        {
            Methods = methods;
            Path = path;
        }

        #endregion

        #region Functionality

        public Rule Return<T>(T data) => new(Methods, Path, () => data);

        public Rule Redirect(string location, bool temporary = true) => new(Methods, Path, (IRequest request) => Basics.Redirect.To(location).Mode(temporary));

        public Rule Run(Delegate action) => new(Methods, Path, action);

        public Rule Respond(ResponseStatus status) => new(Methods, Path, (IRequest request) => request.Respond().Status(status).Build());

        #endregion

    }

}

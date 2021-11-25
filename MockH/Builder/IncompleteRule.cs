using GenHTTP.Api.Protocol;

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

        #endregion

    }

}

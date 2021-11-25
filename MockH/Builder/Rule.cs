using GenHTTP.Api.Protocol;

using GenHTTP.Modules.Functional.Provider;

namespace MockH.Builder
{

    public class Rule
    {

        #region Get-/Setters

        private HashSet<FlexibleRequestMethod> Methods { get; }

        private string? Path { get; }

        private Delegate Action { get; }

        #endregion

        #region Initialization

        public Rule(HashSet<FlexibleRequestMethod> methods, string? path, Delegate action)
        {
            Methods = methods;
            Path = path;
            Action = action;
        }

        #endregion

        #region Functionality

        public void AddTo(InlineBuilder builder)
        {
            builder.On(Action, Methods, Path);
        }

        #endregion

    }

}

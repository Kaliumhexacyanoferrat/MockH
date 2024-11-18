using GenHTTP.Api.Protocol;

using GenHTTP.Modules.Functional.Provider;

namespace MockH.Builder;

/// <summary>
/// A rule that can be executed by the mock server to determine
/// the response to be sent to a requesting client.
/// </summary>
public class Rule
{

        #region Get-/Setters

    private HashSet<FlexibleRequestMethod> Methods { get; }

    private string? Path { get; }

    private Delegate Action { get; }

        #endregion

        #region Initialization

    internal Rule(HashSet<FlexibleRequestMethod> methods, string? path, Delegate action)
    {
        Methods = methods;
        Path = path;
        Action = action;
    }

        #endregion

        #region Functionality

    internal void AddTo(InlineBuilder builder)
    {
        builder.On(Action, Methods, Path);
    }

        #endregion

}
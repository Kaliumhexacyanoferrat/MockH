using GenHTTP.Api.Protocol;

using Red = GenHTTP.Modules.Redirects;

namespace MockH.Builder;

/// <summary>
/// A builder that can be used to specify the response the server
/// will return for a specified request method and path.
/// </summary>
/// <remarks>
/// Use the public methods provided by the builder to create
/// a rule that can actually be executed by the server.
/// </remarks>
public class IncompleteRule
{

    #region Get-/Setters

    private HashSet<FlexibleRequestMethod> Methods { get; }

    private string? Path { get; }

    #endregion

    #region Initialization

    /// <summary>
    /// Creates a new rule that will match the specified HTTP method
    /// and the given URL path.
    /// </summary>
    /// <param name="method">The method the rule should match</param>
    /// <param name="path">The path the rule should match (e.g. "/api/users")</param>
    public IncompleteRule(RequestMethod method, string? path)
        : this(new HashSet<FlexibleRequestMethod>() { new(method) }, path) { }

    /// <summary>
    /// Creates a new rule that will match the specified HTTP methods
    /// and the given URL path.
    /// </summary>
    /// <param name="methods">The methods the rule should match</param>
    /// <param name="path">The path the rule should match (e.g. "/api/users")</param>
    public IncompleteRule(HashSet<FlexibleRequestMethod> methods, string? path)
    {
        Methods = methods;
        Path = path;
    }

    #endregion

    #region Functionality

    /// <summary>
    /// Responds with the specified payload serialized into JSON or XML,
    /// depending on the "Accept" headers of the request (defaults to JSON).
    /// </summary>
    /// <typeparam name="T">The type of the payload to be returned</typeparam>
    /// <param name="data">The payload to be returned to the client</param>
    /// <returns>A rule to be used with the mock server</returns>
    public Rule Return<T>(T data) => new(Methods, Path, () => data);

    /// <summary>
    /// Responds with a "Location" header and a status code indicating that
    /// the resource has moved. 
    /// </summary>
    /// <param name="location">The absolute URL pointing to the new location of the resource</param>
    /// <param name="temporary">true for HTTP 307, false for HTTP 301</param>
    /// <returns>A rule to be used with the mock server</returns>
    public Rule Redirect(string location, bool temporary = true) => new(Methods, Path, () => Red.Redirect.To(location).Mode(temporary));

    /// <summary>
    /// Executes the given delegate to determine the response to be sent
    /// to the client.
    /// </summary>
    /// <param name="action">The delegate to be invoked for matching requests</param>
    /// <returns>A rule to be used with the mock server</returns>
    /// <remarks>
    /// May access path or query paramters, the body of the request and the entire
    /// request. Allows to return payload to be serialized, exceptions, streams or
    /// an entire response to be sent to the client.
    /// 
    /// For typical examples, see the GitHub documentation.
    /// </remarks>
    public Rule Run(Delegate action) => new(Methods, Path, action);

    /// <summary>
    /// Responds with the specified HTTP status code and no payload.
    /// </summary>
    /// <param name="status">The HTTP status to respond with</param>
    /// <returns>A rule to be used with the mock server</returns>
    public Rule Respond(ResponseStatus status) => new(Methods, Path, (IRequest request) => request.Respond().Status(status).Build());

    #endregion

}
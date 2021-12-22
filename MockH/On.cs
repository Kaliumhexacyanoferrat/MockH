using MockH.Builder;

using GenHTTP.Api.Protocol;

namespace MockH
{

    /// <summary>
    /// Entry point to create rules that should be adhered by a mock server instance.
    /// </summary>
    public static class On
    {

        /// <summary>
        /// Creates a rule that will handle an incoming HEAD request for
        /// the specified path.
        /// </summary>
        /// <param name="path">The path to be handled, e.g. "/api/users" (or null, if requests to the server root should be handled)</param>
        /// <returns>The newly created rule builder</returns>
        public static IncompleteRule Head(string? path = null) => new(RequestMethod.HEAD, path);

        /// <summary>
        /// Creates a rule that will handle an incoming GET request for
        /// the specified path.
        /// </summary>
        /// <param name="path">The path to be handled, e.g. "/api/users" (or null, if requests to the server root should be handled)</param>
        /// <returns>The newly created rule builder</returns>
        public static IncompleteRule Get(string? path = null) => new(RequestMethod.GET, path);

        /// <summary>
        /// Creates a rule that will handle an incoming POST request for
        /// the specified path.
        /// </summary>
        /// <param name="path">The path to be handled, e.g. "/api/users" (or null, if requests to the server root should be handled)</param>
        /// <returns>The newly created rule builder</returns>
        public static IncompleteRule Post(string? path = null) => new(RequestMethod.POST, path);

        /// <summary>
        /// Creates a rule that will handle an incoming PUT request for
        /// the specified path.
        /// </summary>
        /// <param name="path">The path to be handled, e.g. "/api/users" (or null, if requests to the server root should be handled)</param>
        /// <returns>The newly created rule builder</returns>
        public static IncompleteRule Put(string? path = null) => new(RequestMethod.PUT, path);

        /// <summary>
        /// Creates a rule that will handle an incoming DELETE request for
        /// the specified path.
        /// </summary>
        /// <param name="path">The path to be handled, e.g. "/api/users" (or null, if requests to the server root should be handled)</param>
        /// <returns>The newly created rule builder</returns>
        public static IncompleteRule Delete(string? path = null) => new(RequestMethod.DELETE, path);

    }

}

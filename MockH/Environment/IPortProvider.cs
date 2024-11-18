namespace MockH.Environment;

/// <summary>
/// Allows the mock server to determine a free port to listen on.
/// </summary>
public interface IPortProvider
{

    /// <summary>
    /// Returns the next port number that can be used to listen on.
    /// </summary>
    /// <returns>The next port number to listen on</returns>
    /// <remarks>
    /// Must be thread safe to prevent multiple server instances from listening
    /// on the same port.
    /// </remarks>
    ushort GetAvailable();

}
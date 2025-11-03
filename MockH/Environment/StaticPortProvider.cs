namespace MockH.Environment;

/// <summary>
/// Provides port numbers by returning a number starting from 20.000
/// and incrementing the number on each call.
/// </summary>
public class StaticPortProvider : IPortProvider
{

#if NET8_0
    private static int _nextPort = 20000;
#else
    private static int _nextPort = 30000;
#endif

    /// <summary>
    /// Fetches the next available port to be used.
    /// </summary>
    /// <returns>The next available port to be used</returns>
    public ushort GetAvailable() => (ushort)Interlocked.Increment(ref _nextPort);

}

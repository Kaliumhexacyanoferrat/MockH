namespace MockH.Environment
{

    public class StaticPortProvider : IPortProvider
    {

        private static int _NextPort = 20000;

        public ushort GetAvailable() => (ushort)Interlocked.Increment(ref _NextPort);

    }

}

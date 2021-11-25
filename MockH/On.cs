using MockH.Builder;

using GenHTTP.Api.Protocol;

namespace MockH
{

    public static class On
    {

        public static IncompleteRule Head(string? path = null) => new(RequestMethod.HEAD, path);

        public static IncompleteRule Get(string? path = null) => new(RequestMethod.GET, path);

        public static IncompleteRule Post(string? path = null) => new(RequestMethod.POST, path);

        public static IncompleteRule Put(string? path = null) => new(RequestMethod.PUT, path);

        public static IncompleteRule Delete(string? path = null) => new(RequestMethod.DELETE, path);

    }

}

using GenHTTP.Api.Protocol;
using MockH.Builder;

namespace MockH
{

    public static class On
    {

        public static IncompleteRule Get(string? path = null) => new(RequestMethod.GET, path);

    }

}

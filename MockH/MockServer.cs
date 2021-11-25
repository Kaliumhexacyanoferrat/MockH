using MockH.Builder;
using MockH.Environment;

namespace MockH
{

    public static class MockServer
    {

        public static Server Run(params Rule[] rules) => Create(rules).Run();

        public static ServerBuilder Create(params Rule[] rules) => new ServerBuilder().Add(rules);

    }

}

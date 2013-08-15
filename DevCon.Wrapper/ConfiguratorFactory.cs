using System.Linq;
using DevCon.Wrapper.Configurators;

namespace DevCon.Wrapper
{
    public class ConfiguratorFactory
    {
        public static Configurator Create(string[] args)
        {
            var result = args.Contains("build") ? new BuildConfigurator() : new Configurator();
            result.Setup(args);

            return result;
        }
    }
}
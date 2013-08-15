using DevCon.Wrapper.Configurators;
using NUnit.Framework;

namespace DevCon.Wrapper.Test
{
    public class ConfiguratorTestBase
    {
        protected Configurator Configutator { get; set; }

        [SetUp]
        public void SetUp()
        {
            Configutator = CreateConfigurator();
        }

        protected virtual Configurator CreateConfigurator()
        {
            return new Configurator();
        }
    }
}
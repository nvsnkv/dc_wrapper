using DevCon.Wrapper.Configurators;
using NUnit.Framework;

namespace DevCon.Wrapper.Test
{
    [TestFixture]
    public class ConfiguratorFactoryShould
    {
        [Test]
        public void CreateConfiguratorByDefault()
        {
            var configurator = ConfiguratorFactory.Create(new[] {"-a"});

            Assert.IsInstanceOf<Configurator>(configurator);
        }

        [Test]
        public void CreateBuildConfiguratorOnCorrectArgs()
        {
            var configurator = ConfiguratorFactory.Create(new[] { "-a", "build", "file.file" });

            Assert.IsInstanceOf<BuildConfigurator>(configurator);
        }
    }
}
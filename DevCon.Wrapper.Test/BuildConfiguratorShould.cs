using DevCon.Wrapper.Configurators;
using NUnit.Framework;

namespace DevCon.Wrapper.Test
{
    [TestFixture]
    public class BuildConfiguratorShould : ConfiguratorTestBase
    {
        protected string[] buildArgs = new[] {"build", "smth.sln"};

        [Test]
        public void CreateMSBuildCommand()
        {
            Configutator.Setup(buildArgs);

            Assert.True(Configutator.Command.Contains("msbuild"));
        }

        [Test]
        public void SetBuildAsDefaultTarget()
        {
            Configutator.Setup(buildArgs);
            Assert.AreEqual("Build", BuildConfigurator.Target);
        }

        [Test]
        public void SetDebugAsDefaultConfiguration()
        {
            Configutator.Setup(buildArgs);
            Assert.AreEqual("Debug", BuildConfigurator.Configuration);
        }

        [Test]
        public void SetMultithredFlagByDefault()
        {
            Configutator.Setup(buildArgs);
            Assert.True((bool)BuildConfigurator.IsMultiThreaded);
        }
            

        protected BuildConfigurator BuildConfigurator { get { return (BuildConfigurator) Configutator; }}

        protected override Configurator CreateConfigurator()
        {
            return new BuildConfigurator();
        }
    }
}
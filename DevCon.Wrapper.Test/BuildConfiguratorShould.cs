using System.ComponentModel.Design;
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

        [Test]
        public void SetReleaseConfigurationWhenReleaseArgPassed()
        {
            Configutator.Setup(new []{"build", "filename", "-release"});

            Assert.AreEqual("Release", BuildConfigurator.Configuration);
        }

        [Test]
        public void SetCustomConfiguration()
        {
            Configutator.Setup(new[] { "build", "filename", "-cfg:Custom" });

            Assert.AreEqual("Custom", BuildConfigurator.Configuration);
        }

        [Test]
        public void SetRebuildTargetWhenRebuildArgPassed()
        {
            Configutator.Setup(new[] { "build", "filename", "-rebuild" });

            Assert.AreEqual("Rebuild", BuildConfigurator.Target);
        }

        [Test]
        public void SetCustomTarget()
        {
            Configutator.Setup(new[] { "build", "filename", "-tgt:Custom" });

            Assert.AreEqual("Custom", BuildConfigurator.Target);
        }

        [Test]
        public void SetLogFileWhenLogArgPassed ()
        {
            Configutator.Setup(new[] { "build", "filename", "-log" });

            Assert.IsNotNullOrEmpty(BuildConfigurator.LogFile);
        }

        [Test]
        public void SetCorrectFileName()
        {
            Configutator.Setup(new[] { "build", "file.file", "-log" });

            Assert.AreEqual("file.file", BuildConfigurator.FileName);
        }

        [Test]
        public void CreateMSBuildLaunchCommand()
        {
            Configutator.Setup(buildArgs);

            Assert.True(Configutator.Command.StartsWith("msbuild"));
        }

        [Test]
        public void CreatecommandWithCorrectTarget ()
        {
            Configutator.Setup(new[] { "build", "filename", "-tgt:Custom" });

            Assert.True(Configutator.Command.Contains("/t:Custom"));
        }

        [Test]
        public void CreateCommandWithCorrectConfiguration()
        {
            Configutator.Setup(new[] { "build", "filename", "-cfg:Custom" });

            Assert.True(Configutator.Command.Contains("/p:Configuration=Custom"));
        }

        [Test]
        public void CreateCommandWithMultithreadKey()
        {
            Configutator.Setup(new[] { "build", "filename", "-cfg:Custom" });

            Assert.True(Configutator.Command.Contains("/m"));
        }

        [Test]
        public void RedirectOutputToFile()
        {
            Configutator.Setup(new[] { "build", "filename", "-log" });

            Assert.True(Configutator.Command.Contains(">"));
        }

        [Test]
        public void CreateCommandWithCorrectFileName()
        {
            Configutator.Setup(new[] { "build", "file.file", "-log" });

            Assert.True(Configutator.Command.Contains("file.file"));
        }

        protected BuildConfigurator BuildConfigurator { get { return (BuildConfigurator) Configutator; }}

        protected override Configurator CreateConfigurator()
        {
            return new BuildConfigurator();
        }
    }
}
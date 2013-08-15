using System;
using NUnit.Framework;

namespace DevCon.Wrapper.Test
{
    [TestFixture]
    public class ConfiguratorShould:ConfiguratorTestBase
    {
        [Test]
        public void CreateProcessStartInfoWithCmdFilePath ()
        {
            var cmdPath = Environment.GetEnvironmentVariable("comspec");

            Configutator.Setup(new string[0]);
            Assert.AreEqual(cmdPath, Configutator.Info.FileName);
        }

        [Test]
        public void SetRequireAdministratorFlagIfShortArgumentGiven()
        {
            Configutator.Setup(new[] {"-a"});
            AssertAdminConfig();
        }

        [Test]
        public void SetRequireAdministratorFlagIfLongArgumentGiven()
        {
            Configutator.Setup(new[] { "--as-admin" });
            AssertAdminConfig();
        }

        [Test]
        public void SetDeveloperConsoleArgsToCmd()
        {
            var args = @"/k ""C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\Tools\VsDevCmd.bat""";
            Configutator.Setup(new string[0]);

            Assert.AreEqual(args, Configutator.Info.Arguments);
        }

        [Test]
        public void CreatePropertiesAfterSetupCommand()
        {
            Assert.Null(Configutator.Info);
            Assert.Null(Configutator.IsAdministratorRequired);

            Configutator.Setup(new string[0]);

            Assert.NotNull(Configutator.Info);
            Assert.NotNull(Configutator.IsAdministratorRequired);
        }

        [Test]
        public void SetUseShellExetueFlgaToFalse()
        {
            Configutator.Setup(new string[0]);
            Assert.False(Configutator.Info.UseShellExecute);
        }

        [Test]
        public void SetEmptyCommandString()
        {
            Configutator.Setup(new string[0]);
            Assert.IsEmpty(Configutator.Command);
        }

        protected void AssertAdminConfig()
        {
            Assert.AreEqual(true, Configutator.IsAdministratorRequired);
            Assert.AreEqual("runas", Configutator.Info.Verb);
        }
    }
}

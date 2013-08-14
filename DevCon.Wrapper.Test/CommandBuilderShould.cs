using System;
using DevCon.Wrapper.Configurators;
using NUnit.Framework;

namespace DevCon.Wrapper.Test
{
    [TestFixture]
    public class CommandBuilderShould
    {
        protected Configurator Configutator { get; set; }

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
        public void PassEscapedArgumentsToCmd()
        {
            var args = new[] {"some", "args with spaces", "and here to"};
            Configutator.Setup(args);

            AssertArgsPassed(args);
        }

        protected void AssertArgsPassed(string[] args)
        {
            foreach (var arg in args)
                Assert.True(Configutator.Info.Arguments.Contains("\""+ arg + "\""));
        }

        protected void AssertAdminConfig()
        {
            Assert.AreEqual(true, Configutator.IsAdministratorRequired);
            Assert.AreEqual("runas", Configutator.Info.Verb);
        }

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

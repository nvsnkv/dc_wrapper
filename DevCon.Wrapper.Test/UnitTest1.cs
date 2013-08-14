using System;
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
            Assert.AreEqual(true, Configutator.IsRequireAdministrator);
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

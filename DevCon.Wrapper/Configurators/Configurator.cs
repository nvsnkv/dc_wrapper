using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DevCon.Wrapper.Configurators
{
    public class Configurator
    {
        protected const string DeveloperConsoleArgs = @"/k ""C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\Tools\VsDevCmd.bat""";
        public ProcessStartInfo Info { get; protected set; }

        public bool? IsAdministratorRequired { get; protected set; }
        public string Command { get; protected set; }

        public virtual void Setup(string[] args)
        {
            var cmdPath = Environment.GetEnvironmentVariable("comspec");
            Info = new ProcessStartInfo
            {
                FileName = cmdPath, Arguments = DeveloperConsoleArgs, 
                UseShellExecute = false
            };

            IsAdministratorRequired = (args.Contains("-a") || args.Contains("--as-admin"));
            if ((bool)IsAdministratorRequired)
                Info.Verb = "runas";

            Command = string.Empty;
        }
    }

    public class BuildConfigurator : Configurator
    {
        public override void Setup(string[] args)
        {
            base.Setup(args);
            var i = LocateBuildArg(args);

            SetDefaults();

            for (i = i + 1; i < args.Length; i++)
            {
                if (args[i] == "-release")
                    Configuration = "Release";

                if (args[i].StartsWith("-cfg:"))
                    Configuration = args[i].Substring(4);

                if (args[i] == "-rebuild")
                    Target = "Rebuild";

                if (args[i].StartsWith("-tgt:"))
                    Target = args[i].Substring(4);

                if (File.Exists(args[i]))
                    FileName = args[i];

                if (args[i] == "-log")
                    LogFile = GenerateLogFileName();
            }
        }

        private string GenerateLogFileName()
        {
            return "msbuild_" + DateTime.Now + ".log";
        }

        public string LogFile { get; protected set; }

        public string FileName { get; protected set; }

        private void SetDefaults()
        {
            Command = "msbuild ";
            IsMultiThreaded = true;
            Configuration = "Debug";
            Target = "Build";
        }

        private static int LocateBuildArg(string[] args)
        {
            var i = 0;
            while (i < args.Length && args[i] != "build") i++;

            if (i == args.Length)
                throw new ArgumentException("Invalid input!");
            return i;
        }

        public bool? IsMultiThreaded { get; protected set; }
        public string Target { get; protected set; }
        public string Configuration { get; protected set; }
    }
}
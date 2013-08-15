using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
}
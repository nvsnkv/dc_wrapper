using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DevCon.Wrapper.Configurators
{
    public class Configurator
    {
        protected IEnumerable<string> GlobalModificators;
        protected IEnumerable<string> CommandModificators;

        protected string Command;
        
        public ProcessStartInfo Info { get; protected set; }

        public bool? IsAdministratorRequired { get; protected set; }

        public virtual void Setup(string[] args)
        {
            var cmdPath = Environment.GetEnvironmentVariable("comspec");
            Info = new ProcessStartInfo {FileName = cmdPath};

            IsAdministratorRequired = (args.Contains("-a") || args.Contains("--as-admin"));
            if ((bool)IsAdministratorRequired)
                Info.Verb = "runas";
        }
    }
}
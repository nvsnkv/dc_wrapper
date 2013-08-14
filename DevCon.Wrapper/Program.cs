using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DevCon.Wrapper
{
    public class Configurator
    {
        protected IEnumerable<string> GlobalModificators;
        protected IEnumerable<string> CommandModificators;

        protected string Command;
        
        public ProcessStartInfo Info { get; protected set; }

        public bool? IsRequireAdministrator { get; protected set; }

        public virtual void Setup(string[] args)
        {
            var cmdPath = Environment.GetEnvironmentVariable("comspec");
            Info = new ProcessStartInfo {FileName = cmdPath};
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

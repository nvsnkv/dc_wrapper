using System;
using System.IO;

namespace DevCon.Wrapper.Configurators
{
    public class BuildConfigurator : Configurator
    {
        public override void Setup(string[] args)
        {
            base.Setup(args);
            var i = LocateBuildArg(args);

            SetDefaults();
            ParseArgs(args, i);
            GenerateCommand();

            Info.RedirectStandardInput = true;
        }

        private void GenerateCommand()
        {
            Command = "msbuild + /t:" + Target + "/p:Configuration="+Configuration+ " ";
            if ((bool) IsMultiThreaded)
            {
                Command += "/m ";
            }

            Command += "\"" + FileName + "\" ";

            if (!string.IsNullOrEmpty(LogFile))
                Command += "> \"" + LogFile + "\" ";

        }

        private void ParseArgs(string[] args, int i)
        {
            for (i = i + 1; i < args.Length; i++)
            {
                if (args[i] == "-release")
                    Configuration = "Release";

                if (args[i].StartsWith("-cfg:"))
                    Configuration = args[i].Substring(5);

                if (args[i] == "-rebuild")
                    Target = "Rebuild";

                if (args[i].StartsWith("-tgt:"))
                    Target = args[i].Substring(5);

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
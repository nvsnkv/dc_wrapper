using System.Diagnostics;
using System.Linq;
using System.Text;
using DevCon.Wrapper.Configurators;

namespace DevCon.Wrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurator = new Configurator();
            configurator.Setup(args);

            var process = Process.Start(configurator.Info);
            process.StandardInput.WriteLine(configurator.Command);
        }
    }
}

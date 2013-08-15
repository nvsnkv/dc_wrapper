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
            var configurator = ConfiguratorFactory.Create(args);
            
            var process = Process.Start(configurator.Info);
            
            if(!string.IsNullOrEmpty(configurator.Command))
            {
                process.StandardInput.WriteLine(configurator.Command);
            }
        }
    }
}

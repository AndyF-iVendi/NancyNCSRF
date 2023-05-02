using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace NancyNCSRF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int port = 5000;
            if (!string.IsNullOrEmpty(args[0]))
            {
                port = int.Parse(args[0]);
            }
            var host = new WebHostBuilder()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseKestrel(x => x.ListenAnyIP(port))
               .UseStartup<Startup>()
               .Build();

            host.Run();
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace NancyNCSRF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseKestrel()
               .UseStartup<Startup>()
               .Build();

            host.Run();
        }
    }
}

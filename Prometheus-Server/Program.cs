using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Prometheus_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSentry("http://95d8f154464645608a4878985709b341@localhost:9000/2");
    }
}

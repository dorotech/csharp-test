using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace api
{
    public class Program
    {
#pragma warning disable CS1591
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
#pragma warning disable CS1591
}

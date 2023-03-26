using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Module.Identity.MigrationManager;

namespace Module.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().IdentityMigrateAndSeed().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
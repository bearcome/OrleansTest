using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace KWKY.WebClient
{
    public class Program
    {
        public static void Main (string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            string ss = null;
            var sstr = ss?.ToString();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

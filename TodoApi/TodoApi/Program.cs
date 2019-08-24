using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        // IServiceScope
        //public static void Main1(string[] args)
        //{
        //    var host = CreateWebHostBuilder(args).Build();

        //    using (var serviceScope = host.Services.CreateScope())
        //    {
        //        var services = serviceScope.ServiceProvider;

        //        try
        //        {
        //            //IServiceScopeFactory.CreateScope 创建 IServiceScope
        //            var serviceContext = services.GetRequiredService<MyScopedService>();
        //            // Use the context here

        //        }
        //        catch (Exception ex)
        //        {
        //            var logger = services.GetRequiredService<ILogger<Program>>();
        //            logger.LogError(ex, "An error occurred.");
        //        }
        //    }

        //    host.Run();
        //}
    }

}

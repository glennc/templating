﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RazorPagesWebApplication
{
    public class Program
    {
        public static IWebHost BuildWebHost(string[] args) => new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureConfiguration((context, builder) => {
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);

                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                        builder.AddUserSecrets<Startup>();
                    }

                    builder.AddEnvironmentVariables();
                })
                .ConfigureLogging(logger => logger
                    .AddConsole()
                    .AddDebug())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

        public static void Main(string[] args)
        {
            var host = BuildWebHost();

            host.Run();
        }
    }
}

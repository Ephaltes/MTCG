using System;
using System.IO;
using System.Net;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Model;
using MTCG.Model.BaseClass;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

namespace MTCG
{
    class Program
    {
        static void Main(string[] args)
        {
            StartLogger();
            
             var webserver = new CustomWebServer(IPAddress.Any, 10001);
             webserver.Start();
             webserver.Listen(5); 
        }
        
        private static void StartLogger()
        {
            string logfile = Directory.GetCurrentDirectory() + "/log.txt";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .WriteTo.File(logfile, LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}

using System.IO;
using System.Net;
using MTCG.Model;
using Serilog;
using Serilog.Events;

namespace MTCG
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StartLogger();

            var webserver = new CustomWebServer(IPAddress.Any, 10001);
            webserver.Start();
            webserver.Listen(5);
            //TODO: unit tests
        }

        private static void StartLogger()
        {
            var logfile = Directory.GetCurrentDirectory() + "/log.txt";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .WriteTo.File(logfile, LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
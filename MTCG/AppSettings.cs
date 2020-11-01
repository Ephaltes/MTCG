using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MTCG
{
    public class AppSettings
    {
        private static AppSettings _settings = null;

        private static readonly object lockobject = new object();

        private readonly string _ConnectionString;

        public string ConnectionString => Settings._ConnectionString;

        /// <summary>
        /// Singleton Instance of AppSettings
        /// </summary>
        public static AppSettings Settings
        {
            //Prüft ob es null ist wenn null dann leg für _settings eine neue AppSettings an
            get
            {
                lock (lockobject)
                {
                    return _settings ??= new AppSettings();
                }
            }
        }

        private AppSettings()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var root = configurationBuilder.Build();

            var server = root.GetSection("Settings");
            _ConnectionString = server.GetSection("ConnectionString").Value;
        }
    }
}
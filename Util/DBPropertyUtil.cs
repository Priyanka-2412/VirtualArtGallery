using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace VirtualArtGallery.Util
{
    public class DBPropertyUtil
    {
        private static IConfigurationRoot configuration;

        static DBPropertyUtil()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        public static string GetConnectionString(string name)
        {
            string? connStr = configuration.GetConnectionString(name);

            if (string.IsNullOrEmpty(connStr))
            {
                throw new ApplicationException($"Connection string '{name}' not found.");
            }

            return connStr;
        }

    }
}
#region Namespaces 

using ChatClient.Domain.SeedWork;
using Microsoft.Extensions.Configuration;
using System;

#endregion

namespace ChatClient.Infrastructure.Configuration
{
    /// <summary>
    /// Bootstraping the configuration settings
    /// </summary>
    public static class ConfigurationBootstraper
    {
        #region Fields

        private static AppConfig _appConfig;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        /// <returns>Returns </returns>
        public static AppConfig AppConfig
        {
            get
            {
                if (_appConfig == null)
                {
                    _appConfig = InitOptions<AppConfig>();
                }

                return _appConfig;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the options.
        /// </summary>
        /// <typeparam name="T">Generic model</typeparam>
        /// <returns>Returns the generic model</returns>
        public static T InitOptions<T>() where T : new()
        {
            var config = InitConfig();
            return config.Get<T>();
        }

        /// <summary>
        /// Initializes the configuration.
        /// </summary>
        /// <returns>Returns the configuration root</returns>
        public static IConfigurationRoot InitConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        #endregion 
    }
}

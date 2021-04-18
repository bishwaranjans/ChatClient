#region Namespaces 

using ChatClient.Domain.Entity;
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

        /// <summary>
        /// The application configuration
        /// </summary>
        private static AppConfig _appConfig;

        /// <summary>
        /// The user
        /// </summary>
        private static User _currentUser;

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

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public static User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = new User(Environment.UserName);
                }

                return _currentUser;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static User InitUser(string userName)
        {
            _currentUser = new User(userName);
            return _currentUser;
        }

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

#region Namespaces

using ChatClient.Infrastructure.Configuration;
using NATS.Client;

#endregion

namespace ChatClient.Infrastructure.Factories
{
    /// <summary>
    /// NatsConnectionFactory
    /// </summary>
    public class NatsConnectionFactory
    {
        #region Methods

        /// <summary>
        /// Connects to nats.
        /// </summary>
        /// <returns></returns>
        public static IConnection ConnectToNats()
        {
            ConnectionFactory factory = new ConnectionFactory();

            var options = ConnectionFactory.GetDefaultOptions();
            options.Url = string.IsNullOrWhiteSpace(ConfigurationBootstraper.AppConfig.NATSServerUrl) ? Defaults.Url : ConfigurationBootstraper.AppConfig.NATSServerUrl;

            return factory.CreateConnection(options);
        }

        #endregion

    }
}

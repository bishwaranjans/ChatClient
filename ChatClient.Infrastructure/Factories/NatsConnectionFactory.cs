#region Namespaces

using ChatClient.Domain.SeedWork;
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
        public static IEncodedConnection ConnectToNats()
        {
            ConnectionFactory factory = new ConnectionFactory();

            var options = ConnectionFactory.GetDefaultOptions();
            options.Url = string.IsNullOrWhiteSpace(ConfigurationBootstraper.AppConfig.NATSServerUrl) ? Defaults.Url : ConfigurationBootstraper.AppConfig.NATSServerUrl;

            var connection= factory.CreateEncodedConnection(options);
            connection.OnDeserialize = Serialization.JsonDeserializer;
            connection.OnSerialize = Serialization.JsonSerializer;

            return connection;
        }

        #endregion

    }
}

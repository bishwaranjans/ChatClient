#region Namespaces

using ChatClient.Domain.SeedWork;
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
        public static IEncodedConnection ConnectToNats(string natsUrl)
        {
            ConnectionFactory factory = new ConnectionFactory();

            var options = ConnectionFactory.GetDefaultOptions();
            options.Url = natsUrl;

            options.AsyncErrorEventHandler += (sender, args) =>
            {
                System.Console.WriteLine("Error: ");
                System.Console.WriteLine("   Server: " + args.Conn.ConnectedUrl);
                System.Console.WriteLine("   Message: " + args.Error);
                System.Console.WriteLine("   Subject: " + args.Subscription.Subject);
            };

            var connection = factory.CreateEncodedConnection(options);
            connection.OnDeserialize = Serialization.JsonDeserializer;
            connection.OnSerialize = Serialization.JsonSerializer;

            return connection;
        }

        #endregion
    }
}

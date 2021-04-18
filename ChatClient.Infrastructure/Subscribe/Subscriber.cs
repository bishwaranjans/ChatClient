#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Infrastructure.Configuration;
using NATS.Client;
using System;
using System.Text;

#endregion

namespace ChatClient.Infrastructure.Subscribe
{
    /// <summary>
    /// NATS Subscriber
    /// </summary>
    public class Subscriber
    {
        #region Fields

        /// <summary>
        /// The connection
        /// </summary>
        private static IEncodedConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriber"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public Subscriber(IEncodedConnection connection)
        {
            _connection = connection;
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        public void Subscribe()
        {
            EventHandler<EncodedMessageEventArgs> msgHandler = (sender, args) =>
            {
                UserMessage userMessage = (UserMessage)args.ReceivedObject;

                Console.WriteLine($"TimeStamp:{userMessage.TimeStamp} - User:{userMessage.User.UserName} - Message: {userMessage.Content}");
            };

            _connection.SubscribeAsync(ConfigurationBootstraper.AppConfig.NATSSubject, msgHandler);
        }

        #endregion
    }
}

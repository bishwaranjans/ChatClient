#region Namespaces

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
        private static IConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriber"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public Subscriber(IConnection connection)
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
            EventHandler<MsgHandlerEventArgs> msgHandler = (sender, args) =>
            {
                var message = Encoding.Default.GetString(args.Message.Data);
                Console.WriteLine($"{message}");
            };

            _connection.SubscribeAsync(ConfigurationBootstraper.AppConfig.NATSSubject, msgHandler);
        }

        #endregion
    }
}

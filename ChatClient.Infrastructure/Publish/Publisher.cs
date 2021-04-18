#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Domain.SeedWork;
using ChatClient.Infrastructure.Configuration;
using NATS.Client;
using System.Text;

#endregion

namespace ChatClient.Infrastructure.Publish
{
    /// <summary>
    /// NATS Publisher
    /// </summary>
    /// <seealso cref="ChatClient.Domain.SeedWork.IPublisher" />
    public class Publisher : IPublisher
    {
        #region Fields

        /// <summary>
        /// The connection
        /// </summary>
        private static IEncodedConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public Publisher(IEncodedConnection connection)
        {
            _connection = connection;
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Publishes the specified user message.
        /// </summary>
        /// <param name="userMessage">The user message.</param>
        public void Publish(UserMessage userMessage)
        {
            _connection.Publish(ConfigurationBootstraper.AppConfig.NATSSubject, userMessage);
        }

        #endregion
    }
}

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
        private static IConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public Publisher(IConnection connection)
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
            byte[] data = Encoding.UTF8.GetBytes($"Timestamp:{userMessage.TimeStamp} - User:{userMessage.User.UserName} - Message: {userMessage.Content}");

            _connection.Publish(ConfigurationBootstraper.AppConfig.NATSSubject, data);
        }

        #endregion
    }
}

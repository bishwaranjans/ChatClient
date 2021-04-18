#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Domain.SeedWork;
using NATS.Client;

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
        private IEncodedConnection _connection;

        /// <summary>
        /// The subscription
        /// </summary>
        private string _subject;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="subject">The subject.</param>
        public Publisher(IEncodedConnection connection, string subject)
        {
            _connection = connection;
            _subject = subject;
        }

        #endregion

        #region Methods       

        /// <summary>
        /// Publishes the message on the NATS subject.
        /// </summary>
        /// <param name="userMessage">The user Message.</param>
        public void Publish(UserMessage userMessage)
        {
            _connection.Publish(_subject, userMessage);
        }

        #endregion
    }
}

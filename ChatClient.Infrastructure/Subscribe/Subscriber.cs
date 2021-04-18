#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Domain.SeedWork;
using NATS.Client;
using System;

#endregion

namespace ChatClient.Infrastructure.Subscribe
{
    /// <summary>
    /// NATS Subscriber
    /// </summary>
    public class Subscriber : ISubscriber
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

        /// <summary>
        /// The subscription
        /// </summary>
        private IAsyncSubscription _subscription;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="subject">The subject.</param>
        public Subscriber(IEncodedConnection connection, string subject)
        {
            _connection = connection;
            _subject = subject;
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

            _subscription = _connection.SubscribeAsync(_subject, msgHandler);
        }

        /// <summary>
        /// Unsubscribe subject.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void UnSubscribe()
        {
            if (_connection.State == ConnState.CONNECTED)
            {
                _subscription.Unsubscribe();
            }
        }

        #endregion
    }
}

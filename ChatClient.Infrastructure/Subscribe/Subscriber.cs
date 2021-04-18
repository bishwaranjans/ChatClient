#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Infrastructure.Configuration;
using NATS.Client;
using System;
using System.Diagnostics;
using System.Threading;

#endregion

namespace ChatClient.Infrastructure.Subscribe
{
    /// <summary>
    /// NATS Subscriber
    /// </summary>
    public class Subscriber
    {
        #region Properties

        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; private set; }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public Options Options { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriber"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">user</exception>
        public Subscriber()
        {
            Subject = ConfigurationBootstraper.AppConfig.NATSSubject;
            Options = ConnectionFactory.GetDefaultOptions();
            Options.Url = string.IsNullOrWhiteSpace(ConfigurationBootstraper.AppConfig.NATSServerUrl) ? Defaults.Url : ConfigurationBootstraper.AppConfig.NATSServerUrl;
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        public void Subscribe()
        {
            using (IEncodedConnection connection = new ConnectionFactory().CreateEncodedConnection(Options))
            {
                TimeSpan elapsed;

                elapsed = receiveAsyncSubscriber(connection);

                Console.WriteLine($"Received msgs in {elapsed.TotalSeconds} seconds ");
            }
        }

        /// <summary>
        /// Receives the asynchronous subscriber.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        private TimeSpan receiveAsyncSubscriber(IEncodedConnection connection)
        {
            Stopwatch sw = new Stopwatch();
            Object testLock = new Object();

            EventHandler<EncodedMessageEventArgs> msgHandler = (sender, args) =>
            {
                UserMessage userMessage = (UserMessage)args.ReceivedObject;
                Console.WriteLine($"Received ArgumentMessage: {args.Message}. User: {userMessage.User} has sent message: {userMessage.Content} at timestamp: {userMessage.TimeStamp}.");

                sw.Stop();
                lock (testLock)
                {
                    Monitor.Pulse(testLock);
                }
            };

            using (IAsyncSubscription s = connection.SubscribeAsync(Subject, msgHandler))
            {
                // just wait until we are done.
                lock (testLock)
                {
                    Monitor.Wait(testLock);
                }
            }

            return sw.Elapsed;
        }

        #endregion
    }
}

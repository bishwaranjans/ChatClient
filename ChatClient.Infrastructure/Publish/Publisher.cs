#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Domain.SeedWork;
using ChatClient.Infrastructure.Configuration;
using NATS.Client;
using System;
using System.Diagnostics;

#endregion

namespace ChatClient.Infrastructure.Publish
{
    /// <summary>
    /// NATS Publisher
    /// </summary>
    /// <seealso cref="ChatClient.Domain.SeedWork.IPublisher" />
    public class Publisher : IPublisher
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
        /// Initializes a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="ArgumentNullException">user</exception>
        public Publisher(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Subject = ConfigurationBootstraper.AppConfig.NATSSubject;
            Options = ConnectionFactory.GetDefaultOptions();
            Options.Url = string.IsNullOrWhiteSpace(ConfigurationBootstraper.AppConfig.NATSServerUrl) ? Defaults.Url : ConfigurationBootstraper.AppConfig.NATSServerUrl;

            Options.SetUserCredentials(user.UserName);
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Publishes the specified user message.
        /// </summary>
        /// <param name="userMessage">The user message.</param>
        public void Publish(UserMessage userMessage)
        {
            Stopwatch sw = null;
            using (IEncodedConnection connection = new ConnectionFactory().CreateEncodedConnection(Options))
            {
                sw = Stopwatch.StartNew();
                connection.Publish(Subject, userMessage);
                connection.Flush();

                sw.Stop();

                Console.WriteLine($"Published '{userMessage.Content}' in {sw.Elapsed.TotalSeconds} seconds.");
            }
        }

        #endregion
    }
}

#region Namespaces

using ChatClient.Console.Configuration;
using ChatClient.Domain.Entity;
using ChatClient.Domain.SeedWork;
using ChatClient.Infrastructure.Factories;
using ChatClient.Infrastructure.Publish;
using ChatClient.Infrastructure.Subscribe;
using NATS.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace ChatClient.Console
{
    /// <summary>
    /// Main program
    /// </summary>
    class Program
    {
        private static IEncodedConnection _connection;
        private static IPublisher _publisher;
        private static ISubscriber _subscriber;

        private static bool isChattingContinue = true;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to ChatClient!!!");

            // User Identity
            System.Console.WriteLine("Please enter your name:");
            var user = System.Console.ReadLine();
            System.Console.WriteLine($"Hello {user} to ChatClient!");
            ConfigurationBootstraper.InitUser(user);

            try
            {
                using (_connection = NatsConnectionFactory.ConnectToNats(string.IsNullOrWhiteSpace(ConfigurationBootstraper.AppConfig.NATSServerUrl) ? Defaults.Url : ConfigurationBootstraper.AppConfig.NATSServerUrl))
                {
                    // Initializes publisher and subscriber
                    _publisher = new Publisher(_connection, ConfigurationBootstraper.AppConfig.NATSSubject);
                    _subscriber = new Subscriber(_connection, ConfigurationBootstraper.AppConfig.NATSSubject);

                    // Subscribe to the Subject
                    SubscribeOnSubject();

                    System.Console.WriteLine("Continuous pub/sub chat");
                    System.Console.WriteLine("=======================");
                    System.Console.WriteLine("Start chatting. Enter 'stop' to stopping the ChatClient.");

                    while (isChattingContinue)
                    {
                        // Continous publish
                        PublishOnSubject();
                    }

                    // Unsubscribe the client
                    _subscriber?.UnSubscribe();

                    System.Console.WriteLine("ChatClient stopped.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Unhandled exception occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Publishes the user input on NATS subject.
        /// </summary>
        private static void PublishOnSubject()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var task = Task.Run(() =>
            {
                while (isChattingContinue)
                {
                    string message = System.Console.ReadLine();
                    if (message.Equals("stop", StringComparison.OrdinalIgnoreCase))
                    {
                        isChattingContinue = false;
                        cts.Cancel();
                        break;
                    }
                    else
                    {                       
                        _publisher.Publish(new UserMessage(ConfigurationBootstraper.CurrentUser, message));
                    }
                }
                cancellationToken.ThrowIfCancellationRequested();
            }, cancellationToken);
        }

        /// <summary>
        /// Subscribes the NATS subject.
        /// </summary>
        private static void SubscribeOnSubject()
        {
            Task.Run(() =>
            {               
                _subscriber.Subscribe(ConfigurationBootstraper.AppConfig.IsPersistReceivedMessage);
            });
        }
    }
}

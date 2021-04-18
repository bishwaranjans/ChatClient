#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Infrastructure.Configuration;
using ChatClient.Infrastructure.Factories;
using ChatClient.Infrastructure.Publish;
using ChatClient.Infrastructure.Subscribe;
using NATS.Client;
using System;
using System.Text;
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
        private static IConnection _connection;
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
                using (_connection = NatsConnectionFactory.ConnectToNats())
                {
                    // Subscribe to the Subject
                    SubscribeOnSubject();

                    System.Console.WriteLine("Continuous pub/sub chat");
                    System.Console.WriteLine("=======================");
                    System.Console.WriteLine("Start chatting. Type 'stop' to stop.");

                    while (isChattingContinue)
                    {
                        // Continous publish
                        PublishOnSubject();
                    }

                    _connection.Close();
                }
            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"Unhandled exception occurred: {ex.Message}");
            }
        }

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
                    }
                    else
                    {
                        var publisher = new Publisher(_connection);
                        publisher.Publish(new UserMessage(ConfigurationBootstraper.CurrentUser, message));
                    }
                }
                cancellationToken.ThrowIfCancellationRequested();
            }, cancellationToken);
        }

        private static void SubscribeOnSubject()
        {
            Task.Run(() =>
            {
                var subscriber = new Subscriber(_connection);
                subscriber.Subscribe();
            });
        }
    }
}

#region Namespaces

using ChatClient.Domain.Entity;
using ChatClient.Infrastructure.Configuration;
using ChatClient.Infrastructure.Publish;
using ChatClient.Infrastructure.Subscribe;
using System;

#endregion

namespace ChatClient.Console
{
    /// <summary>
    /// Main program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to ChatClient!!!");

            // User Identity
            System.Console.WriteLine("Please enter your name:");
            var userName = System.Console.ReadLine();
            System.Console.WriteLine($"Hello {userName} to ChatClient!");
            var user = new User(userName);

            // Setting Publisher
            var publisher = new Publisher(user);

            // Setting Subscriber
            var subscriber = new Subscriber();
            subscriber.Subscribe();

            // User Hrlp section
            var natsServerUrl = string.IsNullOrWhiteSpace(ConfigurationBootstraper.AppConfig.NATSServerUrl) ? "nats://localhost:4222" : ConfigurationBootstraper.AppConfig.NATSServerUrl;
            System.Console.WriteLine($"You are now publish and subscribe messages from NATS Server: {natsServerUrl} on Subject: {ConfigurationBootstraper.AppConfig.NATSSubject}");
            System.Console.WriteLine($"Enter any text message to publish.{Environment.NewLine}To exit, enter: 'exit'");
            
            // Receive message and publish continuously until exit
            while (true)
            {
                var message = System.Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(message) && message.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Environment.Exit(-1);
                }

                UserMessage userMessage = new UserMessage(user, message);
                publisher.Publish(userMessage);
            }
        }
    }
}

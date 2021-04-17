
using ChatClient.Domain.SeedWork;
using ChatClient.Infrastructure.Configuration;

namespace ChatClient.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            var cfg = ConfigurationBootstraper.InitOptions<AppConfig>();

            System.Console.ReadLine();
        }              
    }
}

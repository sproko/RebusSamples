using System;
using System.Threading.Tasks;
using PubSub.Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Logging;
using Rebus.Routing.TypeBased;

namespace PubSub.Subscriber2
{
    class Program
    {
        static void Main()
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                activator.Register(() => new Handler());

                Configure.With(activator)
                         .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
                         //.Transport(t => t.UseMsmq("subscriber2"))
                         .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672/","subscriber2"))
                         .Routing(r => r.TypeBased().MapAssemblyOf<StringMessage>("publisher"))
                         .Start();

                activator.Bus.Subscribe<StringMessage>().Wait();
                activator.Bus.Subscribe<DogMessage>().Wait();

                Console.WriteLine("This is Subscriber 2");
                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
                Console.WriteLine("Quitting...");
            }
        }
    }

    class Handler : IHandleMessages<StringMessage>, IHandleMessages<DogMessage>
    {
        public async Task Handle(StringMessage message)
        {
            Console.WriteLine("Got string: {0}", message.Text);
        }
        public async Task Handle(DogMessage message)
        {
            Console.WriteLine("Got dogMessage: {0}", message.Name);
        }
    }
}


namespace AndrewLock.Scrutor.Example.ConsoleApp
{
    using global::Scrutor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using MSLearn = Microsoft.Learn.ConsoleDI.Example.Lib;

    internal class Program
    {
        static void ExemplifyServiceLifetime(IServiceProvider hostProvider, string lifetime)
        {
            using IServiceScope serviceScope = hostProvider.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            MSLearn.ServiceLifetimeReporter logger = provider.GetRequiredService<MSLearn.ServiceLifetimeReporter>();
            logger.ReportServiceLifetimeDetails(
                $"{lifetime}: Call 1 to provider.GetRequiredService<ServiceLifetimeLogger>()");

            Console.WriteLine("...");

            logger = provider.GetRequiredService<MSLearn.ServiceLifetimeReporter>();
            logger.ReportServiceLifetimeDetails(
                $"{lifetime}: Call 2 to provider.GetRequiredService<ServiceLifetimeLogger>()");

            Console.WriteLine();
        }


        static async Task Main(string[] args)
        {
            var oServiceCollection = new ServiceCollection();
                oServiceCollection.Scan(oTypeSourceSelector => 
                    oTypeSourceSelector
                        .FromCallingAssembly()
                        .AddClasses()                                         // 1. Find the concrete classes to  register
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
                        .AsSelf()                                             // 2. Specify which services they are registered as
                        .WithTransientLifetime()
                    );                                                        // 3. Set the lifetime for the services

            Console.WriteLine("Hello, World!");
        }
    }
}

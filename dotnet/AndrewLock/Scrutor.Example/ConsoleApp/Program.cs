
namespace AndrewLock.Scrutor.Example.ConsoleApp
{
    using global::Scrutor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using MSLearn = Microsoft.Learn.ConsoleDI.Example.Lib;
    using AndrewLock = AndrewLock.Scrutor.Example.Lib;
    using AndrewLock.Scrutor.Example.Lib.Services;

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
                oServiceCollection
                    .Scan(oTypeSourceSelector => oTypeSourceSelector
                        .AddTypes<
                            AndrewLock.Services.Service1, 
                            AndrewLock.Services.Service1>() // FIXME OBSOLETE Use FromTypes instead
                        .AsSelf()
                        .WithTransientLifetime()
                    )
                    .Scan(oTypeSourceSelector => oTypeSourceSelector
                        .FromAssemblyOf<AndrewLock.Services.IService>()
                        .AddClasses()
                        // NOTE: You can use the below filters
                     // .AddClasses(classes => classes.AssignableTo<AndrewLock.Services.IService>())   // Implements IService
                     // .AddClasses(classes => classes.InNamespaces("MyApp"))                          // Namespace
                     // .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")) // Suffix
                     // .AsSelf()
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    )
                    .Scan(oTypeSourceSelector => oTypeSourceSelector
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


namespace AndrewLock.Scrutor.Example.ConsoleApp
{
    using global::Scrutor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using MSLearn = Microsoft.Learn.ConsoleDI.Example;

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
            // using IHost oHost = Host.CreateDefaultBuilder(args)
            //     .ConfigureServices(services =>
            //     {// Manual Dependency Injection of eacch Service
            //         services.AddTransient<MSLearn.Services.ITransientService, MSLearn.Services.TransientService>();
            //         services.AddScoped<MSLearn.Services.IScopedService, MSLearn.Services.ScopedService>();
            //         services.AddSingleton<MSLearn.Services.ISingletonService, MSLearn.Services.SingletonService>();
            //         services.AddTransient<MSLearn.Services.ServiceLifetimeReporter>();
            //     })
            //     .Build();

            // ExemplifyServiceLifetime(oHost.Services, "Lifetime 1");
            // ExemplifyServiceLifetime(oHost.Services, "Lifetime 2");

            // await oHost.RunAsync();


            var oServiceCollection = new ServiceCollection();
                oServiceCollection.Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses()                                         // 1. Find the concrete classes to  register
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
                    .AsSelf()                                             // 2. Specify which services they are registered as
                    .WithTransientLifetime());                            // 3. Set the lifetime for the services

            Console.WriteLine("Hello, World!");
        }
    }
}

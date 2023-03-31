namespace Microsoft.Learn.ConsoleDI.Example.ConsoleApp
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using MSLearn = Microsoft.Learn.ConsoleDI.Example.Services;

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
            using IHost oHost = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {// Manual Dependency Injection of eacch Service
                    services.AddTransient<MSLearn.ITransientService, MSLearn.TransientService>();
                    services.AddScoped<MSLearn.IScopedService, MSLearn.ScopedService>();
                    services.AddSingleton<MSLearn.ISingletonService, MSLearn.SingletonService>();
                    services.AddTransient<MSLearn.ServiceLifetimeReporter>();
                })
                .Build();

            ExemplifyServiceLifetime(oHost.Services, "Lifetime 1");
            ExemplifyServiceLifetime(oHost.Services, "Lifetime 2");

            await oHost.RunAsync();
        }
    }
}

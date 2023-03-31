namespace Microsoft.Learn.ConsoleDI.Example.ConsoleApp
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using MSLearn = Microsoft.Learn.ConsoleDI.Example;

    internal class Program
    {
        static void ExemplifyServiceLifetime(IServiceProvider hostProvider, string lifetime)
        {
            using (IServiceScope oServiceScope = hostProvider.CreateScope())
            { 
                IServiceProvider oServiceProvider = oServiceScope.ServiceProvider;
                MSLearn.ServiceLifetimeReporter oServiceLifetimeReporter = oServiceProvider.GetRequiredService<MSLearn.ServiceLifetimeReporter>();
                oServiceLifetimeReporter
                    .ReportServiceLifetimeDetails($"{lifetime}: Call 1 to oServiceProvider.GetRequiredService<ServiceLifetimeLogger>()");

                Console.WriteLine("...");

                oServiceLifetimeReporter = oServiceProvider.GetRequiredService<MSLearn.ServiceLifetimeReporter>();
                oServiceLifetimeReporter
                    .ReportServiceLifetimeDetails($"{lifetime}: Call 2 to oServiceProvider.GetRequiredService<ServiceLifetimeLogger>()");

                Console.WriteLine();
            }
        }


        static async Task Main(string[] args)
        {
            using IHost oHost = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {// Manual Dependency Injection of eacch Service
                    services.AddTransient<MSLearn.Services.ITransientService, MSLearn.Services.TransientService>();
                    services.AddScoped<MSLearn.Services.IScopedService, MSLearn.Services.ScopedService>();
                    services.AddSingleton<MSLearn.Services.ISingletonService, MSLearn.Services.SingletonService>();
                    services.AddTransient<MSLearn.ServiceLifetimeReporter>();
                })
                .Build();

            ExemplifyServiceLifetime(oHost.Services, "Lifetime 1");
            ExemplifyServiceLifetime(oHost.Services, "Lifetime 2");

            await oHost.RunAsync();
        }
    }
}

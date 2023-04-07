namespace Microsoft.Learn.ConsoleDI.Example.ConsoleApp
{
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
            using (
                IHost oHost = Host
                    .CreateDefaultBuilder(args)
                    .ConfigureServices(oServiceCollection =>
                    {// Manual Dependency Injection of each Service
                        oServiceCollection
                            .AddTransient<MSLearn.Services.ITransientService, MSLearn.Services.TransientService>()
                            .AddScoped<MSLearn.Services.IScopedService, MSLearn.Services.ScopedService>()
                            .AddSingleton<MSLearn.Services.ISingletonService, MSLearn.Services.SingletonService>()

                            .AddTransient<MSLearn.ServiceLifetimeReporter>()
                            .AddTransient<AndrewLock.Services.Service1>()
                            .AddTransient<AndrewLock.Services.Service2>()
                            .AddTransient<AndrewLock.Services.Service3>()

                            .AddTransient<AndrewLock.Other.Foo>()
                         // .AddTransient<AndrewLock.Services.ITransientService, AndrewLock.Services.TransientService>(); // FIXME
                            .AddTransient<AndrewLock.Services.IScopedService, AndrewLock.Services.ScopedService>()

                            .AddTransient<AndrewLock.Services.IFooService, AndrewLock.Services.TransientService>()
                            .AddTransient<AndrewLock.Services.IScopedService, AndrewLock.Services.AnotherService>()

                            .AddSingleton<AndrewLock.Services.TestService>()                                  // .AsSelf()
                            .AddSingleton<AndrewLock.Services.ITestService, AndrewLock.Services.TestService>() // .AsMatchingInterface()
                            .AddSingleton<AndrewLock.Services.IService, AndrewLock.Services.TestService>();    // .AsImplementedInterfaces()
                    })
                    .Build()
            ) { 
                ExemplifyServiceLifetime(oHost.Services, "Lifetime 1");
                ExemplifyServiceLifetime(oHost.Services, "Lifetime 2");

                await oHost.RunAsync();
            }
        }
    }
}

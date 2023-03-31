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
                        oServiceCollection.AddTransient<MSLearn.Services.ITransientService, MSLearn.Services.TransientService>();
                        oServiceCollection.AddScoped<MSLearn.Services.IScopedService, MSLearn.Services.ScopedService>();
                        oServiceCollection.AddSingleton<MSLearn.Services.ISingletonService, MSLearn.Services.SingletonService>();
                        oServiceCollection.AddTransient<MSLearn.ServiceLifetimeReporter>();

                        oServiceCollection.AddTransient<AndrewLock.Services.Service1>();
                        oServiceCollection.AddTransient<AndrewLock.Services.Service2>();
                        oServiceCollection.AddTransient<AndrewLock.Services.Service3>();
                        oServiceCollection.AddTransient<AndrewLock.Other.Foo>();


                     // oServiceCollection.AddTransient<AndrewLock.Services.ITransientService, AndrewLock.Services.TransientService>(); // FIXME
                        oServiceCollection.AddTransient<AndrewLock.Services.IScopedService, AndrewLock.Services.ScopedService>();

                        oServiceCollection.AddTransient<AndrewLock.Services.IFooService, AndrewLock.Services.TransientService>();
                        oServiceCollection.AddTransient<AndrewLock.Services.IScopedService, AndrewLock.Services.AnotherService>();

                        oServiceCollection.AddSingleton<AndrewLock.Services.TestService>();                                   // .AsSelf()
                        oServiceCollection.AddSingleton<AndrewLock.Services.ITestService, AndrewLock.Services.TestService>(); // .AsMatchingInterface()
                        oServiceCollection.AddSingleton<AndrewLock.Services.IService, AndrewLock.Services.TestService>();     // .AsImplementedInterfaces()
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

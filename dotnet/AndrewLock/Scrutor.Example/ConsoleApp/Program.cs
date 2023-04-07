
namespace AndrewLock.Scrutor.Example.ConsoleApp
{
    using global::Scrutor;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
 // using MSLearn = Microsoft.Learn.ConsoleDI.Example.Lib;
    using AndrewLock = AndrewLock.Scrutor.Example.Lib;

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var oServiceCollection = new ServiceCollection();
                oServiceCollection
                    .Scan(oTypeSourceSelector => oTypeSourceSelector // Manual
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

                            .AddClasses()                                             // 1. Find the concrete classes to  register
                                .UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
                                .AsSelf()                                             // 2. Specify which services they are registered as
                                .WithTransientLifetime()                              // 3. Set the lifetime for the services
                    );                                                        

            Console.WriteLine("Hello, World!");
        }
    }
}


namespace JCystems.Hello.Scrutor.ConsoleApp
{
    using global::Scrutor;
    using Microsoft.Extensions.DependencyInjection;

    internal class Program
    {
        static void Main(string[] args)
        {
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

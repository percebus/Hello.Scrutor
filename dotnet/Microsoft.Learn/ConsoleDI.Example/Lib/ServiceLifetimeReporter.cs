
namespace Microsoft.Learn.ConsoleDI.Example.Lib
{
    using Microsoft.Learn.ConsoleDI.Example.Lib.Services;

    public class ServiceLifetimeReporter
    {
        private readonly ITransientService _transientService;
        private readonly IScopedService _scopedService;
        private readonly ISingletonService _singletonService;

        public ServiceLifetimeReporter(
            ITransientService transientService,
            IScopedService scopedService,
            ISingletonService singletonService) =>
            (_transientService, _scopedService, _singletonService) =
                (transientService, scopedService, singletonService);

        public void ReportServiceLifetimeDetails(string lifetimeDetails)
        {
            Console.WriteLine(lifetimeDetails);

            LogService(_transientService, "Always different");
            LogService(_scopedService, "Changes only with lifetime");
            LogService(_singletonService, "Always the same");
        }

        private static void LogService<T>(T service, string message)
            where T : IReportServiceLifetime =>
            Console.WriteLine(
                $"    {typeof(T).Name}: {service.Id} ({message})");
    }
}

namespace Microsoft.Learn.ConsoleDI.Example.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public interface ISingletonService : IReportServiceLifetime
    {
        ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Singleton;
    }
}

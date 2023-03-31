namespace Microsoft.Learn.ConsoleDI.Example.Services.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IScopedService : IReportServiceLifetime
    {
        ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Scoped;
    }
}

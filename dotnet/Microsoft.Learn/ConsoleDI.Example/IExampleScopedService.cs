namespace Microsoft.Learn.ConsoleDI.Example
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IExampleScopedService : IReportServiceLifetime
    {
        ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Scoped;
    }
}

namespace Microsoft.Learn.ConsoleDI.Example.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public interface ITransientService : IReportServiceLifetime
    {
        ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Transient;
    }
}

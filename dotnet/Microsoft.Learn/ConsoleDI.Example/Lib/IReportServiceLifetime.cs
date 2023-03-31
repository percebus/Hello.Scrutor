namespace Microsoft.Learn.ConsoleDI.Example.Lib
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IReportServiceLifetime
    {
        Guid Id { get; }

        ServiceLifetime Lifetime { get; }
    }
}

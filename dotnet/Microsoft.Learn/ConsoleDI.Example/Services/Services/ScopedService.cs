namespace Microsoft.Learn.ConsoleDI.Example.Services
{
    public class ScopedService : IScopedService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

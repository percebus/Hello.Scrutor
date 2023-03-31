namespace Microsoft.Learn.ConsoleDI.Example.Services.Services
{
    public class ScopedService : IScopedService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

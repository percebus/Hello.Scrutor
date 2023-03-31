namespace Microsoft.Learn.ConsoleDI.Example.Lib.Services
{
    public class ScopedService : IScopedService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

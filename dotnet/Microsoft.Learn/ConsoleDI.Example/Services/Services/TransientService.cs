namespace Microsoft.Learn.ConsoleDI.Example.Services
{
    public class TransientService : ITransientService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

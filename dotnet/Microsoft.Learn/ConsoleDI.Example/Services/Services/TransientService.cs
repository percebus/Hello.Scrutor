namespace Microsoft.Learn.ConsoleDI.Example.Services.Services
{
    public class TransientService : ITransientService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

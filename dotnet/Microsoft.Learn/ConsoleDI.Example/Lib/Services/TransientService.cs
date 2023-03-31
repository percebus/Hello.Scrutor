namespace Microsoft.Learn.ConsoleDI.Example.Lib.Services
{
    public class TransientService : ITransientService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

namespace Microsoft.Learn.ConsoleDI.Example.Services
{
    public class SingletonService : ISingletonService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

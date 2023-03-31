namespace Microsoft.Learn.ConsoleDI.Example.Services.Services
{
    public class SingletonService : ISingletonService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

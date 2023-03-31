namespace Microsoft.Learn.ConsoleDI.Example.Lib.Services
{
    public class SingletonService : ISingletonService
    {
        Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
    }
}

﻿namespace Microsoft.Learn.ConsoleDI.Example
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IExampleSingletonService : IReportServiceLifetime
    {
        ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Singleton;
    }
}

﻿namespace Microsoft.Learn.ConsoleDI.Example
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IExampleTransientService : IReportServiceLifetime
    {
        ServiceLifetime IReportServiceLifetime.Lifetime => ServiceLifetime.Transient;
    }
}

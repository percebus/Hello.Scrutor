﻿namespace Microsoft.Learn.ConsoleDI.Example
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IReportServiceLifetime
    {
        Guid Id { get; }

        ServiceLifetime Lifetime { get; }
    }
}

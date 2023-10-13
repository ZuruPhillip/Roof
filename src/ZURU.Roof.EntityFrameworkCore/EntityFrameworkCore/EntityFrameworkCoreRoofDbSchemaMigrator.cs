using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZURU.Roof.Data;
using Volo.Abp.DependencyInjection;

namespace ZURU.Roof.EntityFrameworkCore;

public class EntityFrameworkCoreRoofDbSchemaMigrator
    : IRoofDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreRoofDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the RoofDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<RoofDbContext>()
            .Database
            .MigrateAsync();
    }
}

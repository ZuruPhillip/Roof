using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ZURU.Roof.Data;

/* This is used if database provider does't define
 * IRoofDbSchemaMigrator implementation.
 */
public class NullRoofDbSchemaMigrator : IRoofDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

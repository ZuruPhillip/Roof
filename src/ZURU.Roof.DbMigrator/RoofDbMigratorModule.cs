using ZURU.Roof.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ZURU.Roof.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(RoofEntityFrameworkCoreModule),
    typeof(RoofApplicationContractsModule)
    )]
public class RoofDbMigratorModule : AbpModule
{
}

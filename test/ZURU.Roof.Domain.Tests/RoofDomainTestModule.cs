using ZURU.Roof.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ZURU.Roof;

[DependsOn(
    typeof(RoofEntityFrameworkCoreTestModule)
    )]
public class RoofDomainTestModule : AbpModule
{

}

using Volo.Abp.Modularity;

namespace ZURU.Roof;

[DependsOn(
    typeof(RoofApplicationModule),
    typeof(RoofDomainTestModule)
    )]
public class RoofApplicationTestModule : AbpModule
{

}

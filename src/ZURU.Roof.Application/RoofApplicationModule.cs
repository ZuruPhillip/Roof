using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using ZURU.Roof.OpcUaClients;

namespace ZURU.Roof;

[DependsOn(
    typeof(RoofDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(RoofApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class RoofApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<RoofApplicationModule>();
        });

        var configuration = context.Services.GetConfiguration();
        //context.Services.AddSingleton<IOpcUaClient>(o =>
        //{
        //    var logger = o.GetRequiredService<ILogger<OpcUaClient>>();
        //    return new OpcUaClient(configuration["OpcSimotion"], logger);
        //});

        context.Services.AddSingleton<IOpcUaClient>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<OpcUaClientMock>>();
            return new OpcUaClientMock(logger);
        });

        Configure<AbpSequentialGuidGeneratorOptions>(options =>
        {
            options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsString;
        });

    }
}

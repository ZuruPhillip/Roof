using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ZURU.Roof.Web;

[Dependency(ReplaceServices = true)]
public class RoofBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Roof";
}

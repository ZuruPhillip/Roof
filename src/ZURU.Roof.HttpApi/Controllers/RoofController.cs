using ZURU.Roof.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ZURU.Roof.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class RoofController : AbpControllerBase
{
    protected RoofController()
    {
        LocalizationResource = typeof(RoofResource);
    }
}

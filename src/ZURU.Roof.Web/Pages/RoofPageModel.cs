using ZURU.Roof.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ZURU.Roof.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class RoofPageModel : AbpPageModel
{
    protected RoofPageModel()
    {
        LocalizationResourceType = typeof(RoofResource);
    }
}

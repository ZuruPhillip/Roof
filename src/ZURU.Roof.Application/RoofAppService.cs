using System;
using System.Collections.Generic;
using System.Text;
using ZURU.Roof.Localization;
using Volo.Abp.Application.Services;

namespace ZURU.Roof;

/* Inherit your application services from this class.
 */
public abstract class RoofAppService : ApplicationService
{
    protected RoofAppService()
    {
        LocalizationResource = typeof(RoofResource);
    }
}

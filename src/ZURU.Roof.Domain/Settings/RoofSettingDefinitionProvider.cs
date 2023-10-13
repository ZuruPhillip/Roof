using Volo.Abp.Settings;

namespace ZURU.Roof.Settings;

public class RoofSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(RoofSettings.MySetting1));
    }
}

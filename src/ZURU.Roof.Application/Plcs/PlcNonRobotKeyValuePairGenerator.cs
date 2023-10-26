using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZURU.Roof.Robots;

namespace ZURU.Roof.Plcs
{
    public class PlcNonRobotKeyValuePairGenerator
    {
        private readonly string actionPreStr;
        private List<KeyValuePair<string, object>> nodeValuePairs = new List<KeyValuePair<string, object>>();
        private readonly int plcIndex;
        private readonly PlcRobotAction plcAction;

        public PlcNonRobotKeyValuePairGenerator(PlcRobotAction plcAction, string actionPreStr, int plcIndex)
        {
            this.plcAction = plcAction;
            this.actionPreStr = actionPreStr;
            this.plcIndex = plcIndex;
        }

        public List<KeyValuePair<string, object>> GetPlcKeyValuePairs()
        {
            if (plcAction != null)
            {
                AddUncompleteFlag();
                AddCompositeSwitch();
            }

            return nodeValuePairs;
        }

        private void AddCompositeSwitch()
        {
            //if (plcAction.ActionInfo.SwitchInfo != null)
            //{
            //    if (plcAction.ActionInfo.SwitchInfo.CompositeSwitchInfo.FlipCompositeSwitch == FlipCompositeSwitch.FlipStationOn)
            //    {
            //        nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".TurnActioninfo", (int)plcAction.ActionInfo.SwitchInfo.CompositeSwitchInfo.FlipCompositeSwitch));
            //    }
            //}
        }

        private void AddUncompleteFlag()
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".uncomplete_flag", plcAction.UncompleteFlag));
        }

        private string PreFix()
        {
            return actionPreStr + "[" + plcIndex + "]";
        }
    }
}

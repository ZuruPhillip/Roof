using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZURU.Roof.Robots;

namespace ZURU.Roof.Plcs
{
    public class PlcKeyValuePairGenerator
    {
        public static List<KeyValuePair<string, object>> GetPlcKeyValuePairs(List<PlcRobotAction> robotActions, string actionPreStr, string actionsSumNode, int plcStartIndex = 1, bool isAacCommand = false)
        {
            var keyValuePairs = new List<KeyValuePair<string, object>>();
            int currentIndex = plcStartIndex;

            foreach (var action in robotActions)
            {
                keyValuePairs.AddRange(GetPlcKeyValuePair(action, actionPreStr, currentIndex, isAacCommand));
                currentIndex++;
            }

            keyValuePairs.Add(new KeyValuePair<string, object>(actionsSumNode, robotActions.Count));

            return keyValuePairs;
        }



        public static List<KeyValuePair<string, object>> GetPlcKeyValuePairsWithoutRobot(List<PlcRobotAction> robotActions, string actionPreStr, string actionsSumNode, int plcStartIndex = 1)
        {
            var keyValuePairs = new List<KeyValuePair<string, object>>();
            int currentIndex = plcStartIndex;

            foreach (var action in robotActions)
            {
                keyValuePairs.AddRange(GetPlcKeyValuePairWithoutRobot(action, actionPreStr, currentIndex));
                currentIndex++;
            }

            keyValuePairs.Add(new KeyValuePair<string, object>(actionsSumNode, robotActions.Count));

            return keyValuePairs;
        }

        private static List<KeyValuePair<string, object>> GetPlcKeyValuePair(PlcRobotAction robotAction, string actionPreStr, int plcIndex = 1, bool isAacCommand = false)
        {
            return new PlcRobotActionKeyValueGenerator(robotAction, actionPreStr, plcIndex, isAacCommand).GetPlcKeyValuePairs();
        }

        private static List<KeyValuePair<string, object>> GetPlcKeyValuePairWithoutRobot(PlcRobotAction robotAction, string actionPreStr, int plcIndex = 1)
        {
            return new PlcNonRobotKeyValuePairGenerator(robotAction, actionPreStr, plcIndex).GetPlcKeyValuePairs();
        }
    }
}

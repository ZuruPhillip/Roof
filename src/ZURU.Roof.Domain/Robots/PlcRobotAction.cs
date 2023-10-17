using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof.Robots
{
    public class PlcRobotAction
    {
        public int RobotId { get; set; }
        public ActionType ActionType { get; set; }
        public NextActionType NextActionType { get; set; }
        public bool UncompleteFlag { get; set; }
        public ActionInfo ActionInfo { get; set; }
        public string ActionId { get; set; }
    }
}

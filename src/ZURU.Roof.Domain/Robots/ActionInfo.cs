using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof.Robots
{
    public class ActionInfo
    {
        public RobotMotionInfo RobotMotionInfo { get; set; }
        public DelayInfo DelayInfo { get; set; }
        public SwitchInfo SwitchInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof.Addresses
{
    public interface IAddress
    {
        string PreStr { get; }
        string TaskSent { get; }
        string TaskDone { get; }
        string TaskIndex { get; }
        string TaskSum { get; }
        string ToolInSpindle { get; }
        string TakeTool { get; }
        string DropTool { get; }
        string LaserRead { get; }
        string RobotPosX { get; }
        string RobotPosY { get; }
        string RobotPosZ { get; }
        string AgvEnable { get; }
        string AgvIn { get; }
        string TableIn { get; }
        string TableOut { get; }
        string ProcessDone { get; }
        string DustClean { get; }
        string TableCheck { get; }
        string LaserProfilerScan { get; }
        string LaserProfilerDataDelete { get; }
    }
}

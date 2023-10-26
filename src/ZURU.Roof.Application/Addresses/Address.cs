using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ZURU.Roof.Addresses
{
    public class Address : IAddress, ITransientDependency
    {
        public string PreStr { get; private set; } = "ns=2;s=unit/Robot_Var.RobotAction2";
        public string TaskSent { get; private set; } = "ns=2;s=unit/Robot_Var1.Station2DataSendComplete";
        public string TaskDone { get; private set; } = "ns=2;s=unit/Robot_Var1.Station2Complete";
        public string TaskIndex { get; private set; } = "ns=2;s=unit/KUKA_Station2.index2";
        public string TaskSum { get; private set; } = "ns=2;s=unit/Robot_Var1.IndexSum_station2";
        public string ToolInSpindle { get; private set; } = "ns=2;s=unit/Robot_Var2.KUKA5.Input_1to8[2]";
        public string TakeTool { get; private set; } = "ns=2;s=unit/Robot_Var1.ToolChangeGet_No";
        public string DropTool { get; private set; } = "ns=2;s=unit/Robot_Var1.ToolChangePut_No";
        public string LaserRead { get; private set; } = "ns=2;s=unit/cnc_var.Data_read";
        public string RobotPosX { get; private set; } = "ns=2;s=unit/Robot_Var2.KUKA5.FB_ReadData.ReadActualPosition_Position.X";
        public string RobotPosY { get; private set; } = "ns=2;s=unit/Robot_Var2.KUKA5.FB_ReadData.ReadActualPosition_Position.Y";
        public string RobotPosZ { get; private set; } = "ns=2;s=unit/Robot_Var2.KUKA5.FB_ReadData.ReadActualPosition_Position.Z";
        public string AgvEnable { get; private set; } = "ns=2;s=unit/cnc_var.AGV_Enable"; // For controller to read
        public string AgvIn { get; private set; } = "ns=2;s=unit/cnc_var.AGV_Enter"; // For controller to set
        public string TableIn { get; private set; } = "ns=2;s=unit/cnc_var.CompleteEnter_CNC";
        public string TableOut { get; private set; } = "ns=2;s=unit/cnc_var.CompleteComeOut_CNC";
        public string ProcessDone { get; private set; } = "ns=2;s=unit/Robot_Var1.Station2AllComplete";
        public string DustClean { get; private set; } = "ns=2;s=io/_direct.qbo_BlowDust";
        public string TableCheck { get; private set; } = "ns=2;s=unit/TableSensor_Var.TableCNC";
        public string LaserProfilerScan { get; private set; } = "ns=2;s=unit/CNC_Var.engineering_CNC"; //Decision [1].engineering_BOOLO  value: [1].data0
        public string LaserProfilerScanValue { get; private set; } = "ns=2;s=unit/CNC_Var.engineering_CNC";
        public string LaserProfilerDataDelete { get; private set; } = "ns=2;s=unit/CNC_Var.PC_engineering_Datadelete";

    }
}

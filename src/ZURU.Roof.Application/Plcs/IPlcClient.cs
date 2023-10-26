using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using ZURU.Roof.Robots;

namespace ZURU.Roof.Plcs
{
    public interface IPlcClient:ITransientDependency
    {
        Task SendPlcTasksToPlc(List<PlcRobotAction> plcTasks,string roofId);
        /*
        Task CheckAgvPermission();
        Task CheckIfTableIn();
        Task CheckIfTableOut();
        Task WaitTableToLeave();
        Task WaitTableToArrive();
        bool HasToolInSpindle();
        void SendAirGunStartTaskToPlc();
        void SendAirGunStopTaskToPlc();
        void SendOneSideCompletedToPlc();
        void SendTableArrivedToPlc();
        Task SendPlcTasksToPlc(List<PlcRobotAction> plcTasks);
        Task SendPlcTaskToPlc(PlcRobotAction plcTask);
        Task<List<Queue<List<string>>>> Send2OPCWaitReadLaserData(List<PlcRobotAction> plcTasks, List<bool> scanController);
        Task<Queue<List<string>>> Send2OPCWaitReadLaserData(List<PlcRobotAction> plcTasks);
        void ClearLaserProfilerData();
        Task<List<double>> Send2OPCANDReadLaserProfilerData(List<PlcRobotAction> plcTasks, int sideNum, bool flipped = false);
        */
    }
}

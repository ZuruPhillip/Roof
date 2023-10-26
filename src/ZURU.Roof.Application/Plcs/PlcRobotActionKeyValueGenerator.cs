using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZURU.Roof.Robots;

namespace ZURU.Roof.Plcs
{
    public class PlcRobotActionKeyValueGenerator
    {
        private readonly string actionPreStr; //= "ns=2;s=unit/robot_var.RobotAction";
        private List<KeyValuePair<string, object>> nodeValuePairs = new List<KeyValuePair<string, object>>();
        private readonly int plcIndex;
        private readonly PlcRobotAction plcAction;
        private readonly bool _isAacCommand;

        public PlcRobotActionKeyValueGenerator(PlcRobotAction plcAction, string actionPreStr, int plcIndex, bool isAacCommand = false)
        {
            this.plcAction = plcAction;
            this.actionPreStr = actionPreStr;
            this.plcIndex = plcIndex;
            _isAacCommand = isAacCommand;
        }

        public List<KeyValuePair<string, object>> GetPlcKeyValuePairs()
        {
            if (plcAction != null)
            {
                // Must have field
                AddRobotId();
                AddActionType();
                AddNextActionType();
                AddUncompleteFlag();

                // RobotMotion
                AddActionInfo();

                AddCompositeSwitch();
            }

            return nodeValuePairs;
        }

        private void AddCompositeSwitch()
        {
            if (plcAction.ActionInfo.SwitchInfo != null)
            {
                if (plcAction.ActionInfo.SwitchInfo.CompositeSwitchInfo.SwitchActionName == CompositeSwitch.Motor)
                {
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.compositeSwitchInfo", (int)plcAction.ActionInfo.SwitchInfo.CompositeSwitchInfo.SwitchActionName));
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.motorInfo.speed", plcAction.ActionInfo.SwitchInfo.MotorInfo.Speed));
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.motorInfo.rotateDirection", plcAction.ActionInfo.SwitchInfo.MotorInfo.RotateDirection));
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.motorInfo.status", plcAction.ActionInfo.SwitchInfo.MotorInfo.Status));
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.motorInfo.motorID", plcAction.ActionInfo.SwitchInfo.MotorInfo.MotorId));
                }
                if (plcAction.ActionInfo.SwitchInfo.CompositeSwitchInfo.SwitchActionName == CompositeSwitch.CheckToolData)
                {
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.compositeSwitchInfo", (int)plcAction.ActionInfo.SwitchInfo.CompositeSwitchInfo.SwitchActionName));
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.checkToolDataInfo.toolID", plcAction.ActionInfo.SwitchInfo.CheckToolDataInfo.ToolId));
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.checkToolDataInfo.actionType", plcAction.ActionInfo.SwitchInfo.CheckToolDataInfo.ActionType));
                    nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.checkToolDataInfo.rotation", plcAction.ActionInfo.SwitchInfo.CheckToolDataInfo.Rotation));
                }
                else
                {
                    if (_isAacCommand)
                    {
                        //nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.compositeSwitchInfo", (int)plcAction.ActionInfo.SwitchInfo.CompositeSwitchInfo.AacCutCompositeSwitch));
                    }
                    else
                    {
                        nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.switchInfo.compositeSwitchInfo", (int)plcAction.ActionInfo.SwitchInfo.CompositeSwitchInfo.SwitchActionName));
                    }
                }
            }
        }

        private void AddRobotId()
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".robotID", plcAction.RobotId));
        }

        private void AddActionType()
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionType", (int)plcAction.ActionType));
        }

        private void AddNextActionType()
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".nextActionType", (int)plcAction.NextActionType));
        }

        private void AddUncompleteFlag()
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".uncomplete_flag", plcAction.UncompleteFlag));
        }

        private void AddActionInfo()
        {
            AddRobotMotionInfo();
            AddDelayInfo();
        }

        private void AddDelayInfo()
        {
            if (plcAction.ActionInfo.DelayInfo != null)
            {
                nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.DelayInfo.delayTime", plcAction.ActionInfo.DelayInfo.DelayTime));
            }
        }

        private void AddRobotMotionInfo()
        {
            if (plcAction.ActionInfo.RobotMotionInfo != null)
            {
                AddCoordinateSystem();
                AddKukaMotionType();
                AddE6pos(PreFix() + ".actionInfo.robotMotionInfo.endPoint", plcAction.ActionInfo.RobotMotionInfo.EndPoint);
                if (plcAction.ActionInfo.RobotMotionInfo.KukaMotionType == KukaMotionType.Circ)
                {
                    AddE6pos(PreFix() + ".actionInfo.robotMotionInfo.auxiliaryPoint", plcAction.ActionInfo.RobotMotionInfo.AuxiliaryPoint);
                    AddAngle(plcAction.ActionInfo.RobotMotionInfo.Angle);
                }
                AddVelocity(plcAction.ActionInfo.RobotMotionInfo.Velocity);
                AddOverwrite(plcAction.ActionInfo.RobotMotionInfo.OverWrite);
                if (!_isAacCommand)
                {
                    AddLinAcceleration(plcAction.ActionInfo.RobotMotionInfo.LinAcceleration);
                    AddInprocessFlag(plcAction.ActionInfo.RobotMotionInfo.InprocessFlag);
                }
            }
        }

        private void AddOverwrite(int overWrite)
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.robotMotionInfo.overWrite", overWrite));
        }

        private void AddInprocessFlag(bool inprocessFlag)
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.robotMotionInfo.InProcessFlag", inprocessFlag));
        }

        private void AddLinAcceleration(int linAcceleration)
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.robotMotionInfo.Lin_Acceleration", linAcceleration));
        }

        private void AddVelocity(int velocity)
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.robotMotionInfo.velocity", velocity));
        }

        private void AddAngle(float angle)
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.robotMotionInfo.angle", angle));
        }

        private void AddKukaMotionType()
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.robotMotionInfo.motionType", (int)plcAction.ActionInfo.RobotMotionInfo.KukaMotionType));
        }

        private void AddE6pos(string prefix, E6POS pos)
        {
            nodeValuePairs.Add(new KeyValuePair<string, object>(prefix + ".X", pos.X));
            nodeValuePairs.Add(new KeyValuePair<string, object>(prefix + ".Y", pos.Y));
            nodeValuePairs.Add(new KeyValuePair<string, object>(prefix + ".Z", pos.Z));
            nodeValuePairs.Add(new KeyValuePair<string, object>(prefix + ".A", pos.A));
            nodeValuePairs.Add(new KeyValuePair<string, object>(prefix + ".B", pos.B));
            nodeValuePairs.Add(new KeyValuePair<string, object>(prefix + ".C", pos.C));
            nodeValuePairs.Add(new KeyValuePair<string, object>(prefix + ".Status", pos.S));
            nodeValuePairs.Add(new KeyValuePair<string, object>(prefix + ".Turn", pos.T));
        }

        private void AddCoordinateSystem()
        {
            if (plcAction.ActionInfo.RobotMotionInfo.CoordinateSystem != null)
            {
                nodeValuePairs.Add(new KeyValuePair<string, object>(PreFix() + ".actionInfo.robotMotionInfo.coordinateSystem.coordinateType", (int)plcAction.ActionInfo.RobotMotionInfo.CoordinateSystem.CoordinateType));
                AddE6pos(PreFix() + ".actionInfo.robotMotionInfo.coordinateSystem.coordinateInfo", plcAction.ActionInfo.RobotMotionInfo.CoordinateSystem.CoordinateInfo);
            }
        }

        private string PreFix()
        {
            return actionPreStr + "[" + plcIndex + "]";
        }
    }
}

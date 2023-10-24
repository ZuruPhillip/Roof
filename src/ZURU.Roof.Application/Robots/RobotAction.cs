using ZURU.Roof.Utility.Transforms;

namespace ZURU.Roof.Robots
{
    public class RobotAction
    {
        public RobotAction()
        {
        }

        public static PlcRobotAction GetRobotLinTask(int robotId, Transform target, int velocity, int overwrite, NextActionType nextActionType = NextActionType.WhenMotionDone, int acceleration = 50, bool inprocessFlag = false)
        {
            return new PlcRobotAction
            {
                RobotId = robotId,
                ActionType = ActionType.RobotMotion,
                NextActionType = nextActionType,
                UncompleteFlag = true,
                ActionInfo = new ActionInfo
                {
                    RobotMotionInfo = new RobotMotionInfo
                    {
                        KukaMotionType = KukaMotionType.Lin,
                        EndPoint = RobotController.TransformToE6POS(target),
                        OverWrite = overwrite,
                        Velocity = velocity,
                        LinAcceleration = acceleration,
                        InprocessFlag = inprocessFlag
                    }
                }
            };
        }

        public static PlcRobotAction GetRobotPtpTask(int robotId, Transform target, int velocity, int overwrite, NextActionType nextActionType = NextActionType.WhenMotionDone, int acceleration = 50)
        {
            return new PlcRobotAction
            {
                RobotId = robotId,
                ActionType = ActionType.RobotMotion,
                NextActionType = nextActionType,
                UncompleteFlag = true,
                ActionInfo = new ActionInfo
                {
                    RobotMotionInfo = new RobotMotionInfo
                    {
                        KukaMotionType = KukaMotionType.Ptp,
                        EndPoint = RobotController.TransformToE6POS(target),
                        OverWrite = overwrite,
                        Velocity = velocity,
                        LinAcceleration = acceleration
                    }
                }
            };
        }

        public static PlcRobotAction GetRobotCirTask(int robotId, Transform target, Transform auxiliaryPoint, float angle, int velocity, int overwrite, NextActionType nextActionType = NextActionType.WhenMotionDone, int acceleration = 50)
        {
            return new PlcRobotAction
            {
                RobotId = robotId,
                ActionType = ActionType.RobotMotion,
                NextActionType = nextActionType,
                UncompleteFlag = true,
                ActionInfo = new ActionInfo
                {
                    RobotMotionInfo = new RobotMotionInfo
                    {
                        KukaMotionType = KukaMotionType.Circ,
                        EndPoint = RobotController.TransformToE6POS(target),
                        AuxiliaryPoint = RobotController.TransformToE6POS(auxiliaryPoint),
                        Angle = angle,
                        OverWrite = overwrite,
                        Velocity = velocity,
                        LinAcceleration = acceleration
                    }
                }
            };
        }

        //Update whenMotionDone to Immediately
        public static PlcRobotAction GetCompositeSwitchTask(int robotId, CompositeSwitch compositeSwitch, NextActionType nextActionType = NextActionType.Immediately)
        {
            return new PlcRobotAction
            {
                RobotId = robotId,
                ActionType = ActionType.Switch,
                NextActionType = nextActionType,
                UncompleteFlag = true,
                ActionInfo = new ActionInfo
                {
                    SwitchInfo = new SwitchInfo
                    {
                        CompositeSwitchInfo = new CompositeSwitchInfo
                        {
                            SwitchActionName = compositeSwitch,
                        }
                    }
                }
            };
        }

        public static PlcRobotAction GetCompositeSwitchTask(int robotId, CompositeSwitch compositeSwitch, int motorId, bool status, bool rotationDirection, float speed)
        {
            MotorInfo motorInfo = new MotorInfo
            {
                MotorId = motorId,
                Status = status,
                RotateDirection = rotationDirection,
                Speed = speed,
            };

            SwitchInfo switchInfo = new SwitchInfo
            {
                CompositeSwitchInfo = new CompositeSwitchInfo
                {
                    SwitchActionName = compositeSwitch
                },
                MotorInfo = motorInfo
            };

            return new PlcRobotAction
            {
                RobotId = robotId,
                ActionType = ActionType.Switch,
                NextActionType = NextActionType.Immediately,
                UncompleteFlag = true,
                ActionInfo = new ActionInfo
                {
                    SwitchInfo = switchInfo
                }
            };
        }

        public static PlcRobotAction GetCompositeSwitchTask(int robotId, CompositeSwitch compositeSwitch, int toolChangeType, int toolId, float locationAngle)
        {
            MotorInfo motorInfo = new MotorInfo
            {
                MotorId = 0,
                Status = false,
                RotateDirection = false,
                Speed = 0,
            };
            CheckToolDataInfo toolChangeInfo = new CheckToolDataInfo
            {
                ActionType = toolChangeType,
                ToolId = toolId,
                Rotation = locationAngle
            };
            SwitchInfo switchInfo = new SwitchInfo
            {
                CompositeSwitchInfo = new CompositeSwitchInfo
                {
                    SwitchActionName = compositeSwitch,
                },
                MotorInfo = motorInfo,
                CheckToolDataInfo = toolChangeInfo
            };

            return new PlcRobotAction
            {
                RobotId = robotId,
                ActionType = ActionType.Switch,
                NextActionType = NextActionType.Immediately,
                UncompleteFlag = true,
                ActionInfo = new ActionInfo { SwitchInfo = switchInfo },
            };
        }


        //Immidiately?
        public static PlcRobotAction GetDelayTask(int robotId, float delayTime, NextActionType nextActionType = NextActionType.WhenMotionDone)
        {
            return new PlcRobotAction
            {
                RobotId = robotId,
                ActionType = ActionType.Delay,
                NextActionType = nextActionType,
                UncompleteFlag = true,
                ActionInfo = new ActionInfo
                {
                    DelayInfo = new DelayInfo
                    {
                        DelayTime = delayTime
                    }
                }
            };
        }
    }
}

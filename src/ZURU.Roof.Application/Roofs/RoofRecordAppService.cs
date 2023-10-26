using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using ZURU.Roof.Paths;
using ZURU.Roof.Plcs;
using ZURU.Roof.Robots;
using ZURU.Roof.Utility.Transforms;

namespace ZURU.Roof.Roofs
{
    public class RoofRecordAppService : RoofAppService, IRoofRecordAppService
    {
        private readonly IRoofRecordRepository _roofRecordRepository;
        private readonly IRobotPathRepository _robotPathRepository;
        private readonly IPlcClient _plcClient;

        public RoofRecordAppService(IRoofRecordRepository roofRecordRepository, IRobotPathRepository robotPathRepository, IPlcClient plcClient)
        {
            _roofRecordRepository = roofRecordRepository;
            _robotPathRepository = robotPathRepository;
            _plcClient = plcClient;
        }

        /// <summary>
        /// 添加屋顶信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, Route(RouteAddress.AddRoofRecordAsync)]
        public async Task AddRoofRecordAsync(RoofInputDto input)
        {
            try
            {
                //幂等判断
                var roofRecordExist = await _roofRecordRepository.FindAsync(x => x.RoofId == input.RoofId);
                if (roofRecordExist != null)
                {
                    throw new Exception($"Id 为{input.Id}的屋顶数据已存在");
                }

                List<RoofPoint> roofPoints = new List<RoofPoint>();
                int pointIndex = 1;
                foreach (var cp in input.ContourPoints)
                {
                    var id = GuidGenerator.Create();
                    RoofPoint rp = new RoofPoint(id, pointIndex, PointTypeEnum.ContourPoint, cp.X, cp.Y, cp.Z);
                    pointIndex++;
                    roofPoints.Add(rp);
                }
                foreach (var mp in input.MidPoints)
                {
                    var id = GuidGenerator.Create();
                    RoofPoint cp = new RoofPoint(id, pointIndex, PointTypeEnum.MidPoint, mp.X, mp.Y, mp.Z);
                    pointIndex++;
                    roofPoints.Add(cp);
                }
                RoofRecord record = new RoofRecord(input.RoofId, roofPoints, true);

                await _roofRecordRepository.InsertAsync(record);
            }
            catch (Exception ex)
            {
                throw new Exception("保存异常");
            }
        }

        [HttpPost, Route(RouteAddress.GetRoofRecordAndSendAsync)]
        public async Task GetRoofRecordDataAndSendAsync(GenerateRoofDataInput input)
        {
            try
            {
                int velocity = 10;
                int overwrite = 20;
                var robotPaths = new List<PlcRobotAction>();
                //获取RoofRecord
                var roofRecordEntity = await _roofRecordRepository.FindAsync(x => x.RoofId == input.RoofId);
                if (roofRecordEntity == null)
                {
                    throw new Exception($"编号为{input.RoofId} 的屋顶不存在");
                }

                //路径生成
                float minX = roofRecordEntity.RoofPoints.Select(p => p.X).Min();
                float maxX = roofRecordEntity.RoofPoints.Select(p => p.X).Max();
                var leftPoints = roofRecordEntity.RoofPoints.Where(p => p.X == minX && p.PointType == PointTypeEnum.ContourPoint).ToList();
                var rightPoints = roofRecordEntity.RoofPoints.Where(p => p.X == maxX && p.PointType == PointTypeEnum.ContourPoint).ToList();
                var midPoints = roofRecordEntity.RoofPoints.Where(p => p.PointType == PointTypeEnum.MidPoint).ToList();

                //判断是否存在中间点
                if (midPoints.Count == 0)
                {
                    //不存在中间点
                    var pathTransforms = PathCalculation(leftPoints, rightPoints);
                    robotPaths.Add(RobotAction.GetRobotLinTask(5, pathTransforms[0], velocity, overwrite));
                    robotPaths.Add(RobotAction.GetRobotLinTask(5, pathTransforms[1], velocity, overwrite));
                }
                else
                {
                    //存在中间点
                    //生成路径A->B
                    var pathTransforms1 = PathCalculation(leftPoints, midPoints);
                    robotPaths.Add(RobotAction.GetRobotLinTask(5, pathTransforms1[0], velocity, overwrite));
                    robotPaths.Add(RobotAction.GetRobotLinTask(5, pathTransforms1[1], velocity, overwrite));

                    //生成路径B->A
                    var pathTransforms2 = PathCalculation(midPoints,rightPoints);
                    robotPaths.Add(RobotAction.GetRobotLinTask(5, pathTransforms2[0], velocity, overwrite));
                    robotPaths.Add(RobotAction.GetRobotLinTask(5, pathTransforms2[1], velocity, overwrite));
                }

                //持久化关键数据到数据库,保存小数点后三位
                var robotPathData = GenerateRobotPathData(robotPaths,input.RoofId);
                robotPathData = robotPathData.OrderBy(p => p.Index).ToList();
                await _robotPathRepository.InsertManyAsync(robotPathData);

                //发送数据到PLC
                await _plcClient.SendPlcTasksToPlc(robotPaths);
            }
            catch (Exception ex)
            {
                throw new Exception("发送数据异常");
            }
        }


        private List<RobotPath> GenerateRobotPathData(List<PlcRobotAction> actions,string roofId)
        {
            List<RobotPath> paths = new List<RobotPath>();
            int index = 0;

            foreach (var action in actions)
            {
                var id = GuidGenerator.Create();
                RobotPath path = new RobotPath(id, roofId,index, action.ActionId, action.RobotId, action.ActionType, action.NextActionType,
                    action.ActionInfo.RobotMotionInfo.EndPoint.X,
                    action.ActionInfo.RobotMotionInfo.EndPoint.Y,
                    action.ActionInfo.RobotMotionInfo.EndPoint.Z,
                    action.ActionInfo.RobotMotionInfo.EndPoint.A,
                    action.ActionInfo.RobotMotionInfo.EndPoint.B,
                    action.ActionInfo.RobotMotionInfo.EndPoint.C,
                    action.ActionInfo.RobotMotionInfo.EndPoint.S,
                    action.ActionInfo.RobotMotionInfo.EndPoint.T,
                    action.ActionInfo.RobotMotionInfo.KukaMotionType,
                    action.ActionInfo.RobotMotionInfo.Velocity,
                    action.ActionInfo.RobotMotionInfo.OverWrite);

                index++;
                paths.Add(path);
            }

            return paths;
        }
        /// <summary>
        /// 标定原点坐标
        /// </summary>
        /// <returns></returns>
        private Transform GetOriginalTransform()
        {
            //原点坐标
            Vector3 originalPoint = new Vector3(-427, 1275, 1177);
            Transform originalTrasform = new Transform()
            {
                GlobalPosition = originalPoint,
                GlobalRotation = RobotController.ABCToQuat(0, 0.05f, -90.16f)
            };

            return originalTrasform;
        }


        //通过标定获得的角度值
        private float GetAngleByHeightDiff(float heightDiff)
        {
            float angle = 0;
            if (heightDiff == 0)
            {
                angle = 0;
            }
            else if (heightDiff > 0)
            {
                //angle = 1.1f;
                angle = 1.15f;
            }
            else
            {
                //angle = -0.94f;
                angle = -1.15f;
            }
            return angle;
        }

        private List<Transform> PathCalculation(List<RoofPoint> leftPoints, List<RoofPoint> rightPoints)
        {
            List<Transform> pathTransform = new List<Transform>();

            var leftHeightDiff = HeightDiff(leftPoints);
            var rightHeightDiff = HeightDiff(rightPoints);

            //如果左右侧高度差相等，走直线运动
            if (leftHeightDiff == rightHeightDiff)
            {
                pathTransform = GenerateStraightPathTransform(leftPoints, rightPoints, leftHeightDiff);
            }
            else
            {
                //不偏角度走斜线运动
                if (leftHeightDiff == 0)
                {
                    //从左往右斜线运动
                    pathTransform = GenerateSlashPathTransform(leftPoints, rightPoints, PathDirectionEnum.LeftToRight);

                }
                else if (rightHeightDiff == 0)
                {
                    //从右往左斜线运动
                    pathTransform = GenerateSlashPathTransform(rightPoints, leftPoints, PathDirectionEnum.RightToleft);
                }
                else
                {
                    //暂时不考虑这种情况
                }
            }

            return pathTransform;
        }

        private float HeightDiff(List<RoofPoint> points)
        {
            var yMax = points.Select(p => p.Y).Max();
            var yMin = points.Select(p => p.Y).Min();
            var zTop = points.Where(p => p.Y == yMax).Select(p => p.Z).FirstOrDefault();
            var zBottom = points.Where(p => p.Y == yMin).Select(p => p.Z).FirstOrDefault();
            var diff = zTop - zBottom;
            return diff;
        }

        /// <summary>
        /// 直线运动路径生成
        /// </summary>
        /// <param name="leftPoints"></param>
        /// <param name="rightPoints"></param>
        /// <param name="heightDiff"></param>
        /// <returns></returns>
        private List<Transform> GenerateStraightPathTransform(List<RoofPoint> leftPoints, List<RoofPoint> rightPoints, float heightDiff)
        {
            List<Transform> pathTransforms = new List<Transform>();

            var originalTransform = GetOriginalTransform();
            var angle = GetAngleByHeightDiff(heightDiff);

            var startPointsAveHeight = leftPoints.Select(p => p.Z).Average();
            var endPointsAveHeight = rightPoints.Select(p => p.Z).Average();
            var startPoint = new Vector3(leftPoints[0].X, 0,startPointsAveHeight);
            Transform st = new Transform()
            {
                Parent = originalTransform,
                LocalPosition = startPoint,
                LocalRotation = RobotController.ABCToQuat(0, 0, angle)
            };
            pathTransforms.Add(st);

            var endPoint = new Vector3(rightPoints[0].X, 0, endPointsAveHeight);
            Transform et = new Transform()
            {
                Parent = originalTransform,
                LocalPosition = endPoint,
                LocalRotation = RobotController.ABCToQuat(0, 0, angle)
            };
            pathTransforms.Add(et);

            return pathTransforms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"> 起点坐标</param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private List<Transform> GenerateSlashPathTransform(List<RoofPoint> startPoints, List<RoofPoint> endPoints, PathDirectionEnum direction)
        {
            List<Transform> pathTransforms = new List<Transform>();
            var offSet = new Vector3();
            var originalTransform = GetOriginalTransform();
            var slashDirection = GetSlashDirection(startPoints, endPoints);
            //起点坐标
            var startPoint = new Vector3(startPoints[0].X, 0, startPoints[0].Z);
            Transform st = new Transform()
            {
                Parent = originalTransform,
                LocalPosition = startPoint,
                LocalRotation = RobotController.ABCToQuat(0, 0, 0)
            };
            pathTransforms.Add(st);

            //从左往右
            if (direction == PathDirectionEnum.LeftToRight)
            {
                offSet += new Vector3(1200, 0, 0);
            }
            if (direction == PathDirectionEnum.RightToleft)
            {
                offSet += new Vector3(-1200, 0, 0);
            }

            //向上斜
            if (slashDirection == SlashDirectionEnum.Up)
            {
                offSet += new Vector3(0, 0, 24);
            }
            //向下斜
            if (slashDirection == SlashDirectionEnum.Down)
            {
                offSet += new Vector3(0, 0, -24);
            }

            //终点坐标
            var endPoint = new Vector3(endPoints[0].X, 0, endPoints[0].Z);
            Transform et = new Transform()
            {
                Parent = originalTransform,
                LocalPosition = endPoint + offSet,
                LocalRotation = RobotController.ABCToQuat(0, 0, 0)
            };
            pathTransforms.Add(et);

            return pathTransforms;
        }

        private SlashDirectionEnum GetSlashDirection(List<RoofPoint> startPoints, List<RoofPoint> endPoints)
        {
            var slashDirection = SlashDirectionEnum.Unknown;
            //确定是向上斜还是向下斜
            var startPointsHeight = startPoints.Select(p => p.Z).Average();
            var endPointsHeight = endPoints.Select(p => p.Z).Average();
            if (endPointsHeight > startPointsHeight)
            {
                slashDirection = SlashDirectionEnum.Up;
            }
            else
            {
                slashDirection = SlashDirectionEnum.Down;
            }

            return slashDirection;
        }

        //安全检查
    }
}

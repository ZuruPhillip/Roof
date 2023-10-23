using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZURU.Roof.Roofs
{
    public class RoofRecordAppService : RoofAppService, IRoofRecordAppService
    {
        private readonly IRoofRecordRepository _roofRecordRepository;

        public RoofRecordAppService(IRoofRecordRepository roofRecordRepository)
        {
            _roofRecordRepository = roofRecordRepository;
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
                    RoofPoint rp = new RoofPoint(id, pointIndex, PointType.ContourPoint, cp.X, cp.Y, cp.Z);
                    pointIndex++;
                    roofPoints.Add(rp);
                }
                foreach (var mp in input.MidPoints)
                {
                    var id = GuidGenerator.Create();
                    RoofPoint cp = new RoofPoint(id, pointIndex, PointType.MidPoint, mp.X, mp.Y, mp.Z);
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
    }
}

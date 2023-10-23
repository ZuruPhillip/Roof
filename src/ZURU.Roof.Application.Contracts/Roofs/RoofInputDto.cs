using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace ZURU.Roof.Roofs
{
    public class RoofInputDto : EntityDto<Guid>
    {
        //屋顶编码
        public string RoofId { get; set; }
        //轮廓点
        public List<PointDto> ContourPoints { get; set; }
        //中间点
        public List<PointDto> MidPoints { get; set; }
    }
}

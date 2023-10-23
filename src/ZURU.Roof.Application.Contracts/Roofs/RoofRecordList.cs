using System;
using System.Collections.Generic;
using System.Text;

namespace ZURU.Roof.Roofs
{
    public class RoofRecordList
    {
        //屋顶编码
        public string RoofId { get; set; }
        //轮廓点
        public List<PointDto> ContourPoints { get; set; }
        //中间点
        public List<PointDto> MidPoints { get; set; }
    }
}

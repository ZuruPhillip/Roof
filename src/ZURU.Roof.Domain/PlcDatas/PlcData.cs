using System;
using Volo.Abp.Domain.Entities;

namespace ZURU.Roof.PlcDatas
{
    public class PlcData : Entity<long>
    {
        public string RoofId { get; private set; }
        public string NodeId { get; private set; }

        public string Value { get; private set; }
        public DateTime CreateTime { get; private set; }

        protected PlcData() { }

        public PlcData(string roofId, string nodeId, string value)
        {
            RoofId = roofId;
            NodeId = nodeId;
            Value = value;
            CreateTime = DateTime.Now;
        }
    }
}

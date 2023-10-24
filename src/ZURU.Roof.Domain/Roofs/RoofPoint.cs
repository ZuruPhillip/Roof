using System;
using Volo.Abp.Domain.Entities;

namespace ZURU.Roof.Roofs
{
    public class RoofPoint : Entity<Guid>
    {
        
        public virtual Guid RecordId { get; private set; }
        //点下标
        public virtual int PointIndex { get; private set; }
        //点类型
        public virtual PointTypeEnum PointType { get; private set; }
        public virtual float X { get; private set; }
        public virtual float Y { get; private set; }
        public virtual float Z { get; private set; }

        public virtual RoofRecord RoofRecord { get; set; }

        public RoofPoint(Guid id,int pointIndex, PointTypeEnum pointType, float x, float y, float z) : base(id)
        {
            PointIndex = pointIndex;
            PointType = pointType;
            X = x;
            Y = y;
            Z = z;
        }

        protected RoofPoint() { }
    }
}

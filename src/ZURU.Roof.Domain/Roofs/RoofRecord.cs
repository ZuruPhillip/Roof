using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ZURU.Roof.Roofs
{
    public class RoofRecord : AuditedAggregateRoot<Guid>
    {
        //RoofId
        public virtual string RoofId { get;private set; }
        //屋顶
        public virtual List<RoofPoint> RoofPoints { get;private set; }
        //当前点是否可用
        public virtual bool Status { get; private set; }

        public RoofRecord(string roofId, List<RoofPoint> roofPoints, bool status)
        {
            RoofId = roofId;
            RoofPoints = roofPoints;
            Status = status;
        }

        protected RoofRecord() { }
    }
}

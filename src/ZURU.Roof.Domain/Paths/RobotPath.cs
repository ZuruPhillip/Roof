using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using ZURU.Roof.Robots;

namespace ZURU.Roof.Paths
{
    public class RobotPath : Entity<Guid>
    {
        public virtual string RoofId { get; private set; }
        public virtual int Index { get; private set; }
        public virtual string? ActionId { get; private set; }
        public virtual int RobotId { get; private set; }
        public virtual ActionType ActionType { get; private set; }
        public virtual NextActionType NextActionType { get; private set; }
        public virtual float X { get; private set; }
        public virtual float Y { get; private set; }
        public virtual float Z { get; private set; }
        public virtual float A { get; private set; }
        public virtual float B { get; private set; }
        public virtual float C { get; private set; }
        public virtual int S { get; private set; }
        public virtual int T { get; private set; }
        public virtual KukaMotionType KukaMotionType { get; private set; }
        public virtual int Velocity { get; private set; }
        public virtual int OverWrite { get; private set; }
        public virtual DateTime CreateTime { get; private set; }

        protected RobotPath()
        {

        }

        public RobotPath(Guid id,string roofId,int index,string actionId, int robotId, ActionType actionType, NextActionType nextActionType, float x, float y, float z, float a, float b, float c, int s, int t, KukaMotionType kukaMotionType, int velocity, int overWrite) : base(id)
        {
            RoofId = roofId;
            Index = index;
            ActionId = actionId;
            RobotId = robotId;
            ActionType = actionType;
            NextActionType = nextActionType;
            X = x;
            Y = y;
            Z = z;
            A = a;
            B = b;
            C = c;
            S = s;
            T = t;
            KukaMotionType = kukaMotionType;
            Velocity = velocity;
            OverWrite = overWrite;
            CreateTime = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof.Robots
{
    public class RobotMotionInfo
    {
        public CoordinateSystem CoordinateSystem { get; set; }
        public KukaMotionType KukaMotionType { get; set; }
        public E6POS EndPoint { get; set; }
        public E6POS AuxiliaryPoint { get; set; }
        public float Angle { get; set; }
        public int Velocity { get; set; }
        public int OverWrite { get; set; }
        public int LinAcceleration { get; set; }
        public bool InprocessFlag { get; set; }
    }
}

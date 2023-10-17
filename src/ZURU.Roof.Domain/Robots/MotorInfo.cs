using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof.Robots
{
    public class MotorInfo
    {
        public int MotorId { get; set; }
        public bool Status { get; set; }
        public bool RotateDirection { get; set; }
        public float Speed { get; set; }
    }
}

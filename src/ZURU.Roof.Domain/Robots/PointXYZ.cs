using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZURU.Roof.Utility.Maths;

namespace ZURU.Roof.Robots
{
    public sealed class PointXYZ
    {
        private readonly int roundDigits = 1;
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        internal Vector3 ToVector3() =>
            new Vector3(X.Round(roundDigits), Y.Round(roundDigits), Z.Round(roundDigits));
    }
}

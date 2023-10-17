using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof.Robots
{
    public class E6POS
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }
        public int S { get; set; }
        public int T { get; set; }

        public override string ToString()
        {
            return "X : " + X + " Y : " + Y + " Z : " + Z + " A : " + A + " B : " + B + " C : " + C;
        }
    }
}

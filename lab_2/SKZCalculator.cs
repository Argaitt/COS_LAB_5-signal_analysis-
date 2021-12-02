using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace lab_2
{
    class SKZCalculator
    {
        public double calcSKZ(PointPairList points)
        {
            double SKZ = 0;
            foreach (var point in points)
            {
                SKZ += Math.Pow(point.Y, 2);
            }
            return SKZ / (points.Count + 1) ;
        }
        public double caclSKZError(double SKZ)
        {
            return 0.708 - SKZ;
        }
    }
}

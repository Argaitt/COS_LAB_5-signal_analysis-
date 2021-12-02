using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace lab_2
{
    class SignalGenerator
    {
        public PointPairList generateSignal(int N, int globalN)
        {
            PointPairList points = new PointPairList();
            for (int i = 0; i <= N - 1; i++)
            {
                points.Add(i, Math.Sin(2 * Math.PI * i / N));
            }
            return points;
        }
        public PointPairList generateSignalWithPhase(int N, int globalN)
        {
            PointPairList points = new PointPairList();
            for (int i = 0; i <= N - 1; i++)
            {
                points.Add(i, Math.Sin(2 * Math.PI * i / N + Math.PI / 4));
            }
            return points;
        }
    }
}

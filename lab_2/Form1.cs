using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace lab_2
{
    public partial class Form1 : Form
    {
        SpecterCalculator specter = new SpecterCalculator();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region инициализация объектов
            SignalGenerator signalGenerator = new SignalGenerator();
            GraphDrawer graphDrawer = new GraphDrawer();
            List<PointPairList> signalList = new List<PointPairList>();
            List<PointPairList> signalListPhase = new List<PointPairList>();
            SKZCalculator skzCalculator = new SKZCalculator();
            List<double> skzList = new List<double>();
            PointPairList errDep = new PointPairList();
            PointPairList errDepPhase = new PointPairList();
            int N = 256, K = 3 * N / 4;
            #endregion

            #region генерация и отображение сигналов
            for (int i = K; i < N; i++)
            {
                signalList.Add(signalGenerator.generateSignal(i, N));
                signalListPhase.Add(signalGenerator.generateSignalWithPhase(i, N));
                i += 3;
            }
            for (int i = 0; i < signalList.Count; i++)
            {
                graphDrawer.drawSignal(signalList[i], zedGraphControl1, "signal " + (i + 1).ToString(), Color.Black);
                graphDrawer.drawSignal(signalListPhase[i], zedGraphControl3, "signal " + (i + 1).ToString(), Color.Black);
            }
            #endregion
            #region вычисление СКЗ и погрешности СКЗ, вычисление А и погрешности А, отображение СКЗ и погрешности СКЗ на форме, отображение А и погрешности А на форме
            listBox1.Items.Add("Сигнал          СКЗ                                 Погрешность СКЗ         А");
            listBox2.Items.Add("Сигнал          СКЗ                                 Погрешность СКЗ         А");
            for (int i = 0; i < signalList.Count; i++)
            {
                listBox1.Items.Add("signal " + i.ToString() + "       " + skzCalculator.calcSKZ(signalList[i]).ToString() + "         " + skzCalculator.caclSKZError(skzCalculator.calcSKZ(signalList[i]))
                    +  "         " + Math.Round(specter.dpf(signalList[i]).specterA.Max()).ToString() + "       "+ ((1 - specter.dpf(signalList[i]).specterA.Max())));
                listBox2.Items.Add("signal " + i.ToString() + "       " + skzCalculator.calcSKZ(signalListPhase[i]).ToString() + "         " + skzCalculator.caclSKZError(skzCalculator.calcSKZ(signalListPhase[i]))
                    + "         " + Math.Round(specter.dpf(signalListPhase[i]).specterA.Max()).ToString() + "       " + ((1 - specter.dpf(signalListPhase[i]).specterA.Max())));
            }
            #endregion
            #region построение графика зависимости погрешности от значения M
            for (int i = 0; i < signalList.Count; i++)
            {
                errDep.Add(signalList[i].Count, skzCalculator.caclSKZError(skzCalculator.calcSKZ(signalList[i])));
                errDepPhase.Add(signalListPhase[i].Count, skzCalculator.caclSKZError(skzCalculator.calcSKZ(signalListPhase[i])));
            }
            graphDrawer.drawSignal(errDep, zedGraphControl2, "err", Color.Red);
            graphDrawer.drawSignal(errDepPhase, zedGraphControl4, "err", Color.Red);
            #endregion
        }

    }
    public class GraphDrawer 
    {
        bool clean = false;
        public GraphDrawer(bool clean) 
        {
            this.clean = clean;
        }
        public GraphDrawer()
        {
        }
        public void drawSignal(PointPairList points, ZedGraphControl zedGraphControl, string label, Color color) 
        {
            GraphPane pane = zedGraphControl.GraphPane;
            if (clean)
            {
                pane.CurveList.Clear();
            }
            LineItem curve = pane.AddCurve(label, points, color, SymbolType.None);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        public void drawSpecter(List<double> values, string label, ZedGraphControl zedGraphControl)
        {
            double[] valuseForDiagram = values.ToArray();
            GraphPane pane = zedGraphControl.GraphPane;
            if (clean)
            {
                pane.CurveList.Clear();
            }
            BarItem bar = pane.AddBar("specter", null, valuseForDiagram, Color.Red);
            pane.BarSettings.MinClusterGap = 0.0f;
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
    }

}

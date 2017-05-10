using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using ZedGraph;

namespace Neuro
{
    public partial class Form1 : Form
    {
        int worldHeight = 550, worldWidth = 500;
        public Form1()
        {
            InitializeComponent();
            zedGraphControl1.Width = worldWidth;
            zedGraphControl1.Height = worldHeight;
            World world = new World(worldWidth, worldHeight, 5);
            world.AddAgent(1, 1);
            world.AddAgent(1, 1);
            var semaphore = new SemaphoreSlim(0);
            var factory = new TaskFactory();
            var data = new Queue<KeyValuePair<List<Agent>,List<Food>>>();

            factory.StartNew(() =>
            {   
                while (true)
                {
                    Thread.Sleep(40);
                    data.Enqueue(new KeyValuePair<List<Agent>, List<Food>>(world.agents,world.foodList));
                    semaphore.Release();
                }
            });

            factory.StartNew(() =>
            {
                while (true)
                {
                    semaphore.Wait();
                    var item = data.Dequeue();
                    Draw(item);
                }
            });
        }

        private void Draw(KeyValuePair<List<Agent>, List<Food>> world)
        {
            GraphPane GP = new ZedGraphControl() { Width = worldWidth, Height = worldHeight }.GraphPane;
            zedGraphControl1.GraphPane = GP;
            PointPairList pointlist1 = new PointPairList();
            foreach (var item in world.Key)
            {
                pointlist1.Add(item.x, item.y);
            }
            PointPairList pointlist2 = new PointPairList();
            foreach (var item in world.Value)
            {
                pointlist2.Add(item.x, item.y);
            }
            LineItem myCurve = GP.AddCurve("Agents", pointlist1, Color.Red, SymbolType.Circle);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Fill.Color = Color.Red;
            myCurve.Symbol.Fill.Type = FillType.Solid;
            myCurve.Symbol.Size = 10;
            LineItem myCurve1 = GP.AddCurve("Food", pointlist2, Color.Blue, SymbolType.Circle);
            myCurve1.Line.IsVisible = false;
            myCurve1.Symbol.Fill.Color = Color.Blue;
            myCurve1.Symbol.Fill.Type = FillType.Solid;
            myCurve1.Symbol.Size = 10;
            GP.Legend.IsVisible = false;
            GP.XAxis.IsVisible = false;
            GP.YAxis.IsVisible = false;
            GP.Title.IsVisible = false;
            GP.Border.IsVisible = false;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}

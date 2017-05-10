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

namespace Neuro
{
    public partial class Form1 : Form
    {
        int worldHeight = 100, worldWidth = 100;
        public Form1()
        {
            InitializeComponent();
            var semaphore = new SemaphoreSlim(0);
            var factory = new TaskFactory();
            var queue = new Queue<KeyValuePair<List<Agent>,List<Food>>>();
            World world = new World(worldWidth, worldHeight, 10);

            factory.StartNew(() =>
            {   
                // поток производитель
                while (true)
                {
                    Thread.Sleep(40); // задержка, симуляция долгого производства
                    queue.Enqueue(new KeyValuePair<List<Agent>, List<Food>>(world.agents,world.foodList));
                    semaphore.Release(); // к семафору +1
                }
            });

            factory.StartNew(() =>
            {
                while (true)
                {
                    semaphore.Wait();
                    var item = queue.Dequeue();
                    Draw(item);
                }
            });
        }

        private void Draw(KeyValuePair<List<Agent>, List<Food>> world)
        {
            Bitmap bmp = new Bitmap(worldHeight, worldWidth);
            SolidBrush agentBrush = new SolidBrush(Color.Red);
            SolidBrush foodBrush = new SolidBrush(Color.Blue);
            Graphics formGraphics = Graphics.FromImage(bmp);
            foreach (var agent in world.Key)
            {
                formGraphics.FillEllipse(agentBrush, agent.x, agent.y, 10,10);
            }
            foreach (var food in world.Value)
            {
                formGraphics.FillEllipse(agentBrush, food.x, food.y, 5, 5);
            }
            pictureBox1.Image = bmp;
            agentBrush.Dispose();
            foodBrush.Dispose();
            formGraphics.Dispose();
        }
    }
}

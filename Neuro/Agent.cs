using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro
{
    class Agent
    {
        private double radiusOfView, timeToDeath;
        public double x { get; private set; }
        public double y { get; private set; }
        public Agent(World world, double timeToDeath, double radiusOfView)
        {
            Random rnd = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            this.x = rnd.NextDouble() * world.Width;
            this.y = rnd.NextDouble() * world.Height;
            this.timeToDeath = timeToDeath;
            this.radiusOfView = radiusOfView;
        }
        public double GetDistToAgent(Agent agent)
        {
            return GetDist(agent.x, x, agent.y, y);
        }

        public double GetDist(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }


    }
}

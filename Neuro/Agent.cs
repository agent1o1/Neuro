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
        public float x { get; private set; }
        public float y { get; private set; }
        public Agent(World world, double timeToDeath, double radiusOfView)
        {
            Random rnd = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            this.x = (float)rnd.NextDouble() * world.Width;
            this.y = (float)rnd.NextDouble() * world.Height;
            this.timeToDeath = timeToDeath;
            this.radiusOfView = radiusOfView;
        }
        public double GetDistToAgent(Agent agent)
        {
            return GetDist(agent.x, x, agent.y, y);
        }

        public double GetDist(float x1, float x2, float y1, float y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }


    }
}

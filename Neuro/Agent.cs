using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro
{
    class Agent
    {
        private double x, y, timeToDeath;//x and y is coordinats
        private const double view_radius = 20;
        public void AddAgentIntoWorld(World world, double timeToDeath)
        {

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

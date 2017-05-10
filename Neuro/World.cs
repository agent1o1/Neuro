using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Neuro
{
    class World
    {
        public double Width { get; private set; }
        public double Height { get; private set; }
        private List<Agent> agents = new List<Agent>();

        public World(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void AddAgent(double timeToDeath, double radiusOfView)
        {
            agents.Add(new Agent(this, timeToDeath, radiusOfView));
        }
    }
}
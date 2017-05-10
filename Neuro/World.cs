using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Neuro
{
    struct Food
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public Food(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class World
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Agent> agents { get; private set; }
        public List<Food> foodList { get; private set; }
        Random rnd = new Random((int)(DateTime.Now.Ticks % int.MaxValue));

        public World(int width, int height, int foodCount)
        {
            Width = width;
            Height = height;
            agents = new List<Agent>();
            foodList = new List<Food>();
            for (int i = 0; i < foodCount; i++)
            {
                foodList.Add(new Food((float)rnd.NextDouble() * width, (float)rnd.NextDouble() * height));
            }
        }

        public void AddAgent(double timeToDeath, double radiusOfView)
        {
            agents.Add(new Agent(this, timeToDeath, radiusOfView));
        }
    }
}
using System.Reflection;
using RobotWars.Data.Models.Interfaces;

namespace RobotWars.Data.Models
{
    public class Arena : IArena
    {
        public int arenaX { get; set; }
        public int arenaY { get; set; }
        public int id { get; set; }
        
        public bool lastUsed { get; set; }

        public Arena(int x, int y)
        {
            arenaX = x;
            arenaY = y;
            lastUsed = true;

        }

        public int getX()
        {
            return arenaX;
        }

        public int getY()
        {
            return arenaY;
        }

        public string showSize()
        {
            return "H: " + arenaX + " W: " + arenaY;
        }
    }
}
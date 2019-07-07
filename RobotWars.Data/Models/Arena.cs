using RobotWars.Data.Models.Interfaces;

namespace RobotWars.Data.Models
{
    public class Arena : IArena
    {
        public Arena(int x, int y)
        {
            arenaX = x >= 0 ? x : 3;
            arenaY = y >= 0 ? y : 3;
            lastUsed = true;
        }

        public bool lastUsed { get; set; }
        public int arenaX { get; set; }
        public int arenaY { get; set; }
        public int id { get; set; }
    }
}
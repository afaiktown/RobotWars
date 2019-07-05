using RobotWars.Data.Models.Enums;
using RobotWars.Data.Models.Interfaces;

//   N
//W    E
//   S
namespace RobotWars.Data.Models
{
    public class Location : ILocation
    {
        public int axisX { get; set; }
        public int axisY { get; set; }
        public Compass heading { get; set; }

        public Location(int x = 0, int y = 0, Compass h = Compass.NORTH)
        {
            axisX = x;
            axisY = y;
            heading = h;
        }
    }
}
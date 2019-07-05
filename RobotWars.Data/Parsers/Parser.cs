using RobotWars.Data.Models;
using RobotWars.Data.Models.Enums;

namespace RobotWars.Data.Parsers
{
    public static class Parser
    {
        public static Input getCommand(string[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return Input.ONE;
                case 2:
                    return Input.TWO;
                case 3:
                    return Input.THREE;
                
                default:
                    return Input.NOTIMPLEMENTED;
            }
        }
        
        public static Compass getDirection(string d)
        {
            switch (d)
            {
                case "W":
                    return Compass.WEST;
                case "E":
                    return Compass.EAST;
                case "N":
                    return Compass.NORTH;
                case "S":
                    return Compass.SOUTH;
            }
            return Compass.EAST;
        }
    }
}
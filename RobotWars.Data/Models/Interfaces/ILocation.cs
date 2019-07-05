using RobotWars.Data.Models.Enums;

namespace RobotWars.Data.Models.Interfaces
{
    public interface ILocation
    {
        int axisX { get; set; }
        int axisY { get; set; }
        Compass heading { get; set; }
    }
    
}
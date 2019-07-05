namespace RobotWars.Data.Models.Interfaces
{
    public interface IRobot
    {
        ILocation location { get; set; }
        IArena arena { get; set; }

        int id { get; set; }
    }
}
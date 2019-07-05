using System;
using RobotWars.Data.Models.Enums;
using RobotWars.Data.Models.Interfaces;

namespace RobotWars.Data.Models
{
    public class Robot : IRobot
    {
        public ILocation location { get; set; }
        public IArena arena { get; set; }

        public int id { get; set; }
        
        public bool finishedMovement { get; set; }

        public Robot(Arena a, int uid, int x = 0, int y = 0, Compass h = Compass.NORTH)
        {
            id = uid;
            location = new Location(x, y, h);
            arena = a;
            lastUsed = true;
            finishedMovement = true;
        }
        
        public bool lastUsed { get; set; }

        public string getPos()
        {
            return String.Format("{0} {1} {2}", location.axisX, location.axisY, (char)location.heading);
        }
        
        public int getX()
        {
            return location.axisX;
        }

        public int getY()
        {
            return location.axisY;
        }

        public IArena getArena()
        {
            return arena;
        }

        public void makeMove()
        {
            switch (location.heading)
            {
                case Compass.NORTH:
                    if(location.axisY != arena.arenaY)
                        location.axisY += 1;
                    break;
                case Compass.WEST:
                    if (location.axisX != 0)
                        location.axisX -= 1;
                    break;
                case Compass.SOUTH:
                    if (location.axisY != 0)
                        location.axisY -= 1;
                    break;
                case Compass.EAST:
                    if (location.axisX != arena.arenaX)
                        location.axisX += 1;
                    break;
                
                
            }
        }

        public void rotateLeft()
        {
            switch (location.heading)
            {
                case Compass.NORTH:
                    location.heading = Compass.WEST;
                    break;
                case Compass.WEST:
                    location.heading = Compass.SOUTH;
                    break;
                case Compass.SOUTH:
                    location.heading = Compass.EAST;
                    break;
                case Compass.EAST:
                    location.heading = Compass.NORTH;
                    break;
            }
        }
        
        public void rotateRight()
        {
            switch (location.heading)
            {
                case Compass.NORTH:
                    location.heading = Compass.EAST;
                    break;
                case Compass.EAST:
                    location.heading = Compass.SOUTH;
                    break;
                case Compass.SOUTH:
                    location.heading = Compass.WEST;
                    break;
                        
                case Compass.WEST:
                    location.heading = Compass.NORTH;
                    break;
            }
        }
    }
}
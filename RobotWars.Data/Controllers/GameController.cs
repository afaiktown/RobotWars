using System;
using System.Linq;
using System.Text.RegularExpressions;
using RobotWars.Data.Models;
using RobotWars.Data.Models.Enums;
using RobotWars.Data.Parsers;
using RobotWars.Data.Repositories;
using RobotWars.Data.Utils;

namespace RobotWars.Data.Controllers
{
    public static class GameController
    {
        private static IArenaRepository _arenaRepository = new ArenaRepository();
        private static IRobotRepository _robotRepository = new RobotRepository();
        
        private static readonly string arenaCommand = @"^\d+\s+\d$";
        private static readonly string moveCommand = @"^[m|M|r|R|l|L]+$";
        private static readonly string robotCommand = @"^\d+\s+\d+\s+[n|w|e|s|N|W|E|S]$";
        
        public static bool process(string[] parameters)
        {
            Input inputCmd = Parser.getCommand(parameters);

            switch (inputCmd)
            {
                case Input.ONE:
                    //Movement
                    if(!doMovement(parameters[0])) {
                        Console.WriteLine(SysMsg.ROBOT_NOT_INITIALIZED);
                    }
                    break;
                case Input.TWO:
                    //New arena
                    if (!createArena(parameters)) {
                        Console.WriteLine(SysMsg.CREATE_ARENA_FAILED);
                    }
                    break;
                case Input.THREE:
                    //initial placement
                    if (!placeRobot(parameters)) {
                        Console.WriteLine(SysMsg.PLACE_ROBOT_FAILED);
                    }
                    break;
            }
            
            return true;
        }

        private static int getAvailableRobotId()
        {
            return _robotRepository.Get().Count() + 1;
        }

        public static bool doMovement(string parameters)
        {
            Robot activeRobot = _robotRepository.Get().Count() > 0 ? _robotRepository.Get().First(r => r.lastUsed) : null;
            
            if (Regex.IsMatch(parameters, moveCommand) && activeRobot != null && activeRobot.finishedMovement)
            {
                activeRobot.finishedMovement = false;
                
                foreach (char c in parameters.ToUpper())
                {
                    switch (c)
                    {
                        case 'R':
                            activeRobot.rotateRight();
                            break;
                        case 'L':
                            activeRobot.rotateLeft();
                            break;
                        case 'M':
                                
                                activeRobot.makeMove();
                            break;
                    }
                }

                activeRobot.finishedMovement = true;

                Console.WriteLine(activeRobot.getPos());
                return true;
            }
            
            return false;
        }

        public static bool createArena(string[] parameters)
        {
            Arena checkArena = _arenaRepository.Get().Count() > 0 ? _arenaRepository.Get().First(r => r.lastUsed) : null;
                    
            if (checkArena != null)
                checkArena.lastUsed = false;
                    
                    
            if (Regex.IsMatch(String.Join(" ", parameters), arenaCommand))
            {
                _arenaRepository.Add(new Arena(Convert.ToInt32(parameters[0]), Convert.ToInt32(parameters[1])));
                return true;
            }

            return false;
        }

        public static bool placeRobot(string[] parameters)
        {
            Arena availableArena = _arenaRepository.Get().Count() > 0 ? _arenaRepository.Get().First(a => a.lastUsed) : null;
            Robot activeRobot = _robotRepository.Get().Count() > 0 ? _robotRepository.Get().First(r => r.lastUsed && r.finishedMovement) : null;
                    
            if (activeRobot != null)
                activeRobot.lastUsed = false;

            if (Regex.IsMatch(String.Join(" ", parameters), robotCommand) && availableArena != null)
            {
                int generatedId = getAvailableRobotId();
                Robot newRobot = new Robot(availableArena, generatedId, Convert.ToInt32(parameters[0]),
                    Int32.Parse(parameters[1]), Parser.getDirection(parameters[2]));
                _robotRepository.Add(newRobot);

                return true;
            }

            return false;
        }
    }

}
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
        private static readonly IArenaRepository _arenaRepository = new ArenaRepository();
        private static readonly IRobotRepository _robotRepository = new RobotRepository();

        private static readonly string arenaCommand = @"^\d+\s+\d$";
        private static readonly string moveCommand = @"^[m|M|r|R|l|L]+$";
        private static readonly string robotCommand = @"^\d+\s+\d+\s+[n|w|e|s|N|W|E|S]$";

        public static void process(string[] parameters)
        {
            var inputCmd = Parser.getCommand(parameters);

            switch (inputCmd)
            {
                case Input.ONE:
                    //Movement
                    if (!doMovement(parameters[0])) Console.WriteLine(SysMsg.ROBOT_NOT_INITIALIZED);
                    break;
                case Input.TWO:
                    //New arena
                    if (!createArena(parameters)) Console.WriteLine(SysMsg.CREATE_ARENA_FAILED);
                    break;
                case Input.THREE:
                    //initial placement
                    if (!placeRobot(parameters)) Console.WriteLine(SysMsg.PLACE_ROBOT_FAILED);
                    break;
            }
        }

        private static int getAvailableRobotId()
        {
            return _robotRepository.Get().Count() + 1;
        }

        public static bool doMovement(string parameters)
        {
            var activeRobot = _robotRepository.Get().Count() > 0 ? _robotRepository.Get().First(r => r.lastUsed) : null;

            if (Regex.IsMatch(parameters, moveCommand) && activeRobot != null && activeRobot.finishedMovement)
            {
                activeRobot.finishedMovement = false;

                foreach (var c in parameters.ToUpper())
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

                Console.WriteLine(activeRobot.getPos());
                switchRobots(activeRobot);
                return true;
            }

            return false;
        }

        public static bool createArena(string[] parameters)
        {
            var checkArena = _arenaRepository.Get().Count() > 0 ? _arenaRepository.Get().First(r => r.lastUsed) : null;

            if (checkArena != null)
                checkArena.lastUsed = false;


            if (Regex.IsMatch(string.Join(" ", parameters), arenaCommand))
            {
                _arenaRepository.Add(new Arena(Convert.ToInt32(parameters[0]), Convert.ToInt32(parameters[1])));
                return true;
            }

            return false;
        }

        public static bool placeRobot(string[] parameters)
        {
            var availableArena = _arenaRepository.Get().Count() > 0
                ? _arenaRepository.Get().First(a => a.lastUsed)
                : null;
            var activeRobot = _robotRepository.Get().Count() > 0
                ? _robotRepository.Get().First(r => r.lastUsed && r.finishedMovement)
                : null;

            if (activeRobot != null)
                activeRobot.lastUsed = false;

            if (Regex.IsMatch(string.Join(" ", parameters), robotCommand) && availableArena != null)
            {
                var generatedId = getAvailableRobotId();

                var correctX = Convert.ToInt32(parameters[0]) <= availableArena.arenaX
                    ? Convert.ToInt32(parameters[0])
                    : availableArena.arenaX;

                var correctY = Convert.ToInt32(parameters[1]) <= availableArena.arenaY
                    ? Convert.ToInt32(parameters[1])
                    : availableArena.arenaY;

                var newRobot = new Robot(availableArena, generatedId, correctX,
                    correctY, Parser.getDirection(parameters[2]));
                _robotRepository.Add(newRobot);

                return true;
            }

            return false;
        }

        public static void switchRobots(Robot robot)
        {
            var newRobot = _robotRepository.Get().Count() > robot.id
                ? _robotRepository.Get().FirstOrDefault(r => r.id > robot.id)
                : _robotRepository.Get().FirstOrDefault();

            robot.lastUsed = false;
            robot.finishedMovement = true;

            if (newRobot != null)
            {
                newRobot.lastUsed = true;
                newRobot.finishedMovement = true;
            }
        }
    }
}
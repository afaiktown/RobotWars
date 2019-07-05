using System;
using RobotWars.Data.Controllers;

namespace RobotWars.Game
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                string[] cmds = Console.ReadLine().Split(' ');

                if (cmds[0].Equals("q"))
                    break;

                GameController.process(cmds);
            }

        }
    }
}

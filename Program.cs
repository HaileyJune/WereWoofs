using System;

namespace WereWoofs
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerManager Game = new PlayerManager();

            System.Console.WriteLine("Add Players");


            while (Game.allPlayers.Count < 8)
            {
                Game.AddPlayer();
            }
            string Boop = null;
            while (Boop != "start")
            {
                System.Console.WriteLine("Enter 'start' to play or add another player:");
                Boop = Console.ReadLine();
                if (Boop != "start"){
                    Game.AddPlayer(Boop);
                }
            }

            System.Console.Clear();
            System.Console.WriteLine("Game Starting");
            Game.AssignRoles();

            //first night
            while(Game.Q.Count>0)
            {
                Player currentPlayer = Game.NextPlayer();
                System.Console.WriteLine("{0}, your role is: {1}", currentPlayer.name, currentPlayer.role);
                System.Console.WriteLine("Type 'yay' or 'nay' to tell the void how you feel about that.");
                Console.ReadLine();
                System.Console.Clear();
            }

            System.Console.WriteLine("End of what I've written.");
        }
    }
}

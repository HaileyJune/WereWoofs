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

            System.Console.WriteLine("End of night one.");
            System.Console.WriteLine("Read aloud:");
            System.Console.WriteLine("Yeah, we're jumping into night one");
            //figure out order stuff

            while (Game.livingTownsfolk.Count > 0 && Game.livingWoofs.Count > 0)
            {
            Game.ShuffleQ();
            //night
            while(Game.Q.Count>0)
            {
                Player currentPlayer = Game.NextPlayer();
                if (currentPlayer.role == "Woof")
                {
                    Game.WoofVote();
                }
                else if (currentPlayer.role == "Seer")
                {
                    Game.SeerVote();
                }
                else
                {
                    Game.VillageVote();
                }
            }
            Game.NightEnd();
            //day
            System.Console.WriteLine("Last night we lost {0} to the woofs.", Game.lastDead.name);
            System.Console.WriteLine("Let the lynching begin!");
            Console.ReadLine();


            Game.ShuffleQ();
            while(Game.Q.Count>0)
            {
                Player currentPlayer = Game.NextPlayer();
                Game.LynchVote();
            }

            Game.DayEnd();
            System.Console.WriteLine("You killed {0}, hope that's who you meant to kill!", Game.lastDead.name);
            if (Game.lastDead.team == "Woofs")
            {
                System.Console.WriteLine("They were a woof! Good job!");
                Console.ReadLine();

            }
            else
            {
                System.Console.WriteLine("They were a villager! Bad job!");
                Console.ReadLine();

            }
            }
        }
    }
}

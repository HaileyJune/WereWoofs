using System;

namespace WereWoofs
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerManager Game = new PlayerManager();
            Console.Clear();

            //Game Setup

            System.Console.WriteLine(@"                     .");
            System.Console.WriteLine(@"                    / V\");
            System.Console.WriteLine(@"                  / `  /");
            System.Console.WriteLine(@"                 <<   |");
            System.Console.WriteLine(@"                 /    |");
            System.Console.WriteLine(@"               /      |");
            System.Console.WriteLine(@"             /        |");
            System.Console.WriteLine(@"           /    \  \ /");
            System.Console.WriteLine(@"          (      ) | |");
            System.Console.WriteLine(@"  ________|   _/_  | |");
            System.Console.WriteLine(@"<__________\______)\__)");
            System.Console.WriteLine("*************************");
            System.Console.WriteLine(" * Welcome to WereWoof *");
            System.Console.WriteLine("*************************");
            
            Console.ReadLine();
            Console.Clear();

            System.Console.WriteLine("Add Players");

            //loop to add players

            while (Game.allPlayers.Count < 8)
            {

                Game.AddPlayer();
            }
            string Boop = null; //user imput
            //loop to add players past 8
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
            //backend role assignment
            Game.AssignRoles();

            //first night
            while(Game.Q.Count>0)
            {
                Player currentPlayer = Game.NextPlayer();
                //tell player their role
                System.Console.WriteLine("{0}, your role is: {1}", currentPlayer.name, currentPlayer.role);
                //tell woofs who the other woofs are
                if(currentPlayer.team == "Woofs")
                {
                    System.Console.WriteLine("Team Woof:");
                    foreach (var woof in Game.livingWoofs)
                    {
                        System.Console.WriteLine(woof.name);
                    }

                    
                }
                //pick odd message to display
                string[] sassyMessages = 
                {
                    "Type 'yay' or 'nay' to tell the void how you feel about that.",
                    "Now, enter your favorite color.",
                    "Tell the void: Do you like cats or dogs?",
                    "Tell the void: Sweet or savory?"
                };
                Random rand = new Random();
                int ind = rand.Next(sassyMessages.Length);
                System.Console.WriteLine(sassyMessages[ind]);
                Console.ReadLine();
                System.Console.Clear();
            }
            System.Console.WriteLine("Read aloud:");
            System.Console.WriteLine("Yeah, we're jumping into night one");
            Console.ReadLine();

            while (Game.livingTownsfolk.Count > 0 && Game.livingWoofs.Count > 0)
            {
            Game.ShuffleQ();
            //night
                System.Console.WriteLine("And so night falls again...");
                Console.ReadLine();
                Console.Clear();
            //for each player alive
            while(Game.Q.Count>0)
            {
                Player currentPlayer = Game.NextPlayer();
                    //Woof turn
                    if (currentPlayer.role == "Woof")
                    {
                        if (Game.woofVotes.Count > 0)
                        {
                            System.Console.WriteLine("Other woofs have voted to eat:");
                            foreach (var vote in Game.woofVotes)
                            {
                                System.Console.WriteLine(vote.name);
                                Console.ReadLine();
                            }
                        }
                    Game.WoofVote();
                }
                //seer turn
                else if (currentPlayer.role == "Seer")
                {
                    Game.SeerVote();
                }
                //villager turn
                else
                {
                    Game.VillageVote();
                }
            }
            Game.NightEnd();
            //day
            Console.Clear();
            System.Console.WriteLine("Last night we lost {0} to the woofs.", Game.lastDead.name);
            System.Console.WriteLine("The void recommends nominating a few suspicious villagers before heading to the voting phase.");
            System.Console.WriteLine("Let the lynching begin!");
            Console.ReadLine();


            Game.ShuffleQ();
            //lynching
            while(Game.Q.Count>0)
            {
                Player currentPlayer = Game.NextPlayer();
                if (Game.VV.Count > 0)
                    {
                        System.Console.WriteLine("Other villagers have voted for:");
                        foreach (var vote in Game.VV)
                        {
                            System.Console.WriteLine(vote.Key.name);
                        }
                            Console.ReadLine();
                    }
                Game.LynchVote();
            }

            Game.DayEnd();
            System.Console.WriteLine("Read aloud:");
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

            //win conditions
            if (Game.livingWoofs.Count < 1)
            {
                System.Console.WriteLine("You killed the woofs! The village is safe!");
            }
            else
            {
                System.Console.WriteLine("The woofs have won!");
            }
                System.Console.WriteLine("Please play again!");
                Console.ReadLine();
        }
    }
}

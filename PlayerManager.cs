using System;
using System.Collections.Generic;
using System.Linq;

namespace WereWoofs
{
    class PlayerManager
    {
        public List<Player> livingPlayers;
        public List<Player> allPlayers;
        public List<Player> Q;
        public List<Player> livingWoofs;
        public List<Player> livingTownsfolk;
        public List<Player> woofVotes;
        public Dictionary<Player, Int64> VV;

        public Player lastDead;

        public PlayerManager()
        {
            allPlayers = new List<Player>();
            Q = new List<Player>();
            livingPlayers = new List<Player>();
            livingWoofs = new List<Player>();
            livingTownsfolk = new List<Player>();
            woofVotes = new List<Player>();
            VV = new Dictionary<Player, long>();
        }

        public void AddPlayer()
        {
            System.Console.WriteLine("Player Name:");
            String Boop = Console.ReadLine();
            if (Boop.Length < 1)
            {
                System.Console.WriteLine("Please enter a name.");
                return;
            }
            Player adding = new Player(Boop);
            allPlayers.Add(adding);
        }
        public void AddPlayer(String newName)
        {
            if (newName.Length < 1)
            {
                System.Console.WriteLine("Please enter a name.");
                return;
            }
            Player adding = new Player(newName);
            allPlayers.Add(adding);
        }

        public void AssignRoles()
        {
            int Woofs = 0;
            if (allPlayers.Count < 12 )
            {
                Woofs = 2;
            }
            else if (allPlayers.Count < 17 )
            {
                Woofs = 3;
            }
            else
            {
                Woofs = 4;
            }

            livingPlayers = new List<Player>(allPlayers);
            //assign woofs
            ShuffleQ();
            for (int i = 0; i < Woofs; i++)
            {
                Player RandomPlayer = Q[1];
                RandomPlayer.role = "Woof";
                RandomPlayer.team = "Woofs";
                livingWoofs.Add(RandomPlayer);
                Q.RemoveAt(1);
            }
            // assign seer
            Player Seer = Q[0];
            Seer.role = "Seer";

            foreach (var player in Q)
            {
                livingTownsfolk.Add(player);
            }
            // reset Q
            Q.Clear();
            ShuffleQ();
        }

        public void ShuffleQ()
        {
            Q = new List<Player>(livingPlayers);
            Random rand = new Random();
            for (int i = 0; i < Q.Count; i++)
            {
                int shuffle = rand.Next(Q.Count);
                Player temp = Q[shuffle];
                Q[shuffle] = Q[i];
                Q[i] = temp;
            }
        }

        public Player NextPlayer()
        {
            Player next = Q[0];
            Q.RemoveAt(0);
            System.Console.Clear();
            System.Console.WriteLine("*************************************************************");
            System.Console.WriteLine("*************************************************************");
            System.Console.WriteLine("*************************************************************");
            System.Console.WriteLine("********   This Screen is for {0}'s eyes only   ********", next.name);
            System.Console.WriteLine("*************************************************************");
            System.Console.WriteLine("*************************************************************");
            System.Console.WriteLine("*************************************************************");
            // System.Console.WriteLine("**********   {0}, enter your favorite number.   **********", next.name);
            // System.Console.WriteLine("*************************************************************");
            Console.ReadLine();
            Console.Clear();
            return next;
        }

        public void WoofVote()
        {
            Console.Clear();
                
            if (woofVotes.Count > 0)
            {
                System.Console.WriteLine("Other woofs have voted for:");
                foreach (var vote in woofVotes)
                {
                    System.Console.WriteLine(vote.name);
                }
            }
            System.Console.WriteLine();
            Player voted = Vote("Who do you vote to eat tonight?");
            System.Console.WriteLine();
            System.Console.WriteLine("I hope you enjoy your snack.");
            woofVotes.Add(voted);
            Console.ReadLine();
            Console.Clear();
        }
        public void LynchVote()
        {
        
            Console.Clear();
            Player voted = Vote("Who do you vote to lynch?");
            System.Console.WriteLine();
            System.Console.WriteLine("I hope you're happy.");

            try
            {
                VV.Add(voted, 1);
            }
            catch
            {
                VV[voted] = VV[voted] + 1;
            }

            Console.ReadLine();
            Console.Clear();
        }
        public void SeerVote()
        {
        
            Console.Clear();
            Player voted = Vote("Who would you like to know the identity of?");
            System.Console.WriteLine();

            if (voted.team == "Woofs")
            {
                System.Console.WriteLine("{0} is team WOOF", voted.name);
            }
            else
            {
                System.Console.WriteLine("{0} is a villager", voted.name);
            }
            System.Console.WriteLine();

            Console.ReadLine();
            Console.Clear();


        }
        public void VillageVote()
        {
        
            Console.Clear();
            //randomize lines
            Random rand = new Random();
            string[] message = 
            {
                "Tell the void who you are worried about",
                "Who would look the cutest with wolf ears?",
                "Who has the best hair?",
                "Who will be the next to die, in your opinion?",
            };
            int index = rand.Next(message.Length);
            Vote(message[index]);
            System.Console.WriteLine();
            System.Console.WriteLine("Wow, okay.");
            Console.ReadLine();
            Console.Clear();

        }

        public void NightEnd()
        {
            Random rand = new Random();
            int dead = rand.Next(woofVotes.Count);
            lastDead = woofVotes[dead];
            livingPlayers.Remove(lastDead);
            if(lastDead.team == "Woofs")
            {
                livingWoofs.Remove(lastDead);
            }
            if(lastDead.team == "Village")
            {
                livingTownsfolk.Remove(lastDead);
            }
            woofVotes.Clear();
        }
        public void DayEnd()
        {
            lastDead = VV.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            
            livingPlayers.Remove(lastDead);
            if(lastDead.team == "Woofs")
            {
                livingWoofs.Remove(lastDead);
            }
            if(lastDead.team == "Village")
            {
                livingTownsfolk.Remove(lastDead);
            }
            VV.Clear();
        }

        public Player Vote(String message)
        {
            short curItem = 0, c;
            ConsoleKeyInfo key;
            
            // Our array of Items for the menu (in order)
            List<string> votablePlayers = new List<string>();
            foreach (var player in livingPlayers)
            {
                votablePlayers.Add(player.name);
            }

            string[] menuItems = votablePlayers.ToArray();

                do
                {
                    Console.Clear();
                    // Clear the screen.  One could easily change the cursor position,
                    // but that won't work out well with tabbing out menu items.
                    
                    // Replace this with whatever you want.
                    Console.WriteLine();
                    Console.Write(message);
                    Console.WriteLine();

                
                    // The loop that goes through all of the menu items.
                    for (c = 0; c < menuItems.Length; c++)
                    {
                        // If the current item number is our variable c, tab out this option.
                        // You could easily change it to simply change the color of the text.
                        if (curItem == c)
                        {
                            Console.Write(">>");
                            Console.WriteLine(menuItems[c]);
                        }
                        // Just write the current option out if the current item is not our variable c.
                        else
                        {
                            Console.WriteLine(menuItems[c]);
                        }
                    }
                    // Waits until the user presses a key, and puts it into our object key.
                    Console.Write("Select your choice with the arrow keys.");
                    key = Console.ReadKey(true);

                    // Simply put, if the key the user pressed is a "DownArrow", the current item will deacrease.
                    // Likewise for "UpArrow", except in the opposite direction.
                    // If curItem goes below 0 or above the maximum menu item, it just loops around to the other end.
                    if (key.Key.ToString() == "DownArrow")
                    {
                        curItem++;
                        if (curItem > menuItems.Length - 1) curItem = 0;
                    }
                    else if (key.Key.ToString() == "UpArrow")
                    {
                        curItem--;
                        if (curItem < 0) curItem = Convert.ToInt16(menuItems.Length - 1);
                    }
                    // Loop around until the user presses the enter go.
                } while (key.KeyChar != 13);
                Player voted = livingPlayers.Find(x => x.name == menuItems[curItem]);
                return voted;

        }

    }
}
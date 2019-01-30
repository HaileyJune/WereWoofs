using System;
using System.Collections.Generic;

namespace WereWoofs
{
    class PlayerManager
    {
        public List<Player> livingPlayers;
        public List<Player> allPlayers;
        public List<Player> Q;
        public List<Player> livingWoofs;
        public List<Player> livingTownsfolk;

        public Player lastDead;

        public PlayerManager()
        {
            this.allPlayers = new List<Player>();
            this.livingPlayers = this.allPlayers;
            this.Q = this.allPlayers;
        }

        public void AddPlayer()
        {
            System.Console.WriteLine("Player Name:");
            String Boop = Console.ReadLine();
            Player adding = new Player(Boop);
            this.allPlayers.Add(adding);
        }
        public void AddPlayer(String newName)
        {
            Player adding = new Player(newName);
            this.allPlayers.Add(adding);
        }

        public void AssignRoles()
        {
            this.livingPlayers = this.allPlayers;
            this.Q = this.livingPlayers;
            int Townsfolk = 0;
            int Woofs = 0;
            if (this.allPlayers.Count < 12 )
            {
                Woofs = 2;
                Townsfolk = this.allPlayers.Count - Woofs;
            }
            else if (this.allPlayers.Count < 17 )
            {
                Woofs = 3;
                Townsfolk = this.allPlayers.Count - Woofs;
            }
            else
            {
                Woofs = 4;
                Townsfolk = this.allPlayers.Count - Woofs;
            }

            Random rand = new Random();
            //assign woofs
            ShuffleQ();
            for (int i = 0; i < Woofs; i++)
            {
                Player RandomPlayer = this.Q[0];
                RandomPlayer.role = "Woof";
                RandomPlayer.team = "Woofs";
                // this.livingWoofs.Add(RandomPlayer);
                this.Q.RemoveAt(0);
            }
            // assign seer
            Player Seer = this.Q[0];
            Seer.role = "Seer";

            foreach (var player in this.Q)
            {
                // this.livingTownsfolk.Add(player);
            }
            // reset Q
            this.Q = this.allPlayers;
            ShuffleQ();
        }

        public void ShuffleQ(){
            this.Q = this.livingPlayers;
            Random rand = new Random();
            for (int i = 0; i < this.Q.Count; i++)
            {
                int shuffle = rand.Next(this.Q.Count);
                Player temp = this.Q[shuffle];
                this.Q[shuffle] = this.Q[i];
                this.Q[i] = temp;
            }
        }

        public Player NextPlayer(){
            Player next = this.Q[0];
            this.Q.RemoveAt(0);
            System.Console.Clear();
            System.Console.WriteLine("****************************************");
            System.Console.WriteLine("***This Screen is for {0} eyes only***", next.name);
            System.Console.WriteLine("****************************************");
            System.Console.WriteLine("{0}, enter your favorite number.", next.name);
            Console.ReadLine();
            Console.Clear();
            return next;
        }
    }
}
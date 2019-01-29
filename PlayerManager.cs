using System;
using System.Collections.Generic;

namespace WereWoofs
{
    class PlayerManager
    {
        public int livingPlayers;
        public List<Player> allPlayers;
        public List<Player> Q;
        public int livingWoofs;
        public int livingTownsfolk;

        public PlayerManager()
        {
            livingPlayers = 0;
            allPlayers = new List<Player>();
            Q = new List<Player>();
        }

        public void AddPlayer(string newName)
        {
            Player adding = new Player(newName);
            allPlayers.Add(adding);
        }

        public void AssignRoles()
        {
            if (allPlayers.Count < 12 )
            {
                livingWoofs = 2;
                livingTownsfolk = allPlayers.Count - livingWoofs;
            }
            else if (allPlayers.Count < 17 )
            {
                livingWoofs = 3;
                livingTownsfolk = allPlayers.Count - livingWoofs;
            }
            else
            {
                livingWoofs = 4;
                livingTownsfolk = allPlayers.Count - livingWoofs;
            }

            Random rand = new Random();
            Q = allPlayers;
            //assign woofs
            for (int i = 0; i < livingWoofs; i++){
                int randy = rand.Next(Q.Count);
                Player RandomPlayer = Q[randy];
                RandomPlayer.role = "Woof";
                Q.RemoveAt(randy);
            }
            // assign seer
            Player Seer = Q[rand.Next(Q.Count)];
            Seer.role = "Seer";
            // reset Q
            Q = allPlayers;
        }
    }
}
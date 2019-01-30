using System;
using System.Collections.Generic;

namespace WereWoofs
{
    class Player
    {
        public string name;
        public string role;
        public string team;
        public bool isAlive;
        public List<Player> voteables;


        public Player(string myName)
        {
            name = myName;
            role = "Townsfolk";
            team = "Village";
            isAlive = true;
            voteables = new List<Player>();
        }


    }
}
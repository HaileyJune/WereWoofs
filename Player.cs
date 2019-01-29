using System;
using System.Collections.Generic;

namespace WereWoofs
{
    class Player
    {
        public string name;
        public string role;
        public bool isAlive;
        public List<Player> voteables;


        public Player(string myName)
        {
            name = myName;
            role = "Townsfolk";
            isAlive = true;
            voteables = new List<Player>();
        }


    }
}
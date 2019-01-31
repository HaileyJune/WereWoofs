using System;
using System.Collections.Generic;

namespace WereWoofs
{
    class Player
    {
        public string name;
        public string role;
        public string team;

        public Player(string myName)
        {
            name = myName;
            role = "Townsfolk";
            team = "Village";
        }


    }
}
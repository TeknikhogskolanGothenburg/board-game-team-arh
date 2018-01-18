using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Player
    {
        public string Name { get; set; }
        Boat[] Boats { get; }
        bool[] Trys { get; }

        public Player()
        {
        }

        public bool IsTryAlreadyDone(Position position)
        {
            return false;
        }

        public bool IsItAHit(Position position)
        {
            return false;
        }

        public bool HasPlayerLost()
        {
            return false;
        }
    }
}

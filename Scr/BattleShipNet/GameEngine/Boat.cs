using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Boat
    {
        public Position[] Positions { get;}
        public BoatType Type { get; }
        public Position[] Hits { get; }
        public bool Sink
        {
            get
            {
                return false;
            }
        }

        public Boat(BoatType newBoatType)
        {

        }

        public void SetPositions(Position[] position)
        {

        }

        public bool AreYouHere(Position position)
        {
            return false;
        }

        public bool IsItAHit(Position position)
        {
            return false;
        }
    }
}

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
        public Boat[] Boats { get;set; }
        //public bool[] Trys { get; set;}  
        public List<Position> AlreadyHitPositions{get;set;}
            
        public Player()
        {
            AlreadyHitPositions = new List<Position>();
            Boats = new Boat[10];
        }

        public void LoadBoats(Boat[] boats)
        {
            Boats=boats;
        }
        
        public bool IsTryAlreadyDone(Position position)
        {

            if (AlreadyHitPositions.Contains(position))
            {
                return true;
            }
            return false;
        }

        public bool IsItAHit(Position position)
        {
            if(!IsTryAlreadyDone(position))
            {
                foreach (var boat in Boats)
                {
                    foreach (var pos in boat.Positions)
                    {
                        if (position.X == pos.X && position.Y == pos.Y)
                        {
                            AlreadyHitPositions.Add(position);
                            return true;
                        }
                    }

                }
            }           
            return false;
        }

        public bool HasPlayerLost()
        {
            foreach(var boat in Boats)
            {
               if(!boat.Sink)
                {
                    return false;
                }                            
            }
            return true;
        }



    }

    
}

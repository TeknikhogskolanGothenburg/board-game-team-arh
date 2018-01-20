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
        public Boat[] EnemyBoats { get;set; }
        public bool[] Trys { get; set;}
        public Boat[] OwnBoats{get;set;}
        public List<Position> AlreadyHitPositions{get;set;}

     
        public Player()
        {
           AlreadyHitPositions = new List<Position>();
        }

        public void LoadEnemyBoats(Boat[] enemyBoats)
        {
            EnemyBoats=enemyBoats;
        }

         public void LoadOwnBoats(Boat[] ownBoats)
        {
            OwnBoats=ownBoats;
        }


        public bool IsTryAlreadyDone(Position position)
        {
            
            return false;
        }

        public bool IsAlreadyHit(Position position)
        {
             if(AlreadyHitPositions.Contains(position))
             {
                return true;
             }
              return false;
        }


        public bool IsItAHit(Position position)
        {
            if( IsAlreadyHit(position))
            {
                return false;
            }
            
            foreach (var boat in EnemyBoats)
	        {
                foreach(var pos in boat.Positions)
                {
                    if (position.X ==pos.X && position.Y==pos.Y)
                    {
                        AlreadyHitPositions.Add(position);
                        return true;
                    }
                }

	        }
            return false;
        }

        public bool HasPlayerLost()
        {
            foreach(var boat in OwnBoats)
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

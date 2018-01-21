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
        public Boat[] Boats{ get; }
        public List<Position> AlreadyHitPositions{ get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Player()
        {
            AlreadyHitPositions = new List<Position>();

            Boats = new Boat[10] {
                new Boat(BoatType.Battleship),
                new Boat(BoatType.Cruiser),
                new Boat(BoatType.Cruiser),
                new Boat(BoatType.Destroyer),
                new Boat(BoatType.Destroyer),
                new Boat(BoatType.Destroyer),
                new Boat(BoatType.Submarine),
                new Boat(BoatType.Submarine),
                new Boat(BoatType.Submarine),
                new Boat(BoatType.Submarine)
            };
        }

        /// <summary>
        /// Is position already hit
        /// </summary>
        /// <param name="position">Position to hit (Position)</param>
        /// <returns>Validate result (bool)</returns>
        public bool IsPositionAlreadyHit(Position position)
        {
             if(AlreadyHitPositions.Contains(position))
             {
                return true;
             }

             return false;
        }

        /// <summary>
        /// Check if any Boat was hit
        /// </summary>
        /// <param name="position">Position to hit (Position)</param>
        /// <returns>Validate result (bool)</returns>
        public bool IsABoatHit(Position position)
        {
            if(IsPositionAlreadyHit(position))
            {
                throw new Exception("Position is already hit, try other position!");
            }
            else
            {
                AlreadyHitPositions.Add(position);

                foreach (var boat in Boats)
                {
                    if (boat.IsItAHit(position))
                    {
                        return true;
                    }
                }
            }
            

            return false;
        }

        /// <summary>
        /// Check if player not got any Boats left, if not player has lost
        /// </summary>
        /// <returns>Validate result (bool)</returns>
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

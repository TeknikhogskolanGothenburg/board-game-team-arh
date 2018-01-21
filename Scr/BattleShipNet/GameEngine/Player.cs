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

        public bool HasPlayerLost
        {
            get
            {
                foreach (var boat in Boats)
                {
                    if (!boat.Sink)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

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

            PositionsBoats();
        }

        /// <summary>
        /// Positions Boats random, based on https://github.com/exceptionnotfound/BattleshipModellingPractice/
        /// </summary>
        private void PositionsBoats()
        {
            //Random class creation stolen from http://stackoverflow.com/a/18267477/106356
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            foreach (Boat boat in Boats)
            {
                // Select a random row/column combination, then select a random orientation.
                // If none of the proposed squares are occupied, place the Boat
                // Do this for all Boats

                bool isOpen = false;
                Position[] positions = new Position[2];

                while (isOpen)
                {
                    int startX = rand.Next(1, 11);
                    int startY = rand.Next(1, 11);
                    int endY = startY, endX = startX;
                    var orientation = rand.Next(1, 101) % 2; //0 for Horizontal

                    // Calculate endY (Horizontal) or endX (Vertical)
                    if (orientation == 0)
                    {
                        for (int i = 1; i < boat.Size; i++)
                        {
                            endY++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < boat.Size; i++)
                        {
                            endX++;
                        }
                    }

                    //We cannot place Boats beyond the boundaries of the board
                    if (endY > 10 || endX > 10)
                    {
                        isOpen = true;
                        continue;
                    }

                    positions = new Position[2] {
                        new Position(startX, startY),
                        new Position(endX, endY)
                    };

                    //Check if specified squad are occupied
                    foreach (Boat ship in Boats)
                    {
                        if(ship.AreYouHere(positions))
                        {
                            isOpen = true;
                            continue;
                        }
                    }

                    // Try to set position
                    if (!isOpen)
                    {
                        try
                        {
                            boat.SetPositions(positions);
                        }
                        catch
                        {
                            isOpen = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Is position already hit
        /// </summary>
        /// <param name="position">Position to hit (Position)</param>
        /// <returns>Validate result (bool)</returns>
        public bool IsPositionAlreadyHit(Position position)
        {
             foreach(Position hitPosition in AlreadyHitPositions)
             {
                if(position.X == hitPosition.X && position.Y == hitPosition.Y)
                {
                    return true;
                }
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
    }
}

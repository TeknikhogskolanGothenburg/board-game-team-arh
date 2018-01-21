﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Boat
    {
        public Position[] Positions { get; private set; }
        public BoatType Type { get; }
        public int Size {
            get {
                return (int)Type;
            }
        }
        public Position[] Hits { get; }

        public bool Sink
        {
            get
            {
                if(Hits.Length == Size)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Constructor which takes BoatType
        /// </summary>
        /// <param name="newBoatType">Boat type on Boat (BoatType)</param>
        public Boat(BoatType newBoatType)
        {
            Type = newBoatType;
            Positions = new Position[2];
            Hits = new Position[Size];
        }

        /// <summary>
        /// Set position for Boat
        /// </summary>
        /// <param name="newPositions">Hit position (Position)</param>
        public void SetPositions(Position[] newPositions)
        {
            // Check so position is not diagonally
            if (newPositions[0].Y == newPositions[1].Y || newPositions[0].X == newPositions[1].X)
            {
                // Check so Boat not position over it's size
                if (
                    ((newPositions[1].Y - newPositions[0].Y) == Size || (newPositions[0].Y - newPositions[1].Y) == Size) &&
                    ((newPositions[1].X - newPositions[0].X) == Size || (newPositions[0].X - newPositions[1].X) == Size)
                )
                {
                    // Position[1] should be largest, if it's not switch
                    if(newPositions[1].Y > newPositions[0].Y || newPositions[1].X > newPositions[0].X)
                    {
                        Positions = newPositions;
                    }
                    else
                    {
                        Positions[0] = newPositions[1];
                        Positions[1] = newPositions[0];
                    }
                }
                else
                {
                    throw new FormatException("Boat cannot be position over it's size");
                }
            }
            else
            {
                throw new FormatException("Boat cannot be position diagonally");
            }
        }

        /// <summary>
        /// If Boat is here, return true
        /// </summary>
        /// <param name="position">Positions to check (Position[])</param>
        /// <returns>Validate result (bool)</returns>
        public bool AreYouHere(Position[] positions)
        {
            // Check if it's horizontal or vertical, and loop-through all possible positions to check if Boat is there
            if (positions[0].Y > positions[1].Y)
            {
                for (int i = positions[0].Y; i <= positions[1].Y; i++)
                {
                    if (AreYouHere(new Position(positions[0].X, i)))
                    {
                        return true;
                    }  
                }
            }
            else
            {
                for (int i = positions[0].X; i <= positions[1].X; i++)
                {
                    if (AreYouHere(new Position(i, positions[0].Y)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// If Boat is here, return true
        /// </summary>
        /// <param name="position">Position to check (Position)</param>
        /// <returns>Validate result (bool)</returns>
        public bool AreYouHere(Position position)
        {
            if (position.X >= Positions[0].X && position.X <= Positions[1].X && position.Y >= Positions[0].Y && position.Y <= Positions[1].Y)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// If Boat is hit, return true and add hit to Boat
        /// </summary>
        /// <param name="position">Hit position (Position)</param>
        /// <returns>Validate result (bool)</returns>
        public bool IsItAHit(Position position)
        {
            if (AreYouHere(position))
            {
                Hits[Hits.Length] = position;
                return true;
            }

            return false;
        }
    }
}

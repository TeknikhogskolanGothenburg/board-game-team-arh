using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class GameBoard
    {
        public Player[] Players { get; }
        public string GameKey { get; set; }
        public bool PrivateGame { get; set; }
        public int Turn { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameBoard()
        {
            PrivateGame = false;

            Players = new Player[2] {
                new Player(),
                new Player()
            };

            Turn = 1;
        }

        /// <summary>
        /// Shoot on a square in one a Player's board 
        /// </summary>
        /// <param name="playerId">Id for target player (int)</param>
        /// <param name="position">Position to hit (Position)</param>
        /// <returns>Het result (bool)</returns>
        public bool Shoot(int playerId, Position position)
        {
            // Check so there really are our turn
            if (playerId != Turn)
            {
                Player player = Players[playerId];

                // Check so Position is not already hit
                if (!player.IsPositionAlreadyHit(position))
                {
                    // Check if a Boat was hit, if not change turn to enemy
                    if (!player.IsABoatHit(position))
                    {
                        Turn = playerId;
                        return false;
                    }

                    return true;
                }
                else
                {
                    throw new Exception("Position is already hit!");
                }
            }
            else
            {
                throw new Exception("Not your turn!");
            }
        }

        /// <summary>
        /// Check if game is over, if it's return winner
        /// </summary>
        /// <param name="winner">Return winner's Player object</param>
        /// <returns>Validate result (bool)</returns>
        public bool IsGameEnd(out Player winner)
        {
            if (Players[0].HasPlayerLost)
            {
                winner = Players[1];
                return true;
            }
            if (Players[1].HasPlayerLost)
            {
                winner = Players[0];
                return true;
            }

            winner = null;
            return false;
        }
    }
}

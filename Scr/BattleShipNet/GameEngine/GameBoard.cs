using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GameEngine
{
    public class GameBoard
    {
        public Player[] Players { get; }
        public string GameKey { get; }
        public int Turn { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameBoard()
        {
            Players = new Player[2] {
                new Player(),
                new Player()
            };

            GameKey = GenerateGameKey();
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
                    throw new Exception("Position is already hit");
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
            else if (Players[1].HasPlayerLost)
            {
                winner = Players[0];
                return true;
            }

            winner = null;
            return false;
        }

        /// <summary>
        /// Generate and returns a random game key
        /// </summary>
        /// <returns>Random game key (string)</returns>
        private string GenerateGameKey()
        {
            string gameKey = Guid.NewGuid().ToString();
            gameKey = Regex.Replace(gameKey, @"[^0-9a-zA-Z]+", "");
            gameKey = gameKey.Substring(0,6);

            return gameKey;
        }
    }
}

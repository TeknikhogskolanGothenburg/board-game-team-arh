using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GameEngine
{
    public class GameBoards
    {
        public List<GameBoard> Games { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameBoards()
        {
            Games = new List<GameBoard>();
        }

        /// <summary>
        /// Start new GameBoard
        /// </summary>
        /// <returns>Game key (string)</returns>
        public string New()
        {
            GameBoard game = new GameBoard();
            string gameKey;
            do
            {
                gameKey = GenerateGameKey();
            } while (DoesItExist(gameKey));

            game.GameKey = gameKey;
            Games.Add(game);

            return gameKey;
        }

        /// <summary>
        /// Return existing GameBoard by GameKey
        /// </summary>
        /// <param name="GameKey">Game key (string)</param>
        /// <returns>GameBoard object</returns>
        public GameBoard Get(string gameKey)
        {
            return Games.Find(board => board.GameKey == gameKey);
        }

        /// <summary>
        /// Check if GameBoard exist
        /// </summary>
        /// <param name="GameKey">Game key (string)</param>
        /// <returns></returns>
        public bool DoesItExist(string gameKey)
        {
            return (Get(gameKey) != null);
        }

        /// <summary>
        /// Generate and returns a random game key
        /// </summary>
        /// <returns>Random game key (string)</returns>
        private string GenerateGameKey()
        {
            string gameKey = Guid.NewGuid().ToString();
            gameKey = Regex.Replace(gameKey, @"[^0-9a-zA-Z]+", "");
            gameKey = gameKey.Substring(0, 6);

            return gameKey;
        }
    }
}

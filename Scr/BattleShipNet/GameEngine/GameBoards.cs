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
        /// Return List of GameBoards which is open to join
        /// </summary>
        public List<GameBoard> OpenGames
        {
            get
            {
                List<GameBoard> openGameKeys = new List<GameBoard>();

                foreach (GameBoard gameBoard in Games)
                {
                    if (!gameBoard.PrivateGame && gameBoard.Players[1].Name == null)
                    {
                        openGameKeys.Add(gameBoard);
                    }
                }

                return openGameKeys;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameBoards()
        {
            Games = new List<GameBoard>();
        }

        /// <summary>
        /// Add new GameBoard
        /// </summary>
        /// <param name="newGameBoard">GameBoard object to add</param>
        /// <returns>Result (bool)</returns>
        public bool Add(GameBoard newGameBoard)
        {
            if(newGameBoard != null)
            {
                string gameKey;

                do
                {
                    gameKey = GenerateGameKey();
                } while (DoesItExist(gameKey));

                newGameBoard.GameKey = gameKey;
                Games.Add(newGameBoard);

                return true;
            }

            return false;
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
        /// <returns>Validation result</returns>
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

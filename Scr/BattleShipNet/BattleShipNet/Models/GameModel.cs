using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameEngine;

namespace BattleShipNet.Models
{
    public class GameModel
    {
        private GameBoard gameBoard;
        private List<List<Square>>[] playersBoard;

        /// <summary>
        /// Properties returning session player id - get
        /// </summary>
        public int YourPlayerId
        {
            get
            {
                return (int)HttpContext.Current.Session["playerId"];
            }
        }

        /// <summary>
        /// Properties returning session's enemy's player id - get
        /// </summary>
        public int EnemyPlayerId
        {
            get
            {
                return (YourPlayerId == 1) ? 0 : 1;
            }
        }

        /// <summary>
        /// Properties returning session Player object - get
        /// </summary>
        public Player YourPlayer
        {
            get
            {
                return gameBoard.Players[YourPlayerId];
            }
        }

        /// <summary>
        /// Properties returning enemy Player object - get
        /// </summary>
        public Player EnemyPlayer
        {
            get
            {
                return gameBoard.Players[EnemyPlayerId];
            }
        }

        /// <summary>
        /// Properties returning session player board
        /// </summary>
        public List<List<Square>> YourPlayerBoard {
            get
            {
                return PrepareBoard(YourPlayerId);
            }
        }

        /// <summary>
        /// Properties returning enemy player board
        /// </summary>
        public List<List<Square>> EnemyPlayerBoard
        {
            get
            {
                return PrepareBoard(EnemyPlayerId);
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameModel()
        {
            gameBoard = new GameBoard();

            playersBoard = new List<List<Square>>[2]
            {
                CreateBoard(0),
                CreateBoard(1)
            };

        }

        /// <summary>
        /// Create a player board
        /// </summary>
        /// <param name="playerId">Player id which to make board for (int)</param>
        /// <returns>Player board (List of List of Square objects)</returns>
        private List<List<Square>> CreateBoard(int playerId)
        {
            List<List<Square>> playerBoard = new List<List<Square>>();
            Player player = gameBoard.Players[playerId];

            for (int x = 0; x < 10; x++)
            {
                List<Square> column = new List<Square>();
                for (int y = 0; y < 10; y++)
                {
                    Square square = new Square();
                    square.PositionData = new Position(x, y);

                    // Check if any boat is in this square
                    foreach (Boat boat in player.Boats)
                    {
                        if (boat.AreYouHere(square.PositionData))
                        {
                            square.HaveBoat = true;
                            continue;
                        }
                    }

                    column.Add(square);
                }
                playerBoard.Add(column);
            }

            return playerBoard;
        }

        /// <summary>
        /// Prepare and update a player board
        /// </summary>
        /// <param name="playerId">Player id which to prepare board for (int)</param>
        /// <returns>Player board (List of List of Square objects)</returns>
        private List<List<Square>> PrepareBoard(int playerId)
        {
            List<List<Square>> playerBoard = playersBoard[playerId];

            Player player = gameBoard.Players[playerId];

            foreach (List<Square> column in playerBoard)
            {
                foreach (Square square in column)
                {
                    if (!square.HaveBeenHit)
                    {
                        // Is position hit already?
                        square.HaveBeenHit = player.IsPositionAlreadyHit(square.PositionData);
                    }
                }
            }

            return playerBoard;
        }
    }
}
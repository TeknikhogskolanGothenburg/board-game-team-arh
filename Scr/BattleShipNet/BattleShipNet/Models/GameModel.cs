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

        /// <summary>
        /// Properties returning session player id - get
        /// </summary>
        public int GetYourPlayerId
        {
            get
            {
                return (int)HttpContext.Current.Session["playerId"];
            }
        }

        /// <summary>
        /// Properties returning session Player object - get
        /// </summary>
        public Player GetYourPlayer
        {
            get
            {
                return gameBoard.Players[GetYourPlayerId];
            }
        }

        /// <summary>
        /// Properties returning enemy Player object - get
        /// </summary>
        public Player GetEnemyPlayer
        {
            get
            {
                return (GetYourPlayerId == 1) ? gameBoard.Players[0] : gameBoard.Players[1];
            }
        }

        /// <summary>
        /// Properties returning session player board
        /// </summary>
        public List<List<Square>> YourPlayerBoard {
            get
            {
                return PrepareBoard(GetYourPlayer);
            }
        }

        /// <summary>
        /// Properties returning enemy player board
        /// </summary>
        public List<List<Square>> EnemyPlayerBoard
        {
            get
            {
                return PrepareBoard(GetEnemyPlayer);
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameModel()
        {
            gameBoard = new GameBoard();
        }

        /// <summary>
        /// Prepare a player board
        /// </summary>
        /// <param name="player">Player object to make board for</param>
        /// <returns>Player board inform of a List of List of Square objects</returns>
        private List<List<Square>> PrepareBoard(Player player)
        {
            List<List<Square>> Board = new List<List<Square>>();

            for (int y = 0; y < 10; y++)
            {
                List<Square> Column = new List<Square>();
                for (int x = 0; x < 10; x++)
                {
                    Square square = new Square();
                    square.PositionData = new Position(x, y);

                    // If it's our board show Boats
                    if(player == GetYourPlayer)
                    {
                        foreach (Boat boat in player.Boats)
                        {
                            if (boat.AreYouHere(square.PositionData))
                            {
                                square.HaveBoat = true;
                                continue;
                            }
                        }
                    }
                    
                    // Is position hit already?
                    square.HaveBeenHit = player.IsPositionAlreadyHit(square.PositionData);

                    Column.Add(square);
                }
                Board.Add(Column);
            }

            return Board;
        }
    }
}
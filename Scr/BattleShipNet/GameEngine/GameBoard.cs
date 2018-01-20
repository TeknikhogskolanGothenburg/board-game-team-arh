using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine

{
    
    public class Square
    {
        public Position Position { get; set; }
        public bool HaveBoat { get; set; }
        public bool HaveShoot { get; set; }
    }
    public class GameBoard
    {
        public Player[] Players { get; }
        public string GameKey { get; }
        public int Turn { get; }
        private List<List<Square>> player1Board;
        private List<List<Square>> player2Board;

        private List<List<Square>> CreateBoard()
        {
            var Board = new List<List<Square>>();

            for (int i = 0; i < 10; i++)
            {
                List<Square> Column = new List<Square>();
                for (int j = 0; j < 10; j++)
                {

                    var square = new Square();
                    var position = new Position();
                    position.X = i;
                    position.Y = j;
                    square.Position = position;
                    Column.Add(square);
                }
                Board.Add(Column);
            }

            return Board;
        }

        public GameBoard()
        {
            player1Board = CreateBoard();
            player2Board = CreateBoard();
            
            Player player1 = new Player();
            Player player2 = new Player();

            Players = new Player[2];
            Players[0] = player1;
            Players[1] = player2;
        }

        public bool Shoot(int player, Position position)
        {
            if (player == 0)
            {
                bool hit = player2Board[position.X][position.Y].HaveBoat;
                player2Board[position.X][position.Y].HaveShoot = true;
                return hit;
            }
            else
            {
                bool hit = player1Board[position.X][position.Y].HaveBoat;
                player1Board[position.X][position.Y].HaveShoot = true;
                return hit;
            }
        }

        public bool IsGameEnd(out Player winner)
        {
            bool player1Won = true;
            bool player2Won = true;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(player1Board[i][j].HaveBoat && player1Board[i][j].HaveShoot == false)
                    {
                        player1Won = false;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (player2Board[i][j].HaveBoat && player2Board[i][j].HaveShoot == false)
                    {
                        player2Won = false;
                    }
                }
            }

            if (player1Won == true)
            {
                winner = Players[0];
                return true;
            }
            else if (player2Won == true)
            {
                winner = Players[1];
                return true;
            }

            winner = null;
            return false;
        }
    }
}

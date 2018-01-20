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
        public string GameKey { get; }
        public int Turn { get; }

        public GameBoard()
        {
        }

        public bool Shoot(Position position)
        {
            return false;
        }

        public bool IsGameEnd(out Player winner)
        {
            winner = null;
            return false;
        }
    }

   
}

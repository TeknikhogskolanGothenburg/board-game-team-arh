using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShipNet.Models
{
    public class GamesModel
    {
        public List<GameModel> Games { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public GamesModel()
        {
            Games = new List<GameModel>();
        }
    }
}
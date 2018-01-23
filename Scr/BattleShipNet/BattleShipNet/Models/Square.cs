﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameEngine;

namespace BattleShipNet.Models
{
    public class Square
    {
        public Position PositionData { get; set; }
        public bool HaveBoat { get; set; }
        public bool HaveBeenHit { get; set; }
    }
}
using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    public class PlayerTest
    {
        [TestMethod]
        public void IsPositionAlreadyHit()
        {
            // Arrange
            GameBoard gameBoard = new GameBoard();
            Player player = gameBoard.Players[0];
            Position pos = new Position(1, 2);
            player.AlreadyHitPositions.Add(pos);
            Position pos1 = new Position(1, 2);

            player.IsPositionAlreadyHit(pos1);

            // Act
            var result = player.IsPositionAlreadyHit(pos1);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsAnyBoatHere()
        {
            // Arrange   
            GameBoard gameBoard = new GameBoard();
            Player player1 = gameBoard.Players[0];
            Boat boatCruiser = new Boat(BoatType.Cruiser);
            Position[] positions = new Position[2] {
                        new Position(1, 2),
                        new Position(5, 2)
                    };

            player1.IsAnyBoatHere(new Position(1, 2));

            // Act
            var result = player1.IsAnyBoatHere(new Position(1, 2));
            // Assert
            Assert.AreEqual(false, result);
        }
    }
}

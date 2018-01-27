using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine;

namespace UnitGameTest
{
    [TestClass]
    public class GameEngineTest
    {
        [TestMethod]
        public void IsPositionAlreadyHitShoot()
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

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

        [TestMethod]
        public void IsABoatHit()
        {
            // Arrange   
            GameBoard gameBoard = new GameBoard();
            Player player = gameBoard.Players[0];
            Position pos = new Position(3, 4);
            player.AlreadyHitPositions.Add(pos);
            Position pos1 = new Position(3, 4);

            //Act
            var result = player.IsABoatHit(pos1);

            // Assert
            Assert.AreEqual(true, result);

        }

        [TestMethod]
        public void Sink()
        {
            // Arrange
            GameBoard gameBoard = new GameBoard();
            Player player = gameBoard.Players[0];
            Boat boat = new Boat(BoatType.Submarine);
            Position pos1 = new Position(6, 7);
            Position pos2 = new Position(7, 7);
            Position pos3 = new Position(8, 7);
            boat.Hits.Add(pos1);
            boat.Hits.Add(pos2);
            boat.Hits.Add(pos3);
            
            // Act
            var result = boat.Sink;

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AreYouHere()
        {
            //Arrange
            GameBoard gameBoard = new GameBoard();
            Player player = gameBoard.Players[0];
            Boat boat = new Boat(BoatType.Submarine);
            Position setPos1 = new Position(7, 5);
            boat.Positions[0] = setPos1;
            Position setPos2 = new Position(7, 5);

            // Act
            var result = boat.AreYouHere(setPos2);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsItAHit()
        {
            //Arrange
            GameBoard gameBoard = new GameBoard();
            Player player = gameBoard.Players[0];
            Boat boat = new Boat(BoatType.Submarine);
            Position setPos1 = new Position(7, 5);
            boat.Positions[0] = setPos1;
            Position shootPos1 = new Position(7, 5);
            boat.AreYouHere(shootPos1);

            // Act
            var result = boat.IsItAHit(shootPos1);

            // Assert
            Assert.AreEqual(true, result);
        }

    }
}

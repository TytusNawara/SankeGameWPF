using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeZadanieRekrutacyjne;

namespace UnitTests
{
    [TestClass]
    public class GameLogicTest
    {
        [TestMethod]
        public void HasSingleSnakePartWhenCreated()
        {
            GameLogic SUT = new GameLogic();
            int numberOfSnakeFields = _countNumberOfFieldsdWithGivenType(SUT, TypeOfField.Snake);

            Assert.AreEqual(numberOfSnakeFields, 1);
        }

        [TestMethod]
        public void HasSingleAppleWhenCreated()
        {
            GameLogic SUT = new GameLogic();
            int numberOfAppleFields = _countNumberOfFieldsdWithGivenType(SUT, TypeOfField.Apple);

            Assert.AreEqual(numberOfAppleFields, 1);
        }

        [TestMethod]
        public void RaisesDeathEventOnSnakeDeath()
        {
            GameLogic SUT = new GameLogic();
            bool didRiseEvent = false;
            SUT.GameOverEvent += delegate () { didRiseEvent = true; };

            int count = 0;
            while (!didRiseEvent && count <= GameLogic.SIZE)
            {
                count++;
                SUT.TickFrame(this, EventArgs.Empty);
            }

            Assert.IsTrue(didRiseEvent);
        }

        [TestMethod]
        public void MovesRightOnThatInput()
        {
            GameLogic SUT = new GameLogic();
            Point startLocation = _findLocationOfFieldOfGivenType(SUT, TypeOfField.Snake);
            Point predictedLocation = new Point(startLocation.X + 1, startLocation.Y);
            
            SUT.SnakeDirection = Direction.Right;
            SUT.TickFrame(this, EventArgs.Empty);

            Point endLocation = _findLocationOfFieldOfGivenType(SUT, TypeOfField.Snake);
            Assert.IsTrue(endLocation.X == predictedLocation.X &&
                          endLocation.Y == predictedLocation.Y);
        }

        [TestMethod]
        public void MovesLeftOnThatInput()
        {
            GameLogic SUT = new GameLogic();
            Point startLocation = _findLocationOfFieldOfGivenType(SUT, TypeOfField.Snake);
            Point predictedLocation = new Point(startLocation.X - 1, startLocation.Y);

            SUT.SnakeDirection = Direction.Left;
            SUT.TickFrame(this, EventArgs.Empty);

            Point endLocation = _findLocationOfFieldOfGivenType(SUT, TypeOfField.Snake);
            Assert.IsTrue(endLocation.X == predictedLocation.X &&
                          endLocation.Y == predictedLocation.Y);
        }

        [TestMethod]
        public void MovesUpOnThatInput()
        {
            GameLogic SUT = new GameLogic();
            Point startLocation = _findLocationOfFieldOfGivenType(SUT, TypeOfField.Snake);
            Point predictedLocation = new Point(startLocation.X, startLocation.Y - 1);

            SUT.SnakeDirection = Direction.Up;
            SUT.TickFrame(this, EventArgs.Empty);

            Point endLocation = _findLocationOfFieldOfGivenType(SUT, TypeOfField.Snake);
            Assert.IsTrue(endLocation.X == predictedLocation.X &&
                          endLocation.Y == predictedLocation.Y);
        }

        //HELPER METHODS

        [TestMethod]
        public void MovesDownOnThatInput()
        {
            GameLogic SUT = new GameLogic();
            Point startLocation = _findLocationOfFieldOfGivenType(SUT, TypeOfField.Snake);
            Point predictedLocation = new Point(startLocation.X, startLocation.Y + 1);

            SUT.SnakeDirection = Direction.Down;
            SUT.TickFrame(this, EventArgs.Empty);

            Point endLocation = _findLocationOfFieldOfGivenType(SUT, TypeOfField.Snake);
            Assert.IsTrue(endLocation.X == predictedLocation.X &&
                          endLocation.Y == predictedLocation.Y);
        }

        private int _countNumberOfFieldsdWithGivenType(GameLogic gameLogic, TypeOfField typeOfField)
        {
            var map = gameLogic.Map;
            int numberOfSnakeParts = 0;
            for (int i = 0; i < GameLogic.SIZE; i++)
            for (int j = 0; j < GameLogic.SIZE; j++)
            {
                if (map[i, j] == typeOfField)
                    numberOfSnakeParts++;
            }
            return numberOfSnakeParts;
        }

        private Point _findLocationOfFieldOfGivenType(GameLogic gameLogic, TypeOfField typeOfField)
        {
            Point toReturn = new Point(-1, -1);
            var map = gameLogic.Map;
            
            for (int i = 0; i < GameLogic.SIZE; i++)
            for (int j = 0; j < GameLogic.SIZE; j++)
            {
                if (map[i, j] == typeOfField)
                {
                    toReturn = new Point(i, j);
                    return toReturn;
                }
            }
            return toReturn;
        }
    }
}

using DiceGame.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGameTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestRoll()
        {
            // Arrange
            var randomMock = new RandomMock(0);
            var player = new Player(randomMock, 1);

            // Act
            var result = player.Roll();

            // Assert
            Assert.AreNotEqual(0, result.Count());
        }
    }

    public class RandomMock : IRandom
    {
        private int number;

        public RandomMock(int number)
        {
            this.number = number;
        }

        public int Next(int n) => number;
    }
}

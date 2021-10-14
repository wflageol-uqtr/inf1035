using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiceGame.Game;
using DiceGame;

namespace DiceGameTests
{
    [TestClass]
    public class BonusTileTests
    {
        [TestMethod]
        public void TestActivate()
        {
            // Arrange
            var bonusTile = new BonusTile();
            var controller = new GameControllerMock();

            // Act
            bonusTile.Activate(controller);

            // Assert
            Assert.IsTrue(controller.BonusTileActivated);
        }
    }
}

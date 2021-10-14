using DiceGame;
using DiceGame.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGameTests
{
    class GameControllerMock : IGameController
    {
        public bool BonusTileActivated { get; private set; }

        public Board Board => throw new NotImplementedException();

        public Player CurrentPlayer => throw new NotImplementedException();

        public IEnumerable<Player> Players => throw new NotImplementedException();

        public void ActivateBonusTile()
        {
            BonusTileActivated = true;
        }

        public void Roll()
        {
            throw new NotImplementedException();
        }
    }
}

using DiceGame.Game;
using System.Collections.Generic;

namespace DiceGame
{
    public interface IGameController
    {
        Board Board { get; }
        Player CurrentPlayer { get; }
        IEnumerable<Player> Players { get; }

        void ActivateBonusTile();
        void Roll();
    }
}
using DiceGame.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class GameController : IGameController
    {
        private MainWindow view;
        private Board board;

        internal void EndGame()
        {
            throw new NotImplementedException();
        }

        private Player[] players;
        private int currentPlayerIndex;

        public Board Board => board;
        public IEnumerable<Player> Players => players;
        public Player CurrentPlayer => players[currentPlayerIndex];

        public GameController(MainWindow view, int nbPlayers)
        {
            board = new Board();
            this.view = view;

            CreatePlayers(nbPlayers);
            board.PlacePlayers(players);

            currentPlayerIndex = 1;
        }

        private void CreatePlayers(int nb)
        {
            var random = new MyRandom();

            players = new Player[nb];
            for (int i = 0; i < nb; i++)
                players[i] = new Player(random, i + 1);
        }

        public void Roll()
        {
            var rolledFaces = CurrentPlayer.Roll();

            var directions = new List<Direction>();
            var totalPower = 0;
            foreach (var face in rolledFaces)
            {
                if (face.Direction.HasValue)
                    directions.Add(face.Direction.Value);
                totalPower += face.Power;
            }

            view.ShowDirectionControls(directions);
        }

        public void ActivateBonusTile()
        {
            view.ShowBonusControls();
        }
    }
}

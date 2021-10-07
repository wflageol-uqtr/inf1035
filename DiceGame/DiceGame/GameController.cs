using DiceGame.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class GameController
    {
        private MainWindow view;
        private Board board;
        private Player[] players;
        private int currentPlayerIndex;

        public GameController(MainWindow view, int nbPlayers)
        {
            board = new Board(nbPlayers);
            players = board.Players.ToArray();
            this.view = view;
            currentPlayerIndex = 0;
        }

        public Board Board => board;

        private Player CurrentPlayer => players[currentPlayerIndex];

        public void Roll()
        {
            var rolledFaces = CurrentPlayer.Roll();

            var directions = new List<Direction>();
            var totalPower = 0;
            foreach(var face in rolledFaces)
            {
                if (face.Direction.HasValue)
                    directions.Add(face.Direction.Value);
                totalPower += face.Power;
            }

            view.ShowRollResults(directions, totalPower);

        }
    }
}

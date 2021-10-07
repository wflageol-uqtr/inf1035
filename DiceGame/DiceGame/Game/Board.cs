using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Game
{
    public class Board
    {
        private List<Player> players;
        private ITile[,] tiles = new ITile[50, 50];

        public IEnumerable<Player> Players => players;

        public Board(int nbPlayers)
        {
            CreatePlayers(nbPlayers);
        }

        private void CreatePlayers(int nbPlayers)
        {
            players = new List<Player>();
            for (int i = 0; i < nbPlayers; i++)
                players.Add(new Player());
        }

        private IEnumerable<int> RandomMany(int nb, int max)
        {
            var rnd = new Random();
            var set = new HashSet<int>();
            while(set.Count < nb)
                set.Add(rnd.Next(max));

            return set;
        }

        private void CreateTiles()
        {
            for(int x = 0; x < 50; x++)
            {
                for(int y = 0; y < 50; y++)
                    tiles[x, y] = new NormalTile();
            }

            tiles[25, 25] = new FinalTile();

            // ...
        }
    }
}

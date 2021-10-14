using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Game
{
    public class Board
    {
        public int BoardWidth => 50;
        public int BoardHeight => 50;

        private ITile[,] tiles;

        public Board()
        {
            CreateTiles();
        }

        public ITile GetTile(int x, int y) => tiles[x, y];
        public ITile GetTile((int, int) coord) => GetTile(coord.Item1, coord.Item2);

        public void PlacePlayers(IEnumerable<Player> players)
        {
            var placedPlayers = new List<Player>();
            foreach (var player in players)
            {
                bool valid = false;
                while (!valid)
                {
                    player.Position = RandomCoord(1).First();
                    var tile = GetTile(player.Position);
                    if (tile is NormalTile)
                        valid = true;
                    else
                        continue;

                    foreach (var placed in placedPlayers)
                    {
                        var distance = Distance(placed.Position, player.Position);
                        if (distance > 10 || distance < 5)
                        {
                            valid = false;
                            break;
                        }
                    }
                }
            }
        }

        private int Distance((int, int) coord1, (int, int) coord2)
        {
            (int x1, int y1) = coord1;
            (int x2, int y2) = coord2;

            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private IEnumerable<int> RandomMany(int nb, int max)
        {
            var rnd = new Random();
            var set = new HashSet<int>();
            while (set.Count < nb)
                set.Add(rnd.Next(max));

            return set;
        }

        private IEnumerable<(int, int)> RandomCoord(int nb)
        {
            return RandomMany(nb, BoardWidth * BoardHeight)
                .Select(n => (n / BoardWidth, n % BoardWidth));
        }

        private void CreateTiles()
        {
            tiles = new ITile[BoardWidth, BoardHeight];

            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                    tiles[x, y] = new NormalTile();
            }

            CreateSpecialTiles();
        }

        private void CreateSpecialTiles()
        {
            tiles[24, 24] = new FinalTile();

            var randomTiles = RandomCoord(30);

            foreach (var coord in randomTiles.Take(15))
                tiles[coord.Item1, coord.Item2] = new BonusTile();

            foreach (var coord in randomTiles.Skip(15))
                tiles[coord.Item1, coord.Item2] = new MerchantTile();
        }
    }
}

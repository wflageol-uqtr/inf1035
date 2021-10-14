using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Game
{
    public class Player
    {
        private IRandom rnd;

        public int PlayerNumber { get; }
        public (int, int) Position { get; set; }

        public Player(IRandom rnd, int number) 
        {
            this.rnd = rnd;
            PlayerNumber = number;
        }

        public IEnumerable<IFace> Roll()
        {
            if (rnd.Next(2) == 0)
            {
                return new IFace[] {
                    new DirectionFace(Direction.North),
                    new PowerFace(5)
                };
            }
            else
                return new IFace[0];
        }
    }
}

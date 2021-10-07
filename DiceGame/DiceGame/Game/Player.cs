using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Game
{
    public class Player
    {
        public IEnumerable<IFace> Roll()
        {
            return new IFace[] {
                new DirectionFace(Direction.North),
                new PowerFace(5)
            };
        }
    }
}

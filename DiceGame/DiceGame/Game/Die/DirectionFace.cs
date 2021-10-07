using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Game
{
    public class DirectionFace : IFace
    {
        public Direction Direction { get; }

        public int Power => 0;

        Direction? IFace.Direction => Direction;

        public DirectionFace(Direction direction)
        {
            Direction = direction;
        }
    }
}

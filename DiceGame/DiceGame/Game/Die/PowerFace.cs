using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Game
{
    public class PowerFace : IFace
    {
        public int Value { get; }

        public int Power => Value;

        public Direction? Direction => null;

        public PowerFace(int value)
        {
            Value = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Game
{
    class MyRandom : IRandom
    {
        private Random random = new Random();

        public int Next(int n) => random.Next(n);
    }
}

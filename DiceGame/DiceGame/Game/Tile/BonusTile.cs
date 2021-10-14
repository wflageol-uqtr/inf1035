using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame.Game
{
    public class BonusTile : ITile
    {
        public void Activate(IGameController controller)
        {
            controller.ActivateBonusTile();
        }

    }
}

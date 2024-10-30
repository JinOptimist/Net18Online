using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.Model
{
    /*представляет клетку и её состояние. */
    public class Cell
    {
        public bool IsAlive { get; private set; }

        public Cell(bool isAlive = false)
        {
            IsAlive = isAlive;
        }

        public void SetAlive(bool state)
        {
            IsAlive = state;
        }

        public void ToggleState()
        {
            IsAlive = !IsAlive;
        }
    }
}

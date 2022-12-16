using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Game.Fight
{
    public class FightCase
    {
        public FightCase(int r, int c)
        {
            Row = r;
            Column = c;
        }
        public string Image { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }
        public void Move(int rows, int columns)
        {
            Row = rows;
            Column = columns;
        }
    }
}

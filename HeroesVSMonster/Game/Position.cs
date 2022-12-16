
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Game
{
    public class Position
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        internal void Deconstruct(out int row, out int column)
        {
            row = this.Row;
            column = this.Column;
        }
    }
}

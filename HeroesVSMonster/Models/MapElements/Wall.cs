using HeroesVSMonster.Game;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.MapElements
{
    public class Wall
    {
        public Wall(int sizeGame) 
        {
            //int startRow = 0;
            //while(Tools.StartRow(sizeGame) == sizeGame / 2)
            //{
            //    startRow = Tools.StartRow(sizeGame);
            //}

            //int startColumn = 0;
            //while (Tools.StartColumn(sizeGame) == sizeGame / 2)
            //{
            //    startRow = Tools.StartColumn(sizeGame);
            //}


            Offset = new Position(Tools.StartRow(sizeGame), Tools.StartColumn(sizeGame));
            WallDisgn = "+";
        }

        public Position Offset
        {
            get; set;
        }

        public string WallDisgn { get; set; }
    }
}

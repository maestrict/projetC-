using HeroesVSMonster.Game;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.MapElements
{
    public class Money
    {
        public Money(int _sizeGame)
        {
            Offset = new Position(Tools.StartRow(_sizeGame), Tools.StartColumn(_sizeGame));
            Random rand = new Random();
            MoneyQuantity = rand.Next(1, 5);
        }

        public Position Offset
        {
            get; set;
        }

        public int MoneyQuantity { get; set; }
    }
}

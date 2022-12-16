using HeroesVSMonster.Game;
using HeroesVSMonster.Interfaces;
using HeroesVSMonster.Models.Objet;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.MapElements
{
    public class Market
    {
        private int _sizeGame;

        public Market(int sizeGame)
        {
            Armors = Tools.CreateArmorList(10);
            Armes = Tools.CreateArmedList(10);
            Leather = 10;
            Potion = 10;
            _sizeGame = sizeGame;
            Offset = new Position(Tools.StartRow(_sizeGame), Tools.StartColumn(_sizeGame));
        }

        public List<Armor> Armors
        {
            get; set;
        }
        public List<Arme> Armes
        {
            get; set;
        }
        public int Leather
        {
            get; set;
        }
        public int Potion
        {
            get; set;
        }
        public int Money
        {
            get; set;
        }

        public Position Offset
        {
            get; set;
        }

        //test branche
    }
}

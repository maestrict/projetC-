using HeroesVSMonster.Game;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.Hero.Type
{
    public class Nain : Hero
    {
        public Nain(string nom, int sizeGame) : base(nom , sizeGame){}

        public override void SeSoigne()
        {
            if (LifePoint < MaxLifePoint && Potion > 0) LifePoint += Potion;
        }

    }
}

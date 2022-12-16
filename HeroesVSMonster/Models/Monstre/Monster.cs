using HeroesVSMonster.Interfaces;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.Monstre
{
    public abstract class Monster : Person , IMoney, IPotion
    {
        private static string? nom;
        protected Monster():base(nom)
        {
            Name = GetType().Name;
            Money = Tools.StartMoney();
            Potion = Tools.StartPotion();
        }

        public int Money { get; set; }
        public int Potion { get; set; }

        public override void Hit(Person person)
        {
            if (person is Hero.Hero)
            {
                if (LifePoint > 0) { LifePoint -= Tools.HeroLoseLifePoint((Hero.Hero)person); }
            }
            else
            {
                new Exception("Ne tapez pas sur vos copains !!!");
            }
        }


    }
}

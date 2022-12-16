using HeroesVSMonster.Game;
using HeroesVSMonster.Interfaces;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.Hero
{
    public abstract class Hero : Person, IArme, IArmor, IPotion, ILeather,IMoney
    {
        public string ArmedName { get ; set ; }
        public int ArmedStrength { get; set; }
        public string ArmorName { get ; set; }
        public int ArmorResistance { get ; set; }
        public int Potion { get; set; }
        public int Leather { get; set; }
        public int Money { get ; set ; }

        public int Experience { get; set; } //ajout de l'xp

        private int _sizeGame;

        protected Hero(string nom, int sizeGame) : base(nom)
        {
          
            Potion = Tools.StartPotion();
            Leather = Tools.StartLeather();
            Money= Tools.StartMoney();
            Experience = 0;

            ArmedName = "";
            ArmedStrength = 0;

            ArmorName = "" ;
            ArmorResistance = 0;

            Offset = new Position(sizeGame/2, sizeGame/2);
            _sizeGame= sizeGame;
        }


        public abstract void SeSoigne();

        public void Buy()
        {
            throw new NotImplementedException();
        }
        public void Sell()
        {
            throw new NotImplementedException();
        }

        public void Leave()
        {
            throw new NotImplementedException();
        }

        public override void Hit(Person person)
        {
            if (person is Monstre.Monster)
            {
                if (LifePoint > 0) { LifePoint -= Tools.MonsterLoseLifePoint((Monstre.Monster) person); }
            }
            else
            {
                new Exception("Ne tapez pas sur vos copains !!!");
            }
        }
    }
}

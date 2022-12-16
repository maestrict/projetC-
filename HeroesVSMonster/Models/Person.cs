using HeroesVSMonster.Game;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models
{
    public abstract class Person
    {
        public virtual Position Offset { get; set; }
        protected Person(string nom)
        {
            Name = nom;
            LifePoint = Tools.StartLifePoint();
            Strength = Tools.StartStrenght();
            Resistance = Tools.StartResistance();
            Speed = Tools.StartSpeed();
            MaxLifePoint = LifePoint;
        }

        public string Name { get; init; }
        public int LifePoint { get; set; }
        public int Strength { get; set; }
        public int Resistance { get; set; }
        public int Speed { get; set; }
        public int MaxLifePoint { get; protected set; }
        public void Move(int rows, int columns)
        {
            Offset.Row = rows;
            Offset.Column = columns;
        }
        public abstract void Hit(Person person);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.Objet
{
    public class Armor
    {
        public Armor(string armorName, int armorResistance)
        {
            ArmorName = armorName;
            ArmorResistance = armorResistance;
        }

        public string ArmorName { get; set; }
        public int ArmorResistance { get; set; }
    }
}

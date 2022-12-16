using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.Objet
{
    public class Arme
    {
        public Arme(string armedName, int armedStrength)
        {
            ArmedName = armedName;
            ArmedStrength = armedStrength;
        }

        public string ArmedName { get; set; }
        public int ArmedStrength { get; set; }
    }
}

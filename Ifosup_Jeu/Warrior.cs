using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifosup_Jeu
{
    public class Warrior : Character
    {
        public Warrior(string name) : base(name)
        {
            lifePoints = 120;
            MinDamage = 10;
            MaxDamage = 15;
        }
    }
}

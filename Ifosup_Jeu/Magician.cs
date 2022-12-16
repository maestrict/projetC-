using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifosup_Jeu
{
    public class Magician : Character
    {
        public Magician(string name) : base(name)
        {
            lifePoints = 80;
            MinDamage = 10;
            MaxDamage = 25;
        }
    }
}

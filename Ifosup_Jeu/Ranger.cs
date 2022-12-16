using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifosup_Jeu
{
    public class Ranger : Character
    {
        public Ranger(string name) : base(name)
        {
            lifePoints = 105;
            MinDamage = 15;
            MaxDamage = 20;
        }
    }
}

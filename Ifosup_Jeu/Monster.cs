using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifosup_Jeu
{
    public class Monster : Entity
    {
        public Monster(string name) : base(name)
        {
            this.name = name;
            this.lifePoints = 60;
            this.MinDamage = 5;
            this.MaxDamage = 10;
        }
    }
}

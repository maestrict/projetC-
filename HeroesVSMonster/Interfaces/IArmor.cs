using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Interfaces
{
    public interface IArmor
    {
        public string ArmorName { get; set; }
        public int ArmorResistance { get; set; }
    }
}

using HeroesVSMonster.Models;
using HeroesVSMonster.Models.MapElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Game.Map
{
    public class Case
    {
        public Person? Person { get; set; }
        public Market? Market { get; set; }
        public Money? Money { get; set; }
        public Wall? Wall { get; set; }
    }
}

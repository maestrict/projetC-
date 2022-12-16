using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Game
{
    public class SetupGame
    {
        public SetupGame(string name, string choise, int monsterQty)
        {
            Name = name;
            Choise = choise;
            MonsterQty = monsterQty;
        }

        public string Name { get; set; }
        public string Choise { get; set; }

        public int MonsterQty { get; set; }
    }
}

using HeroesVSMonster.Game;
using HeroesVSMonster.Interfaces;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Models.Monstre.Type
{
    public class Loup : Monster,ILeather
    {
        public Loup(int sizeGame) 
        {
            Offset = new Position(Tools.StartRow(sizeGame), Tools.StartColumn(sizeGame));
        }
        public int Leather { get; set; }
       
    }
}

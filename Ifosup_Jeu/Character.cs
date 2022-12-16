using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ifosup_Jeu
{
    public abstract class Character : Entity
    {
        private int level;
        private int experience;

        public Character(string name) : base(name)
        {
            this.name = name;
            level = 1;
            experience = 0;
        }

        public void GainExperience(int experience)
        {
            this.experience += experience;
            while (this.experience >= requiredExperience())
            {
                level += 1;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Bravo = vous avez atteint le niveau " + level + " !");

                lifePoints += 10; //pourrait-être généré aléatoirement
                MinDamage += 2;
                MaxDamage += 2;
            }
        }

        public double requiredExperience()
        {
            return Math.Round(4 * (Math.Pow(level, 3) / 5)); //courbe d'évolution XP de Pokémon (rapide)
        }

        public string Characteristic()
        {
            return this.name + "\n" +
                "Points de vie : " + lifePoints + "\n" +
                "Niveau : " + level + "\n" +
                "Points d'expérience : (" + experience + "/" + requiredExperience() + ")\n" +
                "Dégats : [" + MinDamage + ";" + MaxDamage + "]";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifosup_Jeu
{
    public abstract class Entity
    {
        protected string name;
        protected bool isdead = false;
        protected int lifePoints;
        protected int MinDamage;
        protected int MaxDamage;
        protected Random random = new Random();

        public Entity(string name)
        {
            this.name = name;
        }

        public void Attack(Entity entity)
        {
            int damages = random.Next(MinDamage, MaxDamage);

            entity.LooseLifePoints(damages);
            Console.WriteLine(this.name + "(" + this.lifePoints + ")" + "attaque : " + entity.name);
            Console.WriteLine(entity.name + " a perdu " + damages + " points de vie");
            Console.WriteLine("Il reste " + entity.lifePoints + " points de vie à " + entity.name);
            if (entity.isdead)
            {
                Console.WriteLine(entity.name + " est mort");
            }
        }

        protected void LooseLifePoints(int lifePoints)
        {
            this.lifePoints -= lifePoints;
            if (this.lifePoints <= 0)
            {
                this.lifePoints = 0; //pour ne pas afficher -2 de vie
                isdead = true;
            }
        }

        public bool IsDead()
        {
            return this.isdead;
        }
    }
}

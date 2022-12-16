using HeroesVSMonster.Game.Fight;
using HeroesVSMonster.Interfaces;
using HeroesVSMonster.Models;
using HeroesVSMonster.Models.Hero;
using HeroesVSMonster.Models.Hero.Type;
using HeroesVSMonster.Models.MapElements;
using HeroesVSMonster.Models.Monstre;
using HeroesVSMonster.Models.Monstre.Type;
using HeroesVSMonster.Models.Objet;
using HeroesVSMonster.Outils.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeroesVSMonster.Outils
{
    public static class Tools
    {
        // c'est ici que nous allons faire toute nos fonctions static qui vont nous servir partout 

        public static void Fight(Hero hero, Monster monster) 
        {
            Console.Clear();
            Console.WriteLine("espace pour frapper et F pour fuir");
            StartFight startFight = new StartFight(hero,monster);

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                startFight.KeyDown(keyinfo);
            }
            while (keyinfo.Key != ConsoleKey.F);
            // player.Stop();
            // string fullPathToSound = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Audio\CoDA-Lullaby.wav");
            // string pathToSound = fullPathToSound.Replace("bin\\Debug\\net6.0", "");
            // player.SoundLocation = pathToSound;
            // player.PlayLooping();
            Console.Clear();
        }

        public static void PrintMenu(Hero hero, List<Person>monsters,int size)
        {
            // Ici Mathieu fait le menu 
            // va s'afficher en haut de la grille du jeux avec l'etat du hero a apres chaque movement

            int column = 5;
            int row = 1;
            int length = 30;
            int height = 10;

            //int test = 0;
            //foreach (Person person in monsters)
            //{
            //    if (test % 2 == 0)
            //    {
            //        person.LifePoint = 0;
            //    }
            //    test++;
            //}

            //affichage du menu hero
            MenuOutline(column, row, length, height);
            CharacteristicDisplay(hero, column + 2, row + 1, length, height);
            //MenuOutline(column + size * 3 + 2, row + height + 3, length - 10, monsters.Count + 3);  //affichage du tableau de chasse
            //MenuOutline(column + size * 3 + 2, row + height + 5, length - 10, monsters.Count + 1);

            //affichage du tableau de chasse
            MenuOutline(column + length * 2 + 4, row , length - 10, 3 + 3); 
            MenuOutline(column + length * 2 + 4, row + 2, length - 10, 3 + 1);
            HuntingBoardDisplay(column + length * 2 + 6, row + 1, monsters);

            //affichage des commande de jeu
            MenuOutline(column + length + 2, row, length, height); 
            MenuOutline(column + length + 2, row + 2, length, height - 2);
            CommandDisplay(column + length + 4, row + 1);

        }


        public static void CommandDisplay(int column, int row)
        {
            Console.SetCursorPosition(column, row);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Commande de jeu");
            Console.SetCursorPosition(column, row + 2);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Pour vous deplacez utiliser");
            Console.SetCursorPosition(column, row + 3);
            Console.WriteLine("les touches directionnelles");
            Console.SetCursorPosition(column, row + 5);
            Console.WriteLine("Barre d'espacement lors");
            Console.SetCursorPosition(column, row + 6);
            Console.WriteLine("des combat");
            Console.SetCursorPosition(column, row + 8);
            Console.WriteLine("F pour fuir les combats");
            Console.ResetColor();
        }

        public static void HuntingBoardDisplay(int column, int row, List<Person> monsters)
        {
            Console.SetCursorPosition(column, row);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tableau de chasse");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            int nbLoup = 0;
            int nbOrque = 0;
            int nbCentaure = 0;
            foreach (Person person in monsters)
            {
                if (person.LifePoint <= 0)
                {
                    if (person.GetType().ToString().Split('.').Last() == "Loup")
                    {
                        nbLoup++;
                    }
                    else if (person.GetType().ToString().Split('.').Last() == "Orque")
                    {
                        nbOrque++;
                    }
                    else
                    {
                        nbCentaure++;
                    }
                }
            }
            Console.SetCursorPosition(column, row + 2);
            if (nbLoup <= 1)
            {
                Console.WriteLine($"{nbLoup} Loup");
            }
            else
            {
                Console.WriteLine($"{nbLoup} Loups");
            }
            Console.SetCursorPosition(column, row + 3);
            if (nbOrque <= 1)
            {
                Console.WriteLine($"{nbOrque} Orque");
            }
            else
            {
                Console.WriteLine($"{nbOrque} Orques");
            }
            Console.SetCursorPosition(column, row + 4);
            if (nbCentaure <= 1)
            {
                Console.WriteLine($"{nbCentaure} Centaure");
            }
            else
            {
                Console.WriteLine($"{nbCentaure} Centaures");
            }
            Console.ResetColor();
        }

        public static void CharacteristicDisplay(Hero hero, int column, int row, int length, int height)
        {
            Console.SetCursorPosition(column, row);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write($"{hero.Name}");
            Console.SetCursorPosition(length - (hero.GetType().ToString().Split('.').Last()).Length + 4, row);
            Console.WriteLine($"{hero.GetType().ToString().Split('.').Last()}");
            Console.ResetColor();
            Console.SetCursorPosition(column, row + 1);
            Console.Write($"HP");
            Console.SetCursorPosition(column + 3, row + 1);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{hero.LifePoint}/{hero.MaxLifePoint}");
            Console.ResetColor();
            Console.SetCursorPosition(column, row + 2);
            Console.Write($"Experience");
            Console.SetCursorPosition(column + 11, row + 2);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{hero.Experience}");
            Console.ResetColor();
            Console.SetCursorPosition(length - (1 + (hero.Money).ToString().Length), row + 2);
            Console.WriteLine($"Gold");
            Console.SetCursorPosition(length - (1 + (hero.Money).ToString().Length) + 5, row + 2);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{hero.Money}");
            Console.ResetColor();
            Console.SetCursorPosition(column, row + 3);
            Console.Write($"Strength");
            Console.SetCursorPosition(column + 9, row + 3);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{hero.Strength}");
            Console.ResetColor();
            Console.SetCursorPosition(length - (7 + (hero.Resistance).ToString().Length), row + 3);
            Console.WriteLine($"Resistance");
            Console.SetCursorPosition(length - ((hero.Resistance).ToString().Length) + 4, row + 3);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{hero.Resistance}");
            Console.ResetColor();
            Console.SetCursorPosition(column, row + 4);
            Console.Write($"Weapon");
            Console.SetCursorPosition(column + 7, row + 4);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{hero.ArmedName}");
            Console.ResetColor();
            Console.SetCursorPosition(length - (2 + (hero.ArmedStrength).ToString().Length), row + 4);
            Console.WriteLine($"Power {hero.ArmedStrength}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(length - (2 + (hero.ArmedStrength).ToString().Length) + 6, row + 4);
            Console.WriteLine($"{hero.ArmedStrength}");
            Console.ResetColor();
            Console.SetCursorPosition(column, row + 5);
            Console.WriteLine($"Armor");
            Console.SetCursorPosition(column + 6, row + 5);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{hero.ArmorName}");
            Console.ResetColor();
            Console.SetCursorPosition(length - (7 + (hero.ArmorResistance).ToString().Length), row + 5);
            Console.WriteLine($"Resistance {hero.ArmorResistance}");
            Console.SetCursorPosition(length - (7 + (hero.ArmorResistance).ToString().Length) + 11, row + 5);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{hero.ArmorResistance}");
            Console.ResetColor();
            Console.SetCursorPosition(column, row + 6);
            if (hero.Potion <= 1)
            {
                Console.WriteLine($"Potion");
                Console.SetCursorPosition(column + 7, row + 6);
            }
            else
            {
                Console.WriteLine($"Potions");
                Console.SetCursorPosition(column + 8, row + 6);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{hero.Potion}");
            Console.ResetColor();
            if (hero.Leather <= 1)
            {
                Console.SetCursorPosition(length - (1 + (hero.Leather).ToString().Length), row + 6);
                Console.WriteLine($"Cuir");
                Console.SetCursorPosition(length - (1 + (hero.Leather).ToString().Length) + 5, row + 6);
            }
            else
            {
                Console.SetCursorPosition(length - (2 + (hero.Leather).ToString().Length), row + 6);
                Console.WriteLine($"Cuirs");
                Console.SetCursorPosition(length - (1 + (hero.Leather).ToString().Length) + 5, row + 6);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{hero.Leather}");
            Console.ResetColor();
            Console.SetCursorPosition(column, row + 7);
            Console.WriteLine("Offensive power");
            Console.SetCursorPosition(column + 16, row + 7);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(hero.Strength + hero.ArmedStrength);
            Console.ResetColor();
            //Console.SetCursorPosition(length - (((hero.Strength + hero.ArmedStrength).ToString().Length) - 4), row + 7);
            //Console.WriteLine(hero.Strength + hero.ArmedStrength);
            Console.SetCursorPosition(column, row + 8);
            Console.WriteLine("Defensive power");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(column + 16, row + 8);
            Console.WriteLine(hero.Resistance + hero.ArmorResistance);
            Console.ResetColor();
        }

        //public static int RacePosition(Hero hero)
        //{
        //    if (hero.GetType().ToString().Split('.').Last() == "Elfe" || hero.GetType().ToString().Split('.').Last() == "Nain")
        //    {
        //        return 25;
        //    }
        //    else
        //    {
        //        return 23;
        //    }
        //}

        public static void MenuOutline(int column, int row, int length, int height)
        {
            for (int c = column; c <= column + length; c++)
            {
                for (int r = row; r <= row + height; r++)
                {
                    if (c == column && r != row && r != row + height)
                    {
                        Console.SetCursorPosition(c, r);
                        Console.Write("|");
                    }
                    else if (c == column + length && r != row && r != row + height)
                    {
                        Console.SetCursorPosition(c, r);
                        Console.Write("|");
                    }
                    else if ((r == row || r == row + height) && c != column && c != column + length)
                    {
                        Console.SetCursorPosition(c, r);
                        Console.Write("-");
                    }

                }
            }
        }

        public static void OpenMarket(Market market,Hero hero)
        {

            // Pour Christophe ;) fait un fonction qui appel ton code stp
            // utiliste la clase marcket a ta sauce 
            // dans Tools tu trouvera certain fonctionnalité utile

            Console.Clear();
            string marketClose = "";
            while (marketClose != "O")
            {
                Console.WriteLine($"open market : ");
                Console.WriteLine("--------------");
                Console.WriteLine();
                Console.WriteLine($"potion : {market.Potion}");
                Console.WriteLine();
                Console.WriteLine("--------------");
                Console.WriteLine();
                Console.WriteLine($"cuir : {market.Leather}");
                Console.WriteLine();
                Console.WriteLine("--------------");
                Console.WriteLine();
                Console.WriteLine("Les amures :");
                Console.WriteLine();
                foreach (Armor armor in market.Armors)
                {
                    Console.WriteLine($"armure : {armor.ArmorName}");
                    Console.WriteLine($"resistance de l'armure : {armor.ArmorResistance}");
                }
                Console.WriteLine("--------------");
                Console.WriteLine();
                Console.WriteLine("Les armes");
                Console.WriteLine();
                foreach (Arme arme in market.Armes)
                {
                    Console.WriteLine($"arme : {arme.ArmedName}");
                    Console.WriteLine($"force de l'arme : {arme.ArmedStrength}");
                }
                Console.WriteLine("--------------");
                Console.WriteLine();
                Console.Write("Tapez O pour quitter le marché : ");
                marketClose = Console.ReadLine();

            }
            Console.Clear();

        }

        public static int StartMoney()
        {
            Random rand = new Random();
            return rand.Next(10, 20);
        }
        public static int StartLeather()
        {
            Random rand = new Random();
            return rand.Next(3, 6);
        }
        public static int StartPotion()
        {
            Random rand = new Random();
            return rand.Next(3, 6);
        }

        public static int StartLifePoint()
        {
            Random rand = new Random();
            return rand.Next(8, 16);
        }

        public static int StartStrenght()
        {
            Random rand = new Random();
            return rand.Next(8, 16);
        }

        public static int StartResistance()
        {
            Random rand = new Random();
            return rand.Next(8, 16);
        }
        public static int StartSpeed()
        {
            Random rand = new Random();
            return rand.Next(8, 16);
        }

        public static void StartArmed() { }
        public static void StartArmor() { }

        public static int MonsterLoseLifePoint(Monster monster)
        {
            Random rand = new Random();
            if(monster is Centor) return rand.Next(1,4);    
            if(monster is Loup) return rand.Next(1,4);
            if(monster is Orque) return rand.Next(1,4);
            return 0;
        }

        public static int HeroLoseLifePoint(Hero hero)
        {
            Random rand = new Random();
            if (hero is Elfe) return rand.Next(1,5);
            if (hero is Nain) return rand.Next(1,5);
            if (hero is Humain) return rand.Next(1,5);
            return 0;
        }


        public static List<Arme> CreateArmedList(int quantity)
        {
            List<Arme> ListArmes = new List<Arme>();
            for (int i = 0; i < quantity; i++)
            {
                ListArmes.Add(CreateArmed());
            }

            return ListArmes;
        }

        public static List<Armor> CreateArmorList(int quantity)
        {
            List<Armor> ListArmors = new List<Armor>();
            for (int i = 0; i < quantity; i++)
            {
                ListArmors.Add(CreateArmor());
            }
            return ListArmors;
        }

        public static Arme? CreateArmed()
        {
            Random rand = new Random();
            int value = rand.Next(1, 6);

            if (value == 1) return new Arme(ArmeEnum.Epée.ToString(), CalculedStrenghArmed());
            if (value == 2) return new Arme(ArmeEnum.Epée.ToString(), CalculedStrenghArmed());
            if (value == 3) return new Arme(ArmeEnum.Epée.ToString(), CalculedStrenghArmed());
            if (value == 4) return new Arme(ArmeEnum.Epée.ToString(), CalculedStrenghArmed());
            if (value == 5) return new Arme(ArmeEnum.Epée.ToString(), CalculedStrenghArmed());
            return null;
        }

        public static Armor? CreateArmor()
        {
            Random rand = new Random();
            int value = rand.Next(1, 6);

            if (value == 1) return new Armor(ArmorEnum.Bois.ToString(), CalculedResitanceArmuor());
            if (value == 2) return new Armor(ArmorEnum.Fer.ToString(), CalculedResitanceArmuor());
            if (value == 3) return new Armor(ArmorEnum.Metal.ToString(), CalculedResitanceArmuor());
            if (value == 4) return new Armor(ArmorEnum.Nikel.ToString(), CalculedResitanceArmuor());
            if (value == 5) return new Armor(ArmorEnum.Or.ToString(), CalculedResitanceArmuor());
            return null;
        }

        public static int CalculedStrenghArmed()
        {
            Random rand = new Random();
            return rand.Next(5, 10);
        }

        public static int CalculedResitanceArmuor()
        {
            Random rand = new Random();
            return rand.Next(4, 8);
        }

        public static Hero? CreateHero(string heroChoise, string heroName, int sizeGame) 
        {
            if (heroChoise == "H") return new Humain(heroName, sizeGame);
            if (heroChoise == "N") return new Nain(heroName, sizeGame);
            if (heroChoise == "E") return new Elfe(heroName, sizeGame);
            return null;
        }

        public static Monster? CreateMonstre(int sizeGame)
        {
            Random rand = new Random();
            int value = rand.Next(1, 4);

            if (value == 1) return new Centor(sizeGame);
            if (value == 2) return new Loup(sizeGame);
            if (value == 3) return new Orque(sizeGame);
            return null;
        }

        public static List<Person?> CreateMonsterList(int quantity, int sizeGame)
        {
            List<Person?> ListMonsters = new List<Person?>();
            for (int i = 0; i < quantity; i++)
            {
                ListMonsters.Add(CreateMonstre(sizeGame));
            }
            return ListMonsters;
        }

        public static int StartRow(int sizeGame)
        {
            Random rand = new Random();
            return rand.Next(0, sizeGame);
        }

        public static int StartColumn(int sizeGame)
        {
            Random rand = new Random();
            return rand.Next(0, sizeGame);
        }


     
        public static List<Wall> CreateWallList(int sizeGame)
        {
            List<Wall?> ListWalls = new List<Wall?>();
            for (int i = 0; i < sizeGame; i++)
            {
                ListWalls.Add(new Wall(sizeGame));
            }
            return ListWalls;
        }

        public static List<Money> CreateMoneyList( int sizeGame)
        {
            List<Money?> ListMoneys = new List<Money?>();
            for (int i = 0; i < sizeGame; i++)
            {
                ListMoneys.Add(new Money(sizeGame));
            }
            return ListMoneys;
        }


    }
}

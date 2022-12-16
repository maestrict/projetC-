using System;
using System.Diagnostics;

namespace Ifosup_Jeu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Play(Character character)
        {
            Monster monster = new Monster("Mbayo enragé");
            bool victory = true;
            bool next = false;

            while (!monster.IsDead())
            {
                //Tour du monstre
                Console.ForegroundColor = ConsoleColor.Red;
                monster.Attack(character);
                Console.WriteLine();
                Console.ReadKey(true);

                if (character.IsDead())
                {
                    victory = false;
                    break;
                }

                //Tour du personnage
                Console.ForegroundColor = ConsoleColor.Green;
                character.Attack(monster);
                Console.WriteLine();
                Console.ReadKey(true);
            }
            if (victory)
            {
                character.GainExperience(5);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine(character.Characteristic());

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();

                while (!next)
                {
                    Console.WriteLine("Salle suivante ? (O/N)");
                    string input = Console.ReadLine().ToUpper();
                    if (input == "O")
                    {
                        next = true;
                        Play(character);
                    }
                    else if (input == "N")
                    {
                        Environment.Exit(0); //le processus se termine correctement
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("C'est perdu...");
                Console.ReadKey();
            }
        }
        static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Le jeu");
            Console.WriteLine();
            Console.WriteLine("Choisis ta classe : ");
            Console.WriteLine("1. Guerrier");
            Console.WriteLine("2. Magicien");
            Console.WriteLine("3. Rôdeur");
            Console.WriteLine("4. Quitter");
            Console.WriteLine();

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Vous avez choisis Guerrier !");
                    Console.WriteLine();
                    Play(new Warrior("Pastofarian"));
                    break;
                case "2":
                    Console.WriteLine("Vous avez choisis Magicien !");
                    Console.WriteLine();
                    Play(new Magician("Pastofarian"));
                    break;
                case "3":
                    Console.WriteLine("Vous avez choisis Rôdeur !");
                    Console.WriteLine();
                    Play(new Ranger("Pastofarian"));
                    break;
                case "4":
                    break;
            }
        }
    }
}
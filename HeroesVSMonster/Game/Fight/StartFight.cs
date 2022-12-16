using HeroesVSMonster.Models;
using HeroesVSMonster.Models.Hero;
using HeroesVSMonster.Models.MapElements;
using HeroesVSMonster.Models.Monstre;
using HeroesVSMonster.Models.Monstre.Type;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVSMonster.Game.Fight
{
    public class StartFight
    {
        private GameGrid _fightGrid;
        private FightCase[,] _fightCases;
        private Hero _hero;
        private Monster _monster;
        private string _message;
        private string _Stat;
        private string _messageAttackHero;
        private string _messageAttackMonster;
        //private SoundPlayer _player;
        public StartFight(Hero hero, Monster monster)
        {
            _fightGrid = new GameGrid(10, 4);
            _hero = hero;
            _monster = monster;
            _fightCases = SetupFight();
            CreateImage();
            DrawFightGrid();
            // player.Stop();
            // _player = player;
        }

        public FightCase[,] SetupFight()
        {
            FightCase[,] fightCases = new FightCase[_fightGrid.Rows, _fightGrid.Columns];

            for (int r = 0; r < _fightGrid.Rows; r++)
            {
                for (int c = 0; c < _fightGrid.Columns; c++)
                {
                    fightCases[r, c] = new FightCase(r, c);
                }
            }
            return fightCases;
        }

        private void CreateImage()
        {
            foreach (FightCase fightCase in _fightCases)
            {
                if (fightCase.Row == 2 && fightCase.Column == 0) fightCase.Image = "♦";
                if (fightCase.Row == 2 && fightCase.Column == 1) fightCase.Image = "♦";
                if (fightCase.Row == 3 && fightCase.Column == 1) fightCase.Image = "♦";
                if (fightCase.Row == 2 && fightCase.Column == 2) fightCase.Image = "♦";
                if (fightCase.Row == 1 && fightCase.Column == 3) fightCase.Image = "♦";
                if (fightCase.Row == 3 && fightCase.Column == 3) fightCase.Image = "♦";

                if (_monster is Loup)
                {
                    if (fightCase.Row == 1 + 5 && fightCase.Column == 1) fightCase.Image = "♠";
                    if (fightCase.Row == 2 + 5 && fightCase.Column == 2) fightCase.Image = "♠";
                    if (fightCase.Row == 3 + 5 && fightCase.Column == 2) fightCase.Image = "♠";
                    if (fightCase.Row == 3 + 5 && fightCase.Column == 3) fightCase.Image = "♠";
                    if (fightCase.Row == 1 + 5 && fightCase.Column == 3) fightCase.Image = "♠";
                }

                if (_monster is Orque)
                {
                    if (fightCase.Row == 2 + 5 && fightCase.Column == 0) fightCase.Image = "♠";
                    if (fightCase.Row == 2 + 5 && fightCase.Column == 1) fightCase.Image = "♠";
                    if (fightCase.Row == 1 + 5 && fightCase.Column == 1) fightCase.Image = "♠";
                    if (fightCase.Row == 2 + 5 && fightCase.Column == 2) fightCase.Image = "♠";
                    if (fightCase.Row == 1 + 5 && fightCase.Column == 3) fightCase.Image = "♠";
                    if (fightCase.Row == 3 + 5 && fightCase.Column == 3) fightCase.Image = "♠";
                }

                if (_monster is Centor)
                {
                    if (fightCase.Row == 1 + 5 && fightCase.Column == 1) fightCase.Image = "♠";
                    if (fightCase.Row == 2 + 5 && fightCase.Column == 2) fightCase.Image = "♠";
                    if (fightCase.Row == 3 + 5 && fightCase.Column == 2) fightCase.Image = "♠";
                    if (fightCase.Row == 3 + 5 && fightCase.Column == 3) fightCase.Image = "♠";
                    if (fightCase.Row == 1 + 5 && fightCase.Column == 3) fightCase.Image = "♠";
                    if (fightCase.Row == 2 + 5 && fightCase.Column == 0) fightCase.Image = "♠";
                    if (fightCase.Row == 2 + 5 && fightCase.Column == 1) fightCase.Image = "♠";
                }
            }
        }

        private void DrawFightGrid()
        {
            Console.Clear();
            Console.WriteLine();
            Console.SetCursorPosition(3, 2);
            Console.WriteLine($"{_hero.Name} VS {_monster.Name}");
            Console.SetCursorPosition(3,5);
            CharacteristicDisplay(_hero, _monster, 1 + 2, 4 + 1, 2, 2);
            Console.SetCursorPosition(3, 10);
            Console.WriteLine(_message);

            foreach (FightCase fightCase in _fightCases)
            {
                Console.SetCursorPosition(fightCase.Row * 3 + 25, fightCase.Column * 2 + 18);

                if (fightCase.Image == "♦")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(fightCase.Image);
                }
                if (fightCase.Image == "♠")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(fightCase.Image);
                }
            }
            Console.SetCursorPosition(3, 44);
            Console.WriteLine(_messageAttackHero);
            Console.SetCursorPosition(3, 46);
            Console.WriteLine(_messageAttackMonster);
            Console.CursorVisible = false;
            Console.ResetColor();
        }

        public void KeyDown(ConsoleKeyInfo keyinfo)
        {
            switch (keyinfo.Key)
            {
                case ConsoleKey.Spacebar:
                    Attack(_hero, _monster);
                    break;
                default:
                    return;
            }
            DrawFightGrid();
        }

        private void Attack(Hero hero, Monster monster)
        {
            int time = 500;
            // string fullPathToSound = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Audio\punch.wav");
            // string pathToSound = fullPathToSound.Replace("bin\\Debug\\net6.0", "");
            // _player.SoundLocation = pathToSound;

            HeroMove(1);
            Thread.Sleep(time);
            _Stat = "";
            _message = $"{_hero.Name} dit : Hop !! ./n";
            _messageAttackHero ="";
            _messageAttackMonster = "";
            DrawFightGrid();

            HeroMove(1);
            Thread.Sleep(time);
            _message = $"{_hero.Name} dit : Tiens prend ca dans ta guele !!";
           // _player.Play();
            DrawFightGrid();

            HeroMove(-1);
            Thread.Sleep(time);
            _message = $"{_hero.Name} dit : je t'ai bien niqué Louis ";
            DrawFightGrid();

            HeroMove(-1);
            Thread.Sleep(time);
            _message = $"{_monster.Name} dit : Ouille ouille ouille";
            HeroAttack(_hero, _monster);
            _messageAttackHero = $"Le héro {hero.Name} infligue {hero.Strength} de dégat au monstre, il reste {monster.LifePoint} point de vie";
            DrawFightGrid();
            if(monster.LifePoint==0){
                MonsterDeath(hero);
            }

            MonsterMove(-1);
            Thread.Sleep(time);
            _message = $"{_monster.Name} dit : Hop hahaha ";
            DrawFightGrid();

            MonsterMove(-1);
            Thread.Sleep(time);
            _message = $"{_monster.Name} dit : Pataaaaaaaaaaaate";
           // _player.Play();
            DrawFightGrid();

            MonsterMove(1);
            Thread.Sleep(time);
            _message = "";
            _messageAttackMonster = $"Le monstre infligue {monster.Strength} de dégat au héro, il reste {hero.LifePoint} point de vie au héros";
            MonsterAttack(_hero,_monster);
            DrawFightGrid();
            

            MonsterMove(1);
            Thread.Sleep(time);
            _message = $"{_monster.Name} dit : Bas toi encore si tu l'ose !";
            DrawFightGrid();
            if(hero.LifePoint==0){
                GameOver();
            }

            // fullPathToSound = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Audio\victory.wav");
            // pathToSound = fullPathToSound.Replace("bin\\Debug\\net6.0", "");
            // _player.SoundLocation = pathToSound;
            // _player.Play();
            // Thread.Sleep(2000);
            // _player.Stop();
        }

        private void HeroMove(int move)
        {
            foreach (FightCase fightCase in _fightCases)
            {
                if (fightCase.Image == "♦")
                {
                    if (IsInsideFightGrid(fightCase.Row + move, fightCase.Column))
                    {
                        fightCase.Move(fightCase.Row + move, fightCase.Column);
                    }
                }
            }
        }

        private void MonsterMove(int move)
        {
            foreach (FightCase fightCase in _fightCases)
            {
                if (fightCase.Image == "♠")
                {
                    if (IsInsideFightGrid(fightCase.Row + move, fightCase.Column))
                    {
                        fightCase.Move(fightCase.Row + move, fightCase.Column);
                    }
                }
            }
        }


        private bool IsInsideFightGrid(int r, int c)
        {
            return r >= 0 && r < _fightGrid.Rows && c >= 0 && c < _fightGrid.Columns;
        }

        private void MonsterAttack(Hero hero, Monster monster)
        {
            if(hero.LifePoint>monster.Strength){
                hero.LifePoint -= monster.Strength;
            }else{
                hero.LifePoint = 0;
            }
            // System.Console.WriteLine("");
            // System.Console.WriteLine($"Le monstre {monster.Name} infligue {monster.Strength} de dégat au héro");
            // System.Console.WriteLine($"Il reste {hero.LifePoint} au héro");
            // Thread.Sleep(2000);
            
        }
         private void HeroAttack(Hero hero, Monster monster)
        {
            if(monster.LifePoint>hero.Strength){
                monster.LifePoint -= hero.Strength;
            }else{
                monster.LifePoint = 0;
            }
            // System.Console.WriteLine("");
            // System.Console.WriteLine($"Le héro {hero.Name} infligue {hero.Strength} de dégat au monstre");
            // System.Console.WriteLine($"Il reste {monster.LifePoint} au monstre");
            // Thread.Sleep(2000);
        }

        private void GameOver(){
            Console.Clear();
            System.Console.WriteLine("GAME OVER");
            System.Console.WriteLine("Appuyer sur f pour quitter");
            Thread.Sleep(2000);
        }
        private void MonsterDeath(Hero hero){
            Console.Clear();
            System.Console.WriteLine($"Vous avez vaincu le monstre ! Il vous reste {hero.LifePoint} point de vie");
            System.Console.WriteLine("Appuyer sur f pour quitter");
            Thread.Sleep(2000);
        }
        public void CharacteristicDisplay(Hero hero, Monster monster, int column, int row, int length, int height)
        {
            
            //Stat Héro
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
            Console.Write($"Strength");
            Console.SetCursorPosition(column + 9, row + 2);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{hero.Strength}");
            Console.ResetColor();
            Console.SetCursorPosition(column, row + 3);
            Console.Write($"Weapon");
            Console.SetCursorPosition(column + 7, row + 3);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{hero.ArmedName}");
            Console.ResetColor();

            //Stats Monstre
            Console.SetCursorPosition(column + 14, row+1);
            Console.Write($"Monstre type :{monster.Name}");
            Console.ResetColor();
            Console.SetCursorPosition(column + 14, row + 2);
            Console.Write($"HP");
            Console.SetCursorPosition(column + 17, row + 2);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{monster.LifePoint}/{monster.MaxLifePoint}");
            Console.ResetColor();
            Console.SetCursorPosition(column +14, row + 3);
            Console.Write($"Strength");
            Console.SetCursorPosition(column + 23, row + 3);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{monster.Strength}");
            Console.ResetColor();
           
        }


    }
}



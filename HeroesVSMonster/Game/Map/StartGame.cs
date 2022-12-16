using HeroesVSMonster.Models;
using HeroesVSMonster.Models.Hero;
using HeroesVSMonster.Models.MapElements;
using HeroesVSMonster.Models.Monstre;
using HeroesVSMonster.Outils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HeroesVSMonster.Game.Map
{
    public class StartGame
    {
        private readonly Case[,] _cases;
        private List<Person?> _persons = new List<Person?>();
        private List<Person?> _monsters;
        private Hero? _hero;
        private Market _market;
        private List<Wall> _walls;
        private List<Money> _moneys;
        private int _sizeGame;
        public GameGrid GameGrid { get; set; }
        //public SoundPlayer Player { get; set; }

        public StartGame(int monsterQty, string heroChoise, string heroName, int sizeGame)
        {
            //Player = player;
            _sizeGame = sizeGame;
            GameGrid = new GameGrid(sizeGame, sizeGame);
            _cases = SetupGame(GameGrid, new SetupGame(heroName, heroChoise, monsterQty));


            // player.Stop();
            // string fullPathToSound = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Audio\CoDA-Lullaby.wav");
            // string pathToSound = fullPathToSound.Replace("bin\\Debug\\net6.0", "");
            // player.SoundLocation = pathToSound;
            // //player.SoundLocation = @"C:\Users\bster\Desktop\Perso\Jeux C#\HvsM\Ifosup_Jeu-master\Ifosup_Jeu-master\HeroesVSMonster\Audio\CoDA-Lullaby.wav";
            // player.PlayLooping();

            Step(GameGrid);
        }



        public Case[,] SetupGame(GameGrid grid, SetupGame person)
        {

            Case[,] cases = new Case[grid.Rows, grid.Columns];
            Random ran = new Random();

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    cases[r, c] = new Case();
                }
            }
            _hero = Tools.CreateHero(person.Choise, person.Name, _sizeGame);
            
            cases[_hero.Offset.Row, _hero.Offset.Column].Person = _hero;
            _market = new Market(_sizeGame);
            while(cases[_market.Offset.Row, _market.Offset.Row].Person != null)
            {
                _market.Offset.Row = Tools.StartRow(_sizeGame);
                _market.Offset.Column = Tools.StartColumn(_sizeGame);
            }
            cases[_market.Offset.Row, _market.Offset.Column].Market = _market;

            _monsters = Tools.CreateMonsterList(person.MonsterQty, _sizeGame);
            foreach(Monster monster in _monsters)
            {
                (int row, int column) = monster.Offset;
                while( cases[row,column].Person != null || cases[row,column].Market != null)
                {
                    row = Tools.StartRow(_sizeGame);
                    column = Tools.StartColumn(_sizeGame);
                }
                monster.Offset = new Position(row, column);
                cases[row, column].Person = monster;
            }
            
            _walls = Tools.CreateWallList(_sizeGame);
            foreach (Wall wall in _walls)
            {
                (int row, int column) = wall.Offset;
                    while (cases[row, column].Person != null || cases[row,column].Wall != null || cases[row, column].Market != null)
                    {
                        row = Tools.StartRow(_sizeGame);
                        column = Tools.StartColumn(_sizeGame);
                    }
                wall.Offset = new Position(row, column);
                cases[row, column].Wall = wall;
            }

            _moneys = Tools.CreateMoneyList(_sizeGame);
           
            foreach (Money money in _moneys)
            {
                (int row, int column) = money.Offset;
                while (cases[row,column].Wall != null  || cases[row,column].Market != null)
                {
                    row = Tools.StartRow(_sizeGame);
                    column = Tools.StartColumn(_sizeGame);
                }
                money.Offset = new Position(row, column);
                cases[row, column].Money = money;
            }
            

            foreach (Person? monster in _monsters)
            {
                _persons.Add(monster);
            }
            _persons.Add(_hero);

            return cases;
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Console.SetCursorPosition(r * 3 + 5, c * 2 + 14);

                    _cases[r, c].Wall = _walls.Where(w => w.Offset.Row == r && w.Offset.Column == c).FirstOrDefault();
                    _cases[r, c].Person = _persons.Where(p => p.Offset.Row == r && p.Offset.Column == c && p.LifePoint > 0).FirstOrDefault();
                    _cases[r, c].Money = _moneys.Where(m => m.Offset.Row == r && m.Offset.Column == c && m.MoneyQuantity > 0).FirstOrDefault();

                    if (_market.Offset.Row == r && _market.Offset.Column == c) _cases[r, c].Market = _market;

                    if (_cases[r, c].Person is Hero)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        //Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(_cases[r, c].Person.Name[0]);
                    }
                    else if (_cases[r, c].Market is Market)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        //Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("M");
                    }
                    else if (_cases[r, c].Person is Monster)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        //Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(_cases[r, c].Person.Name[0]);
                    }
                    else if (_cases[r, c].Wall is Wall)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        //Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(_cases[r, c].Wall.WallDisgn);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        //Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write("♦");
                    }
                }
            }
            Console.ResetColor();
        }

        private void Step(GameGrid gameGrid)
        {
            MoveMonster(_monsters, gameGrid);
            CheckMarket();
            Person? monster = CheckFight();
            if (monster is not null) Tools.Fight(_hero, (Monster)monster);
            CheckMoney();
            Tools.PrintMenu(_hero, _monsters,_sizeGame);
            DrawGrid(gameGrid);
            Console.CursorVisible = false;
        }

        public void KeyDown(ConsoleKeyInfo keyinfo)
        {
            switch (keyinfo.Key)
            {
                case ConsoleKey.UpArrow:// "LeftArrow"
                    MoveLeft();
                    break;
                case ConsoleKey.DownArrow: // "RightArrow"
                    MoveRight();
                    break;
                case ConsoleKey.RightArrow: //"DownArrow"
                    MoveDown();
                    break;
                case ConsoleKey.LeftArrow: //"UpArrow"
                    MoveUp();
                    break;
                default:
                    return;
            }
            Step(GameGrid);
        }


        public void MoveLeft()
        {
            if (GameGrid.IsInsideGrid(_hero.Offset.Row, _hero.Offset.Column - 1)
                && !IsWallPlace(_hero.Offset.Row, _hero.Offset.Column - 1))
                _hero.Move(_hero.Offset.Row, _hero.Offset.Column - 1);
        }

        public void MoveRight()
        {
            if (GameGrid.IsInsideGrid(_hero.Offset.Row, _hero.Offset.Column + 1)
                 && !IsWallPlace(_hero.Offset.Row, _hero.Offset.Column + 1))
                _hero.Move(_hero.Offset.Row, _hero.Offset.Column + 1);
        }

        public void MoveDown()
        {
            if (GameGrid.IsInsideGrid(_hero.Offset.Row + 1, _hero.Offset.Column)
                 && !IsWallPlace(_hero.Offset.Row +1 , _hero.Offset.Column))
                _hero.Move(_hero.Offset.Row + 1, _hero.Offset.Column);
        }

        public void MoveUp()
        {
            if (GameGrid.IsInsideGrid(_hero.Offset.Row - 1, _hero.Offset.Column)
                 && !IsWallPlace(_hero.Offset.Row -1, _hero.Offset.Column))
                _hero.Move(_hero.Offset.Row - 1, _hero.Offset.Column);
        }


        private Person? CheckFight()
        {
            foreach (Person? monster in _monsters)
            {
                if (monster.Offset.Column == _hero.Offset.Column && monster.Offset.Row == _hero.Offset.Row)
                {
                    return monster;
                }
            }
            Console.ResetColor();
            return null;
        }

        private void CheckMarket()
        {
            if (_market.Offset.Row == _hero.Offset.Row && _market.Offset.Column == _hero.Offset.Column)
            {
                Tools.OpenMarket(_market, _hero);
            }
            else
            {
                Console.Clear();
            }
        }

        private void CheckMoney()
        {
            foreach (Money money in _moneys)
            {
                if (money.Offset.Row == _hero.Offset.Row && money.Offset.Column == _hero.Offset.Column)
                {
                    _hero.Money += money.MoneyQuantity;
                    money.MoneyQuantity = 0;
                }
            }
        }

        private void MoveMonster(List<Person> Monsters, GameGrid grid)
        {
            Random rand = new Random();
            foreach (Person monster in Monsters)
            {

                _cases[_hero.Offset.Row, _hero.Offset.Column].Person = null;
                int row = rand.Next(-1, 2);
                int col = rand.Next(-1, 2);

                if (grid.IsInsideGrid(monster.Offset.Row + row, monster.Offset.Column + col)
                    && !IsMarketPlace(monster.Offset.Row + row, monster.Offset.Column + col)
                    && !IsWallPlace(monster.Offset.Row + row, monster.Offset.Column + col)
                    )
                {

                    monster.Move(monster.Offset.Row + row, monster.Offset.Column + col);
                }
            }
        }



        public bool IsMarketPlace(int row, int col)
        {
            if (col == _market.Offset.Column && row == _market.Offset.Row) return true;
            return false;
        }

        public bool IsWallPlace(int row, int col)
        {
            foreach (Wall wall in _walls)
            {
                if (wall.Offset.Row == row && wall.Offset.Column == col)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

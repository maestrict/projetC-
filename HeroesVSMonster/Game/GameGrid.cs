using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesVSMonster.Game.Map;

namespace HeroesVSMonster.Game
{
    public class GameGrid
    {
        private readonly int[,] _grid;

        public int Rows { get; set; }
        public int Columns { get; set; }

        public GameGrid (int rows, int columns)
        {
            Columns = columns; 
            Rows= rows;
            _grid = new int[rows,columns];
        }

        public int this[int row, int column]
        {
            get => _grid[row, column];
            set => _grid[row, column] = value;
        }

        
        // verifier si la case est dans la grille
        public bool IsInsideGrid(int r,int c)
        {
            return r >= 0 && r < Rows && c>=0 && c < Columns;
        }


        //// verifier si la case est remplie ou pas
        //public bool IsCellEmpty(int r, int c, Case _case)
        //{
        //    return IsInsideGrid(r, c) && _case.Person is null;
        //}

       

    }
}

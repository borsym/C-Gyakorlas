using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TiliToli.Model
{
    class TiliToliTable
    {
        
        private Int32[,] _fieldValues; // mezőértékek

        /// Játéktábla méretének lekérdezése.
        /// </summary>
        public Int32 Size { get { return _fieldValues.GetLength(0); } }

      
        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }

        public Boolean IsFilled
        {
            get
            {
                //for(int i = 0; i < _fieldValues.GetLength(0); i++)
                //{
                //    for(int j = 0; j < _fieldValues.GetLength(0) - 1; j++)
                //    {
                //        if (i == (Size - 1) && j == (Size - 1)) return true;
                //        if (_fieldValues[i,j] > _fieldValues[i,j + 1]) return false;
                //    }
                //}
                //return true;
                return false;
            }
        }

        #region Constructors

        /// <summary>
        /// Sudoku játéktábla példányosítása.
        /// </summary>
        public TiliToliTable() : this(5) { }

        /// <summary>
        /// Sudoku játéktábla példányosítása.
        /// </summary>
        /// <param name="tableSize">Játéktábla mérete.</param>
        /// <param name="regionSize">Ház mérete.</param>
        public TiliToliTable(Int32 tableSize)
        {
            if (tableSize < 0)
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");
           
            _fieldValues = new Int32[tableSize, tableSize];
        }

        public Boolean IsEmpty(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == 0;
        }

  
        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y];
        }

        public void SetValue(Int32 x, Int32 y, Int32 value)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                // if (!CheckStep(x, y)) 
                //     return;
                Debug.Write("IGEN ITT VAGYOK " + value+"\n");
            _fieldValues[x, y] = value;
        }


        public void StepValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            //if(CheckStep(x,y))
        }


        
        private Boolean CheckStep(Int32 x, Int32 y)  //itt fogom majd megnézni a körneyezetét hogy léphetek e
        {
            if (_fieldValues[x, y] == 0)
                return true;
            return false;   
        }

        #endregion
    }
}

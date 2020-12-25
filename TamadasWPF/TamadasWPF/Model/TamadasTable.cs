using System;
using System.Collections.Generic;
using System.Text;

namespace TamadasWPF.Model
{
    class TamadasTable
    {

        private Int32[,] _fieldValues; // mezőértékek
        public Int32 Size { get { return _fieldValues.GetLength(0); } }
        public Boolean IsGameOver
        {
            get
            {
                if (_fieldValues[0, Size - 1] > 4) return true;
                if (_fieldValues[Size - 1, 0] <= 4 && _fieldValues[Size - 1, 0] != 0) return true;
                return false;
            }
        }
        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }


        #region Constructors

        public TamadasTable() : this(6) { }

        public TamadasTable(Int32 tableSize)
        {
            if (tableSize < 0)
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");
            _fieldValues = new Int32[tableSize, tableSize];
        }

        #endregion

        #region Public methods

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
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
        //    if (!CheckStep(x, y)) // ha a beállítás érvénytelen, akkor nem végezzük el
        //        return;

            _fieldValues[x, y] = value;

        }

        public void StepValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            
           
        }

  

        #endregion

        #region Private methods

        /// <summary>
        /// Lépésellenőrzés.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>Igaz, ha a lépés engedélyezett, különben hamis.</returns>
        private Boolean CheckStep(Int32 x, Int32 y)
        {
            return false;
        }

        #endregion
    }
}

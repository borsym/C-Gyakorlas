using System;
using System.Collections.Generic;
using System.Text;

namespace amongus.Model
{
    class AmongUsTable
    {
        private Int32[,] _fieldValues; // mezőértékek
        public List<KeyValuePair<int, int>> positions;// Tasks;
        public List<KeyValuePair<int, int>> positionsCompletedTasks;
        public Int32 completedTasksIndex = 0;
        //private List<KeyValuePair<int, int>> positionPlayers;


        /// <summary>
        /// Játéktábla kitöltöttségének lekérdezése.
        /// </summary>
        public Boolean IsFilled
        {
            get
            {
                foreach (Int32 value in _fieldValues)
                    if (value == 0)
                        return false;
                return true;
            }
        }


        /// <summary>
        /// Játéktábla méretének lekérdezése.
        /// </summary>
        public Int32 Size { get { return _fieldValues.GetLength(0); } }

        /// <summary>
        /// Mező értékének lekérdezése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>Mező értéke.</returns>
        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }





        /// <summary>
        /// Sudoku játéktábla példányosítása.
        /// </summary>
        public AmongUsTable() : this(5) { positions = new List<KeyValuePair<int, int>>(); positionsCompletedTasks = new List<KeyValuePair<int, int>>(); }

        /// <summary>
        /// Sudoku játéktábla példányosítása.
        /// </summary>
        /// <param name="tableSize">Játéktábla mérete.</param>
        /// <param name="regionSize">Ház mérete.</param>
        public AmongUsTable(Int32 tableSize)
        {
            if (tableSize < 0)
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");
            positions = new List<KeyValuePair<int, int>>();
            positionsCompletedTasks = new  List<KeyValuePair<int, int>>();
            _fieldValues = new Int32[tableSize, tableSize];

        }



        /// <summary>
        /// Mező kitöltetlenségének lekérdezése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>Igaz, ha a mező ki van töltve, egyébként hamis.</returns>
        public Boolean IsEmpty(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == 0;
        }

        /// <summary>
        /// Mező zároltságának lekérdezése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>Igaz, ha a mező zárolva van, különben hamis.</returns>

        /// <summary>
        /// Mező értékének lekérdezése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>A mező értéke.</returns>
        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y];
        }

        /// <summary>
        /// Mező értékének beállítása.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <param name="value">Érték.</param>
        /// <param name="lockField">Zárolja-e a mezőt.</param>
        public void SetValue(Int32 x, Int32 y, Int32 value)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            

            _fieldValues[x, y] = value;
        }

        /// <summary>
        /// Mező léptetése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        public void StepValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");


 //           do
 //           {
 //               _fieldValues[x, y] = (_fieldValues[x, y] + 1) % (_fieldValues.GetLength(0) + 1); // ciklikus generálás
 //           }
 //           while (!CheckStep(x, y)); // amíg nem jó az érték
        }
    }
}

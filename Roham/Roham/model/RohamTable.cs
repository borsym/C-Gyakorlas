using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Roham.model
{
    public class RohamTable
    {
        private Int32[,] _fieldValues;
       

        //jatektabla merete
        public Int32 Size { get { return _fieldValues.GetLength(0); } }
        


        public Boolean IsOnlyOnePlayer  //ezt lehet a modellbe kell megvalosítani...
        {
            get
            {
                bool isEnemy = false;
                //megnézem, hogy 1 esnek van e enemy
                foreach (Int32 value in _fieldValues)
                    if (value == 2)
                        isEnemy = true; //ha van a pályán 2 es már szól

                if (isEnemy == false) return true;
                isEnemy = false;
                foreach (Int32 value in _fieldValues)
                    if (value == 1)
                        isEnemy = true;

                if (isEnemy == false) return true;
                
                return false;
            }
        }

        //mezo ertekenek lekerdezese
        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }

        public RohamTable() : this(8) {}
        public RohamTable(Int32 tableSize)
        {
            if (tableSize < 0)
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");

            _fieldValues = new Int32[tableSize, tableSize];
        }
        //mezoertek
        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The x is out of the range");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The y is out of the range");

            return _fieldValues[x, y];
        }

        public void SetValue(Int32 x, Int32 y, Int32 value)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
          //  if (!CheckStep(x, y)) // ha a beállítás érvénytelen, akkor nem végezzük el
          //      return;

            _fieldValues[x, y] = value;
        }

        private bool CheckStep(int x, int y)  //ezt at kellessz írni valahogy gondolom 4 eset lesz attol függ hova lép?
        {
            if (x - 1 < 0 || y - 1 < 0 || y + 1 > Size || x + 1 > Size) return false;
            return true;
        } //itt majd azt kéne nézni hogy jol lepett e?
        public void StepValue(Int32 x, Int32 y, Int32 value) //itt majd léptetni kéne kattintásra? ide kell e a value vagy legyen egy playerem itt
        {

            Debug.Write("ugye most itt vagyok?");
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            //itt valamit néznem kell majd hova lépked ez a szar meg hogy
            //do
            //{
            //itt majd csekkolni kell és 4 eset hogy merre lép ugye
                _fieldValues[x, y] = value; //player % 2 + 1;
            //}
            //while (CheckStep(x, y)); // amíg nem jó az érték
        }

        public Boolean IsEmpty(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == 0;
        }
    }
}

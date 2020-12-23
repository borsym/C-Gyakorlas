using System;
using System.Collections.Generic;
using System.Text;

namespace BlockDocuWPF.Model
{
    class BlockDocuTable
    {
        private Int32[,] _fieldValues;
        public Int32 Size { get { return _fieldValues.GetLength(0); } }
        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }
        public BlockDocuTable() : this(6) { }

        public BlockDocuTable(Int32 tableSize)
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
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            //if (!CheckStep(x, y)) // ha a beállítás érvénytelen, akkor nem végezzük el
            //    return;

            _fieldValues[x, y] = value;
        }

        /*
         * 1 0
         * 1 0
         * */
        private void First(Int32 x, Int32 y)
        {
            _fieldValues[x, y] = 1;
           // _fieldValues[x, y + 1] =  0;
            _fieldValues[x + 1, y] =  1;
          //  _fieldValues[y + 1, x + 1] =  0;
        }
        /*
          0 0
          1 1 
         */
        private void Second(Int32 x, Int32 y)
        {
            _fieldValues[x, y] = 1;
            _fieldValues[x, y + 1] = 1;
            //_fieldValues[x + 1, y] = 0;
            //_fieldValues[x + 1, y + 1] = 0;
           
        }
        /*
         1 0
         1 1
         */
        private void Third(Int32 x, Int32 y)
        {
            _fieldValues[x, y] = 1;
           // _fieldValues[x, y + 1] = 0;
            _fieldValues[x + 1, y] = 1;
            _fieldValues[x + 1, y + 1] = 1;
        }
        /*
         1 1
         0 1
         */
        private void Forth(Int32 x, Int32 y)
        {
            _fieldValues[x, y] = 1;
            _fieldValues[x, y + 1] = 1;
            //_fieldValues[x + 1, y] = 0;
            _fieldValues[x + 1, y + 1] = 1;
        }

        public void StepValue(Int32 x, Int32 y, Int32 r)  // erre sztm nem lesz szükség
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            switch (r)
            {
                case 0:
                    First(x,y);
                    break;
                case 1:
                    Second(x, y);
                    break;
                case 2:
                    Third(x, y);
                    break;
                case 3:
                    Forth(x, y);
                    break;

            }

        }

        
    }
}

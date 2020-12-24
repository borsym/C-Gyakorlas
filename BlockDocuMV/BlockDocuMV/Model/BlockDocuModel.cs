using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BlockDocuMV.Model
{
    class BlockDocuModel
    {
        private BlockDocuTable _table;
        private Int32 _gameTime;
        private int r;
        public Int32 GameTime { get { return _gameTime; } }
        public BlockDocuTable Table { get { return _table; } }
        public Boolean IsGameOver { get { return false; } }

        public event EventHandler<BlockDocuEventArgs> GameAdvanced;
        public event EventHandler<BlockDocuEventArgs> GameOver;
        public event EventHandler<BlockDocuEventArgs> GameCreated;

        public BlockDocuModel()
        {
            _table = new BlockDocuTable();
        }

       
        public void NewGame()
        {
            _table = new BlockDocuTable();
            _gameTime = 0;
            GenerateFields();
            OnGameCreated();
        }

        public void AdvanceTime()
        {
            if (IsGameOver) // ha már vége, nem folytathatjuk
                return;

            _gameTime++;
            OnGameAdvanced();

            if (_gameTime == 0) // ha lejárt az idő, jelezzük, hogy vége a játéknak
                OnGameOver(false);
        }

        private void deleteRow(int x)
        {
            for (int i = 0; i < 4; i++)
            {
                _table.SetValue(x, i, 0);
            }
        }
        private void deleteColumn(int y)
        {
            for (int i = 0; i < 4; i++)
            {
                _table.SetValue(i, y, 0);
            }
        }
        public void Check()
        {
            for (int i = 0; i < 4; i++)
            {
                Boolean isOnlyBlueRow = true;
                Boolean isOnlyBlueColumn = true;
                for (int j = 0; j < 4; j++)
                {
                    if (_table.GetValue(i, j) == 0) isOnlyBlueRow = false;
                    if (_table.GetValue(j, i) == 0) isOnlyBlueColumn = false;
                }
                if (isOnlyBlueRow) deleteRow(i);
                if (isOnlyBlueColumn) deleteColumn(i);
            }


        }
        private Boolean IsValidStep(Int32 x, Int32 y)
        {
            Debug.Write("is validba vagyok " + r + "\n");
            switch (r)
            {
                case 0:
                    if (x == 3 || (_table.GetValue(x, y) != 0 || _table.GetValue(x + 1, y) != 0)) return false;
                    else return true;
                case 1:
                    if (y == 3 || (_table.GetValue(x, y) != 0 || _table.GetValue(x, y + 1) != 0)) return false;
                    else return true;
                case 2:
                    if (x == 3 || y == 3 || (_table.GetValue(x, y) != 0 || _table.GetValue(x + 1, y) != 0 || _table.GetValue(x + 1, y + 1) != 0)) return false;
                    else return true;
                case 3:
                    if (x == 3 || y == 3 || (_table.GetValue(x, y) != 0 || _table.GetValue(x, y + 1) != 0 || _table.GetValue(x + 1, y + 1) != 0)) return false;
                    else return true;
            }

            return true;


        }
        public void Step(Int32 x, Int32 y)
        {
            if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
                return;
            if (!IsValidStep(x, y))
                return;
            Debug.Write("király " + "\n");
            _table.StepValue(x, y, r);
            GenerateObject();



            OnGameAdvanced();

            //if (_table.IsFilled) // ha már nem fogok tudni hova tenni
            //{
            //    OnGameOver(true);
            //}
        }
        private void GenerateFields()
        {
            for (int i = 0; i < _table.Size; i++)
            {
                for (int j = 0; j < _table.Size; j++)
                {
                    if (((i == 0 || i == 1 || i == 2 || i == 3) && (j == 4 || j == 5)) || ((i == 4 || i == 5) && (j == 0 || j == 1 || j == 2 || j == 3)))
                    {
                        _table.SetValue(i, j, 2);
                    }
                    else
                    {
                        _table.SetValue(i, j, 0);
                    }
                }
            }
            GenerateObject();


        }
        /*
        4 4
        4 5
        5 4
        5 5
        */
        /*
         * 1 0
         * 1 0
         * */
        private void First()
        {
            _table.SetValue(4, 4, 1);
            _table.SetValue(4, 5, 0);
            _table.SetValue(5, 4, 1);
            _table.SetValue(5, 5, 0);
        }
        /*
          0 0
          1 1 
         */
        private void Second()
        {
            _table.SetValue(4, 4, 0);
            _table.SetValue(4, 5, 0);
            _table.SetValue(5, 4, 1);
            _table.SetValue(5, 5, 1);
        }
        /*
         1 0
         1 1
         */
        private void Third()
        {
            _table.SetValue(4, 4, 1);
            _table.SetValue(4, 5, 0);
            _table.SetValue(5, 4, 1);
            _table.SetValue(5, 5, 1);
        }
        /*
         1 1
         0 1
         */
        private void Forth()
        {
            _table.SetValue(4, 4, 1);
            _table.SetValue(4, 5, 1);
            _table.SetValue(5, 4, 0);
            _table.SetValue(5, 5, 1);
        }
        private void GenerateObject()
        {
            Random rand = new Random();
            r = rand.Next(0, 4);
            Debug.Write("random: " + r + "\n");
            switch (r)
            {
                case 0:
                    First();
                    break;
                case 1:
                    Second();
                    break;
                case 2:
                    Third();
                    break;
                case 3:
                    Forth();
                    break;

            }
        }
        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new BlockDocuEventArgs(false, _gameTime));
        }

        /// <summary>
        /// Játék vége eseményének kiváltása.
        /// </summary>
        /// <param name="isWon">Győztünk-e a játékban.</param>
        private void OnGameOver(Boolean isGameOver)
        {
            if (GameOver != null)
                GameOver(this, new BlockDocuEventArgs(isGameOver, _gameTime));
        }

        /// <summary>
        /// Játék létrehozás eseményének kiváltása.
        /// </summary>
        private void OnGameCreated()
        {
            if (GameCreated != null)
                GameCreated(this, new BlockDocuEventArgs(false, _gameTime));

        }
    }
    }

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TiliToli.Model
{
    class TiliToliModel
    {
        private TiliToliTable _table;
        private Int32 _gameTime;
        public Int32 GameTime { get { return _gameTime; } }
        public TiliToliTable Table { get { return _table; } }
        public Boolean IsGameOver { get { return _table.IsFilled; } }

        public event EventHandler<TiliToliEventArgs> GameAdvanced;
        public event EventHandler<TiliToliEventArgs> GameOver;
        public event EventHandler<TiliToliEventArgs> GameCreated;


        public TiliToliModel()
        {
            _table = new TiliToliTable();
        }

        public void NewGame(int n)
        {
            _table = new TiliToliTable(n);
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

            if (IsGameOver) // ha lejárt az idő, jelezzük, hogy vége a játéknak
                OnGameOver(false);
        }

        public void Step(Int32 x, Int32 y)
        {
            if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
                return;
            
            if(x + 1 < _table.Size && _table.GetValue(x + 1,y) == -1)
            {
                _table.SetValue(x+1, y, _table.GetValue(x,y));
                _table.SetValue(x, y, -1);
            } 
            else if(x - 1 >= 0 && _table.GetValue(x - 1, y) == -1)
            {
                _table.SetValue(x - 1, y, _table.GetValue(x, y));
                _table.SetValue(x, y, -1);
            }
            else if (y - 1 >= 0 && _table.GetValue(x, y - 1) == -1)
            {
                _table.SetValue(x , y - 1, _table.GetValue(x, y));
                _table.SetValue(x, y, -1);
            }
            else if (y + 1 < _table.Size && _table.GetValue(x, y + 1) == -1)
            {
                _table.SetValue(x, y + 1, _table.GetValue(x, y));
                _table.SetValue(x, y, -1);
            }


            OnGameAdvanced();

            //if (_table.IsFilled) // ha vége a játéknak, jelezzük, hogy győztünk
            //{
            //    OnGameOver(true);
            //}
        }


        private void GenerateFields()
        {
            Random random = new Random();
            List<int> numberHolder = new List<int>();
            for(int i = 0; i < _table.Size*_table.Size - 1; i++)
            {
                numberHolder.Add(i + 1);
            }
            int fix_i = random.Next(0, _table.Size);
            int fix_j = random.Next(0, _table.Size);
            _table.SetValue(fix_i, fix_j, -1);
            for (int i = 0; i < _table.Size; ++i)
            {
                for (int j = 0; j < _table.Size; ++j)
                {
                    if (numberHolder.Count == 0) return;
                    int tmp = random.Next(0, numberHolder.Count);
                    while(!numberHolder.Contains(numberHolder[tmp]))
                    {
                        tmp = random.Next(0, numberHolder.Count);
                    }

                    if(_table.GetValue(i,j) != -1)
                    {
                        _table.SetValue(i, j, numberHolder[tmp]);
                        numberHolder.RemoveAt(tmp);
                    }
                    
                    Debug.Write(numberHolder.Count+"\n");
                    
                }
            }

        }
        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new TiliToliEventArgs(false, _gameTime));
        }

        /// <summary>
        /// Játék vége eseményének kiváltása.
        /// </summary>
        /// <param name="isWon">Győztünk-e a játékban.</param>
        private void OnGameOver(Boolean isWon)
        {
            if (GameOver != null)
                GameOver(this, new TiliToliEventArgs(isWon, _gameTime));
        }

        /// <summary>
        /// Játék létrehozás eseményének kiváltása.
        /// </summary>
        private void OnGameCreated()
        {
            if (GameCreated != null)
                GameCreated(this, new TiliToliEventArgs(false, _gameTime));
        }


    }
   }

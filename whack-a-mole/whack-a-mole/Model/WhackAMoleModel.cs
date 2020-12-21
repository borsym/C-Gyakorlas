using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace whack_a_mole.Model
{

    class WhackAMoleModel
    {
        private WhackAMoleTable _table;
        private Int32 _gameTime;
        private Int32 _gamePointsCount;
        private Queue<KeyValuePair<int,int> > deleteMoles;

        public Int32 GamePointsCount { get { return _gamePointsCount;  } set => _gamePointsCount = value; }
        public Int32 GameTime { get { return _gameTime; } }
        public WhackAMoleTable Table { get { return _table; } }
        public Boolean IsGameOver { get { return _gameTime == 0; } }


        public event EventHandler<WhackAMoleEventArgs> GameAdvanced;
        public event EventHandler<WhackAMoleEventArgs> GameOver;
        public event EventHandler<WhackAMoleEventArgs> GameCreated;

        public WhackAMoleModel()
        {
            _table = new WhackAMoleTable();
        }

        public void NewGame()
        {
            _table = new WhackAMoleTable();
            deleteMoles = new Queue<KeyValuePair<int, int>>();
            _gameTime = 30;
            _gamePointsCount = 0;
            GenerateFields();
            OnGameCreated();
        }

        public void AdvanceTime()
        {
            if (IsGameOver) // ha már vége, nem folytathatjuk
                return;

            MakeMole();
            if (GameTime % 2 == 0) DeleteMole();
            _gameTime--;
            OnGameAdvanced();

            if (_gameTime == 0) // ha lejárt az idő, jelezzük, hogy vége a játéknak
                OnGameOver(false);
        }

        private void DeleteMole()
        {
            Debug.Write("bent vagyok es: " + deleteMoles.Count + "\n");
            KeyValuePair<int, int> tmp;
            switch (deleteMoles.Count)
            {
                case 0: return;
                case 1:
                case 2:
                     tmp = deleteMoles.Dequeue();
                    _table.SetValue(tmp.Key, tmp.Value, 0);                
                    break;
                case 3:
                    tmp = deleteMoles.Dequeue();
                    _table.SetValue(tmp.Key, tmp.Value, 0);
                    tmp = deleteMoles.Dequeue();
                    _table.SetValue(tmp.Key, tmp.Value, 0);
                    break;
                default:
                    tmp = deleteMoles.Dequeue();
                    _table.SetValue(tmp.Key, tmp.Value, 0);
                    tmp = deleteMoles.Dequeue();
                    _table.SetValue(tmp.Key, tmp.Value, 0);
                    break;
            }
        }
        private void MakeMole()
        {
            Random a = new Random();
            double random = a.NextDouble();
            int countMoles = 0;
            Debug.Write(random + "\n");
            if (random < 0.2) return;
            if (random < 0.6)
            {
                while(countMoles != 2)
                {
                    int i = a.Next(0, 5);
                    int j = a.Next(0, 5);
                    if((i + j) % 2 != 0 && _table.GetValue(i,j) != 2 )
                    {
                        ++countMoles;
                        _table.SetValue(i, j, 2);
                        KeyValuePair<int, int> tmp = new KeyValuePair<int, int>(i, j);
                        deleteMoles.Enqueue(tmp);
                        
                    } 
                }
                return;
                
            }
            else
            {
                while (countMoles != 1)
                {
                    int i = a.Next(0, 5);
                    int j = a.Next(0, 5);
                    if ((i + j) % 2 != 0 && _table.GetValue(i, j) != 2)
                    {
                        ++countMoles;
                        _table.SetValue(i, j, 2);
                        KeyValuePair<int, int> tmp = new KeyValuePair<int, int>(i, j);
                        deleteMoles.Enqueue(tmp);
                    }
                }
                return;
            }
        }
        public void Step(Int32 x, Int32 y)
        {
            if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
                return;

            //oszt most ezt hogy vegyem ki a sorbol??
             if(deleteMoles.Count != 0) deleteMoles.Dequeue();
            _table.SetValue(x, y,0);

            OnGameAdvanced();
        }

        // 1 vakond 0 pálya
        private void GenerateFields()
        {
            for (int i = 0; i < _table.Size; ++i)
            {
                for (int j = 0; j < _table.Size; ++j)
                {
                    if ((j+i) % 2 == 0) _table.SetValue(i, j, 1);
                    else _table.SetValue(i, j, 0);
                }
            }
        }

        private void OnGameAdvanced()
        {
            GameAdvanced?.Invoke(this, new WhackAMoleEventArgs(false, _gameTime));
        }

        /// <summary>
        /// Játék vége eseményének kiváltása.
        /// </summary>
        /// <param name="isWon">Győztünk-e a játékban.</param>
        private void OnGameOver(Boolean isGameOver)
        {
            GameOver?.Invoke(this, new WhackAMoleEventArgs(isGameOver, _gameTime));
        }

        /// <summary>
        /// Játék létrehozás eseményének kiváltása.
        /// </summary>
        private void OnGameCreated()
        {
            GameCreated?.Invoke(this, new WhackAMoleEventArgs(false, _gameTime));
        }

    }
}

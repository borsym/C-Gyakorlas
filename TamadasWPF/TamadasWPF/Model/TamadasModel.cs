using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TamadasWPF.Model
{
    class TamadasModel
    {
        // 1-4 egyik 5-8 a másik és majd a pályán amikor kirajzolom akkor a megjelenítésnél 1 est rakok 
        private TamadasTable _table;
        private Int32 _gameTime;
        private Int32 _player;
        private Int32 _roundGame;

        public Int32 Player { get { return _player; } }
        public Int32 RoundGame { get { return _roundGame; } }
        public Int32 GameTime { get { return _gameTime; } }
        public TamadasTable Table { get { return _table; } }

        public Boolean IsGameOver { get { return false; } }

        public event EventHandler<TamadasEventArgs> GameAdvanced;
        public event EventHandler<TamadasEventArgs> GameOver;
        public event EventHandler<TamadasEventArgs> GameCreated;

        public TamadasModel()
        {
            _table = new TamadasTable();
        }

        public void NewGame(int s)
        {
            _table = new TamadasTable(s);
            _player = 1;
            _roundGame = 1;  // az 1 es számúakkal lép mind a kettő / aztán 2 / aztán 3
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

            //if (_gameTime == 0) // ha lejárt az idő, jelezzük, hogy vége a játéknak
            //    OnGameOver(false);
        }

        public void Step(Int32 x, Int32 y, Int32 number) // itt 8-5 figyelni ekll
        {
            Debug.Write(x + 1 < _table.Size && (_table.GetValue(x + 1, y) == number || (_table.GetValue(x + 1, y) >= 5)));
            //Debug.Write(x + 1 < _table.Size && (_table.GetValue(x + 1, y) == number || (_table.GetValue(x - 1, y) >= 5 && _table.GetValue(x - 1, y) - 4 == number)));
            if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
                return;
           

            if (Player == 1)
            {
                if (x + 1 < _table.Size && (_table.GetValue(x + 1, y) >= 5 && _table.GetValue(x + 1, y) - 4 == number && _table.GetValue(x,y) <= 4))
                {
                    _table.SetValue(x, y, _table.GetValue(x + 1, y));
                    _table.SetValue(x + 1, y, 0);
                }
                else if (x - 1 >= 0 &&  (_table.GetValue(x - 1, y) >= 5 && _table.GetValue(x - 1, y) - 4 == number && _table.GetValue(x, y) <= 4))
                {
                    _table.SetValue(x, y, _table.GetValue(x - 1, y));
                    _table.SetValue(x - 1, y, 0);
                }
                else if (y - 1 >= 0 && (_table.GetValue(x, y - 1) >= 5 && _table.GetValue(x, y - 1) - 4 == number && _table.GetValue(x, y) <= 4))
                {
                    _table.SetValue(x, y, _table.GetValue(x, y - 1));
                    _table.SetValue(x, y - 1, 0);
                }
                else if (y + 1 < _table.Size && (_table.GetValue(x, y + 1) >= 5 && _table.GetValue(x, y + 1) - 4 == number && _table.GetValue(x, y) <= 4))
                {
                    _table.SetValue(x, y, _table.GetValue(x, y + 1));
                    _table.SetValue(x, y + 1, 0);
                }
                else
                {
                    return;
                }

            }
           else
            {
                if (x + 1 < _table.Size && (_table.GetValue(x + 1, y) == number && (_table.GetValue(x, y) > 4 || _table.GetValue(x, y) == 0)))
                {
                    _table.SetValue(x, y, _table.GetValue(x + 1, y));
                    _table.SetValue(x + 1, y, 0);
                }
                else if (x - 1 >= 0 && (_table.GetValue(x - 1, y) == number && (_table.GetValue(x, y) > 4 || _table.GetValue(x, y) == 0)))
                {
                    _table.SetValue(x, y, _table.GetValue(x - 1, y));
                    _table.SetValue(x - 1, y, 0);
                }
                else if (y - 1 >= 0 && (_table.GetValue(x, y - 1) == number && (_table.GetValue(x, y) > 4 || _table.GetValue(x, y) == 0)))
                {
                    _table.SetValue(x, y, _table.GetValue(x, y - 1));
                    _table.SetValue(x, y - 1, 0);
                }
                else if (y + 1 < _table.Size && (_table.GetValue(x, y + 1) == number && (_table.GetValue(x, y) > 4 || _table.GetValue(x, y) == 0)))
                {
                    _table.SetValue(x, y, _table.GetValue(x, y + 1));
                    _table.SetValue(x, y + 1, 0);
                }
                else
                {
                    return;
                }
            }


           
           
            if (_player == 2) _roundGame = _roundGame % 4 + 1;
            _player = _player % 2 + 1;
            

            
            OnGameAdvanced();

           if (_table.IsGameOver) // ha vége a játéknak, jelezzük, hogy győztünk
           {
               OnGameOver(true);
           }
        }

        private void GenerateFields()
        {
            for (int i = 0; i < _table.Size; i++)
            {
                for (int j = 0; j < _table.Size; j++)
                {
                    _table.SetValue(i, j, 0);
                }
            }

            _table.SetValue(0, _table.Size - 1, 4);
            _table.SetValue(0, _table.Size - 2, 3);
            _table.SetValue(1, _table.Size - 2, 2);
            _table.SetValue(1, _table.Size - 1, 1);
            

            _table.SetValue(_table.Size - 2, 0, 5);
            _table.SetValue(_table.Size - 2, 1, 6);
            _table.SetValue(_table.Size - 1, 0, 8);
            _table.SetValue(_table.Size - 1, 1, 7);
        }

        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new TamadasEventArgs(false, _player, _roundGame, _gameTime));
        }

        private void OnGameOver(Boolean isWon)
        {
            if (GameOver != null)
                GameOver(this, new TamadasEventArgs(isWon, _player, _roundGame, _gameTime));
        }

        private void OnGameCreated()
        {
            if (GameCreated != null)
                GameCreated(this, new TamadasEventArgs(false, _player, _roundGame, _gameTime));
        }
    }
}

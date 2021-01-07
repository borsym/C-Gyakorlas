using System;
using System.Collections.Generic;
using System.Text;

namespace ZHMV.Model
{
    class ModelMV
    {
        private ModelTable _table;
        private Int32 _pointCount;
        private Int32 _gameTime;
        private Int32 _currentSize;
        public KeyValuePair<int, int> packMan;
        public List<KeyValuePair<int, int>> ghostsContainer;
        public List<KeyValuePair<int, int>> walls;
        public List<KeyValuePair<int, int>> bogyok;
        public Int32 PointCount { get { return _pointCount; } }
        public Int32 GameTime { get { return _gameTime; } }
        public ModelTable Table { get { return _table; } }
        public Boolean IsGameOver { get { return false; } }
        public int CurrentSize { get => _currentSize;}

        public event EventHandler<ModelEventArgs> GameAdvanced;
        public event EventHandler<ModelEventArgs> GameOver;

        public ModelMV()
        {
            _currentSize = 12;
            _table = new ModelTable();
        }

        public void NewGame(int size)
        {
            packMan = new KeyValuePair<int, int>(_table.Size - 1, 0);
            walls =  new List<KeyValuePair<int, int>>();
            bogyok = new List<KeyValuePair<int, int>>();
            ghostsContainer = new List<KeyValuePair<int, int>>();
        _table = new ModelTable(size);
            _currentSize = size;
            _pointCount = 0;
            _gameTime = 0;
            GenerateFields();
        }

        private void CheckGameOver()
        {
            for(int i = 0; i < _table.Size; i++)
            {
                for(int j = 0; j < _table.Size; j++)
                {
                    if (_table.GetValue(i, j) == 4) return;
                }
            }
            OnGameOver(true);
        }
        private void MoveRandomGhost()
        {
            Random rand = new Random();
            int randomN = rand.Next(0, 8);
           // int count = 0;
            for(int i = 0; i < ghostsContainer.Count /*|| count < _table.Size / 3*/; i++)
            {
                switch(randomN)
                {
                    //fel le jobb bal
                    case 0:
                        if(ghostsContainer[i].Key + 1 < _table.Size && _table.GetValue(ghostsContainer[i].Key + 1, ghostsContainer[i].Value) != 2 && _table.GetValue(ghostsContainer[i].Key + 1, ghostsContainer[i].Value) != 3)
                        {
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 0);
                            ghostsContainer[i] = new KeyValuePair<int, int>(ghostsContainer[i].Key + 1, ghostsContainer[i].Value);
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 2);
                           // count++;
                        }
                        break;
                    case 1:
                        if (ghostsContainer[i].Key - 1 >= 0 && _table.GetValue(ghostsContainer[i].Key - 1, ghostsContainer[i].Value) != 2 && _table.GetValue(ghostsContainer[i].Key - 1, ghostsContainer[i].Value) != 3)
                        {
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 0);
                            ghostsContainer[i] = new KeyValuePair<int, int>(ghostsContainer[i].Key - 1, ghostsContainer[i].Value);
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 2);
                            //count++; 
                        }
                        break;
                    case 2:
                        if (ghostsContainer[i].Value - 1 >= 0 && _table.GetValue(ghostsContainer[i].Key, ghostsContainer[i].Value - 1) != 2 && _table.GetValue(ghostsContainer[i].Key , ghostsContainer[i].Value - 1) != 3)
                        {
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 0);
                            ghostsContainer[i] = new KeyValuePair<int, int>(ghostsContainer[i].Key  ,ghostsContainer[i].Value - 1);
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 2);
                            //count++;
                        }
                        break;
                    case 3:
                        if (ghostsContainer[i].Value + 1 <_table.Size && _table.GetValue(ghostsContainer[i].Key, ghostsContainer[i].Value + 1) != 2 && _table.GetValue(ghostsContainer[i].Key, ghostsContainer[i].Value + 1) != 3)
                        {
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 0);
                            ghostsContainer[i] = new KeyValuePair<int, int>(ghostsContainer[i].Key, ghostsContainer[i].Value + 1);
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 2);
                            //count++;
                        }
                        break;
                    case 4:
                        if (ghostsContainer[i].Value + 1 < _table.Size && ghostsContainer[i].Key + 1 < _table.Size && _table.GetValue(ghostsContainer[i].Key + 1, ghostsContainer[i].Value + 1) != 2 && _table.GetValue(ghostsContainer[i].Key + 1, ghostsContainer[i].Value + 1) != 3)
                        {
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 0);
                            ghostsContainer[i] = new KeyValuePair<int, int>(ghostsContainer[i].Key + 1, ghostsContainer[i].Value + 1);
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 2);
                            //count++;
                        }
                        break;
                    case 5:
                        if (ghostsContainer[i].Value - 1 >= 0 && ghostsContainer[i].Key + 1 < _table.Size && _table.GetValue(ghostsContainer[i].Key + 1, ghostsContainer[i].Value - 1) != 2 && _table.GetValue(ghostsContainer[i].Key + 1, ghostsContainer[i].Value - 1) != 3)
                        {
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 0);
                            ghostsContainer[i] = new KeyValuePair<int, int>(ghostsContainer[i].Key + 1, ghostsContainer[i].Value - 1);
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 2);
                            //count++;
                        }
                        break;
                    case 6:
                        if (ghostsContainer[i].Value + 1 < _table.Size && ghostsContainer[i].Key - 1 >= 0 && _table.GetValue(ghostsContainer[i].Key - 1, ghostsContainer[i].Value + 1) != 2 && _table.GetValue(ghostsContainer[i].Key - 1, ghostsContainer[i].Value + 1) != 3)
                        {
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 0);
                            ghostsContainer[i] = new KeyValuePair<int, int>(ghostsContainer[i].Key - 1, ghostsContainer[i].Value + 1);
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 2);
                            //count++;
                        }
                        break;
                    case 7:
                        if (ghostsContainer[i].Value - 1 >= 0 && ghostsContainer[i].Key - 1 >= 0 && _table.GetValue(ghostsContainer[i].Key - 1, ghostsContainer[i].Value - 1) != 2 && _table.GetValue(ghostsContainer[i].Key - 1, ghostsContainer[i].Value - 1) != 3)
                        {
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 0);
                            ghostsContainer[i] = new KeyValuePair<int, int>(ghostsContainer[i].Key - 1, ghostsContainer[i].Value - 1);
                            _table.SetValue(ghostsContainer[i].Key, ghostsContainer[i].Value, 2);
                            //count++;
                        }
                        break;

                }
                KeyValuePair<int, int> tmp = new KeyValuePair<int, int>(packMan.Key, packMan.Value);
                if (ghostsContainer.Contains(tmp))
                {
                    OnGameOver(true);
                }
            }
        }
        private void putBackBogyok()
        {
            for(int i = 0; i < bogyok.Count; i++)
            {
                if(_table.GetValue(bogyok[i].Key, bogyok[i].Value) != 2) _table.SetValue(bogyok[i].Key, bogyok[i].Value, 4);
            }
        }

        public void AdvanceTime()
        {
            if (IsGameOver)
                return;

            _gameTime++;
            MoveRandomGhost();
            putBackBogyok();
            CheckGameOver();
            OnGameAdvanced();

            //if (_gameTime == 99) 
            //    OnGameOver(false);
        }

       

        private void putPackMan()
        {
            _table.SetValue(_table.Size - 1, 0, 1);
        }

        private void ghosts()
        {
            Random rand = new Random();
            int j = rand.Next(0, _table.Size);
            for(int i = 0; i < _table.Size / 3; i++)
            {
                while(_table.GetValue(0,j) != 0)
                {
                    j = rand.Next(0, _table.Size);
                }
                _table.SetValue(0, j, 2);
                ghostsContainer.Add(new KeyValuePair<int, int>(0, j));
            }
        }

        // 3 fal 4 bogyo
        private void putWallsAndBogyok()
        {
            
            for(int i = 1; i <_table.Size - 1; i++)
            {
                _table.SetValue(i, 2, 3);
                walls.Add(new KeyValuePair<int, int>(i, 2));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 4, 3);
                walls.Add(new KeyValuePair<int, int>(i, 4));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 6, 3);
                walls.Add(new KeyValuePair<int, int>(i, 6));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 8, 3);
                walls.Add(new KeyValuePair<int, int>(i, 8));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 10, 3);
                walls.Add(new KeyValuePair<int, int>(i, 10));
            }


            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 1, 4);
                bogyok.Add(new KeyValuePair<int, int>(i, 1));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 3, 4);
                bogyok.Add(new KeyValuePair<int, int>(i, 3));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 5, 4);
                bogyok.Add(new KeyValuePair<int, int>(i, 5));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 7, 4);
                bogyok.Add(new KeyValuePair<int, int>(i, 7));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 9, 4);
                bogyok.Add(new KeyValuePair<int, int>(i, 9));
            }

            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, 0, 4);
                bogyok.Add(new KeyValuePair<int, int>(i, 0));
            }
            for (int i = 1; i < _table.Size - 1; i++)
            {
                _table.SetValue(i, _table.Size - 1, 3);
                walls.Add(new KeyValuePair<int, int>(i, _table.Size - 1));
            }


            for(int i = 1; i < _table.Size; i++)
            {
                _table.SetValue(_table.Size - 1, i, 4);
                bogyok.Add(new KeyValuePair<int, int>(_table.Size - 1, i));

            }

            for(int i = 0; i < _table.Size; i++)
            {
                if (_table.GetValue(0, i) != 2)
                {
                    _table.SetValue(0, i, 4);
                    bogyok.Add(new KeyValuePair<int, int>(0, i));
                }
            }

        }

        private void GenerateFields()
        {
            for(Int32 i = 0; i < _table.Size; i++)
            {
                for(Int32 j = 0; j < _table.Size; j++)
                {
                    _table.SetValue(i, j, 0);
                }
            }
            ghosts();
            putPackMan();
            putWallsAndBogyok();
        }

        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new ModelEventArgs(false, _pointCount, _gameTime));
        }
        private void OnGameOver(Boolean isWon)
        {
            if (GameOver != null)
                GameOver(this, new ModelEventArgs(isWon, _pointCount, _gameTime));
        }

        public void movePackMan(int x, int y)
        {
            if (packMan.Key + x < 0 || packMan.Key + x >= _table.Size || packMan.Value + y >= _table.Size || packMan.Value + y < 0) return;
            if (_table.GetValue(packMan.Key + x, packMan.Value + y) == 3) return;


            _table.SetValue(packMan.Key, packMan.Value, 0);
            KeyValuePair<int, int> tmp = new KeyValuePair<int, int>(packMan.Key + x, packMan.Value + y);
            if (_table.GetValue(packMan.Key + x, packMan.Value + y) == 4)
            {
                
                bogyok.Remove(tmp);
                _pointCount++;
                OnGameAdvanced();
            }
            if(ghostsContainer.Contains(tmp))
            {
                OnGameOver(true);
            }
            packMan = new KeyValuePair<int, int>(packMan.Key + x, packMan.Value + y);
            _table.SetValue(packMan.Key, packMan.Value, 1);
        }
    }
}

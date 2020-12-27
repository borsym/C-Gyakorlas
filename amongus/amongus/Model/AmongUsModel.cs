using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace amongus.Model
{
    class AmongUsModel
    {
        private AmongUsTable _table;
        private Int32 _gameTime;
        public Int32 GameTime { get { return _gameTime; } }
        public AmongUsTable Table { get { return _table; } }
        public Boolean IsGameOver { get { return false; } }

        public event EventHandler<AmongUsModelEventArgs> GameAdvanced;

        /// <summary>
        /// Játék végének eseménye.
        /// </summary>
        public event EventHandler<AmongUsModelEventArgs> GameOver;

        /// <summary>
        /// Játék létrehozásának eseménye.
        /// </summary>
        public event EventHandler<AmongUsModelEventArgs> GameCreated;

        public AmongUsModel()
        {
            _table = new AmongUsTable();
        }

        public void NewGame()
        {
            _table = new AmongUsTable();
            _gameTime = 0;
            GenerateFields();
            OnGameCreated();
        }

        public void AdvanceTime()
        {
            if (IsGameOver) // ha már vége, nem folytathatjuk
                return;

            _gameTime++;
            Step();
            OnGameAdvanced();

            if (_gameTime == 99) // ha lejárt az idő, jelezzük, hogy vége a játéknak
                OnGameOver(false);
        }

        private void ImpostorWithAnotherCrewmate()
        {
            for (int i = 5; i < _table.positions.Count - 1; i++)
            {
                if (_table.positions[i].Key == _table.positions[_table.positions.Count - 1].Key && _table.positions[i].Value == _table.positions[_table.positions.Count - 1].Value)
                {
                    _table.SetValue(_table.positions[i].Key, _table.positions[i].Value, 0);
                    _table.positions.RemoveAt(i);
                    Debug.Write("toroltem\n");
                }
            }
        }

        
        public void Step()
        {
            if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
                return;

            Random rand = new Random();
            double random = rand.NextDouble();
            ImpostorWithAnotherCrewmate();

            //Debug.Write(_table.positions.Count + " " + _table.positionsCompletedTasks.Count + " \n");
            if (_table.positions.Count == 6 || _table.positionsCompletedTasks.Count == 5)
            {
                OnGameOver(true);
            }

            for(int i = 5; i < _table.positions.Count; i++) //key = x, value = y
            {
                Boolean isGoodPos = false;
                while(!isGoodPos) 
                { 
                    if(random <= 0.25) //bal
                    {
                        if (_table.positions[i].Value - 1 >= 0)
                        {
                            if (_table.positions.Contains(new KeyValuePair<int, int>(_table.positions[i].Key, _table.positions[i].Value - 1)) &&
                                _table.GetValue(_table.positions[i].Key, _table.positions[i].Value - 1) == 1 &&
                                _table.GetValue(_table.positions[i].Key, _table.positions[i].Value) == 3)
                            {
                                _table.positionsCompletedTasks.Add(new KeyValuePair<int, int>(_table.positions[i].Key, _table.positions[i].Value - 1));
                                _table.completedTasksIndex++;
                            }
                            _table.SetValue(_table.positions[i].Key, _table.positions[i].Value, 0);
                            _table.positions[i] = new KeyValuePair<int, int>(_table.positions[i].Key, _table.positions[i].Value - 1);

                        }
                        isGoodPos = true;
                        random = rand.NextDouble();
                    }
                    else if(random <= 0.5) //jobb
                    {
                        if (_table.positions[i].Value + 1 < _table.Size)
                        {
                            if (_table.positions.Contains(new KeyValuePair<int, int>(_table.positions[i].Key, _table.positions[i].Value + 1)) &&
                                _table.GetValue(_table.positions[i].Key, _table.positions[i].Value + 1) == 1 &&
                                _table.GetValue(_table.positions[i].Key, _table.positions[i].Value) == 3)
                            {
                                _table.positionsCompletedTasks.Add(new KeyValuePair<int, int>(_table.positions[i].Key, _table.positions[i].Value + 1));
                                _table.completedTasksIndex++;
                            }
                            _table.SetValue(_table.positions[i].Key, _table.positions[i].Value, 0);
                            _table.positions[i] = new KeyValuePair<int, int>(_table.positions[i].Key, _table.positions[i].Value + 1);
                            isGoodPos = true;
                        }
                        random = rand.NextDouble();
                    }
                    else if (random <= 0.75) // le
                    {
                        if (_table.positions[i].Key - 1 >= 0)
                        {
                            if (_table.positions.Contains(new KeyValuePair<int, int>(_table.positions[i].Key - 1, _table.positions[i].Value )) &&
                                _table.GetValue(_table.positions[i].Key - 1, _table.positions[i].Value) == 1 &&
                                _table.GetValue(_table.positions[i].Key, _table.positions[i].Value) == 3)
                            {
                                _table.positionsCompletedTasks.Add( new KeyValuePair<int, int>(_table.positions[i].Key - 1, _table.positions[i].Value));
                                _table.completedTasksIndex++;
                            }
                            _table.SetValue(_table.positions[i].Key, _table.positions[i].Value, 0);
                            _table.positions[i] = new KeyValuePair<int, int>(_table.positions[i].Key - 1, _table.positions[i].Value);
                            isGoodPos = true;
                        }
                        random = rand.NextDouble();
                    }
                    else  // fel
                    {
                        if (_table.positions[i].Key + 1 < _table.Size)
                        {
                            if (_table.positions.Contains(new KeyValuePair<int, int>(_table.positions[i].Key + 1, _table.positions[i].Value)) &&
                                _table.GetValue(_table.positions[i].Key + 1, _table.positions[i].Value) == 1 &&
                                _table.GetValue(_table.positions[i].Key, _table.positions[i].Value) == 3)
                            {
                                _table.positionsCompletedTasks.Add( new KeyValuePair<int, int>(_table.positions[i].Key + 1, _table.positions[i].Value ));
                                _table.completedTasksIndex++;
                            }

                            _table.SetValue(_table.positions[i].Key, _table.positions[i].Value, 0);
                            _table.positions[i] = new KeyValuePair<int, int>(_table.positions[i].Key + 1, _table.positions[i].Value);
                            isGoodPos = true;
                        }
                        random = rand.NextDouble();
                    }
                }
            }

            for (int i = 0; i < _table.positions.Count; i++)
            {
                if (i < 5) 
                { 
                    _table.SetValue(_table.positions[i].Key, _table.positions[i].Value, 1);
                }
                else 
                {
                    _table.SetValue(_table.positions[i].Key, _table.positions[i].Value, 3);
                }
            }

            for(int i = 0; i < _table.positionsCompletedTasks.Count; i++)
            {
                _table.SetValue(_table.positionsCompletedTasks[i].Key, _table.positionsCompletedTasks[i].Value, 2);
            }
            
            // for (int i = 0; i < _table.Size; i++)
            // {
            //     for (int j = 0; j < _table.Size; j++)
            // ezzel állítom majd be a pályán az értékeket
            OnGameAdvanced();

            //if (_table.IsFilled) // ha vége a játéknak, jelezzük, hogy győztünk
            //{
            //    OnGameOver(true);
            //}
        }

        //csak itt állítgathatok értékeket 
        private void GenerateFields()  // itt jon a bigbrain time, mindig kell majd egy set value, 9. indexen lévő ember az impostor, 5-9 kikérem majd mindig az indexeket és odarakom aztán majd a positionoknál fogom váltogatni az értékeket nem a pályán
        {
            for(int i = 0; i < _table.Size; i++)
            {
                for(int j = 0; j < _table.Size; j++)
                {
                    _table.SetValue(i, j, 0);
                }
            }


            for(int i = 0; i < 5; i++)  // belerakom a pozi helyekre megnézem van e benne már ugyan az az érték ha igen ujra generálom
            {
                Random rand = new Random();
                int k = rand.Next(0, _table.Size);
                int j = rand.Next(0, _table.Size);
                while(_table.GetValue(k,j) != 0)
                {
                    k = rand.Next(0, _table.Size);
                    j = rand.Next(0, _table.Size);
                }
                _table.SetValue(k, j, 1);
                _table.positions.Add(new KeyValuePair<int, int>(k, j));
            }

            for (int i = 0; i < 5; i++)
            {
                Random rand = new Random();
                int k = rand.Next(0, _table.Size);
                int j = rand.Next(0, _table.Size);
                while (_table.GetValue(k, j) != 0)
                {
                    k = rand.Next(0, _table.Size);
                    j = rand.Next(0, _table.Size);
                }
                _table.SetValue(k, j, 3);
                _table.positions.Add(new KeyValuePair<int, int>(k, j));
            }

            


        }

        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new AmongUsModelEventArgs(false,_gameTime));
        }

        /// <summary>
        /// Játék vége eseményének kiváltása.
        /// </summary>
        /// <param name="isWon">Győztünk-e a játékban.</param>
        private void OnGameOver(Boolean isWon)
        {
            if (GameOver != null)
                GameOver(this, new AmongUsModelEventArgs(isWon, _gameTime));
        }

        /// <summary>
        /// Játék létrehozás eseményének kiváltása.
        /// </summary>
        private void OnGameCreated()
        {
            if (GameCreated != null)
                GameCreated(this, new AmongUsModelEventArgs(false, _gameTime));
        }
    }
}

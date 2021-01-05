using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ZH.Model
{
    public class GameModel
    {
        private ModelTable _table;
        private Int32 _gameStepCount;
        private Int32 _gameTime;
        private Int32 countPosition;
        public Boolean spacePushed;

        Dictionary<int, int> crazyHorses  = new Dictionary<int, int>();  //i és 2 élettel kezd
        public Int32 GameStepCount { get { return _gameStepCount; } }
        public Int32 GameTime { get { return _gameTime; } }
        public ModelTable Table { get { return _table; } }

        public Boolean IsGameOver { get { return (_gameTime == 99); } }

        public event EventHandler<ModelEventArgs> GameAdvanced;
        public event EventHandler<ModelEventArgs> GameOver;
        public event EventHandler<ModelEventArgs> GameCreated;

        public GameModel()
        {
            _table = new ModelTable();
            crazyHorses = new Dictionary<int, int>();
            spacePushed = false;
        }

        public void NewGame(int size)
        {
            spacePushed = false;
            crazyHorses = new Dictionary<int, int>();
            _table = new ModelTable(size);
            countPosition = -2;
            _gameStepCount = 0;
            _gameTime = 0;
            GenerateFields();
            OnGameCreated();
        }

        public void AdvanceTime()
        {
            if (!spacePushed) return;
            if (IsGameOver)
                return;
            Step();
            _gameTime++;
            OnGameAdvanced();

            if (_table.isFilled()) 
                OnGameOver(false);
        }

        public void Step() // ha tikre mozog a pálya előre akkor hasznos és akkor paraméter se kell
        {
            if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
                return;
            Random rand = new Random();
            double random = rand.NextDouble();
            for(int i = 0; i < _table.Size; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(random <= 0.3)
                    {
                        if(_table.GetValue(i, j) >= 2)
                        {
                            crazyHorses.Add(j, 2);
                            if (i + 1 < _table.Size)
                            {
                                _table.SetValue(i + 1, j, _table.GetValue(i, j));
                                _table.SetValue(i, j, 0);

                                
                            }
                        }
                        for (int k = 0; k < 5; k++)
                        {
                            if (crazyHorses.ContainsKey(k))
                            {
                                crazyHorses[k] -= crazyHorses[k];
                                if (crazyHorses[k] == 0)
                                {
                                    crazyHorses.Remove(k);
                                }
                            }
                        }
                    }
                    else
                    {
                        if(_table.GetValue(0,j) >= 2)
                        {
                            _table.SetValue(0, j, countPosition);
                            countPosition--;
                            //Debug.Write(-2 - j);
                        }
                        if (_table.GetValue(i, j) >= 2 && i - 1 >= 0 && !crazyHorses.ContainsKey(i))
                        {
                            Debug.Write(i + " " + j + "\n");
                            _table.SetValue(i - 1, j, _table.GetValue(i, j));
                            _table.SetValue(i, j, 0);
                            
                        }
                    }
                    random = rand.NextDouble();
                }
            }

            _gameStepCount++; // lépésszám növelés

            OnGameAdvanced();

            //if (_table.IsFilled)
            //{
            //    OnGameOver(true);
            //}
        }

        private void GenerateFields()
        {
            for(Int32 i = 0; i < _table.Size; i++)
            {
                for (Int32 j = 0; j < 5; j++)
                {
                    _table.SetValue(i, j, 0);

                    if(i == 0) _table.SetValue(i, j, 1);
                    if(i == _table.Size - 1 ) _table.SetValue(i, j, 2+j);
                }
            }

        }

        private void OnGameAdvanced()
        {
            if (GameAdvanced != null)
                GameAdvanced(this, new ModelEventArgs(false, _gameStepCount, _gameTime));
        }
        private void OnGameOver(Boolean isWon)
        {
            if (GameOver != null)
                GameOver(this, new ModelEventArgs(isWon, _gameStepCount, _gameTime));
        }
        private void OnGameCreated()
        {
            if (GameCreated != null)
                GameCreated(this, new ModelEventArgs(false, _gameStepCount, _gameTime));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FoxAndChees.Model
{
    class FoxAndCheesModel
    {
        private FoxAndCheesTable _table; // játéktábla
        private Int32 _eatenChees; // lépések száma
        private Int32 _foxLife;
        private Int32 _gameTime; // játékidő
        private KeyValuePair<int, int> foxPosition;
        private KeyValuePair<int, int> listIndex;

        public Int32 FoxLife { get { return _foxLife; } }
        public Int32 EatenChees { get { return _eatenChees; } }
        public Int32 GameTime { get { return _gameTime; } }
        public FoxAndCheesTable Table { get { return _table; } }
        public Boolean IsGameOver { get { return _foxLife == 0; } }

        public event EventHandler<FoxAndCheesEventArgs> GameAdvanced;
        public event EventHandler<FoxAndCheesEventArgs> GameOver;


        public FoxAndCheesModel()
        {
            _table = new FoxAndCheesTable();
            listIndex = new KeyValuePair<int, int>(-1, -1);
        }

        public void NewGame()
        {
            _table = new FoxAndCheesTable();
            _eatenChees = 0;
            _gameTime = 0;
            _foxLife = _table.Size * 3;
            GenerateFields();
        }
        public void NewGame(int size)
        {
            _table = new FoxAndCheesTable(size);
            _eatenChees = 0;
            _gameTime = 0;
            _foxLife = _table.Size * 3;
            GenerateFields();
        }
        private void makeChees()
        {
            deleteChees();
            Random rand = new Random();
            int i = rand.Next(0, _table.Size);
            int j = rand.Next(0, _table.Size);
            listIndex = new KeyValuePair<int, int>(i, j);
            _table.SetValue(i, j, 2);
        }
        private void deleteChees()
        {
            if (listIndex.Key == -1 || listIndex.Value == -1) return;
            _table.SetValue(listIndex.Key, listIndex.Value,0);
        }

        void pickChees()
        {
            if (foxPosition.Key == listIndex.Key && foxPosition.Value == listIndex.Value)
            {
                ++_eatenChees;
                _foxLife += 2;
                deleteChees();
                _table.SetValue(listIndex.Key, listIndex.Value, 1);
                listIndex = new KeyValuePair<int, int>(-1, -1);
            }
        }
        public void AdvanceTime()
        {
            if (IsGameOver) // ha már vége, nem folytathatjuk
                return;

            if (_gameTime % 2 == 0) makeChees();
            _gameTime++;
            _foxLife--;
            OnGameAdvanced();

            if (_foxLife == 0) // ha lejárt az idő, jelezzük, hogy vége a játéknak
                OnGameOver(false);
        }

        public void Step(char s)  // itt fogom majd a lépéseket csinálni?
        {
            //if (IsGameOver) // ha már vége a játéknak, nem játszhatunk
            //    return;
            
            switch(s)
            {
                case 'w':
                    if (foxPosition.Key - 1 < 0) return;
                    _table.SetValue(foxPosition.Key, foxPosition.Value, 0);
                    foxPosition = new KeyValuePair<int, int>(foxPosition.Key - 1, foxPosition.Value);
                    _table.SetValue(foxPosition.Key, foxPosition.Value, 1);
                    pickChees();
                    break;
                case 's':
                    if (foxPosition.Key + 1 >= _table.Size) return;
                    _table.SetValue(foxPosition.Key, foxPosition.Value, 0);
                    foxPosition = new KeyValuePair<int, int>(foxPosition.Key + 1, foxPosition.Value);
                    _table.SetValue(foxPosition.Key, foxPosition.Value, 1);
                    pickChees();
                    break;
                case 'd':
                    if (foxPosition.Value + 1 >= _table.Size) return;
                    _table.SetValue(foxPosition.Key, foxPosition.Value, 0);
                    foxPosition = new KeyValuePair<int, int>(foxPosition.Key, foxPosition.Value + 1);
                    _table.SetValue(foxPosition.Key, foxPosition.Value, 1);
                    pickChees();
                    break;
                case 'a':
                    if (foxPosition.Value - 1 < 0) return;
                    _table.SetValue(foxPosition.Key, foxPosition.Value, 0);
                    foxPosition = new KeyValuePair<int, int>(foxPosition.Key, foxPosition.Value - 1);
                    _table.SetValue(foxPosition.Key, foxPosition.Value, 1);
                    pickChees();
                    break;
                default:
                    break;
            }
            OnGameAdvanced();

        }

        /*
         * 0 pálya
         * 1 róka
         * 2 sajt
         * */
        private void GenerateFields()
        {
            for (int i = 0; i < _table.Size; ++i)
            {
                for (int j = 0; j < _table.Size; ++j)
                {
                    _table.SetValue(i, j, 0);
                }
            }
            _table.SetValue(0, 0, 1);
            foxPosition = new KeyValuePair<int, int>(0, 0);
        }

        private void OnGameAdvanced()
        {
             GameAdvanced?.Invoke(this, new FoxAndCheesEventArgs(false,_foxLife, _eatenChees, _gameTime));
        }

        private void OnGameOver(Boolean isGameOver)
        {
            if (GameOver != null)
                GameOver(this, new FoxAndCheesEventArgs(isGameOver, _foxLife, _eatenChees, _gameTime));
        }
    }
}

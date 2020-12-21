using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Roham.model
{
    /*
     * nem kene egy tablatorlo metodus?
     * 
     * 
     */
    class RohamModel
    {
        private Int32 player = 1;
        private Int32 firstPlayerPuppetCount;// = gameSize;
        private Int32 secondPlayerPuppetCount;// = gameSize;

        private RohamTable _table;

        public Int32 FirstPlayerPuppetCount { get { return firstPlayerPuppetCount; } }
        public Int32 SecondPlayerPuppetCount { get { return secondPlayerPuppetCount; } }
        public Int32 Player { get { return player; } set => player = value;  }

        //jatektabla lekerdezese
        public RohamTable Table { get { return _table; }  }

        public Boolean IsGameOver { get { return (firstPlayerPuppetCount == 0 || secondPlayerPuppetCount == 0); } }


        public event EventHandler<RohamEventArgs> GameAdvanced; //sztm ez nem kell most
        public event EventHandler<RohamEventArgs> GameOver;
        
        public event EventHandler<RohamEventArgs> GameCreated;

        public RohamModel()
        {
            _table = new RohamTable(8);
        }

        public void NewGame(int size)
        {
            _table = new RohamTable(size);
            firstPlayerPuppetCount = secondPlayerPuppetCount = _table.Size;
            GenerateFields(Table.Size);
            OnGameCreated();
        }

        //jatekidot kene leptetni sztm nem fogom hasznalni
        public void AdvaceTime()
        {
            if (IsGameOver) return;
        }


        public void StepClear(Int32 x, Int32 y)
        {
            _table.StepValue(x, y, 0);
        }

        public void Step(Int32 x, Int32 y)
        {
            
            if (IsGameOver) return;
            
            _table.StepValue(x, y, player);  //ezt lehet nem így kell
            player = (player % 2 + 1);
            firstPlayerPuppetCount = 0;
            secondPlayerPuppetCount = 0;

            for (Int32 i = 0; i < _table.Size; ++i)
            {
                for (Int32 j = 0; j < _table.Size; ++j)
                {
                    Debug.Write(_table.GetValue(i, j) + " ");
                    if (_table.GetValue(i, j) == 1) firstPlayerPuppetCount++;
                    if (_table.GetValue(i, j) == 2) secondPlayerPuppetCount++;
                }
                Debug.Write("\n");
            }


            OnGameAdvanced(); // ez ide valószínűleg nem kell

            if (_table.IsOnlyOnePlayer)  // ezt valahogy meg kéne írni, kikérni a tablabol hogy csak 1 érték van e
            {
                OnGameOver(true,player);
            }
            
        }

        public void GenerateFields(Int32 size)
        {
            
            for (Int32 i = 0; i < size; ++i)
            {
                for(Int32 j = 0; j < size; ++j)
                {
                    if(i == 0)
                    {
                        _table.SetValue(i, j, 1);
                    }
                    else if(i == (size - 1))
                    {
                        _table.SetValue(i, j, 2);
                    }
                    else
                    {
                        _table.SetValue(i, j, 0);
                    }
                    
                }
            }


        }
        private void OnGameCreated()
        {
            GameCreated?.Invoke(this, new RohamEventArgs(false, player));
        }

        public void OnGameOver(Boolean isWon, int player)
        {
            GameOver?.Invoke(this, new RohamEventArgs(isWon, player));
        }
     

        private void OnGameAdvanced()
        {
            GameAdvanced?.Invoke(this, new RohamEventArgs(false, player));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TamadasWPF.Model
{
    class TamadasEventArgs : EventArgs
    {
        private Int32 _gameTime;
        private Int32 _player;
        private Int32 _puppetNumber;
        private Boolean _isWon;
        public Int32 GameTime { get { return _gameTime; } }
        public Int32 Player { get { return _player; } }
        public Int32 PuppetNumber { get { return _puppetNumber; } }
        public Boolean IsWon { get { return _isWon; } }
        public TamadasEventArgs(Boolean isWon, Int32 player, Int32 puppetNumber, Int32 gameTime)
        {
            _isWon = isWon;
            _player = player;
            _puppetNumber = puppetNumber;
            _gameTime = gameTime;
        }
    }
}

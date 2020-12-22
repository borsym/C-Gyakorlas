using System;
using System.Collections.Generic;
using System.Text;

namespace TiliToli.Model
{
    class TiliToliEventArgs : EventArgs
    {
        private Int32 _gameTime;
        private Boolean _isWon;

        public Int32 GameTime { get { return _gameTime; } }
        public Boolean IsWon { get { return _isWon; } }

        public TiliToliEventArgs( bool isWon, int gameTime)
        {
            _gameTime = gameTime;
            _isWon = isWon;
        }
    }
}

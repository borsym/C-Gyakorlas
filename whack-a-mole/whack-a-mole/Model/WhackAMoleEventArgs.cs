using System;
using System.Collections.Generic;
using System.Text;

namespace whack_a_mole.Model
{
    class WhackAMoleEventArgs
    {
        private Int32 _gameTime;
        private Boolean _isGameOver;

        public Int32 GameTime { get { return _gameTime; } }
        public Boolean IsGameOver { get { return _isGameOver; } }

        public WhackAMoleEventArgs(Boolean isGameOver, Int32 gameTime)
        {
            _isGameOver = isGameOver;
            _gameTime = gameTime;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BlockDocuWPF.Model
{
    class BlockDocuEventArgs : EventArgs
    {
        private Int32 _gameTime;
        private Boolean _isGameOver;

        /// <summary>
        /// Játékidő lekérdezése.
        /// </summary>
        public Int32 GameTime { get { return _gameTime; } }

        /// <summary>
        /// Győzelem lekérdezése.
        /// </summary>
        public Boolean IsGameOver { get { return _isGameOver; } }


        public BlockDocuEventArgs(Boolean isGameOver, Int32 gameTime)
        {
            _isGameOver = isGameOver;
            _gameTime = gameTime;
        }
    }
}

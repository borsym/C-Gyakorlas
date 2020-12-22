using System;
using System.Collections.Generic;
using System.Text;

namespace FoxAndChees.Model
{
    class FoxAndCheesEventArgs : EventArgs
    {
        private Int32 _gameTime;
        private Int32 _eatenChees;
        private Boolean _isGameOver;
        private Int32 _foxLife;
        /// <summary>
        /// Játékidő lekérdezése.
        /// </summary>
        public Int32 GameTime { get { return _gameTime; } }
        public Int32 FoxLife { get { return _foxLife; } }
        public Boolean IsGameOver { get { return _isGameOver; } }

        /// <summary>
        /// Játéklépések számának lekérdezése.
        /// </summary>
        public Int32 EatenChees { get { return _eatenChees; } }

        /// <summary>
        /// Győzelem lekérdezése.
        /// </summary>
        public FoxAndCheesEventArgs(bool isGameOver,Int32 foxLife , Int32 eatenChees, Int32 gameTime)
        {
            _isGameOver = isGameOver;
            _eatenChees = eatenChees;
            _gameTime = gameTime;
            _foxLife = foxLife;
        }

    }
}

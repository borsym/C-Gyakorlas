using System;
using System.Collections.Generic;
using System.Text;

namespace amongus.Model
{
    class AmongUsModelEventArgs : EventArgs
    {
        private Int32 _gameTime;
        private Boolean _isWon;

        /// <summary>
        /// Játékidő lekérdezése.
        /// </summary>
        public Int32 GameTime { get { return _gameTime; } }


        /// <summary>
        /// Győzelem lekérdezése.
        /// </summary>
        public Boolean IsWon { get { return _isWon; } }

        /// <summary>
        /// Sudoku eseményargumentum példányosítása.
        /// </summary>
        /// <param name="isWon">Győzelem lekérdezése.</param>
        /// <param name="gameStepCount">Lépésszám.</param>
        /// <param name="gameTime">Játékidő.</param>
        public AmongUsModelEventArgs(Boolean isWon, Int32 gameTime)
        {
            _isWon = isWon;
            _gameTime = gameTime;
        }
    }
}

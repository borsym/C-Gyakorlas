using System;
using System.Collections.Generic;
using System.Text;

namespace Roham.model
{
    class RohamEventArgs
    {
        private Boolean _isWon;
        private Int32 _player;
        //lehet le kellesz majd kénri a játékosok bábuit
        public Boolean IsWon { get { return _isWon; } }
        public Int32 Player { get { return _player; } }
        public RohamEventArgs(Boolean isWon, Int32 player)
        {
            _isWon = isWon;
            _player = player;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMathGame.Exceptions
{
    public class GameRoomIsFullException:Exception
    {
        public GameRoomIsFullException() { }

        public GameRoomIsFullException(string message) : base(message) { }
    }
}

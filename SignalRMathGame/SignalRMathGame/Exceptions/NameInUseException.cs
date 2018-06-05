using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMathGame.Exceptions
{
    public class NameInUseException:Exception
    {
        public NameInUseException() { }

        public NameInUseException(string message) : base(message) { }
    }
}

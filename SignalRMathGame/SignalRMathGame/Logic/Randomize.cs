using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//----------------------------
using SignalRMathGame.Enums;

namespace SignalRMathGame.Logic
{
    public static class Randomize
    {

        public static int GenerateRandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1,10);
        }

        public static OperationsEnum GenerateRandomOperation()
        {
            Random rnd = new Random();
            return (OperationsEnum)Enum.ToObject(typeof(OperationsEnum), rnd.Next(0, 3));
        }

        public static bool GenerateRandomBoolean()
        {
            Random rnd = new Random();
            return rnd.Next(0, 1) == 0;
        }
    }
}

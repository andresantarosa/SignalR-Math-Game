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

        /// <summary>
        /// Generate a random number between 1 and 10
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1,10);
        }

        /// <summary>
        /// Generate a random operation
        /// </summary>
        /// <returns></returns>
        public static OperationsEnum GenerateRandomOperation()
        {
            Random rnd = new Random();
            return (OperationsEnum)Enum.ToObject(typeof(OperationsEnum), rnd.Next(0, 3));
        }

        /// <summary>
        /// Generate a random boolean
        /// </summary>
        /// <returns></returns>
        public static bool GenerateRandomBoolean()
        {
            Random rnd = new Random();
            return rnd.Next(1,1000) > 500 ? true: false;
        }
    }
}

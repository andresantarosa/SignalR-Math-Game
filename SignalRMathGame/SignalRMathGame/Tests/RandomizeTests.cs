using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//----------------------------
using NUnit.Framework;
using SignalRMathGame.Logic;
using SignalRMathGame.Enums;

namespace SignalRMathGame.Tests
{
    [TestFixture]
    public class RandomizeTests
    {
        [TestCase]
        public void TestOperationValue()
        {
            int value = Randomize.GenerateRandomNumber();
            Assert.GreaterOrEqual(value,0);
            Assert.LessOrEqual(value,10);
        }
    }
}

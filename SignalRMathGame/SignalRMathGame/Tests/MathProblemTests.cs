using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//---------------------------
using NUnit.Framework;
using SignalRMathGame.Logic;
using SignalRMathGame.Model;
using SignalRMathGame.Enums;
using SignalRMathGame.Interfaces;

namespace SignalRMathGame
{
    [TestFixture]
    public class MathProblemTests
    {
        private MathProblem _mathProblem;

        public MathProblemTests()
        {
            _mathProblem = new MathProblem();
        }

        [Test]
        public void TestResultAdd()
        {
            Assert.AreEqual(10f, _mathProblem.GetResult(OperationsEnum.Addition, 5, 5, true));
        }

        [Test]
        public void TestResultSub()
        {            
            Assert.AreEqual(2f, _mathProblem.GetResult(OperationsEnum.Subtraction, 5, 3, true));
        }

        [Test]
        public void TestResultMultip()
        {             
            Assert.AreEqual(15f, _mathProblem.GetResult(OperationsEnum.Multiplication, 5, 3, true));
        }

        [Test]
        public void TestResultDiv()
        {
            Assert.AreEqual(5f, _mathProblem.GetResult(OperationsEnum.Division, 10, 2, true));
        }

        [Test]
        public void TestTypeAdd()
        {
            Assert.AreEqual(_mathProblem.GetSymbol(OperationsEnum.Addition), "+");
        }

        [Test]
        public void TestTypeSub()
        {
            Assert.AreEqual(_mathProblem.GetSymbol(OperationsEnum.Subtraction), "-");
        }

        [Test]
        public void TestTypeMultip()
        {
            Assert.AreEqual(_mathProblem.GetSymbol(OperationsEnum.Multiplication), "x");
        }

        [Test]
        public void TestTypeDiv()
        {
            Assert.AreEqual(_mathProblem.GetSymbol(OperationsEnum.Division), "/");
        }
    }


}
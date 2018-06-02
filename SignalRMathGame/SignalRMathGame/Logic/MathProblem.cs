using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//--------------------------
using SignalRMathGame.Model;
using SignalRMathGame.Interfaces;
using SignalRMathGame.Enums;

namespace SignalRMathGame.Logic
{
    public class MathProblem : MathProblemModel, IMathProblem
    {

        private MathProblem _mathProblem { get; set; }

        public MathProblem()
        {
            _mathProblem = CreateProblem();
        }

        public MathProblem GetMathProblem()
        {
            return _mathProblem;
        }
        public MathProblem CreateProblem()
        {
            this.number1 = Randomize.GenerateRandomNumber();
            this.number2 = Randomize.GenerateRandomNumber();
            this.operation = Randomize.GenerateRandomOperation();
            this.expression = $"{number1} {GetSymbol(this.operation)} {number2}";
            this.result = GetResult(this.operation, this.number1, this.number2, true);
            this.possibleResult = GetResult(this.operation, this.number1, this.number2, Randomize.GenerateRandomBoolean());
            
            return this;
        }

        public float GetResult(OperationsEnum operation, int number1, int number2, bool matchResult)
        {
            switch (operation)
            {
                case OperationsEnum.Addition:
                    return matchResult == true ? number1 + number2 : Randomize.GenerateRandomNumber() + Randomize.GenerateRandomNumber();
                case OperationsEnum.Subtraction:
                    return matchResult == true ? number1 - number2 : Randomize.GenerateRandomNumber() - Randomize.GenerateRandomNumber();
                case OperationsEnum.Multiplication:
                    return matchResult == true ? number1 * number2 : Randomize.GenerateRandomNumber() * Randomize.GenerateRandomNumber();
                case OperationsEnum.Division:
                    return matchResult == true ? number1 / number2 : Randomize.GenerateRandomNumber() / Randomize.GenerateRandomNumber();
            }
            return 0f;
        }

        public string GetSymbol(OperationsEnum operation)
        {
            switch (operation)
            {
                case OperationsEnum.Addition:
                    return "+";
                case OperationsEnum.Subtraction:
                    return "-";
                case OperationsEnum.Multiplication:
                    return "x";
                case OperationsEnum.Division:
                    return "/";
            }
            return "";
        }
    }
}

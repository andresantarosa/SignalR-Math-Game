using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//--------------------------
using SignalRMathGame.Model;
using SignalRMathGame.Interfaces;
using SignalRMathGame.Enums;
using SignalRMathGame.Exceptions;

namespace SignalRMathGame.Logic
{
    public class MathProblem : MathProblemModel, IMathProblem
    {

        private MathProblem _mathProblem { get; set; }
        private IScore _IScore;

        public MathProblem(IScore IScore)
        {
            _IScore = IScore;
            _mathProblem = CreateProblem();
        }

        public MathProblem() { }

        /// <summary>
        /// Get current math challenge
        /// </summary>
        /// <returns>Return current math challenge</returns>
        public MathProblem GetMathProblem()
        {
            return _mathProblem;
        }

        /// <summary>
        /// Create new math challenge
        /// </summary>
        /// <returns>Return a new math challenge</returns>
        public MathProblem CreateProblem()
        {
            this.number1 = Randomize.GenerateRandomNumber();
            this.number2 = Randomize.GenerateRandomNumber();
            this.operation = Randomize.GenerateRandomOperation();
            this.expression = $"{number1} {GetSymbol(this.operation)} {number2}";
            this.result = GetResult(this.operation, this.number1, this.number2, true);
            this.possibleResult = GetResult(this.operation, this.number1, this.number2, Randomize.GenerateRandomBoolean());
            this.isAnswered = false;
            this.playersInMatch = _IScore.GetScore().Select(x => x.playerName).ToList();

            return this;
        }

        /// <summary>
        /// Calculate the result of an expression
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="matchResult">True: Result will be exact, False: Result can be differente of the real result</param>
        /// <returns></returns>
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

        /// <summary>
        /// Return the operation symbol based on OperationsEnum enumerator
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if the result is correct
        /// </summary>
        /// <returns></returns>
        public bool IsCorrect()
        {
            return this.result == this.possibleResult;
        }

        /// <summary>
        /// Set challenge as answered
        /// </summary>
        public void setAnswered()
        {
            this.isAnswered = true;
        }

        /// <summary>
        /// Add new player to current match
        /// </summary>
        /// <param name="name"></param>
        public void AddPlayerToMatch(string name)
        {
            this.playersInMatch.Add(name);
        }

        /// <summary>
        /// Remove player from current match
        /// </summary>
        /// <param name="name"></param>
        public void removePlayerInMatch(string name)
        {
            this.playersInMatch.Remove(name);
        }

        /// <summary>
        /// Return a list of players in match
        /// </summary>
        /// <returns></returns>
        public List<string> GetPlayersInMatch()
        {
            return this.playersInMatch;
        }
    }
}

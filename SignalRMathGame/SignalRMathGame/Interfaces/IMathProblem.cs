using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//---------------------------
using SignalRMathGame.Model;
using SignalRMathGame.Enums;
using SignalRMathGame.Logic;

namespace SignalRMathGame.Interfaces
{
    public interface IMathProblem
    {
        MathProblem CreateProblem();

        string GetSymbol(OperationsEnum operation);

        float GetResult(OperationsEnum operation, int number1, int number2, bool matchResult);

        void AddPlayerToMatch(string name);

        void removePlayerInMatch(string name);

        List<string> GetPlayersInMatch();

        MathProblem GetMathProblem();

        bool IsCorrect();

        void setAnswered();
    }
}

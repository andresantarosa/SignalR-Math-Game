using System;
using Microsoft.AspNetCore.SignalR;
using SignalRMathGame.Interfaces;
using System.Threading.Tasks;

namespace SignalRMathGame
{
    public class MathGame:Hub
    {
        IScore _IScore;
        IMathProblem _IMathProblem;
        public MathGame(IScore score, IMathProblem mathProblem)
        {
            _IScore = score;
            _IMathProblem = mathProblem;
        }

        public async Task receiveMessage(string message)
        {
            await Clients.All.SendAsync("receiveMessage", _IMathProblem.GetMathProblem());
        }

        public async Task refreshChallenge()
        {
            _IMathProblem.CreateProblem();
            await Clients.All.SendAsync("receiveMessage", _IMathProblem.GetMathProblem());
        }
    }
}
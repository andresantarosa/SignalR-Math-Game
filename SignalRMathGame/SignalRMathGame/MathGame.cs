using System;
using Microsoft.AspNetCore.SignalR;
using SignalRMathGame.Interfaces;
using System.Threading.Tasks;
//-------------------------
using SignalRMathGame.Model;
using SignalRMathGame.Exceptions;
using System.Threading;

namespace SignalRMathGame
{
    public class MathGame : Hub
    {
        IScore _IScore;
        IMathProblem _IMathProblem;
        public MathGame(IScore score, IMathProblem mathProblem)
        {
            _IScore = score;
            _IMathProblem = mathProblem;
        }

        public async Task enterGame(string name)
        {
            try
            {
                _IScore.AddPlayer(name);
                _IMathProblem.AddPlayerToMatch(name);
                await addedToGameRoom();
                await receiveScore();
                await receiveMessage("teste");
            }
            catch (GameRoomIsFullException)
            {
                await riseAlert("Gameroom is full");
            }
            catch (NameInUseException)
            {
                await riseAlert("The name is already in use, please choose another one");
            }
            catch (Exception ex)
            {
                await riseAlert(ex.Message);
            }
        }

        public async Task addedToGameRoom()
        {
            await Clients.Caller.SendAsync("addedToGameRoom", "");
        }

        public async Task receiveMessage(string message)
        {
            await Clients.All.SendAsync("receiveMessage", _IMathProblem.GetMathProblem());
        }

        public async Task receiveScore()
        {
            await Clients.All.SendAsync("receiveScore", _IScore.GetScore());
        }

        public async Task refreshChallenge()
        {
            _IMathProblem.CreateProblem();
            await Clients.All.SendAsync("receiveMessage", _IMathProblem.GetMathProblem());
        }

        public async Task answerQuestion(AnswerModel answer)
        {
            var corr = _IMathProblem.IsCorrect();

            if (_IMathProblem.IsCorrect() && answer.isCorrect || !_IMathProblem.IsCorrect() && !answer.isCorrect)
            {
                if (!_IMathProblem.GetMathProblem().isAnswered)
                {
                    _IMathProblem.setAnswered();
                    _IScore.AddPoint(answer.playerName);
                    _IMathProblem.setAnswered();
                }
                await riseAlert("Correct!");
            }
            else
            {
                _IScore.RemovePoint(answer.playerName);
                await riseAlert("Wrong answer");
            }
            _IMathProblem.removePlayerInMatch(answer.playerName);

            if (_IMathProblem.GetPlayersInMatch().Count == 0)
            {
                _IMathProblem.CreateProblem();
                Thread.Sleep(5000);
                await receiveMessage("teste");
            }

            await receiveScore();
        }

        public async Task riseAlert(string message)
        {
            await Clients.Caller.SendAsync("riseAlert", message);
        }

        public async Task removeUser(string name)
        {
            _IScore.RemovePlayer(name);
            await receiveScore();
        }
    }
}
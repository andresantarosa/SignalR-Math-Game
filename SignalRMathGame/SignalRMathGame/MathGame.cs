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

        /// <summary>
        /// Add player to game room
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task enterGame(string name)
        {
            try
            {
                // Add player to score
                _IScore.AddPlayer(name);
                // Add player to mach
                _IMathProblem.AddPlayerToMatch(name);
                // Communicate the frontend that the entrance was accepted
                await addedToGameRoom();
                // Send the score to frontedn
                await receiveScore();
                // Get match challenge
                await receiveMessage("teste");
            }
            catch (GameRoomIsFullException)
            {
                // If the game room is full, an alert will be sent
                await riseAlert("Gameroom is full");
            }
            catch (NameInUseException)
            {
                // If the player name is already in use, an alert will be sent
                await riseAlert("The name is already in use, please choose another one");
            }
            catch (Exception ex)
            {
                await riseAlert(ex.Message);
            }
        }

        // Communicate the frontend that the entrance was accepted
        public async Task addedToGameRoom()
        {
            await Clients.Caller.SendAsync("addedToGameRoom", "");
        }

        // Send new math challenge
        public async Task receiveMessage(string message)
        {
            await Clients.All.SendAsync("receiveMessage", _IMathProblem.GetMathProblem());
        }

        // Send score to front-end
        public async Task receiveScore()
        {
            await Clients.All.SendAsync("receiveScore", _IScore.GetScore());
        }

        // Refresh challenge
        public async Task refreshChallenge()
        {
            _IMathProblem.CreateProblem();
            await Clients.All.SendAsync("receiveMessage", _IMathProblem.GetMathProblem());
        }

        // Answer Question
        public async Task answerQuestion(AnswerModel answer)
        {

            // Check if response is correct
            if (_IMathProblem.IsCorrect() && answer.isCorrect || !_IMathProblem.IsCorrect() && !answer.isCorrect)
            {
                // If the challenge was not answered yet, add +1 to player score
                if (!_IMathProblem.GetMathProblem().isAnswered)
                {
                    _IMathProblem.setAnswered();
                    _IScore.AddPoint(answer.playerName);
                    _IMathProblem.setAnswered();
                }
                await riseAlert("Correct!");
            }
            // If response is incorrect, remove -1 point of score of the player
            else
            {
                _IScore.RemovePoint(answer.playerName);
                await riseAlert("Wrong answer");
            }
            // Remove the player from the match
            _IMathProblem.removePlayerInMatch(answer.playerName);

            // If there is no more players in match, wait 5 seconds then create a new challenge and send it to playerss
            if (_IMathProblem.GetPlayersInMatch().Count == 0)
            {
                _IMathProblem.CreateProblem();
                Thread.Sleep(5000);
                await receiveMessage("teste");
            }

            await receiveScore();
        }

        // Rise alert on front-end
        public async Task riseAlert(string message)
        {
            await Clients.Caller.SendAsync("riseAlert", message);
        }

        // Remove user from match an from score
        // Used when a User is disconnected from the HUb
        public async Task removeUser(string name)
        {
            _IScore.RemovePlayer(name);
            _IMathProblem.removePlayerInMatch(name);
            await receiveScore();
        }
    }
}
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
        public async Task EnterGame(string name)
        {
            try
            {
                // Add player to score
                _IScore.AddPlayer(name);
                // Add player to mach
                _IMathProblem.AddPlayerToMatch(name);
                // Communicate the frontend that the entrance was accepted
                await AddedToGameRoom();
                // Send the score to frontedn
                await ReceiveScore();
                // Get match challenge
                await GetChallenge();
            }
            catch (GameRoomIsFullException)
            {
                // If the game room is full, an alert will be sent
                await RiseAlert("Gameroom is full");
            }
            catch (NameInUseException)
            {
                // If the player name is already in use, an alert will be sent
                await RiseAlert("The name is already in use, please choose another one");
            }
            catch (Exception ex)
            {
                await RiseAlert(ex.Message);
            }
        }

        // Communicate the frontend that the entrance was accepted
        public async Task AddedToGameRoom()
        {
            await Clients.Caller.SendAsync("AddedToGameRoom", "");
        }

        // Send new math challenge
        public async Task GetChallenge()
        {
            await Clients.All.SendAsync("GetChallenge", _IMathProblem.GetMathProblem());
        }

        // Send score to front-end
        public async Task ReceiveScore()
        {
            await Clients.All.SendAsync("ReceiveScore", _IScore.GetScore());
        }

        // Refresh challenge
        public async Task RefreshChallenge()
        {
            _IMathProblem.CreateProblem();
            await GetChallenge();
        }

        // Answer Question
        public async Task AnswerQuestion(AnswerModel answer)
        {
            bool isCorrect = false; ;
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
                isCorrect = true;
            }
            // If response is incorrect, remove -1 point of score of the player
            else
            {
                _IScore.RemovePoint(answer.playerName);
                isCorrect = false;
            }
            // Remove the player from the match
            _IMathProblem.removePlayerInMatch(answer.playerName);

            

            // If there is no more players in match, wait 5 seconds then create a new challenge and send it to playerss
            if (_IMathProblem.GetPlayersInMatch().Count == 0)
            {
                await ReceiveScore();
                await Clients.Caller.SendAsync("RiseAnswer", isCorrect == true ? "Correct" : "Wrong answer");
                await Clients.All.SendAsync("ChallengeFinished");
            }
            else
            {
                await ReceiveScore();
                await Clients.Caller.SendAsync("RiseAnswer", isCorrect == true ? "Correct" : "Wrong answer");
            }
            


        }

             // Rise alert on front-end
        public async Task RiseAlert(string message)
        {
            await Clients.Caller.SendAsync("RiseAlert", message);
        }

        // Remove user from match an from score
        // Used when a User is disconnected from the HUb
        public async Task RemoveUser(string name)
        {
            _IScore.RemovePlayer(name);
            _IMathProblem.removePlayerInMatch(name);
            await ReceiveScore();
        }
    }
}
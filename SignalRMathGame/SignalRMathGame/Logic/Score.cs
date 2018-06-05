using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//--------------------------
using SignalRMathGame.Model;
using SignalRMathGame.Interfaces;
using SignalRMathGame.Exceptions;

namespace SignalRMathGame.Logic
{
    public class Score : IScore
    {
        private ScoreModel _score;
        private ScoreItemModel _scoreItemModel;
        public Score(ScoreModel score, ScoreItemModel scoreItemModel)
        {
            _score = score;
            _scoreItemModel = scoreItemModel;
        }

        /// <summary>
        /// Get current score
        /// </summary>
        /// <returns>Return current score</returns>
        public List<ScoreItemModel> GetScore()
        {
            return _score.score;
        }

        /// <summary>
        /// Add a new player to the score
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns>Return current score</returns>
        public List<ScoreItemModel> AddPlayer(string playerName)
        {
            try
            {
                if (_score.score.Count >= 10)
                    throw new GameRoomIsFullException();
                if (_score.score.Count(x => x.playerName == playerName) > 0)
                    throw new NameInUseException();

                ScoreItemModel playerScore = new ScoreItemModel();
                playerScore.playerName = playerName;
                _score.score.Add(playerScore);
                return GetScore();
            }
            catch (GameRoomIsFullException)
            {
                throw new GameRoomIsFullException();
            }
            catch(NameInUseException)
            {
                throw new NameInUseException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add 1 point from player
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns>Return current score</returns>
        public List<ScoreItemModel> AddPoint(string playerName)
        {
            _score.score.FirstOrDefault(x => x.playerName == playerName).score += 1;
            return GetScore();
        }

        /// <summary>
        /// Remove 1 point from player
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns>Return current score</returns>
        public List<ScoreItemModel> RemovePoint(string playerName)
        {
            _score.score.FirstOrDefault(x => x.playerName == playerName).score -= 1;
            return GetScore();
        }

        /// <summary>
        /// Remove player from score
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns>Return current score</returns>
        public List<ScoreItemModel> RemovePlayer(string playerName)
        {
            ScoreItemModel data = _score.score.FirstOrDefault(x => x.playerName == playerName);
            _score.score.Remove(data);
            return GetScore();
        }
    }

}

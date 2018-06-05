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

        public List<ScoreItemModel> GetScore()
        {
            return _score.score;
        }

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

        public List<ScoreItemModel> AddPoint(string playerName)
        {
            _score.score.FirstOrDefault(x => x.playerName == playerName).score += 1;
            return GetScore();
        }

        public List<ScoreItemModel> RemovePoint(string playerName)
        {
            _score.score.FirstOrDefault(x => x.playerName == playerName).score -= 1;
            return GetScore();
        }

        public List<ScoreItemModel> RemovePlayer(string playerName)
        {
            ScoreItemModel data = _score.score.FirstOrDefault(x => x.playerName == playerName);
            _score.score.Remove(data);
            return GetScore();
        }
    }

}

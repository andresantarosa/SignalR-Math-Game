using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//--------------------------
using SignalRMathGame.Model;
using SignalRMathGame.Interfaces;

namespace SignalRMathGame.Logic
{
    public class Score:IScore
    {
        private List<ScoreModel> _score;

        public Score()
        {
            _score = new List<ScoreModel>();
            _score.Add(new ScoreModel
            {
                playerName = "aa",
                score = 1
            });
        }

        public List<ScoreModel> GetScore()
        {
            return _score;
        }

        public List<ScoreModel> AddPlayer(string playerName)
        {
            ScoreModel score = new ScoreModel();
            score.playerName = playerName;
            _score.Add(score);
            return _score;
        }

        public List<ScoreModel> AddPoint(string playerName)
        {
            _score.FirstOrDefault(x => x.playerName == playerName).score += 1;
            return _score;
        }

        public List<ScoreModel> RemovePoint(string playerName)
        {
            _score.FirstOrDefault(x => x.playerName == playerName).score -= 1;
            return _score;
        }

        public List<ScoreModel> RemovePlayer(string playerName)
        {
            ScoreModel data = _score.FirstOrDefault(x => x.playerName == playerName);
            _score.Remove(data);
            return _score;
        }
    }

}

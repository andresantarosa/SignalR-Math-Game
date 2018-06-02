using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//--------------------------
using SignalRMathGame.Model;

namespace SignalRMathGame.Interfaces
{
    public interface IScore
    {
        List<ScoreModel> GetScore();

        List<ScoreModel> AddPlayer(string playerName);

        List<ScoreModel> AddPoint(string playerName);

        List<ScoreModel> RemovePoint(string playerName);

        List<ScoreModel> RemovePlayer(string playerName);
    }
}

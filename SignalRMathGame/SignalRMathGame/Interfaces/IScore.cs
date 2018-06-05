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
        List<ScoreItemModel> GetScore();

        List<ScoreItemModel> AddPlayer(string playerName);

        List<ScoreItemModel> AddPoint(string playerName);

        List<ScoreItemModel> RemovePoint(string playerName);

        List<ScoreItemModel> RemovePlayer(string playerName);
    }
}

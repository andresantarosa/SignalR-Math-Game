using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMathGame.Model
{
    /// <summary>
    /// Score model
    /// </summary>
    public class ScoreModel
    {
        public List<ScoreItemModel> score;
        public ScoreModel()
        {
            score = new List<ScoreItemModel>();
        }
    }
}

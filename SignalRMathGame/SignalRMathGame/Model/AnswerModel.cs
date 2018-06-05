using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMathGame.Model
{
    /// <summary>
    /// Model contains the playername an a true/false about the challenge result proposed
    /// </summary>
    public class AnswerModel
    {
        public string playerName { get; set; }
        public bool isCorrect { get; set; }
    }
}

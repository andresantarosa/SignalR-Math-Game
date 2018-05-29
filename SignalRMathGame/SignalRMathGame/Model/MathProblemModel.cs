using SignalRMathGame.Enums;
namespace SignalRMathGame.Model
{
    public abstract class MathProblemModel
    {
        public string expression{ get; set; }
        public int number1 { get; set; }
        public int number2 { get; set; }
        public OperationsEnum operation{ get; set; }
        public float result { get; set; }
        public float possibleResult { get; set; }
    }
}

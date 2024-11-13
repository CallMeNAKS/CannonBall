namespace CodeBase.Money.Convertor
{
    public class ScoreToMoneyConverter : IToMoneyConverter
    {
        public int ConvertScoreToMoney(int score)
        {
            return score / 10;
        }
    }
}
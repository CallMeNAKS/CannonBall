namespace CodeBase.Money
{
    public class MoneyBank
    {
        public int Money { get; private set; }

        public void AddMoney(int amount)
        {
            if (amount < 0)
                return;
            Money += amount;
        }

        public void RemoveMoney(int amount)
        {
            if (amount > 0)
                return;
            Money -= amount;
        }
    }
}
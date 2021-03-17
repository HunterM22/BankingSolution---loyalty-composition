namespace BankingDomain
{
    public interface INotifyTheFed
    {
        void NotifyOfWithdrawal(BankAccount bankAccount, decimal amountToWithdraw);
    }
}
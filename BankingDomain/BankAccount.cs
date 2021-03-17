using System;

namespace BankingDomain
{
    public class BankAccount
    {
        private decimal _balance = 5000; // Class Variable "Field"
        private ICanCalculateBankAccountBonuses _bankAccountBonusCalculator;
        private INotifyTheFed _fedNotifier;

        public BankAccount(ICanCalculateBankAccountBonuses bankAccountBonusCalculator, INotifyTheFed fedNotifier)
        {
            _bankAccountBonusCalculator = bankAccountBonusCalculator;
            _fedNotifier = fedNotifier;
        }

        public decimal GetBalance()
        {
            return _balance;
        }

        public void Deposit(decimal amountToDeposit)
        {
            // write the code I wish I had

            decimal bonus = _bankAccountBonusCalculator.For(_balance,
                amountToDeposit);

            _balance += amountToDeposit + bonus;
        }

        public void Withdraw(decimal amountToWithdraw)
        {
            var id = Guid.NewGuid();

            _fedNotifier.NotifyOfWithdrawal(this, amountToWithdraw);
            _balance -= amountToWithdraw;
        }
    }
}
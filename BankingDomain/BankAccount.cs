using System;

namespace BankingDomain
{
    public class BankAccount
    {
        private decimal _balance = 5000; // Class Variable "Field"
        private ICanCalculateBankAccountBonuses _bankAccountBonusCalculator;
        private INotifyTheFeds _fedNotifier;

        public BankAccount(ICanCalculateBankAccountBonuses bankAccountBonusCalculator, INotifyTheFeds fedNotifier)
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
            GuardNoNegatives(amountToDeposit);
            // write the code I wish I had
            decimal bonus = _bankAccountBonusCalculator.For(_balance,
                amountToDeposit);

            _balance += amountToDeposit  + bonus;
        }

        public void Withdraw(decimal amountToWithdraw)
        {
            GuardNoNegatives(amountToWithdraw);

            if (amountToWithdraw > _balance)
            {
                throw new OverdraftException();
            }
            _fedNotifier.NotifyOfWithdrawal(this, amountToWithdraw);
            _balance -= amountToWithdraw;
        }

        private  void GuardNoNegatives(decimal amount)
        {
            if (amount < 0)
            {
                throw new NoNegativeNumberTransactionsException();
            }
        }
    }
}
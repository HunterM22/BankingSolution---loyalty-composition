using BankingDomain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BankingTests
{
    public class BankAccountInteractionTests
    {

        [Fact]
        public void WithdrawalsNotifyTheFed()
        {
            var mockedFedNotifier = new Mock<INotifyTheFeds>();
            var account = new BankAccount(null, mockedFedNotifier.Object);
            var openingBalance = account.GetBalance();
            var amountToWithdraw = 142M;

            account.Withdraw(amountToWithdraw);

            // ?? WHAT DO WE ASSERT ON???
            mockedFedNotifier.Verify(f => 
            f.NotifyOfWithdrawal(account, amountToWithdraw));
        }

        [Fact]
        public void DepositUsesTheBonusCalculator()
        {
            // Given
            var stubbedCalculator = new Mock<ICanCalculateBankAccountBonuses>();

            //var account = new BankAccount(stubbedCalculator.Object, null);
            var account = new BankAccount(stubbedCalculator.Object, 
                new Mock<INotifyTheFeds>().Object
                );
            var openingBalance = account.GetBalance();
            var amountToDeposit = 10M;
            stubbedCalculator
                .Setup(c => c.For(openingBalance, amountToDeposit))
                .Returns(42);


            // When
            account.Deposit(amountToDeposit);
            /// Then
            Assert.Equal(
                openingBalance +
                amountToDeposit +
                42, account.GetBalance());
            
        }
    }

  
}

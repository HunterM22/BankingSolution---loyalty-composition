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
    public class BankAccountOverdraftTests
    {
       

        [Fact]
        public void OverdraftDoesNotDecreaseBalance()
        {
            var account = new BankAccount(null, new Mock<INotifyTheFeds>().Object);
            var openingBalance = account.GetBalance();

            try
            {
                account.Withdraw(openingBalance + 1);
            }
            catch (OverdraftException)
            {

               // Ignoring this.
            } finally
            {
                Assert.Equal(openingBalance, account.GetBalance());
            }

           
        }

        [Fact]
        public void OverdraftShouldThrowAnException()
        {
            var account = new BankAccount(null, new Mock<INotifyTheFeds>().Object);
            var openingBalance = account.GetBalance();


            Assert.Throws<OverdraftException>(() => account.Withdraw(openingBalance + 1));
        }

    }
}

using System;
using Xunit;
using TestverktygUnitTestingSHFK;
using System.Collections.Generic;
using Moq;

namespace TestTestverktygUnitTestingSHFKXunit
{
    public class TestBank
    {
        MockRepository mockRepository = new MockRepository(MockBehavior.Strict);
        Mock<DatabaseContext> mockdbContext;
        public TestBank()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            //Mock<DatabaseContext> mockdbContext = new Mock<DatabaseContext>();
            mockdbContext = mockRepository.Create<DatabaseContext>();
        }

        [Fact]
        [Trait("Read", "Customer")]
        public void GetCustomers_DoesNotContainNullValues()
        {
            Bank bank = new(mockdbContext.Object);

            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List < Customer > customerList = bank.GetCustomers();

            Assert.All(customerList, customer =>
            Assert.All(customer.GetType().GetProperties(), attribute =>
            Assert.NotNull(attribute.GetValue(customer)))
            );


            /*foreach (var cust in bank.GetCustomers())
            {
                Assert.NotNull(cust.firstName);
                Assert.NotNull(cust.personalNumber);
                Assert.NotNull(cust.surName);
            }*/
        }

        [Fact]
        [Trait("Read", "Customer")]
        public void GetCustomers_CheckIfThereAre3Customers()
        {
            Bank bank = new(mockdbContext.Object);

            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            int expected = 3;
            int actual = bank.GetCustomers().Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Create", "Customer")]
        public void AddCustomer_ReturnsTrueWhenAddingCustomers()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bool addedCustomer = bank.AddCustomer("Simon", "12345");
            Assert.True(addedCustomer);

        }

        [Fact]
        [Trait("Create", "Customer")]
        public void AddCustomer_ShouldNotAddSimonAsCustomerWhenNotUniquePersonalnumber()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List<Customer> customerList = bank.GetCustomers();

            bank.AddCustomer("Simon", "19760314");
            string expected = "19760314";


            //Vid Assert.NotEqual så utesluts Manuel för att undvika att jämföra samma personnummer med rätt person
            Assert.All(customerList, customer => 
            Assert.NotEqual(expected, 
            customer.firstName == "Manuel" ? "" : customer.personalNumber));
        } 
        
        [Fact]
        [Trait("Read", "Customer")]
        public void GetCustomerInfo_CheckIfFirstNameIsOnTheFirstRow()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List<string> customerAccount = bank.GetCustomerInfo("19860107");

            string expected = "Linnea";

                Assert.Equal(expected, customerAccount[0]);
            

        }
        [Fact]
        [Trait("Read", "Customer")]
        public void GetCustomerInfo_RetrieveAccountInfoAndAccountNumber()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List<string> customerAccount = bank.GetCustomerInfo("19911111");
            string [] expected = { "1001","1002" };
            string[] actual = { 
                customerAccount[2].Substring(0, 4), 
                customerAccount[3].Substring(0, 4) };

            // Den här koden funkar. Det som fattas är att den behöver kompletteras med en Outputhelper,
            // för att förtydliga vilket element som påverkas vid en felrapport
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        [Trait("Update", "Customer")]
        public void ChangeCustomerName_ReturnsTrueWhenGivenNewName()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bool changedName = bank.ChangeCustomerName("Madelaine", "19860107");

            Assert.True(changedName);
        }

        [Fact]
        [Trait("Update", "Customer")]
        public void ChangeCustomerName_NameHasChanged()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            string expected = "Madelaine";
            bank.ChangeCustomerName("Madelaine", "19860107");
            string actual = bank.GetCustomer("19860107").firstName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Update", "Customer")]
        public void ChangeCustomerName_ReturnsFalseIfCustomerDoesNotExist()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bool changedName = bank.ChangeCustomerName("Madelaine", "1276138726");

            Assert.False(changedName);
        }

        [Fact]
        [Trait("Delete", "Customer")]
        public void RemoveCustomer_RemovesACustomerFromDatabase()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            var customerList = bank.GetCustomers();
            bank.RemoveCustomer("19860107");

            Assert.All(customerList, customer =>
            Assert.DoesNotContain("19860107", customer.personalNumber));
        }

        [Fact]
        [Trait("Delete", "Customer")]
        public void RemoveCustomer_CheckThatBalanceIsNotLostAfterRemoval()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            var customerList = bank.GetCustomers();
            bank.RemoveCustomer("19860107");

            List<string> accountInfo = bank.GetCustomerInfo("19860107");

            Assert.All(accountInfo, balance => Assert.Equal("500", balance));
        }

        [Fact]
        [Trait("Create", "Accounts")]
        public void AddAccount_RecieveAccountNumberAfterAddingAccount()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            //int newAccountNumber = bank.AddAccount("19760314");

            for (int i = 0; i < 1000; i++)
            {
                Assert.InRange(bank.AddAccount("19760314"), 1000, 1999);
            }
            
        }

        [Fact]
        [Trait("Create", "Accounts")]
        public void AddAccount_ReturnsMinus1WhenNotSpecifyingPersonalNumber()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            int expected = -1;
            int actual = bank.AddAccount("");

            Assert.Equal(expected, actual);

        }

        [Fact]
        [Trait("Create", "Accounts")]
        public void AddAccount_OnlyAddsUniqueAccountNumbers()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            List<int> newAccounts = new List<int>();
            bool unique = true;

            for (int i = 0; i < 1000; i++)
            {
                newAccounts.Add(bank.AddAccount("19760314"));

                for (int j = 0; j < newAccounts.Count; j++)
                {
                    if (newAccounts[i] == newAccounts[j] && j != i)
                    {
                        unique = false;
                        break;
                    }
                }
                if (!unique)
                {
                    break;
                }
            }
            Assert.True(unique);
        }

        [Fact]
        [Trait("Read", "Accounts")]
        public void GetAccount_ReturnsAnAccountFromExistingCustomer()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            string expected = bank.GetCustomerInfo("19911111")[2];
            var actual = bank.GetAccount("19911111", 1001).GetType().GetProperties();

            Assert.All(actual, accountDetails => 
            Assert.Contains(accountDetails.GetValue(actual).ToString(), expected));
        }

        [Fact]
        [Trait("Read", "Accounts")]
        public void GetAccount_ReturnsNullWhenGivingEmptyValues()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            var account = bank.GetAccount("", 0);

;           Assert.Null(account);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Deposit_ReturnsTrueWhenAddingFunds()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            bool deposit = bank.Deposit("19760314", 1005, 35000);

            Assert.True(deposit);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Deposit_ReturnsFalseWhenCustomerAccountIsNonExisting()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            bool deposit = bank.Deposit("", 0, 35000);

            Assert.False(deposit);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Deposit_CheckBalanceAfterAddingFunds()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            bank.Deposit("19760314", 1005, 35000);
            string account = bank.GetCustomerInfo("19760314")[2];
            
            int space = account.LastIndexOf(" ");

            string expected = "35200";
            string actual = account.Substring(space).Trim();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Withdraw_AndCheckIfTheBalanceHasBeenSubtracted()
        {
            Bank bank = new Bank(mockdbContext.Object);

            Customer customer = new Customer();
            customer.firstName = "Fredrik";
            customer.personalNumber = "990901";

            int balance = 600;
            int toWithdraw = 300;
            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = balance });

            mockdbContext.Setup(mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            bank.Withdraw("990901", 1500, toWithdraw);

            int expected = balance - toWithdraw;
            int actual = (int)customer.customerAccounts[0].balance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Withdraw_ReturnsTrueIfSuccessfulTransaction()
        {
            Bank bank = new Bank(mockdbContext.Object);

            Customer customer = new Customer();
            customer.firstName = "Fredrik";
            customer.personalNumber = "990901";

            int balance = 600;
            int toWithdraw = 300;
            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = balance });

            mockdbContext.Setup(mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);
            bool hasWithdrawn = bank.Withdraw("990901", 1500, toWithdraw);

            Assert.True(hasWithdrawn);
        }

        [Fact]
        [Trait("Read", "Accounts")]
        public void GetAccountInfo_CheckIfFullAccountInfoIsReturned()
        {
            Bank bank = new Bank(mockdbContext.Object);
            Customer customer = new Customer();
            customer.firstName = "Fredrik";
            customer.personalNumber = "990901";

            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = 600 });

            mockdbContext.Setup(mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            string expected = "1500 debit 600";
            string actual = bank.GetAccountInfo("990901", 1500);

            Assert.Contains(expected, actual);
        }

        [Fact]
        [Trait("Read", "Accounts")]
        public void GetAccountInfo_CheckIfTheCorrectAccountIsReturned()
        {
            Bank bank = new Bank(mockdbContext.Object);
            Customer customer = new Customer();
            customer.firstName = "Simon";
            customer.personalNumber = "990901";

            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = 600 });
            mockdbContext.Setup(mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            string expected = "1500";
            string actual = bank.GetAccountInfo("990901", 1500);


            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Delete", "Accounts")]
        public void CloseAccount_BalanceShouldExistAfterAccountShutdown()
        {
            Bank bank = new Bank(mockdbContext.Object);
            Customer customer = new Customer();
            customer.firstName = "Laban";
            customer.personalNumber = "990901";

            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = 200 });

            mockdbContext.Setup(mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            string expected = "200";
            string actual = bank.CloseAccount("990901", 1500);


            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Delete", "Accounts")]
        public void CloseAccount_CheckIfAccountIsRemoved()
        {
            Bank bank = new Bank(mockdbContext.Object);
            Customer customer = new Customer();
            customer.firstName = "Laban";
            customer.personalNumber = "990901";

            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = 200 });

            mockdbContext.Setup(mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            bank.CloseAccount("", 1500);
            string actual = customer.customerAccounts[0].accountNumber.ToString();

            Assert.NotEqual("1500", actual);
        }
    }
}
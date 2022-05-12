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
        public void TestGetCustomers_DoesNotContainNullValues()
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
        public void TestGetCustomers_CheckNumberOfCustomers()
        {
            Bank bank = new(mockdbContext.Object);

            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            int expected = 3;
            int actual = bank.GetCustomers().Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddCustomer_IfReturnTrueWhenAddingCustomers()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bool addedCustomer = bank.AddCustomer("Simon", "12345");
            /*string expected = "Simon";
            string actual = bank.GetCustomer("12345").firstName;
            Assert.Equal(expected, actual);*/
            Assert.True(addedCustomer);

        }

        [Fact]
        public void AddCustomer_CheckIfPersonalnumberIsUnique()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List<Customer> customerList = bank.GetCustomers();

            bank.AddCustomer("Simon", "12345");
            bank.AddCustomer("CJ", "12345");
            string expected = "12345";

            Assert.All(customerList, customer => Assert.NotEqual(expected, customer.personalNumber));
        } 
        
        [Fact]
        public void GetCustomerInfo_CheckIfFirstNameIsOnTheFirstRow()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List<string> customerAccount = bank.GetCustomerInfo("19860107");

            string expected = "Linnea";

                Assert.Equal(expected, customerAccount[0]);
            

        }
        [Fact]
        public void GetCustomerInfo_RetrieveAccountInfo_AccountNumber()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
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
        public void ChangeCustomerName_ReturnsTrueWhenGivenNewName()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bool expected = bank.ChangeCustomerName("Madelaine", "19860107");

            Assert.True(expected);
        }

        [Fact]
        public void ChangeCustomerName_ReturnsFalseIfCustomerDoesNotExist()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bool expected = bank.ChangeCustomerName("Madelaine", "1276138726");

            Assert.False(expected);
        }

        [Fact]
        public void RemoveCustomer_RemovesACustomerFromDataFile()
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
        public void RemoveCustomer_CheckIfBalanceCanBeReturnedToCustomerAfterRemoval()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            var customerList = bank.GetCustomers();
            bank.RemoveCustomer("19860107");

            List<string> accountInfo = bank.GetCustomerInfo("19860107");

            Assert.All(accountInfo, balance => Assert.Equal("500", balance));
        }

        [Fact]
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

        //WIP
        [Fact]
        public void AddAccount_OnlyAddsUniqueAccountNumbers()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            List<int> newAccounts = new List<int>();
            bool unique = true;

            for (int i = 0; i < 1000; i++)
            {
                newAccounts.Add(bank.AddAccount("19760314"));

                for (int j = 0; j < newAccounts.Count - 1; j++)
                {
                    if (newAccounts[i] == newAccounts[j+1])
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
        public void Deposit_ReturnsFalseWhenCustomerIsNonExisting()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            bool deposit = bank.Deposit("", 0, 35000);

            Assert.False(deposit);
        }

        [Fact]
        public void Deposit_CheckBalanceAfterAddingFunds()
        {
            Bank bank = new(mockdbContext.Object);
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            int expected = 35000;
            bank.Deposit("19760314", 1005, expected);
            string account = bank.GetCustomerInfo("19760314")[2];


            // Kontot innehöll sen innan ett saldo på 200kr, därav subtraktionen på 200
            int space = account.LastIndexOf(" ");
            int actual = int.Parse(account.Substring(space)) - 200;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Withdraw()
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

            int expected = balance - toWithdraw;
            int actual = (int)customer.customerAccounts[0].balance;

            //Assert.Equal(expected, actual);
            Assert.True(hasWithdrawn);
            
        }
    }
}
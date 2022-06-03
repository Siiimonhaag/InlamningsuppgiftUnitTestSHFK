using System;
using Xunit;
using Xunit.Abstractions;
using TestverktygUnitTestingSHFK;
using System.Collections.Generic;
using Moq;

namespace TestTestverktygUnitTestingSHFKXunit
{
    public class BankFixture : IDisposable
    {
        static MockRepository mockRepository = new MockRepository(MockBehavior.Strict);
        public Mock<DatabaseContext> mockdbContext = mockRepository.Create<DatabaseContext>();

        public Bank Bank => new Bank(mockdbContext.Object);

        public void Dispose()
        {
            //Dispose code here
        }
    }

    public class TestBank : IClassFixture<BankFixture>
    {
        private BankFixture bankFixture;
        private ITestOutputHelper testConsole;
        public TestBank(BankFixture _bankFixture, ITestOutputHelper _testConsole)
        {
            bankFixture = _bankFixture;
            testConsole = _testConsole;
        }

        [Fact]
        [Trait("Read", "Customer")]
        public void GetCustomer_ReturnsFirstNameOfACustomer()
        {
            Bank bank = bankFixture.Bank;
            bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            string expected = "Rafael";
            string actual = bank.GetCustomer("19911111").firstName;

            Assert.Equal(expected, actual);

            testConsole.WriteLine("The returned firstname is: " + actual + " and is supposed to be: " + expected);
        }

        [Theory]
        [Trait("Read", "Customer")]
        [InlineData("")]
        [InlineData("12329878335651212")]
        public void GetCustomer_ReturnsNullWhenSearchParamIsInvalid(string personalNumber)
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
            bool isNull = false;
            Customer customer = bank.GetCustomer(personalNumber);
            isNull = customer == null ? true : false;

            Assert.Null(customer);
            testConsole.WriteLine("The returned value is null: " + isNull);
        }

        [Fact]
        [Trait("Read", "Customer")]
        public void GetCustomers_DoesNotContainNullValues()
        {
            Bank bank = bankFixture.Bank;

            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
            List <Customer> customerList = bank.GetCustomers();

            Assert.All(customerList, customer => 
            Assert.All(customer.GetType().GetProperties(), attribute => 
            Assert.NotNull(attribute.GetValue(customer))));


        }

        [Fact]
        [Trait("Read", "Customer")]
        public void GetCustomers_CheckIfThereAre3Customers()
        {
            Bank bank = bankFixture.Bank;

            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
            int expected = 3;
            int actual = bank.GetCustomers().Count;

            Assert.Equal(expected, actual);
            testConsole.WriteLine("Number of customers: " + actual);
        }

        [Fact]
        [Trait("Create", "Customer")]
        public void AddCustomer_ReturnsTrueWhenAddingCustomers()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
            bool addedCustomer = bank.AddCustomer("Simon", "12345");
            Assert.True(addedCustomer);
        }

        [Fact]
        [Trait("Create", "Customer")]
        public void AddCustomer_ShouldNotAddSimonAsCustomerWhenNotUniquePersonalnumber()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
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
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
            List<string> customerAccount = bank.GetCustomerInfo("19860107");

            string expected = "Linnea";

                Assert.Equal(expected, customerAccount[0]);

            testConsole.WriteLine("The first name is:" + customerAccount[0] + " and we expect " + expected);

        }
        [Fact]
        [Trait("Read", "Customer")]
        public void GetCustomerInfo_RetrieveAccountInfoAndAccountNumber()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
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
                testConsole.WriteLine("We expect " + expected[i] + " and the actual is " + actual[i]);
            }
        }

        [Fact]
        [Trait("Update", "Customer")]
        public void ChangeCustomerName_ReturnsTrueWhenGivenNewName()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
            bool changedName = bank.ChangeCustomerName("Madelaine", "19860107");

            Assert.True(changedName);

            testConsole.WriteLine("When we change name of the customer we expect a true returnvalue: " + changedName);
        }

        [Fact]
        [Trait("Update", "Customer")]
        public void ChangeCustomerName_NameHasChanged()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
            string expected = "Madelaine";
            bank.ChangeCustomerName("Madelaine", "19860107");
            string actual = bank.GetCustomer("19860107").firstName;

            Assert.Equal(expected, actual);

            testConsole.WriteLine("We expect the name to be " + expected + " and the actual name is: " + actual);
        }

        [Fact]
        [Trait("Update", "Customer")]
        public void ChangeCustomerName_ReturnsFalseIfCustomerDoesNotExist()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            
            bool changedName = bank.ChangeCustomerName("Madelaine", "1276138726");

            Assert.False(changedName);
            testConsole.WriteLine("If we try to change the name of a customer that does not exist we expect false: " + changedName);
        }

        [Fact]
        [Trait("Delete", "Customer")]
        public void RemoveCustomer_RemovesACustomerFromDatabase()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            var customerList = bank.GetCustomers();
            bank.RemoveCustomer("19860107");

            Assert.All(customerList, customer =>
            Assert.DoesNotContain("19860107", customer.personalNumber));

            int customerCount = 0;
            testConsole.WriteLine("Customer with personalnumber \"19860107\" should not exist");
            foreach (var cust in customerList)
            {
                customerCount++;
                testConsole.WriteLine("Customer " + customerCount + ": " + cust.firstName + ", personalnumber: " + cust.personalNumber);
            }
            //testConsole.WriteLine("The list of customers does not contain a customer with the personalnumber: 19860107");
        }

        [Fact]
        [Trait("Delete", "Customer")]
        public void RemoveCustomer_CheckThatBalanceIsNotLostAfterRemoval()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            var customerList = bank.GetCustomers();
            bank.RemoveCustomer("19860107");

            List<string> accountInfo = bank.GetCustomerInfo("19860107");

            Assert.All(accountInfo, balance => Assert.Equal("500", balance));

        }

        [Fact]
        [Trait("Create", "Accounts")]
        public void AddAccount_RecieveAccountNumberAfterAddingAccount()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            int newAccountNumber = bank.AddAccount("19760314");
            Assert.InRange(newAccountNumber, 1000, 1999);

            testConsole.WriteLine("This test checks if the new account number " + "\"" + newAccountNumber + "\"" +" is in range of 1000-1999");
        }
        

        [Theory]
        [InlineData("")]
        [InlineData("99478889736815§133")]
        [Trait("Create", "Accounts")]
        public void AddAccount_ReturnsMinus1WhenSendingInvalidInput(string personalNumber)
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            int expected = -1;
            int actual = bank.AddAccount(personalNumber);

            Assert.Equal(expected, actual);

            testConsole.WriteLine("If no personal number is given,\nthe returnvalue should be " + expected + 
                ".\nActual: " + actual);

        }

        [Fact]
        [Trait("Create", "Accounts")]
        public void AddAccount_OnlyAddsUniqueAccountNumbers()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            List<int> newAccounts = new List<int>();
            bool unique = true;
            int counter = 1;
            for (int i = 0; i < 1000; i++)
            {
                newAccounts.Add(bank.AddAccount("19760314"));

                for (int j = 0; j < newAccounts.Count; j++)
                {
                    if (newAccounts[i] == newAccounts[j] && j != i)
                    {
                        testConsole.WriteLine(counter + " accounts were created until an already existing account number was created.\n" +
                            "Account number: " + newAccounts[i]);
                        unique = false;
                        break;
                    }
                }
                if (!unique)
                {
                    break;
                }
                counter++;
            }
            Assert.True(unique);
        }

        [Fact]
        [Trait("Read", "Accounts")]
        public void GetAccount_ReturnsAnAccountFromExistingCustomer()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            string expected = bank.GetCustomerInfo("19911111")[2];
            var actual = bank.GetAccount("19911111", 1001);

            Assert.All(actual.GetType().GetProperties(), accountDetails => 
            Assert.Contains(accountDetails.GetValue(actual).ToString(), expected));

            foreach (var details in actual.GetType().GetProperties())
            {
                testConsole.WriteLine("Testdata "+ details.Name + ": " + details.GetValue(actual));
            }
        }

        [Fact]
        [Trait("Read", "Accounts")]
        public void GetAccount_ReturnsNullWhenGivingEmptyValues()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            var account = bank.GetAccount("", 0);

            Assert.Null(account);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Deposit_ReturnsTrueWhenAddingFunds()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            bool deposit = bank.Deposit("19760314", 1005, 35000);

            Assert.True(deposit);
            testConsole.WriteLine("When adding funds, function should return true. Returned value: " + deposit);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Deposit_ReturnsFalseWhenCustomerAccountIsNonExisting()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            bool deposit = bank.Deposit("", 0, 35000);

            Assert.False(deposit);
            testConsole.WriteLine("When account does not exist, return false. Returned value: " + deposit);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Deposit_CheckIfFundsAreAddedAfterDeposit()
        {
            Bank bank = bankFixture.Bank;
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            bank.Deposit("19760314", 1005, 35000);
            string account = bank.GetCustomerInfo("19760314")[2];
            
            int space = account.LastIndexOf(" ");

            string expected = "35200";
            string actual = account.Substring(space).Trim();

            Assert.Equal(expected, actual);
            testConsole.WriteLine("Deposit amount: " + 35000 + 
                " and amount before deposit was 200, actual balance: " + actual); ;
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Withdraw_AndCheckIfTheBalanceHasBeenSubtracted()
        {
            Bank bank = bankFixture.Bank;

            Customer customer = new Customer();
            customer.firstName = "Fredrik";
            customer.personalNumber = "990901";

            int balance = 600;
            int toWithdraw = 300;
            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = balance });

            bankFixture.mockdbContext.Setup
                (mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            bank.Withdraw("990901", 1500, toWithdraw);

            int expected = balance - toWithdraw;
            int actual = (int)customer.customerAccounts[0].balance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Update", "Accounts")]
        public void Withdraw_ReturnsTrueIfSuccessfulTransaction()
        {
            Bank bank = bankFixture.Bank;

            Customer customer = new Customer();
            customer.firstName = "Fredrik";
            customer.personalNumber = "990901";

            int balance = 600;
            int toWithdraw = 300;
            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = balance });

            bankFixture.mockdbContext.Setup
                (mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);
            bool hasWithdrawn = bank.Withdraw("990901", 1500, toWithdraw);

            Assert.True(hasWithdrawn);
        }

        [Fact]
        [Trait("Read", "Accounts")]
        public void GetAccountInfo_CheckIfFullAccountInfoIsReturned()
        {
            Bank bank = bankFixture.Bank;
            Customer customer = new Customer();
            customer.firstName = "Fredrik";
            customer.personalNumber = "990901";

            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = 600 });

            bankFixture.mockdbContext.Setup
                (mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            string expected = "1500 debit 600";
            string actual = bank.GetAccountInfo("990901", 1500);

            Assert.Contains(expected, actual);
        }

        [Fact]
        [Trait("Read", "Accounts")]
        public void GetAccountInfo_CheckIfTheCorrectAccountIsReturned()
        {
            Bank bank = bankFixture.Bank;
            Customer customer = new Customer();
            customer.firstName = "Simon";
            customer.personalNumber = "990901";

            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = 600 });

            bankFixture.mockdbContext.Setup
                (mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            string expected = "1500";
            string actual = bank.GetAccountInfo("990901", 1500);


            Assert.Contains(expected, actual);
        }

        [Fact]
        [Trait("Delete", "Accounts")]
        public void CloseAccount_BalanceShouldExistAfterAccountShutdown()
        {
            Bank bank = bankFixture.Bank;
            Customer customer = new Customer();
            customer.firstName = "Laban";
            customer.personalNumber = "990901";

            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = 200 });

            bankFixture.mockdbContext.Setup
                (mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            string expected = "200";
            string actual = bank.CloseAccount("990901", 1500);


            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Delete", "Accounts")]
        public void CloseAccount_CheckIfAccountIsRemoved()
        {
            Bank bank = bankFixture.Bank;
            Customer customer = new Customer();
            customer.firstName = "Laban";
            customer.personalNumber = "990901";

            customer.customerAccounts = new List<Account>();
            customer.customerAccounts.Add
                (new Account() { accountNumber = 1500, accountType = "debit", balance = 200 });

            bankFixture.mockdbContext.Setup
                (mock => mock.getCustomerByPersonalNumber("990901")).Returns(customer);

            //The requirements specify that you should be able to close an account by only 
            //using the account number. Hence why the personal number is left empty in the 
            //first parameter of CloseAccount
            bank.CloseAccount("", 1500);
            string actual = customer.customerAccounts[0].accountNumber.ToString();

            Assert.NotEqual("1500", actual);
        }
    }
}
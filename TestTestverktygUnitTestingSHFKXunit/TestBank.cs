using System;
using Xunit;
using TestverktygUnitTestingSHFK;
using System.Collections.Generic;

namespace TestTestverktygUnitTestingSHFKXunit
{
    public class TestBank
    {
        [Fact]
        public void TestGetCustomers_DoesNotContainNullValues()
        {
            Bank bank = new();

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
            Bank bank = new();

            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            int expected = 3;
            int actual = bank.GetCustomers().Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddCustomer_IfReturnTrueWhenAddingCustomers()
        {
            Bank bank = new();
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
            Bank bank = new();
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
            Bank bank = new();
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List<string> customerAccount = bank.GetCustomerInfo("19860107");

            string expected = "Linnea";

                Assert.Equal(expected, customerAccount[0]);
            

        }
        [Fact]
        public void GetCustomerInfo_RetrieveAccountInfo_AccountNumber()
        {
            Bank bank = new();
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List<string> customerAccount = bank.GetCustomerInfo("19911111");
            string [] expected = { "1002","1002" };
            string[] actual = { 
                customerAccount[2].Substring(0, 4), 
                customerAccount[3].Substring(0, 4) };

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
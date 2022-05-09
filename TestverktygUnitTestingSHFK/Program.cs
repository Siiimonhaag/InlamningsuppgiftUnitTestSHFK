using System;
using System.Collections.Generic;

namespace TestverktygUnitTestingSHFK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a list of all customers\n");

            Bank bank = new Bank();
            bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            List<string> customerList = bank.GetCustomerInfo("19911111");

            foreach (var customer in customerList)
            {
                Console.WriteLine(customer);
            }

        }
    }
}

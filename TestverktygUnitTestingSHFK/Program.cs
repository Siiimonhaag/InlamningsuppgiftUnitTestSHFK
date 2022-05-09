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
            bank.Load(@"C:\Users\simon\source\repos\TestverktygUnitTestingSHFK\data.txt");
            List<Customer> customerList = bank.GetCustomers();

            foreach (Customer customer in customerList)
            {
                Console.WriteLine("This is a new customer \n");
                foreach (var details in customer.GetType().GetProperties())
                {
                    Console.WriteLine(details.GetValue(customer));
                }
            }

        }
    }
}

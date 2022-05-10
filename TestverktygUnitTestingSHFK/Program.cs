using System;
using System.Collections.Generic;

namespace TestverktygUnitTestingSHFK
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new();
            //bank.Load(@"C:\Users\simon\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            //bank.Load(@"C:\Users\Fredrik\source\repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");
            bank.Load(@"C:\Users\F\Source\Repos\InlamningsuppgiftUnitTestSHFK\TestverktygUnitTestingSHFK\data.txt");

            List<int> newAccounts = new List<int>();
            int[] numbers = new int[1000];
            bool unique = true;

            for (int i = 0; i < 1000; i++)
            {
                newAccounts.Add(bank.AddAccount("19760314"));
                numbers[i] = newAccounts[i];
                Console.WriteLine(numbers[i]);
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (newAccounts[i] == numbers[j] && !(j == 0 && i == 0))
                    {
                        Console.WriteLine("Samma nummer: " + newAccounts[i]);
                        Console.WriteLine("Samma nummer: " + numbers[j]);
                        unique = false;
                        break;
                    }
                }
                if (!unique)
                {
                    break;
                }
            }
        }
    }
}

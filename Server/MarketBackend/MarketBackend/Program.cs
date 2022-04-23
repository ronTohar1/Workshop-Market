using MarketBackend.BusinessLayer.Buyers.Guests;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Example example = new Example();
            Console.WriteLine("Hello World!");
            if (example.IsEven(2))
                Console.WriteLine($"the number 2 is even");
            else
                Console.WriteLine($"the number 2 is not even");
        }
    }
}
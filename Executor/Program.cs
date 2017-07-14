using Models;
using System;
using DataStructuresNAlgorithms.Stacks;

namespace Executor
{
    class Program
    {
        static void Main(string[] args)
        {
            PostfixCalculator calculator = new PostfixCalculator();
            string command = Console.ReadLine();
            string[] values = command.Split(' ');
            Console.WriteLine(calculator.Calculate(values));
            Console.ReadLine();
        }
    }
}

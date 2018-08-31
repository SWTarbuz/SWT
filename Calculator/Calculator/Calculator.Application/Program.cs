using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Calculator();

            Console.WriteLine($"Test 5 + 2: {test.Add(5, 2)}");
            Console.WriteLine($"Test 5 - 2: {test.Subtract(5, 2)}");
            Console.WriteLine($"Test 5 * 2: {test.Multiply(5, 2)}");
            Console.WriteLine($"Test 5^2 : {test.Power(5, 2)}");
        }
    }
}

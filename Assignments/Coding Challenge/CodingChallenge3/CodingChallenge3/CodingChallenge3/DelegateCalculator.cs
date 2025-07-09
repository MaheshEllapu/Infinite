using System;

namespace CodingChallenge3
{
    class DelegateCalculator
    {
        delegate int CalcOperation(int a, int b);

        static int Perform(int a, int b, CalcOperation op)
        {
            return op(a, b);
        }

        static int Add(int x, int y) => x + y;
        static int Subtract(int x, int y) => x - y;
        static int Multiply(int x, int y) => x * y;

        static void Main()
        {
            Console.Write("Enter first number : ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("Enter second number : ");
            int num2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Addition : " + Perform(num1, num2, Add));
            Console.WriteLine("Subtraction : " + Perform(num1, num2, Subtract));
            Console.WriteLine("Multiplication : " + Perform(num1, num2, Multiply));

            Console.Read();
        }
    }
}

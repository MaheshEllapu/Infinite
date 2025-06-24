using System;

namespace Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Checking number equality
            NumberEquality();

            //Checking number is positive or negative
            PosOrNeg();

            //Performing the required operations
            Operations();

            //Multiplication Table
            MultiplicationTable();

            //Checking number equality
            SumOfNumbers();

            Console.Read();
        }

        public static void NumberEquality()
        {
            Console.Write("Input 1st number :");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input 2nd number :");
            int num2 = Convert.ToInt32(Console.ReadLine());
            if (num1 == num2)
                Console.WriteLine(num1 + " and " + num2 + " are equal. ");
            else
                Console.WriteLine(num1 + " and " + num2 + " are not equal. ");
        }

        public static void PosOrNeg()
        {
            int num = Convert.ToInt32(Console.ReadLine());
            if (num > 0)
                Console.WriteLine(num + " is a positive number. ");
            else if(num<0)
                    Console.WriteLine(num + " is a negative number. ");
            else
                Console.WriteLine(num + " is neither positive nor negative number. ");
        }

        public static void Operations()
        {
            Console.Write("Input 1st number :");
            var a = Convert.ToDouble(Console.ReadLine());

            Console.Write("Input operation :");
            char oper =Convert.ToChar( Console.ReadLine());

            Console.Write("Input 2nd number :");
            var b = Convert.ToDouble(Console.ReadLine());

            double answer;
            switch(oper)
            {
                case '+':
                    answer = a + b;
                    Console.WriteLine(a + " + " + b + " = " + answer);
                    break;
                case '-':
                    answer = a - b;
                    Console.WriteLine(a + " - " + b + " = " + answer);
                    break;
                case '*':
                    answer = a * b;
                    Console.WriteLine(a + " * " + b + " = " + answer);
                    break;
                case '/':
                    answer = a / b;
                    Console.WriteLine(a + " / " + b + " = " + answer);
                    break;
                default:
                    Console.WriteLine("Invalid operator");
                    break;
            }
        }
        public static void MultiplicationTable()
        {
            Console.Write("Enter the number : ");
            int number = Convert.ToInt32(Console.ReadLine());
            for(int i=0;i<11;i++)
            {
                Console.WriteLine($"{number}x{i}={number * i}");
            }
        }

        public static void SumOfNumbers()
        {
            Console.Write("Input 1st number :");
            int number1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input 2nd number :");
            int number2 = Convert.ToInt32(Console.ReadLine());
            int sum = number1 + number2;
            if (number1 == number2)
                Console.WriteLine(3 * sum);
            else
                Console.WriteLine(sum);
        }
    }
}

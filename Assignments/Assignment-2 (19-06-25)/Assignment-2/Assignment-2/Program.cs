using System;

namespace Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*******Swapping of Numbers*******");
            numberSwap();
            Console.ReadLine();

            Console.WriteLine("*******Repetition of a Number*******");
            repeatPattern();
            Console.ReadLine();

            Console.WriteLine("*******Days of a Week*******");
            dayOfTheWeek();
            Console.ReadLine();

            Console.WriteLine("------------------ARRAYS------------------");

            Console.WriteLine("*******Average,Minimum and Maximum Elements*******");
            arrayAvgMinMax();
            Console.ReadLine();

            Console.WriteLine("*******Marks*******");
            displayMarks();
            Console.ReadLine();

            Console.WriteLine("*******Copying of Elements in an Array*******");
            copyArrayElements();
            Console.ReadLine();

            Console.WriteLine("------------------STRINGS------------------");

            Console.WriteLine("*******Length of the Word*******");
            lengthOfTheWord();
            Console.ReadLine();

            Console.WriteLine("*******Reversing a Word*******");
            wordReverse();
            Console.ReadLine();

            Console.WriteLine("*******Comparing the Words*******");
            wordComparison();
            Console.Read();
        }

        public static void numberSwap()
        {
            int a, b;

            Console.Write("Enter 1st number :");
            a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter 2nd number :");
            b = Convert.ToInt32(Console.ReadLine());

            a = a + b;
            b = a - b;
            a = a - b;

            Console.WriteLine("After swapping :");
            Console.WriteLine("1st number :" + a);
            Console.WriteLine("2nd number :" + b);
        }

        public static void repeatPattern()
        {
            Console.Write("Enter a digit: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("{0} {0} {0} {0}",number);
            Console.WriteLine("{0}{0}{0}{0}",number);
            Console.WriteLine("{0} {0} {0} {0}", number);
            Console.WriteLine("{0}{0}{0}{0}", number);
        }

        public static void dayOfTheWeek()
        {
            int day = Convert.ToInt32(Console.ReadLine());
            string dayOfTheWeek;

            switch(day)
            {
                case 1:
                    dayOfTheWeek = "Monday";
                    break;
                case 2:
                    dayOfTheWeek = "Tuesday";
                    break;
                case 3:
                    dayOfTheWeek = "Wednesday";
                    break;
                case 4:
                    dayOfTheWeek = "Thursday";
                    break;
                case 5:
                    dayOfTheWeek = "Friday";
                    break;
                case 6:
                    dayOfTheWeek = "Saturday";
                    break;
                case 7:
                    dayOfTheWeek = "Sunday";
                    break;
                default:
                    dayOfTheWeek = "Invalid Day";
                    break;
            }
            Console.WriteLine(dayOfTheWeek);

        }

        public static void arrayAvgMinMax()
        {
            int[] arr = { 5, 12, 7, 22, 9 };
            int sum = 0, min = arr[0], max = arr[0];

            foreach (int num in arr)
            {
                sum += num;
                if (num < min)
                {
                    min = num;
                }
                if (num > max)
                {
                    max = num;
                }
            }
            double average = (double)sum / arr.Length;
            Console.WriteLine("Average :" + average);
            Console.WriteLine("Minimum :" + min);
            Console.WriteLine("Maximum :" + max);
        }

        public static void displayMarks()
        {
            int[] marks = new int[10];
            Console.WriteLine("Enter 10 marks :");
            
            for(int i=0;i<10;i++)
            {
                marks[i] = Convert.ToInt32(Console.ReadLine());
            }

            int total = 0, min = marks[0], max = marks[0];

            for (int i = 0; i < 10; i++)
            {
                total += marks[i];

                if (marks[i] < min)
                    min = marks[i];

                if (marks[i] > max)
                    max = marks[i];
            }

                double avg = (double)total / marks.Length;
                Console.WriteLine("Total :" + total);
                Console.WriteLine("Average :" + avg);
                Console.WriteLine("Minimum :" + min);
                Console.WriteLine("Maximum :" + max);

                for(int i=0;i<marks.Length-1;i++)
                {
                    for(int j=i+1;j<marks.Length;j++)
                    {
                        if(marks[i] > marks[j])
                        {
                            int temp = marks[i];
                            marks[i] = marks[j];
                            marks[j] = temp;
                        }
                    }
                }
            Console.WriteLine("Ascending Order :");

            foreach (int mark in marks)
                    Console.Write(mark + " ");

                Console.WriteLine();
            for (int i = 0; i < marks.Length - 1; i++)
            {
                for (int j = i + 1; j < marks.Length; j++)
                {
                    if (marks[i] < marks[j])
                    {
                        int temp = marks[i];
                        marks[i] = marks[j];
                        marks[j] = temp;
                    }
                }
            }
            Console.WriteLine("Descending Order :");
            foreach(int mark in marks)
            {
                Console.Write(mark + " ");
            }
            Console.WriteLine();
        }
        public static void copyArrayElements()
        {
            Console.Write("Enter the number of elements you want to copy : ");
            int size = Convert.ToInt32(Console.ReadLine());

            int[] src = new int[size];
            int[] dest = new int[size];

            Console.WriteLine("Enter " + size + " integers :");
            for (int i = 0; i<size;i++)
            {
                Console.Write("Element " + (i + 1) + ":");
                src[i] = Convert.ToInt32(Console.ReadLine());
            }                     

            for(int i = 0; i<size;i++)
            {
                dest[i] = src[i];
            }
            Console.WriteLine("Elements copied to destination array :");
            for(int i = 0; i<size;i++)
            {
                Console.Write(dest[i] + " ");
            }
        }

        public static void lengthOfTheWord()
        {
            Console.Write("Enter a word :");
            string word = Console.ReadLine();
            Console.WriteLine("Length :" + word.Length);
        }

        public static void wordReverse()
        {
            Console.Write("Enter a word :");
            string word = Console.ReadLine();
            string rev = "";
            for(int i = word.Length-1; i>=0; i--)
            {
                rev += word[i];
            }
            Console.WriteLine("Reversed word :" + rev);
        }

        public static void wordComparison()
        {
            Console.Write("Enter 1st word :");
            string word1 = Console.ReadLine();
            Console.Write("Enter 2nd word :");
            string word2 = Console.ReadLine();

            if (word1 == word2)
                Console.WriteLine("Both words are same");
            else
                Console.WriteLine("Words are different");
        }
    }
}

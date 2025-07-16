using System;

namespace CodingChallenge3
{
    class Box
    {
        public int Length { get; set; }
        public int Breadth { get; set; }

        public Box() { }

        public Box (int length, int breadth)
        {
            Length = length;
            Breadth = breadth;
        }

        public Box Add(Box other)
        {
            return new Box(this.Length + other.Length, this.Breadth + other.Breadth);
        }
        public void Display()
        {
            Console.WriteLine($"Length : {Length}, Breadth : {Breadth}");
        }
    }
    class BoxApp
    {
        static void Main()
        {
            Console.WriteLine("Enter length and breadth for Box 1 : ");
            int l1 = int.Parse(Console.ReadLine());
            int b1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter length and breadth for Box 2 : ");
            int l2 = int.Parse(Console.ReadLine());
            int b2 = int.Parse(Console.ReadLine());

            Box box1 = new Box(l1, b1);
            Box box2 = new Box(l2, b2);
            Box box3 = box1.Add(box2);

            Console.WriteLine("Resulting Box : ");
            box3.Display();

            Console.Read();
        }
    }
}

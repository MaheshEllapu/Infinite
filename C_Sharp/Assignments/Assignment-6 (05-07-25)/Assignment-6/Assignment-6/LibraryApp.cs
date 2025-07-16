using System;

namespace Assignment_6
{
    class Book
    {
        public string BookName;
        public string AuthorName;

        public Book(string name, string author)
        {
            BookName = name;
            AuthorName = author;
        }

        public void Display()
        {
            Console.WriteLine($"{BookName} by {AuthorName}");
        }
    }

    class BookShelf
    {
        Book[] books = new Book[5];
        public Book this[int i]
        {
            get => books[i];
            set => books[i] = value;
        }
    }

    class LibraryApp
    {
        static void Main(string[] args)
        {
            BookShelf shelf = new BookShelf();

            for(int i=0; i<5; i++)
            {
                Console.Write("Enter Book Name : ");
                string name = Console.ReadLine();

                Console.Write("Enter Author Name : ");
                string author = Console.ReadLine();

                shelf[i] = new Book(name, author);
                Console.Write("\n");
            }
            Console.WriteLine("\nBooks in Shelf : ");
            for (int i = 0; i < 5; i++)
                shelf[i].Display();
            Console.Read();
        }
    }
}

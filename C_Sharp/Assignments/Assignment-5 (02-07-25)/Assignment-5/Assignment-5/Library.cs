using System;

namespace Assignment_5
{
    class Books
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }

        public Books(string book, string author)
            {
            BookName = book;
            AuthorName = author;
        }
        public void Display() => Console.WriteLine(BookName + " by "+AuthorName);
    }

    class BookShelf
    {
        Books[] books = new Books[5];
        public Books this[int index]
        {
            get => books[index];
            set => books[index] = value;
        }
        
        public void ShowAll()
        {
            for(int i=0; i<5; i++)
            {
                books[i].Display();
            }
        }
    }
    class Library
    {
        static void Main()
        {
            BookShelf shelf = new BookShelf();
            shelf[0] = new Books("C# Basics", "Joha");
            shelf[1] = new Books("OOP Concepts", "Emma");
            shelf[2] = new Books("LINQ in Depth", "Ryan");
            shelf[3] = new Books("ASP.NET Core", "Maya");
            shelf[4] = new Books("Entity Framework", "Sara");
            shelf.ShowAll();
            Console.Read();
        }
    }
}

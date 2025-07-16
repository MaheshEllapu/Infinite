using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge2
{

    class Product
    {
        public int ProductId;
        public string ProductName;
        public double Price;

        public Product(int id, string name, double price)
        {
            ProductId = id;
            ProductName = name;
            Price = price;
        }

        public void Display()
        {
            Console.WriteLine($"ID : {ProductId} | Name : {ProductName} | Price : Rs.{Price}");
        }
    }

    class ProductApp
    {
        static void Main()
        {
            Product[] products = new Product[10];

            Console.WriteLine("Enter details for 10 products : \n");

            for(int i=0;i<10;i++)
            {
                Console.WriteLine($"Product {i+1} : ");

                Console.Write("Product Id : ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Product Name : ");
                string name = Console.ReadLine();

                Console.Write("Price : Rs.");
                double price = double.Parse(Console.ReadLine());

                products[i] = new Product(id, name, price);
                Console.WriteLine();
            }

            for (int i=0; i<products.Length-1; i++)
            {
                for(int j=i+1; j<products.Length; j++)
                {
                    if(products[i].Price>products[j].Price)
                    {
                        var temp = products[i];
                        products[i] = products[j];
                        products[j] = temp;
                    }
                }
            }
            Console.WriteLine("Products sorted by price :\n");

            foreach(var p in products)
            {
                p.Display();
            }
            Console.Read();
        }
    }
}

using System;

namespace Assignment_3
{
    class Sales
    {
        static void Main()
        {
            salesDetails.ShowData();
            Console.Read();
        }
    }
    class salesDetails
    {
        static int salesNo;
        static int productNo;
        static double price;
        static string dateOfSale;
        static int qty;
        static double totalAmount;
        static salesDetails()
        {
            Console.Write("Enter Sales No. :");
            salesNo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Product No. :");
            productNo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Price :");
            price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Quantity :");
            qty = Convert.ToInt32(Console.ReadLine());


            Console.Write("Enter Date of Sale (dd-mm-yyyy) :");
            dateOfSale = Console.ReadLine();
            Sales();
        }
        public static void Sales()
        {
            totalAmount = qty * price;
        }
        public static void ShowData()
        {
            Console.WriteLine("");
            Console.WriteLine("************ Sale Details ************");
            Console.WriteLine("Sales No. : " + salesNo);
            Console.WriteLine("Product No. : " + productNo);
            Console.WriteLine("Price : " + price);
            Console.WriteLine("Quantity : " + qty);
            Console.WriteLine("Date of Sale : " + dateOfSale);
            Console.WriteLine("Total Amount : " + totalAmount);
        }
    }
}

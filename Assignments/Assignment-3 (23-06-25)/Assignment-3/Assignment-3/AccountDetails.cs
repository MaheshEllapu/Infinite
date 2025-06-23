using System;

namespace Assignment_3
{
    class AccountDetails
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Account Number : ");
            int accNo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Customer Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter Account Type : ");
            string accType = Console.ReadLine();

            Console.Write("Enter Initial Balance : ");
            int initialBalance = Convert.ToInt32(Console.ReadLine());

            Accounts acc = new Accounts(accNo, name, accType, initialBalance);

            Console.Write("Enter Transaction Type - D for Deposit, W for Withdrawal : ");
            char transType = Convert.ToChar(Console.ReadLine());

            Console.Write("Enter amount : ");
            int amount = Convert.ToInt32(Console.ReadLine());

            acc.PerformTransaction(transType, amount);
            acc.ShowData();
            Console.Read();
        }
    }
    class Accounts
    {
        private int accountNo;
        private string customerName;
        private string accountType;
        private char transactionType;
        private int amount;
        private int balance;

        public Accounts(int accNo, string name, string accType, int initialBalance )
        {
            accountNo = accNo;
            customerName = name;
            accountType = accType;
            balance = initialBalance;
        }

        public void PerformTransaction(char transType, int amt)
        {
            transactionType = transType;
            amount = amt;
            if(transactionType=='D'||transactionType=='d')
            {
                Credit(amount);
            }
            else if(transactionType=='W'||transactionType=='w')
            {
                Debit(amount);
            }
            else
            {
                Console.WriteLine("Invalid transaction type.");
            }
        }
        private void Credit(int amount)
        {
            balance += amount;
        }
        private void Debit(int amount)
        {
            if(amount<=balance)
            {
                balance -= amount;
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public void ShowData()
        {
            Console.WriteLine("Account Details : ");
            Console.WriteLine("Account No. : "+accountNo);
            Console.WriteLine("Customer Name : "+customerName);
            Console.WriteLine("Account Type : "+accountType);
            Console.WriteLine("Transaction Type : "+(transactionType=='D'||transactionType=='d'?"Deposit":"Withdrawal"));
            Console.WriteLine("Transaction Amount : "+amount);
            Console.WriteLine("Current Balance : "+balance);
        }
    }
}

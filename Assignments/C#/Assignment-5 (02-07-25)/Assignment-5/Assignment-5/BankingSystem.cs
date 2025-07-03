using System;

namespace Assignment_5
{
    class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string msg) : base(msg) { }
    }

    class Bank
    {
        float balance = 1000;
        public void Deposit(float amt) => balance += amt;

        public void Withdraw(float amt)
        {
            if (amt > balance)
                throw new InsufficientBalanceException("Not enough balance");
            balance -= amt;
        }
        public void ShowBalance() => Console.WriteLine("Balance : " +balance);
    }
    class BankingSystem
    {
        static void Main(string[] args)
        {
            Bank b = new Bank();

            Console.Write("Enter amount to withdraw : ");
            float amount = float.Parse(Console.ReadLine());


            try
            {
                b.Withdraw(amount);
                Console.WriteLine("Withdrawal Successful.");
            }
            catch(InsufficientBalanceException e)
            {
                Console.WriteLine("Error : "+e.Message);
            }
            b.ShowBalance();
            Console.Read();
        }
    }
}

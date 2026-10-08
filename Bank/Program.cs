namespace Bank
{
    public class Program
    {
        static void Main(string[] args)
        {

            BankAccount account1 = new BankAccount("Andrei", 1000000);
            BankAccount account2 = new BankAccount("Lena", 10);
            Console.WriteLine($"account  {account1.Balance} №{account1.Number} {account1.Owner}");
            Console.WriteLine($"account {account2.Balance} №{account2.Number} {account2.Owner}");

            account1.MakeDeposit(2000000, DateTime.UtcNow, ":)");
            Console.WriteLine(account1.Balance);
            account1.MakeWithdrawal(200, DateTime.UtcNow, ":(");
            Console.WriteLine(account1.Balance);

            Console.WriteLine(account1.GetAccountHistory());

            try
            {
                account2.MakeWithdrawal(10000, DateTime.UtcNow, ":(");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            InterestEarningAccount interestEarn = new("Andrei", 1000m);
            interestEarn.MakeDeposit(100m, DateTime.UtcNow, ";)");
            interestEarn.MakeWithdrawal(10m, DateTime.UtcNow, ";(");
            interestEarn.PerformMonthAndTransactions();

            Console.WriteLine(interestEarn.ToString());
            Console.WriteLine(interestEarn.GetAccountHistory());

            GiftCartAccount giftCart = new("Andrei", 1000m, 5000m);
            giftCart.MakeDeposit(100m, DateTime.UtcNow, ";)");
            giftCart.MakeWithdrawal(10m, DateTime.UtcNow, ";(");
            giftCart.PerformMonthAndTransactions();

            Console.WriteLine(giftCart);
            Console.WriteLine(giftCart.GetAccountHistory());
        }
    }
}

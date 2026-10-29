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

            LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("Ugagi", 1000m, 1000m);
            lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "credit");

            GiftCartAccount giftcard = new GiftCartAccount("Ugagi", 1000m, 5000m);

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(lineOfCredit);
            accounts.Add(giftcard);
            accounts.Add(account1);
            accounts.Add(interestEarn);

            foreach (BankAccount bankAccount in accounts)
            {
                Console.WriteLine(bankAccount);
                Console.WriteLine(bankAccount.GetAccountHistory());
            }

            foreach (BankAccount bankAccount in accounts)
            {
                Console.WriteLine(bankAccount.GetAccountAudit());
            }

            BankAccount test1 = new BankAccount("tester1", 100000);
            BankAccount test2 = new BankAccount("tester2", 1000);

            test1.Transfer(5000, test2);
            Console.WriteLine(test1.GetAccountHistory());
            Console.WriteLine(test2.GetAccountHistory());

            test1.ChangeStatus(1);
            test1.PerformMonthAndTransactions();
            Console.WriteLine(test1.GetAccountHistory());
        }
    }
}

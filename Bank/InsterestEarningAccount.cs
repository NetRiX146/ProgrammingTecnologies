using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class InterestEarningAccount : BankAccount
    {
        public InterestEarningAccount(string name, decimal initialBalance) :
            base(name, initialBalance)
        { }

        // ovveride позволяет в дочернем классе определить новую реализацию

        public override void PerformMonthAndTransactions()
        {
            if (Balance > 500m)
            {
                decimal interest = Balance * 0.02m;
                MakeDeposit(interest, DateTime.UtcNow, "Apply mont");
            }
        }
    }
}

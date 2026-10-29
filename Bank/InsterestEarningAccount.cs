using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    /// <summary>
    /// Класс InterestEarningAccount наследующий BankAccount
    /// </summary>
    public class InterestEarningAccount : BankAccount
    {
        /// <summary>
        /// Конструктор класса InterestEarningAccount 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialbalance"></param>
        public InterestEarningAccount(string name, decimal initialBalance) :
            base(name, initialBalance)
        { }

        // ovveride позволяет в дочернем классе определить новую реализацию
        /// <summary>
        /// Метод дающий информацию о месячных транзакциях
        /// </summary>
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class GiftCartAccount : BankAccount
    {
        private readonly decimal _mqnthlyDeposit = 0m;

        public GiftCartAccount(string name, decimal initialBalance, decimal mqnthlyDeposit = 0) :
            base(name, initialBalance) => _mqnthlyDeposit = mqnthlyDeposit;

        public override void PerformMonthAndTransactions()
        {
            if (_mqnthlyDeposit != 0)
            {
                MakeDeposit(_mqnthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
            }
        }
        // base.ToString() - вызов базовой реализации => реализация из класса BankAccount
        public override string ToString()
        {
            return base.ToString() + $"monthly deposit: {_mqnthlyDeposit}";
        }
    }
}

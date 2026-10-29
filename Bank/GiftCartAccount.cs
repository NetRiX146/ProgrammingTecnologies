using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    /// <summary>
    /// Класс Подарочной карты наследуещий BankAccount
    /// </summary>
    public class GiftCartAccount : BankAccount
    {
        /// <summary>
        /// Депозит в месяц по умолчанию
        /// </summary>
        private readonly decimal _mqnthlyDeposit = 0m;
        /// <summary>
        /// Конструкор GiftCardAccount принимающий 3 параметра
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialbalance"></param>
        /// <param name="monthlyDeposit"></param>
        public GiftCartAccount(string name, decimal initialBalance, decimal mqnthlyDeposit = 0) :
            base(name, initialBalance) => _mqnthlyDeposit = mqnthlyDeposit;
        /// <summary>
        /// Метод добавляющий проценты по счёту
        /// </summary>
        public override void PerformMonthAndTransactions()
        {
            if (_mqnthlyDeposit != 0)
            {
                MakeDeposit(_mqnthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
            }
        }
        // base.ToString() - вызов базовой реализации => реализация из класса BankAccount
        /// <summary>
        ///  Метод предоставляющий информацию о типе,владельце и номере аккаунта,ежемесячный депозит
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $"monthly deposit: {_mqnthlyDeposit}";
        }
    }
}

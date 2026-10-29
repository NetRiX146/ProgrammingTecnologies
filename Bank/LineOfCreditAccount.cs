using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    /// <summary>
    /// Класс LineOfCreditAccount наследующий BankAccount
    /// </summary>
    public class LineOfCreditAccount : BankAccount
    {
        /// <summary>
        /// Конструктор класса LineOfCreditAccount принимающий 3 параметра
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="initialBalance">Начальный баланс</param>
        /// <param name="creditLimit">Лимит по кредиту</param>
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) :
            base(name, initialBalance, -creditLimit)
        { }
        /// <summary>
        /// Метод снимающий проценты по кредиту
        /// </summary>
        public override void PerformMonthAndTransactions()
        {
            if (Balance < 0)
            {
                decimal interest = -Balance * 0.07m;
                MakeWithdrawal(interest, DateTime.UtcNow, "Charge monthly interest");
            }
        }
        /// <summary>
        /// Метод сообщающий о overdraftе
        /// </summary>
        /// <param name="isOverdrawn">Проверка на овердрафт</param>
        /// <returns></returns>
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
            isOverdrawn ? new Transaction(-20, DateTime.UtcNow, "apply overdreft") : default;
    }
}

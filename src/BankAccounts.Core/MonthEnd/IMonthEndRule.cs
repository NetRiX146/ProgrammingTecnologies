using System;
using System.Collections.Generic;
using System.Text;
using BankAccounts.Core.Accounts;

namespace BankAccounts.Core.MonthEnd;

// Каждое правило - отдельный класс. 
// Новые правила добавляются без изменения старых (OCP, SRP)
public interface IMonthEndRule
{
    void Apply(IAccountOperations account, DateTime asOf);
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccounts.Core.Withdrawals;

public interface IWithdrawalPolicy
{
    WithdrawalDecision Evaluate(
        decimal currentBalance,
        decimal amount,
        decimal minimumBalance);
}

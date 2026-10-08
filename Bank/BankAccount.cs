using System;
using System.Collections.Generic;
using System.Text;

namespace Bank;
// Потомок класса object => можно переопределить виртуальные методы, находяящиеся в object
public class BankAccount
{
    private readonly decimal _minimumBalance;
    static private int s_accountNuberSeed = 1000000000;
    public string Number { get; }
    public string Owner { get; private set; }
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }

            return balance;
        }
    }

    private List<Transaction> _allTransactions = new List<Transaction>();

    public BankAccount(string name, decimal initialBalance) :
        this(name, initialBalance, 0)
    { }

    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {

        Owner = name; // this.Owner = name
        _minimumBalance = minimumBalance;
        if (initialBalance < 0) MakeDeposit(minimumBalance, DateTime.UtcNow, "Initial balance");
        MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
        Number = s_accountNuberSeed.ToString();
        s_accountNuberSeed++;
    }
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        //if (amount <= 0)
        //{
        //    throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        //}

        //if (Balance < amount)
        //{
        //    throw new InvalidOperationException("Not sufficient rubls for this withdawal");
        //}

        //var withdrawal = new Transaction(-amount, date, note);
        //_allTransactions.Add(withdrawal);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);
        _allTransactions.Add(withdrawal);

        if (overdraftTransaction is not null) _allTransactions.Add(overdraftTransaction);
    }

    private Transaction? CheckWithdrawalLimit(bool v)
    {
        if (v)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }

        return default;
    }
}
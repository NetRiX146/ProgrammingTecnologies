using System;
using System.Collections.Generic;
using System.Text;

namespace Bank;

// BankAccount - потомок класс object => можно переопределить 
// виртуальные методы находящиеся в object
/// <summary>
/// Класс банка предопределяющий его работу
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Минимальный баланс
    /// </summary>
    private readonly decimal _minimumBalance;
    /// <summary>
    /// Номер с которого начинается отсчёт id аккаунта
    /// </summary>
    static private int s_accountNuberSeed = 1000000000;
    /// <summary>
    /// Номер задаваемый клиентом
    /// </summary>
    public string Number { get; }
    /// <summary>
    /// Имя владельца счёта
    /// </summary>
    public string Owner { get; private set; }
    /// <summary>
    /// Баланс аккаунта
    /// </summary>
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
    /// <summary>
    /// Статус клиента банка
    /// </summary>
    public string Status { get; private set; } = "Unknown";

    /// <summary>
    /// Список всех транзакций для аккаунта
    /// </summary>
    private List<Transaction> _allTransactions = new List<Transaction>();
    /// <summary>
    /// Список всех аудитов для аккаунта
    /// </summary>
    private List<AuditLog> _allAuditLogs = new List<AuditLog>();
    /// <summary>
    /// Конструктор BankAccount с 2 параметрами
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="initialBalance"></param>
    /// <summary>
    /// Конструктор BankAccount с 2 параметрами
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="initialBalance"></param>
    public BankAccount(string name, decimal initialBalance) :
        this(name, initialBalance, 0)
    { }
    /// <summary>
    /// Конструктор класса банк c минимальным баланса
    /// </summary>
    /// <param name="owner">Имя владельца</param>
    /// <param name="initialBalance">Начальный баланс</param>
    /// <param name="minimumbalance">Минимальный баланс</param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {

        Owner = name; // this.Owner = name
        _minimumBalance = minimumBalance;
        if (initialBalance < 0) MakeDeposit(minimumBalance, DateTime.UtcNow, "Initial balance");
        MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
        Number = s_accountNuberSeed.ToString();
        s_accountNuberSeed++;
    }

    /// <summary>
    /// Метод позволяющий делать депозиты
    /// </summary>
    /// <param name="amount">сумма которая положится на счёт</param>
    /// <param name="date">время операции</param>
    /// <param name="note">комментарий к операции</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
        var audit = new AuditLog(s_accountNumberSeed, "MakeDeposit", amount, date);
        _allAuditLogs.Add(audit);
    }

    /// <summary>
    /// Метод снятия денег с аккаунта
    /// </summary>
    /// <param name="amount">сумма которую нужно снять</param>
    /// <param name="date"> время операции</param>
    /// <param name="note">комментарий к операции</param>
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
        AuditLog? withdrawalaudit = new AuditLog(s_accountNumberSeed, "MakeWithDrawal", amount, date);
        _allAuditLogs.Add(withdrawalaudit);
        if (overdraftTransaction is not null) _allTransactions.Add(overdraftTransaction);
    }

    /// <summary>
    /// Метод проверки на лимит вывода средств
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">
    /// Бросается если оведрафт не возможен </exception>
    private Transaction? CheckWithdrawalLimit(bool v)
    {
        if (v)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }

        return default;
    }

    /// <summary>
    /// Метод предоставляющий информацию об аккаунта
    /// </summary>
    /// <returns></returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"" +
                $"{item.Date.ToShortDateString()}\t" +
                $"{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }

    /// <summary>
    /// Функция возвращащая все аудиты на аккаунте
    /// </summary>
    /// <returns></returns>
    public string GetAccountAudit()
    {
        var audit = new StringBuilder();

        audit.AppendLine("NumberAccount\tTypeOfOperation\tAmount\tDate");
        foreach (var item in _allAuditLogs)
        {
            audit.AppendLine($"" + $"{item.NumberOfAccount}\t" + $"{item.TypeOfOperation}\t" + $"{item.Amount}\t" + $"{item.Date}");
        }
        return audit.ToString();
    }

    // virtual позволяет в дочернем классе предоставить другую реализацию
    /// <summary>
    /// Метод добавляющий проценты по счёту
    /// </summary>
    public virtual void PerformMonthAndTransactions()
    {
        if (Status == "Regular")
        {
            MakeWithdrawal(50, DateTime.UtcNow, "regular month service");
        }
        if (Status == "Premium")
        {
            if (Balance > 10000m)
            {
                decimal monthpay = (Balance - 10000) * 0.01m;
                MakeWithdrawal(monthpay, DateTime.UtcNow, "regular month service");
            }
        }
        if (Status == "VIP")
        {
            if (Balance > 10000m)
            {
                decimal monthpay = (Balance - 10000) * 0.03m;
                MakeWithdrawal(monthpay, DateTime.UtcNow, "regular month service");
                MakeDeposit(monthpay * 0.05m, DateTime.UtcNow, "cashback");
            }
        }
    }
    /// <summary>
    /// Метод предоставляющий информацию о типе,владельце и номере аккаунта
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"Type: {GetType().Name}\tOwner: {Owner}\tNumber of Account:{Number}";
    }
    /// <summary>
    /// Метод перевода денег с одного аккаунта на другой
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool Transfer(decimal amount, BankAccount target)
    {
        if (this.Balance - amount < 0)
        {
            return false;
        }
        try
        {
            target.MakeDeposit(amount, DateTime.UtcNow, $"Transfer from account {this.Owner} to {target.Owner}");
        }
        catch
        {
            return false;
        }
        this.MakeWithdrawal(amount, DateTime.UtcNow, $"Transfer from your account to {target.Owner}");
        return true;
    }
    /// <summary>
    /// Метод определяющий статус клиента банка
    /// </summary>
    /// <param name="i"> один из статусов на выбор 0,1,2</param>
    public void ChangeStatus(int i)
    {
        switch (i)
        {
            case (0): this.Status = "Regular"; break;
            case (1): this.Status = "Premium"; break;
            case (2): this.Status = "VIP"; break;
            default: this.Status = Status; break;
        }
    }
}
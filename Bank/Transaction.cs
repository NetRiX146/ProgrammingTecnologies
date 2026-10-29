using System;
using System.Collections.Generic;
using System.Text;

namespace Bank;

// record - Состояние объектов этого класса нельзя изменить
/// <summary>
/// Класс Транзакций, принмающий 3 параметра
/// </summary>
/// <param name="Amount">Сумма</param>
/// <param name="Date">Дата</param>
/// <param name="Note">Описание о транзакции</param>
public record Transaction(decimal Amount, DateTime Date, string Note);

//internal record Transaction
//{
//    public decimal Amount { get; }
//    public DateTime Date { get; }
//    public string Note { get; }
//    public Transaction(decimal Amount, DateTime Date, string Note)
//    {
//        this.Amount = Amount;
//        this.Note = Note;
//        this.Date = Date;
//    }
//}

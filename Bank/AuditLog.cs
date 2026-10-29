using System;
using System.Collections.Generic;
using System.Text;

namespace Bank;

public record AuditLog(int NumberOfAccount, string TypeOfOperation, decimal Amount, DateTime Date);

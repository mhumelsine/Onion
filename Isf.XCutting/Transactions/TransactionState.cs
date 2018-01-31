using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Transactions
{
    public enum TransactionState
    {
        NotStarted,
        Pending,
        Committed,
        RolledBack
    }
}

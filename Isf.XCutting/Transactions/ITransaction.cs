using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Transactions
{
    public interface ITransaction : IDisposable
    {
        TransactionState State { get; }
        void Commit();

        void Rollback();
    }
}

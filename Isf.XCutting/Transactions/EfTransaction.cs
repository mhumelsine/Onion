using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Transactions
{
    public class EfTransaction : ITransaction
    {
        private IDbContextTransaction transaction;

        public TransactionState State { get; private set; }

        public EfTransaction(IDbContextTransaction transaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            State = TransactionState.NotStarted;
        }

        public void Commit()
        {
            transaction.Commit();
            State = TransactionState.Committed;
        }

        public void Rollback()
        {
            transaction.Rollback();
            State = TransactionState.RolledBack;
        }

        public void Dispose()
        {
            transaction?.Dispose();
        }
    }
}

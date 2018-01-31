using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Transactions
{
    public class EfTransactionFactory : ITransactionFactory
    {
        private readonly DbContext dbContext;

        public EfTransactionFactory(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ITransaction CreateInstance()
        {
            return new EfTransaction(
                dbContext.Database.BeginTransaction());
        }
    }
}

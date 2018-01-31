using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Transactions
{
    public interface ITransactionFactory
    {
        ITransaction CreateInstance();
    }
}

using Isf.XCutting.Logging;
using Isf.XCutting.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    public class CommandContext
    {
        public ITransactionFactory TransactionFactory { get; set; }

        public ILogger Logger { get; set; }
    }
}

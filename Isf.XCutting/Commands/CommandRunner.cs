using Isf.XCutting.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    public class CommandRunner
    {
        private readonly ITransactionFactory transactionFactory;

        public CommandRunner(ITransactionFactory transactionFactory)
        {
            this.transactionFactory = transactionFactory;
        }

        public void Execute(Command command)
        {
            if (command.RequiresTransaction)
            {
                TransactCommand(command);
            }
            else
            {
                ExecuteInternal(command);
            }          
        }

        private void ExecuteInternal(Command command)
        {
            command.State = command.State
                .Validate(command)
                .Execute(command);
        }

        private void TransactCommand(Command command)
        {
            try
            {
                var transaction = transactionFactory.CreateInstance();

                Execute(command);

                if (command.State == CommandState.Succeeded)
                {
                    transaction.Commit();
                }

                transaction.Rollback();
            }
            catch (Exception ex)
            {
                command.AddError(
                    string.Format("An error occurred while completing the transaction.  Database error: {0}", ex.Message));
            }
        }
    }
}

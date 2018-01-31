using Isf.XCutting.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    // Dispatcher should instanced per request
    public class CommandDispatcher
    {
        private CommandContext context;

        public CommandDispatcher(CommandContext context)
        {
            this.context = context;
        }
        public CommandResult Execute(ICommand command)
        {
            bool isTransactionRequired = command.GetType()
                .GetCustomAttributes(typeof(TransactionRequiredAttribute), false)
                .Length > 0;

            if (isTransactionRequired)
            {
                StartTransaction();
            }

            // validate
            var validationResult = command.PreValidate();

            // if validation fails, return the result as failed (not attempted) command
            if (!validationResult.IsValid)
            {
                if (isTransactionRequired)
                {
                    Rollback();
                }

                return CommandResult.Create(validationResult);
            }

            var commandResult = ExecuteCommand(command, false);

            if (commandResult.State == CommandState.Succeeded)
            {
                if (isTransactionRequired)
                {
                    Commit();
                }

                return CommandResult.OK;
            }

            if (isTransactionRequired)
            {
                Rollback();
            }
            return commandResult;
        }

        private void StartTransaction()
        {
            if(context.CurrentTransaction == null)
            {
                context.CurrentTransaction = context.TransactionFactory.CreateInstance();
                return;
            }

            // transaction cannot be attempted and failed
            if(context.CurrentTransaction.State < TransactionState.Committed)
            {
                return;
            }

            RaiseInvalidTransactionStateException();
        }

        private void Rollback()
        {
            var transaction = context.CurrentTransaction;

            if(transaction == null || transaction.State == TransactionState.RolledBack)
            {
                return;
            }

            transaction.Rollback();
        }

        private void Commit()
        {
            var transaction = context.CurrentTransaction;

            if (transaction == null || 
                transaction.State == TransactionState.RolledBack ||
                transaction.State == TransactionState.Committed)
            {
                RaiseInvalidTransactionStateException();
            }

            transaction.Commit();
        }

        private void RaiseInvalidTransactionStateException()
        {
            throw new InvalidOperationException(
                string.Format("A transaction was found, but the transaction state was {0}.  Transaction must not have been attempted",
                    context.CurrentTransaction.State));
        }

        private CommandResult ExecuteCommand(ICommand command, bool recordExecutionTime = false)
        {
            CommandResult result = null;
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            try
            {
                command.Execute();
                result = CommandResult.OK;
            }
            catch (Exception ex)
            {
                var error = string.Format("Error executing command {0}", command.GetType().FullName);

                context.Logger.Error(error, ex);

                result = CommandResult.Create(ex);

                result.AddError(error, CommandState.ExecutionFailed);
            }
            finally
            {
                timer.Stop();
            }

            if (recordExecutionTime)
            {
                context.Logger.Info(string.Format("Command {0} executed in {1}", timer.Elapsed));
            }

            return result;
        }
    }
}

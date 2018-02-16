//using Isf.XCutting.Transactions;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Isf.XCutting.Commands
//{
//    // Executor should instanced per request
//    public class CommandExecutor
//    {
//        CommandContext context;

//        public CommandExecutor(CommandContext context)
//        {
//            this.context = context;
//        }
//        public void Execute(Command command)
//        {
//            using (var transaction = context.TransactionFactory.CreateInstance())
//            {
//                try
//                {
//                    command.ExecuteCommand();

//                    if (command.State != CommandState.Succeeded)
//                    {
//                        transaction.Rollback();
//                    }

//                    transaction.Commit();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();

//                    command.AddTransactionError(
//                        string.Format("A database error occurred: {0}", ex.Message));

//                    //TODO: maybe we should serialize the object here?
//                    context.Logger.Error("Error during command execution", ex);
//                }
//            }
//        }
//    }
//}

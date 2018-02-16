using Isf.XCutting.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isf.XCutting.Commands
{  
    public abstract class CommandState
    {
        /// <summary>
        /// hold singleton instances of each commandstate class.  Cannot use static because cannot use inheritance.
        /// instances have no state and can therefore be reused
        /// </summary>
        public static readonly CommandState
            NotValidated = new CommandStateNotValidated(),
            Invalid = new CommandStateInvalid(),
            Valid = new CommandStateValid(),
            Succeeded = new CommandStateSucceeded(),
            Failed = new CommandStateFailed(),
            Undone = new CommandStateUndone();


        public abstract CommandState Validate(Command command);

        public abstract CommandState Execute(Command command);

        public abstract CommandState Undo(Command command);

        protected CommandState RaiseAlreadyValidatedError()
        {
            throw new InvalidOperationException("The Command has already been validated and cannot be validated again");
        }

        protected CommandState RaiseNotExecutedUndoError()
        {
            throw new InvalidOperationException("Command has not been executed and cannot be undone");
        }

        protected CommandState RaiseAlreadyExecutedError()
        {
            throw new InvalidOperationException("Command has not alerady been executed and cannot be executed again");
        }

        protected CommandState UndoInternal(Command command)
        {
            command.Undo();

            return CommandState.Undone;
        }

        protected void CreateErrorFromException(Command command, Exception ex)
        {
            command.AddError(
                    string.Format("Command: {0} failed. {1}",
                        command.GetType().FullName,
                        ex.Message));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isf.XCutting.Commands
{
    public class CommandStateNotValidated : CommandState
    {
        public override CommandState Validate(Command command)
        {
            try
            {
                var validationErrors = command.Validate();

                foreach (var error in validationErrors)
                {
                    command.AddError(error.Property, error.ErrorMessage);
                }

                if (validationErrors.Any())
                {
                    return CommandState.Invalid;
                }

                return CommandState.Valid;
            }
            catch (Exception ex)
            {
                CreateErrorFromException(command, ex);

                return CommandState.Invalid;
            }
        }

        public override CommandState Execute(Command command)
        {
            throw new InvalidOperationException("Command has not been validated and cannot be executed");
        }

        public override CommandState Undo(Command command)
        {
            return RaiseNotExecutedUndoError();
        }
    }
}

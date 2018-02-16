using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    public class CommandStateInvalid : CommandState
    {
        public override CommandState Validate(Command command)
        {
            return RaiseAlreadyValidatedError();
        }

        public override CommandState Execute(Command command)
        {
            throw new InvalidOperationException("The Command failed validation and cannot be executed");
        }

        public override CommandState Undo(Command command)
        {
            return RaiseNotExecutedUndoError();
        }
    }
}

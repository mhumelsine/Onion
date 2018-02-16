using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    public class CommandStateFailed : CommandState
    {
        public override CommandState Validate(Command command)
        {
            return RaiseAlreadyValidatedError();
        }

        public override CommandState Execute(Command command)
        {
            return RaiseAlreadyExecutedError();
        }

        public override CommandState Undo(Command command)
        {
            return UndoInternal(command);
        }
    }
}

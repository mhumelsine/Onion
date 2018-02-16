using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    public class CommandStateUndone : CommandState
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
            throw new InvalidOperationException("The Command has already been undone and cannot undone again");
        }
    }
}

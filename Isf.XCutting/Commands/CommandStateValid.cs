using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    public class CommandStateValid : CommandState
    {
        public override CommandState Validate(Command command)
        {
            return RaiseAlreadyValidatedError();
        }
        public override CommandState Execute(Command command)
        {
            try
            {
                command.Execute();

                return CommandState.Succeeded;
            }
            catch(Exception ex)
            {
                CreateErrorFromException(command, ex);

                return CommandState.Failed;
            }            
        }

        public override CommandState Undo(Command command)
        {
            return RaiseNotExecutedUndoError();
        }
    }
}

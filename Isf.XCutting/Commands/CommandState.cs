using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    public enum CommandState
    {
        NotAttempted,
        Succeeded,
        ValidationFailed,
        ExecutionFailed
    }
}

using Isf.XCutting.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    public abstract class CommandBase : ICommand
    {
        public abstract CommandResult Execute();

        // TODO:  How should this work?
        public CommandResult Undo() {
            return CommandResult.OK;
        }

        // make implementing validations optional
        public virtual ObjectValidationResult PostValidate()
        {
            return ObjectValidationResult.OK;
        }

        public virtual ObjectValidationResult PreValidate()
        {
            return ObjectValidationResult.OK;
        }
    }
}

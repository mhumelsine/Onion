using Isf.XCutting.Validations;

namespace Isf.XCutting.Commands
{
    public interface ICommand
    {
        ObjectValidationResult PreValidate();

        ObjectValidationResult PostValidate();

        CommandResult Execute();

        CommandResult Undo();
    }
}

using Isf.XCutting.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isf.XCutting.Commands
{
    public class CommandResult
    {
        private IDictionary<string, List<string>> errors;

        public IDictionary<string, IEnumerable<string>> ErrorDictionary
        {
            get
            {
                if (errors == null)
                {
                    return new Dictionary<string, IEnumerable<string>>();
                }

                return errors.ToDictionary(x => x.Key, x => x.Value.AsEnumerable());
            }
        }

        public IEnumerable<string> ErrorMessages
        {
            get
            {
                if (errors == null)
                {
                    return Enumerable.Empty<string>();
                }

                return errors.SelectMany(x => x.Value.Select(y => y));
            }
        }

        public CommandState State { get; set; }


        public static CommandResult OK = new CommandResult
        {
            State = CommandState.Succeeded
        };

        public static CommandResult Create(ObjectValidationResult validationResult)
        {
            var result = new CommandResult();

            foreach (var error in validationResult.Errors)
            {
                result.AddError(
                    error.MemberNames.FirstOrDefault(), 
                    error.ErrorMessage, 
                    CommandState.ValidationFailed);
            }

            return result;
        }

        public static CommandResult Create(Exception exception)
        {
            var result = new CommandResult();

            result.AddError(exception.Message, CommandState.ExecutionFailed);

            return result;
        }

        public void AddError(string errorMessage, CommandState state)
        {
            AddError(string.Empty, errorMessage, state);
        }
        public void AddError(string property, string errorMessage, CommandState state)
        {
            if (errors == null)
            {
                errors = new Dictionary<string, List<string>>();
            }

            List<string> errorCollection;

            //if no collection exists, create one
            if (!errors.TryGetValue(property, out errorCollection))
            {
                errorCollection = new List<string>();
                errors[property] = errorCollection;
            }

            errorCollection.Add(errorMessage);
            State = state;
        }
    }
}

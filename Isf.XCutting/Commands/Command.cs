using Isf.XCutting.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isf.XCutting.Commands
{
    public abstract class Command
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

        private CommandState state;
        public CommandState State {
            get {
                return state;
            }
            set {
                if(state != value)
                {
                    state = value;

                    foreach (var observer in observers)
                    {                        
                        observer(state);
                    }
                }

            }
        }

        public bool RequiresTransaction { get; }

        public Command()
        {
            State = CommandState.NotValidated;
        }

        public void AddError(string errorMessage)
        {
            AddError(string.Empty, errorMessage);
        }
        public void AddError(string property, string errorMessage)
        {
            if (errors == null)
            {
                errors = new Dictionary<string, List<string>>();
            }

            //if no collection exists, create one
            if (!errors.TryGetValue(property, out List<string> errorCollection))
            {
                errorCollection = new List<string>();
                errors[property] = errorCollection;
            }

            errorCollection.Add(errorMessage);
        }

        public virtual IEnumerable<ValidationError> Validate()
        {
            return Enumerable.Empty<ValidationError>();
        }

        public abstract void Execute();

        public virtual void Undo() { }

        private HashSet<Action<CommandState>> observers = new HashSet<Action<CommandState>>();

        public void OnStateChange(Action<CommandState> handler)
        {
            observers.Add(handler);
        }
    }
}

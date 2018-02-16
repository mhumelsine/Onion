//using Isf.XCutting.Validations;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;

//namespace Isf.XCutting.Commands
//{ 
//    //public enum CommandState
//    //{
//    //    NotValidated,
//    //    Validated,
//    //    ValidationFailed,
//    //    Succeeded,
//    //    Failed
//    //}

//    public abstract class CommandBase
//    {
//        #region command state 
//        abstract class CommandStateBase
//        {
//            protected CommandBase command;
//            public abstract CommandState State { get; }
//            public abstract void Validate();
//            public abstract void Execute();

//            public CommandStateBase(CommandBase command)
//            {
//                this.command = command;
//            }

//            protected virtual void RaiseAlreadyValidatedError()
//            {
//                throw new InvalidOperationException("Command has already been validated");
//            }

//            protected virtual void RaiseAlreadyExecutedError()
//            {
//                throw new InvalidOperationException("Command has already been executed");
//            }
//        }

//       class CommandStateNotValidated : CommandStateBase
//        {
//            public override CommandState State { get { return CommandState.NotValidated; } }
//            public CommandStateNotValidated(CommandBase command) : base (command)
//            {            
//            }

//            public override void Execute()
//            {
//                throw new InvalidOperationException("Command has not been validated and cannot be executed");
//            }

//            public override void Validate()
//            {
//                var validationErrors = command.Validate();

//                foreach (var error in validationErrors)
//                {
//                    command.AddError(error.Property, error.ErrorMessage);
//                }

//                if (command.errors.Count > 0)
//                {
//                    command.state = new CommandStateInvalid(command);
//                }
//                else
//                {
//                    command.state = new CommandStateValid(command);
//                }
//            }
//        }

//        class CommandStateValid : CommandStateBase
//        {
//            public override CommandState State { get { return CommandState.Validated; } }
//            public CommandStateValid(CommandBase command) : base(command)
//            {
//            }
//            public override void Execute()
//            {
//                try
//                {
//                    command.Execute();
//                    command.state = new CommandStateSucceeded(command);
//                }
//                catch (Exception ex)
//                {
//                    command.state = new CommandStateFailed(command);
//                    throw ex;
//                }
//            }

//            public override void Validate()
//            {
//                RaiseAlreadyValidatedError();
//            }
//        }

//        class CommandStateInvalid : CommandStateBase
//        {
//            public override CommandState State { get { return CommandState.ValidationFailed; } }

//            public CommandStateInvalid(CommandBase command) : base(command)
//            {

//            }

//            public override void Execute()
//            {
//                //throw new InvalidOperationException("Command failed validation and cannot be executed");
//                //do nothing; we already have the error
//            }

//            public override void Validate()
//            {
//                RaiseAlreadyValidatedError();
//            }
//        }

//        class CommandStateSucceeded : CommandStateBase
//        {
//            public override CommandState State { get { return CommandState.Succeeded; } }

//            public CommandStateSucceeded(CommandBase command) : base(command)
//            {

//            }

//            public override void Execute()
//            {
//                RaiseAlreadyExecutedError();
//            }

//            public override void Validate()
//            {
//                RaiseAlreadyValidatedError();
//            }
//        }

//        class CommandStateFailed : CommandStateBase
//        {
//            public override CommandState State { get { return CommandState.Failed; } }

//            public CommandStateFailed(CommandBase command) : base(command)
//            {

//            }
//            public override void Execute()
//            {
//                RaiseAlreadyExecutedError();
//            }

//            public override void Validate()
//            {
//                RaiseAlreadyValidatedError();
//            }
//        }

//        #endregion

//        public CommandBase()
//        {
//            state = new CommandStateNotValidated(this);
//        }

//        private CommandStateBase state;

//        public CommandState State {  get { return state.State; } }

//        private IDictionary<string, List<string>> errors;

//        public IDictionary<string, IEnumerable<string>> ErrorDictionary
//        {
//            get
//            {
//                if (errors == null)
//                {
//                    return new Dictionary<string, IEnumerable<string>>();
//                }

//                return errors.ToDictionary(x => x.Key, x => x.Value.AsEnumerable());
//            }
//        }

//        public IEnumerable<string> ErrorMessages
//        {
//            get
//            {
//                if (errors == null)
//                {
//                    return Enumerable.Empty<string>();
//                }

//                return errors.SelectMany(x => x.Value.Select(y => y));
//            }
//        }

//        protected abstract IEnumerable<ValidationError> Validate();
//        protected abstract void Execute();

//        private void AddError(string errorMessage)
//        {
//            AddError(string.Empty, errorMessage);
//        }
//        private void AddError(string property, string errorMessage)
//        {
//            if (errors == null)
//            {
//                errors = new Dictionary<string, List<string>>();
//            }

//            //if no collection exists, create one
//            if (!errors.TryGetValue(property, out List<string> errorCollection))
//            {
//                errorCollection = new List<string>();
//                errors[property] = errorCollection;
//            }

//            errorCollection.Add(errorMessage);
//        }               

//        public void ExecuteCommand()
//        {
//            state.Validate();
//            state.Execute();
//        }

//        public void AddTransactionError(string error)
//        {
//            AddError(error);
//        }
//    }
//}

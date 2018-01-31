using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Isf.XCutting.Validations
{
    public class ObjectValidationResult
    {
        private List<ValidationResult> errors;

        static ObjectValidationResult()
        {
            OK = new ObjectValidationResult();
        }

        public static ObjectValidationResult OK { get; private set; }

        public object Object { get; set; }

        public bool IsValid
        {
            get { return errors == null; }
        }

        public IEnumerable<ValidationResult> Errors
        {
            get { return errors.ToList(); }
        }

        private List<ValidationResult> ErrorList
        {
            get
            {
                if (errors == null)
                {
                    errors = new List<ValidationResult>();
                }

                return errors;
            }
        }

        public void AddError(string errorMessage, params string[] properties)
        {
            ErrorList.Add(new ValidationResult(errorMessage, properties));
        }

        public void AddErrors(IEnumerable<ValidationResult> errorsMessages)
        {
            ErrorList.AddRange(errorsMessages);
        }

        public static ObjectValidationResult Create(string errorMessage, string property)
        {
            var result = new ObjectValidationResult();

            result.AddError(errorMessage, property);

            return result;
        }
    }
}

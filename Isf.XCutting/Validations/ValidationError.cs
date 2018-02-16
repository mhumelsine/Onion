using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Validations
{
    public class ValidationError
    {
        public string Property { get; }
        public string ErrorMessage { get; }

        public ValidationError(string property, string errorMessage)
        {
            this.Property = property;
            this.ErrorMessage = errorMessage;
        }
    }
}
